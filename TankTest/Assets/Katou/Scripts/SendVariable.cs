using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SendVariable : MonoBehaviourPunCallbacks,IPunObservable
{
    //プレイヤー数
    public int playerNum=0;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //データの送信
            stream.SendNext(playerNum);
        }
        else
        {
            //データの受信
            this.playerNum = (int)stream.ReceiveNext();
        }
    }
}
