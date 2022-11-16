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
        // Turretの最初の角度を取得する。
        angle = transform.eulerAngles;
    }

    void Update()
    {
        float sensitiveRotate = 5.0f;

        //float rotateY = Input.GetAxis("Mouse Y") * sensitiveRotate;

        //transform.Rotate(rotateY, 0.0f, 0.0f);

        angle.x -= Input.GetAxis("Mouse Y") * sensitiveRotate;

        transform.eulerAngles = new Vector3(angle.x, transform.parent.parent.eulerAngles.y, 0);

        // 移動できる角度に制限を加える。
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