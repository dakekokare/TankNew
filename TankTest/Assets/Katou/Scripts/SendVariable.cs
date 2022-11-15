using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SendVariable : MonoBehaviourPunCallbacks,IPunObservable
{
    public int playerNum = 0;
    //変数同期
    #region IPunObservable implementation
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
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
    #endregion
}
