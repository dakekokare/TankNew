using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SendVariable : MonoBehaviourPunCallbacks,IPunObservable
{
    public int playerNum = 0;
    //�ϐ�����
    #region IPunObservable implementation
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
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
    #endregion
}
