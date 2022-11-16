using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class SendVariable : MonoBehaviourPunCallbacks, IPunObservable
{
    public int playerNum = 0;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        Debug.Log("関数に入っています");
        // オーナーの場合
        if (stream.IsWriting)
        {
            Debug.Log("trueです");
            stream.SendNext(this.playerNum);
        }
        // オーナー以外の場合
        else
        {
            Debug.Log("falseです");
            this.playerNum = (int)stream.ReceiveNext();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //internal void OnPhotonSerializeView(bool isMine)
    //{
    //    throw new NotImplementedException();
    //}
}
