using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class BulletNet : MonoBehaviour
{
    [SerializeField]
    Material mat;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        //3秒後に破壊
        Destroy(this.gameObject, 3.0f);
    }
    // 弾のIDを返すプロパティ
    public int Id { get; private set; }
    // 弾を発射したプレイヤーのIDを返すプロパティ
    public int OwnerId { get; private set; }
    // 同じ弾かどうかをIDで判定するメソッド
    public bool Equals(int id, int ownerId) => id == Id && ownerId == OwnerId;

    public void Init(int id, int ownerId)
    {
        Id = id;
        OwnerId = ownerId;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Barrier")
        {
            //バリア接触処理
            HitBarrier(other);
        }
        //プレイヤーだったら
        else if (other.gameObject.layer == 8)
        {
            //プレイヤー接触処理
            HitPlayer(other);
        }
        else 
        {
            //その他の接触処理
            HitOthers(other);
        }
    }
    public void ChengeMaterial(Vector3 col)
    {
        // 生成したプレハブのマテリアルを設定
        //this.gameObject.GetComponent<MeshRenderer>().material = mat;
        this.gameObject.GetComponent<MeshRenderer>().material.color 
            = new Vector4(col.x, col.y, col.z, 1.0f);
    }
    private void DestroyShellOtherPlayer()
    {
        ////弾の削除
        player.transform.
        GetChild(0).
        GetChild(0).
        GetChild(0).
        GetChild(1).
        GetChild(0).
        GetComponent<ShotShell>().DeleteShellOther(Id, OwnerId);
    }

    public void SetPlayer(GameObject obj)
    {
        player = obj;
    }

    private void HitBarrier(Collider other) 
    {
        //自分のバリア
        if (other.GetComponent<PhotonView>().IsMine)
        {
            //自分の弾の時
            if (this.gameObject.tag == "Shell")
            {
                Debug.Log("Barrier shell Hit return");
                return;
            }
            //敵の弾の時
            else if (this.gameObject.tag == "EnemyShell")
            {
                Debug.Log("Barrier enemyShell Hit");
                //自分と相手の弾を消す
                DestroyShellOtherPlayer();

                //バリア音再生
                other.gameObject.GetComponent<Barrier>().PlaySound();

                Destroy(this.gameObject);
            }
        }
    }

    private void HitPlayer(Collider other)
    {
        //enemyShellだったら
        if (gameObject.tag == "EnemyShell")
        {

            /////////////////////////////////////////////
            // 
            ///////////////////////////////////////////
            //自分じゃなかったら
            if (!other.GetComponent<PhotonView>().IsMine)
            {
                Debug.Log("Boat EnemyShell Hit return");
                return;
            }
            else
            {
                Debug.Log("Boat EnemyShell Hit");
                //船と接触したら,ダメージ処理
                other.gameObject.GetComponent<TankHealth>().HitBullet();

                //自分と相手の弾を消す
                DestroyShellOtherPlayer();
                Destroy(this.gameObject);
            }
        }
    }
    private void HitOthers(Collider other)
    {
        Debug.Log("Other Hit");
        //自分と相手の弾を消す
        DestroyShellOtherPlayer();
        Destroy(this.gameObject);
    }
}
