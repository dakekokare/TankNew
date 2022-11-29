using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BoatTurret : MonoBehaviourPunCallbacks
{
    private Vector3 angle;

    void Start()
    {
        // Turret�̍ŏ��̊p�x���擾����B
        angle = transform.eulerAngles;
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            // ���_�ړ����x
            float sensitiveRotate = 1.0f;

            // 
            float rotateX = Input.GetAxis("Mouse X") * sensitiveRotate;
            transform.Rotate(0.0f, rotateX, 0.0f);

            angle.z = Input.GetAxis("Mouse Y") * sensitiveRotate;

            transform.parent.eulerAngles = new Vector3(0, transform.parent.parent.eulerAngles.y, angle.z);

            // �ړ��ł���p�x�ɐ�����������B
            if (angle.z < 0)
            {
                angle.z = 0;
            }

            if (angle.z > 30)
            {
                angle.z = 30;
            }

        }

    }
}
