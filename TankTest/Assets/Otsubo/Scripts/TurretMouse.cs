using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class TurretMouse : MonoBehaviourPunCallbacks
{
    private Vector3 angle;
    private AudioSource audioS;

    void Start()
    {
        // Turret�̍ŏ��̊p�x���擾����B
        angle = transform.eulerAngles;
    }

    void Update()
    {
        float sensitiveRotate = 5.0f;

        //float rotateY = Input.GetAxis("Mouse Y") * sensitiveRotate;

        //transform.Rotate(rotateY, 0.0f, 0.0f);

        angle.x -= Input.GetAxis("Mouse Y") * sensitiveRotate;

        transform.eulerAngles = new Vector3(angle.x, transform.parent.parent.eulerAngles.y, 0);

        // �ړ��ł���p�x�ɐ�����������B
        if (angle.x < 80)
        {
            angle.x = 80;
        }

        if (angle.x > 95)
        {
            angle.x = 95;
        }



    }
}