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
    //    Debug.Log("�֐��ɓ����Ă��܂�");
    //    // �I�[�i�[�̏ꍇ
    //    if (stream.IsWriting)
    //    {
    //        Debug.Log("true�ł�");
    //        stream.SendNext(this.playerNum);
    //    }
    //    // �I�[�i�[�ȊO�̏ꍇ
    //    else
    //    {
    //        Debug.Log("false�ł�");
    //        this.playerNum = (int)stream.ReceiveNext();
    //    }
    //}
    private void Start()
    {
        Debug.Log("a");
    }

    private void Update()
    {
        //Debug.Log("�X�V");
        //int count = 0;
        ////foreach (var p in PhotonNetwork.PlayerList)
        ////{
        ////    count++;
        ////}
    }
    //ID��Ԃ��v���p�e�B
    public int Id { get; private set; }
    // �v���C���[��ID��Ԃ��v���p�e�B
    public int OwnerId { get; private set; }
    // �������ǂ�����ID�Ŕ��肷�郁�\�b�h
    public bool Equals(int id, int ownerId) => id == Id && ownerId == OwnerId;

    public void Init(int id, int ownerId)
    {
        Id = id;
        OwnerId = ownerId;
    }
}
