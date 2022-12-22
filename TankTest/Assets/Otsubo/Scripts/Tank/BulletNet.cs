using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class BulletNet : MonoBehaviour
{
    [SerializeField]
    Material mat;
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
        if(other.gameObject.tag=="Barrier")
        {
            //自分のバリア
            if (other.GetComponent<PhotonView>().IsMine)
            {
                //自分の弾の時
                if (this.gameObject.tag == "Shell")
                {
                    return;
                }
                //敵の弾の時
                else if (this.gameObject.tag == "EnemyShell")
                    Destroy(this.gameObject);
            }
            //敵のバリアに当たった時
            else 
            {
                //敵 の弾の時
                if (this.gameObject.tag == "EnemyShell")
                {
                    return;
                }
                //弾の時
                else if (this.gameObject.tag == "Shell")
                    Destroy(this.gameObject);
            }
        }
    }
    public void ChengeMaterial()
    {
        // 生成したプレハブのマテリアルを設定
        this.gameObject.GetComponent<MeshRenderer>().material = mat;
    }
}
