using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class ShotShell : MonoBehaviourPunCallbacks
{
    public float shotSpeed;

    //// privateの状態でもInspector上から設定できるようにするテクニック。
    //[SerializeField]
    //private GameObject shellPrefab;

    [SerializeField]
    private AudioClip shotSound;
    AudioSource audioSource;

    private float timeBetweenShot = 0.1f;
    private float timer;
    private int nextBulletId = 0;
    [SerializeField]
    private BulletNet bulletPre;

    private bool shotLock = true;
    //色情報保持　オブジェクト
    //private SaveColor sColor;
    private Vector3 pColor;
    private Vector3 eColor;

    void Start()
    {
        //Componentを取得
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        //if (photonView.IsMine)
        //{

        //    Debug.Log("[p" + pColor + "]");
        //    Debug.Log("[e" + eColor + "]");
        //}

        // タイマーの時間を動かす
        timer += Time.deltaTime;

        if (photonView.IsMine)
        {
            // もしもSpaceキーを押したならば（条件）
            // 「Space」の部分を変更することで他のキーにすることができる（ポイント）
            if (Input.GetMouseButton(0) && timer > timeBetweenShot && shotLock == false)
            {
                //タレットが非表示なら打たない
                if (gameObject.transform.parent.gameObject == false)
                    return;
                
                // タイマーの時間を０に戻す。
                timer = 0.0f;


                // 弾を発射するたびに弾のIDを1ずつ増やしていく
                photonView.RPC(nameof(FireBullet), RpcTarget.All, nextBulletId++,pColor,eColor);


                ////マスタークライアントなら
                //if(PhotonNetwork.IsMasterClient)
                //    // 弾を発射するたびに弾のIDを1ずつ増やしていく
                //    photonView.RPC(nameof(FireBullet), RpcTarget.All, nextBulletId++,pColor,eColor);
                //else
                //    // 弾を発射するたびに弾のIDを1ずつ増やしていく
                //    photonView.RPC(nameof(FireBullet), RpcTarget.All, nextBulletId++, eColor, pColor);
            }
        }
    }

    [PunRPC]
    private void FireBullet(int id,Vector3 p,Vector3 e)
    {

        // 砲弾のプレハブを実体化（インスタンス化）する。
        var shell = Instantiate(bulletPre);

        //idを弾に渡す
        shell.Init(id, photonView.OwnerActorNr);

        // 砲弾に付いているRigidbodyコンポーネントにアクセスする。
        Rigidbody shellRb = shell.GetComponent<Rigidbody>();

        //座標移動
        shellRb.transform.position = transform.position;

        // forward（青軸＝Z軸）の方向に力を加える。
        shellRb.AddForce(transform.forward * shotSpeed);

        // 砲弾の発射音を出す。
        audioSource.PlayOneShot(shotSound);

        if (shell.OwnerId == PhotonNetwork.LocalPlayer.ActorNumber)
        {
                //マテリアルの変更
                ChangeMaterial(shell, p);

        }
        else
        {
            //敵の弾だったらtagを変える
            shell.tag = "EnemyShell";

                //マテリアルの変更
                ChangeMaterial(shell, e);

        }
        shell.SetPlayer(gameObject.transform.
            parent.parent.parent.parent.parent.gameObject);
    }
    

    public void ShotUnlock()
    {
        shotLock = false;
    }
    public void ShotLock()
    {
        shotLock = true;
    }
    //相手方shell削除
    public void DeleteShellOther(int id,int ownerId)
    {
        Debug.Log("DeleteShellOther");
        //他プレイヤーにダメージ処理
        photonView.RPC(nameof(FindAndDeleteShell), RpcTarget.Others,id,ownerId);
    }
    [PunRPC]
    private void FindAndDeleteShell(int id,int ownerId)
    {
        Debug.Log("DeleteShell");
        //弾を削除する
        var bullets = FindObjectsOfType<BulletNet>();
        foreach (var bullet in bullets)
        {
            if (bullet.Equals(id, ownerId))
            {
                Destroy(bullet.gameObject);
                break;
            }
        }
    }

    private void SearchSaveColor()
    {
        //// ルーム内のネットワークオブジェクト
        //foreach (var photonView in PhotonNetwork.PhotonViewCollection)
        //{
        //    //Color オブジェクト
        //    if (photonView.gameObject.name == "Color(Clone)")
        //    {
        //        sColor = PhotonView.Find(photonView.ViewID).gameObject.GetComponent<SaveColor>();

        //    }
        //}
    }

    public void SetColor(Vector3 p,Vector3 e)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            //色情報取得
            pColor = p;
            eColor = e;
        }
        else
        {
            //色情報取得
            pColor = e;
            eColor = p;
        }
    }

    private void ChangeMaterial(BulletNet obj,Vector3 vec)
    {
        //マテリアル色変え
        obj.ChengeMaterial(vec);

        //軌跡の色を変える
        TrailRenderer tr = obj.GetComponent<TrailRenderer>();
        Color color = new Color(vec.x, vec.y, vec.z);
        tr.startColor = color;
        tr.endColor = color;

    }

}