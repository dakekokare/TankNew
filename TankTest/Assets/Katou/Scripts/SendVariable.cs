using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SendVariable : MonoBehaviourPunCallbacks,IPunObservable
{
    //�v���C���[��
    public int playerNum=0;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //�f�[�^�̑��M
            stream.SendNext(playerNum);
        }
        else
        {
            //�f�[�^�̎�M
            this.playerNum = (int)stream.ReceiveNext();
        }
    }
}
