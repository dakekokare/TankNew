using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// MonoBehaviourPunCallbacks���p�����āAPUN�̃R�[���o�b�N���󂯎���悤�ɂ���
public class Canvas : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        // ���g�����������I�u�W�F�N�g�����Ɉړ��������s��
        if (photonView.IsMine)
        {
            var position = new Vector3(0.0f, 0.1f, 0.0f);

            // �L�����o�X�ǉ�
            //PhotonNetwork.Instantiate("Canvas", position, Quaternion.identity);
        }
    }
}
