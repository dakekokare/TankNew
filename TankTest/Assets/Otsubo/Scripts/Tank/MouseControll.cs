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
        // Turret�̍ŏ��̊p�x���擾����B
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


        //// ���蓖�Ă�{�^���i�L�[�j�͎��R�ɕύX�\
        //if (Input.GetKey(KeyCode.Q))
        //{
        //    //audioS.enabled = true;

        //    angle.y -= 0.2f;

        //    // �i�|�C���g�j�e�́u����p�x�v�ɍ��킹��̂��utransform.root.eulerAngles.y�v�̕���
        //    transform.eulerAngles = new Vector3(0, transform.root.eulerAngles.y + angle.y, 0);

        //    // �ړ��ł���p�x�ɐ�����������B
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