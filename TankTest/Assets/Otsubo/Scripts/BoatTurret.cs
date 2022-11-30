using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BoatTurret : MonoBehaviourPunCallbacks
{
    private Vector3 angle;

    void Start()
    {
        // Turretの最初の角度を取得する。
        angle = transform.eulerAngles;
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            // 視点移動感度
            float sensitiveRotate = 1.5f;

            // 
            float rotateX = Input.GetAxis("Mouse X") * sensitiveRotate;
            //transform.Rotate(0.0f, rotateX, 0.0f);

            angle.x += Input.GetAxis("Mouse X") * sensitiveRotate;

            angle.y -= Input.GetAxis("Mouse Y") * sensitiveRotate;

            transform.parent.eulerAngles = new Vector3(angle.y, transform.parent.parent.parent.eulerAngles.y + angle.x, -transform.parent.parent.parent.eulerAngles.x);

            // 移動できる角度に制限を加える。
            if (angle.y < -30)
            {
                angle.y = -30;
            }

            if (angle.y > 10)
            {
                angle.y = 10;
            }

        }

    }
}
