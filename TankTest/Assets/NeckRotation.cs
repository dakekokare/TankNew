using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class NeckRotation : MonoBehaviourPunCallbacks
{
    //private Vector3 angle;

    //void Start()
    //{
    //    // Turret�̍ŏ��̊p�x���擾����B
    //    angle = transform.eulerAngles;
    //}

    //void Update()
    //{
    //    if (photonView.IsMine)
    //    {
    //        // ���_�ړ����x
    //        float sensitiveRotate = 1.5f;


    //        angle.x += Input.GetAxis("Mouse X") * sensitiveRotate;

    //        transform.eulerAngles = new Vector3(
    //            transform.eulerAngles.x,
    //            transform.parent.parent.eulerAngles.y+ angle.x,
    //            transform.eulerAngles.z
    //            );

    //        // �ړ��ł���p�x�ɐ�����������B
    //        if (angle.x > 10)
    //        {
    //            angle.x = 10;
    //        }
    //    }

    //}
}
