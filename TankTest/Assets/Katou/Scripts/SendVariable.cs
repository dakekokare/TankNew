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
        Debug.Log("�֐��ɓ����Ă��܂�");
        // �I�[�i�[�̏ꍇ
        if (stream.IsWriting)
        {
            Debug.Log("true�ł�");
            stream.SendNext(this.playerNum);
        }
        // �I�[�i�[�ȊO�̏ꍇ
        else
        {
            Debug.Log("false�ł�");
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
