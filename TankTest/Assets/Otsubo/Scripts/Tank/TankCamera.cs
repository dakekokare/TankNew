using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TankCamera : MonoBehaviourPunCallbacks
{
    private Camera camera;

    private void Start()
    {
        // ���g�����������I�u�W�F�N�g�����Ɉړ��������s��
        if (photonView.IsMine)
        {

            // �J�����ǉ�
            gameObject.AddComponent<Camera>();
            camera = GetComponent<Camera>();
            camera.depth = 1;
        }
    }
}
