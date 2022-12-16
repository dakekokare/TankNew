using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Barrier : MonoBehaviourPunCallbacks
{
    // プレイヤー
    private GameObject player;

    private float Barriertime = 10.0f;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // タイマーの時間を動かす
        timer += Time.deltaTime;

        // 10秒経ったら
        if(timer > Barriertime)
        {
            // タイマーの時間を０に戻す。
            timer = 0.0f;

            // バリアを消す。
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Shell")
        return;

        if (!photonView.IsMine)
        {
            //shell に接触した場合
            if (other.gameObject.tag == "Shell")
            {
                // ぶつかってきた相手方（敵の砲弾）を破壊する。
                Destroy(other.gameObject);
            }

            ////ミサイルと接触したら
            if (other.gameObject.tag == "Missile")
            {
                // ぶつかってきた相手方（敵の砲弾）を破壊する。
                photonView.RPC(nameof(DeleteMissile), RpcTarget.Others, other.GetComponent<PhotonView>().ViewID);
            }
        }
    }

    //public void SearchPlayer()
    //{
    //    // ルーム内のネットワークオブジェクト
    //    foreach (var photonView in PhotonNetwork.PhotonViewCollection)
    //    {
    //        //boat かつ　自分
    //        if (photonView.gameObject.name == "Boat(Clone)")
    //        {
    //            if (photonView.IsMine)
    //            {
    //                Debug.Log("[" + GetInstanceID() + "]" + "Player Find");
    //                player = PhotonView.Find(photonView.ViewID).gameObject;
    //            }
    //        }
    //    }
    //}

    [PunRPC]
    private void DeleteMissile(int obj)
    {
        GameObject boat = PhotonView.Find(obj).gameObject;
        //missile を削除
        PhotonNetwork.Destroy(boat);
    }
}
