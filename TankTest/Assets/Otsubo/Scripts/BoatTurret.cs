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
            float sensitiveRotate = 1.0f;

            // 
            float rotateX = Input.GetAxis("Mouse X") * sensitiveRotate;
            transform.Rotate(0.0f, rotateX, 0.0f);

            angle.z = Input.GetAxis("Mouse Y") * sensitiveRotate;

            transform.parent.eulerAngles = new Vector3(0, transform.parent.parent.eulerAngles.y, angle.z);

            // 移動できる角度に制限を加える。
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
