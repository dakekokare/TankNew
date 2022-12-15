using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletNet : MonoBehaviour
{
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
}
