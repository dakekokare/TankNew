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

    private float timeBetweenShot = 0.1f;
    private float timer;
    private int nextBulletId = 0;
    [SerializeField]
    private BulletNet bulletPre;

    private bool shotLock = false;


    void Update()
    {

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
                photonView.RPC(nameof(FireBullet), RpcTarget.All, nextBulletId++);

            }
        }
    }

     [PunRPC]
    private void FireBullet(int id)
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
        AudioSource.PlayClipAtPoint(shotSound, transform.position);

        //敵の弾だったらtagを変える
        if (shell.OwnerId != PhotonNetwork.LocalPlayer.ActorNumber)
        {
            shell.tag = "EnemyShell";
            //マテリアル色変え
            shell.ChengeMaterial();

            //軌跡の色を変える
            TrailRenderer tr = shell.GetComponent<TrailRenderer>();
            Color color = new Color(255, 0, 0);
            tr.startColor = color;
            tr.endColor = color;
        }

        shell.SetPlayer(gameObject.transform.parent.parent.parent.parent.gameObject);
    }

    public void ShotUnlock()
    {
        shotLock = false;
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
}