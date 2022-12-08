using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HpEnemyTransform : MonoBehaviourPunCallbacks
{
    private GameObject player;
    //���̃v���C���\
    private GameObject otherPlayer;

    // Update is called once per frame
    void Update()
    {
        if (otherPlayer == null)
        {
            //Debug.Log("[" + GetInstanceID() + "]" + "otherPlayer Null Return");
            return;
        }
        //else
            //Debug.Log("[" + GetInstanceID() + "]" + "otherPlayer �����Ă܂�");
        transform.position = otherPlayer.transform.position;


        Vector3 p = player.transform.position;
        p.y = transform.position.y;
        //�@�J�����Ɠ��������ɐݒ�
        transform.LookAt(p);

    }

    public void SearchPlayer()
    {
        // ���[�����̃l�b�g���[�N�I�u�W�F�N�g
        foreach (var photonView in PhotonNetwork.PhotonViewCollection)
        {
            //boat ���@��������Ȃ�������
            if (photonView.gameObject.name == "Boat(Clone)")
            {
                if (!photonView.IsMine)
                {
                    Debug.Log("[" + GetInstanceID() + "]" + "otherPlayer Find");
                    otherPlayer = PhotonView.Find(photonView.ViewID).gameObject;
                }
                else
                {
                    Debug.Log("[" + GetInstanceID() + "]" + "Player Find");
                    player = PhotonView.Find(photonView.ViewID).gameObject;
                }

            }
        }
    }
}
