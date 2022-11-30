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
        //var shell = PhotonNetwork.Instantiate(
        //    "Shell", transform.position, Quaternion.identity
        //    ).GetComponent<BulletNet>();
        var shell = Instantiate(bulletPre);

        shell.Init(id, photonView.OwnerActorNr);
        // 砲弾のプレハブを実体化（インスタンス化）する。
        //GameObject shell = PhotonNetwork.Instantiate("Shell", transform.position, Quaternion.identity);

        // 砲弾に付いているRigidbodyコンポーネントにアクセスする。
        Rigidbody shellRb = shell.GetComponent<Rigidbody>();

        //座標移動
        shellRb.transform.position = transform.position;

        // forward（青軸＝Z軸）の方向に力を加える。
        shellRb.AddForce(transform.forward * shotSpeed);
        //shellRb.AddForce(-transform.up * (shotSpeed * 0.5f));

        // 発射した砲弾を３秒後に破壊する。
        // （重要な考え方）不要になった砲弾はメモリー上から削除すること。
        //Destroy(shell, 3.0f);
        PhotonView.Destroy(shell, 3.0f);
        // 砲弾の発射音を出す。
        AudioSource.PlayClipAtPoint(shotSound, transform.position);

    }

    public void ShotUnlock()
    {
        shotLock = false;
    }
}