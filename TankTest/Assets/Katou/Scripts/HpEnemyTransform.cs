using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HpEnemyTransform : MonoBehaviourPunCallbacks
{
    //���̃v���C���\
    private GameObject otherPlayer;

    // Start is called before the first frame update
    void Start()
    {
        // ���[�����̃l�b�g���[�N�I�u�W�F�N�g
        foreach (var photonView in PhotonNetwork.PhotonViewCollection)
        {
            //boat ���@��������Ȃ�������
            if (photonView.gameObject.name == "Boat(Clone)")
            {
                if (!photonView.IsMine)
                {
                    otherPlayer=PhotonView.Find(photonView.ViewID).gameObject;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (otherPlayer == null)
            return;
        this.transform.position = otherPlayer.transform.position;
        this.transform.rotation = otherPlayer.transform.rotation;

    }
}
