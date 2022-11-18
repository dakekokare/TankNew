using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class SendVariable : MonoBehaviourPunCallbacks/*, IPunObservable*/
{
    //public int playerNum = 0;
    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    Debug.Log("関数に入っています");
    //    // オーナーの場合
    //    if (stream.IsWriting)
    //    {
    //        Debug.Log("trueです");
    //        stream.SendNext(this.playerNum);
    //    }
    //    // オーナー以外の場合
    //    else
    //    {
    //        Debug.Log("falseです");
    //        this.playerNum = (int)stream.ReceiveNext();
    //    }
    //}
    private void Start()
    {
        Debug.Log("a");
    }

    private void Update()
    {
        //Debug.Log("更新");
        //int count = 0;
        ////foreach (var p in PhotonNetwork.PlayerList)
        ////{
        ////    count++;
        ////}
    }
    //IDを返すプロパティ
    public int Id { get; private set; }
    // プレイヤーのIDを返すプロパティ
    public int OwnerId { get; private set; }
    // 同じかどうかをIDで判定するメソッド
    public bool Equals(int id, int ownerId) => id == Id && ownerId == OwnerId;

    public void Init(int id, int ownerId)
    {
        Id = id;
        OwnerId = ownerId;
    }
}
