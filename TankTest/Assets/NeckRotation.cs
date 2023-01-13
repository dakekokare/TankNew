using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class NeckRotation : MonoBehaviourPunCallbacks
{
    //private Vector3 angle;

    //void Start()
    //{
    //    // Turretの最初の角度を取得する。
    //    angle = transform.eulerAngles;
    //}

    //void Update()
    //{
    //    if (photonView.IsMine)
    //    {
    //        // 視点移動感度
    //        float sensitiveRotate = 1.5f;


    //        angle.x += Input.GetAxis("Mouse X") * sensitiveRotate;

    //        transform.eulerAngles = new Vector3(
    //            transform.eulerAngles.x,
    //            transform.parent.parent.eulerAngles.y+ angle.x,
    //            transform.eulerAngles.z
    //            );

    //        // 移動できる角度に制限を加える。
    //        if (angle.x > 10)
    //        {
    //            angle.x = 10;
    //        }
    //    }

    //}
}
