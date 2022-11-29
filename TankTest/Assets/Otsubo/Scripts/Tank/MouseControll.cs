using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MouseControll : MonoBehaviourPunCallbacks
{
    private Vector3 angle;
    private AudioSource audioS;

    void Start()
    {
        // Turretの最初の角度を取得する。
        angle = transform.eulerAngles;

        //audioS = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (photonView.IsMine)
        {

            float sensitiveRotate = 1.0f;

            float rotateX = Input.GetAxis("Mouse X") * sensitiveRotate;

            transform.Rotate(0.0f, rotateX, 0.0f);
        }


        //// 割り当てるボタン（キー）は自由に変更可能
        //if (Input.GetKey(KeyCode.Q))
        //{
        //    //audioS.enabled = true;

        //    angle.y -= 0.2f;

        //    // （ポイント）親の「旋回角度」に合わせるのが「transform.root.eulerAngles.y」の部分
        //    transform.eulerAngles = new Vector3(0, transform.root.eulerAngles.y + angle.y, 0);

        //    // 移動できる角度に制限を加える。
        //    if (angle.y < 70)
        //    {
        //        //angle.y = 70;
        //    }
        //}
        //else if (Input.GetKey(KeyCode.E))
        //{
        //    //audioS.enabled = true;
        //    angle.y += 0.2f;
        //    transform.eulerAngles = new Vector3(0, transform.root.eulerAngles.y + angle.y, 0);

        //    if (angle.y > 90)
        //    {
        //        //angle.y = 90;
        //    }
        //}
        //else
        //{
        //    //audioS.enabled = false;
        //}
    }
}