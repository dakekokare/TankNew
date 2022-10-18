using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHeadMovement : MonoBehaviour
{
    private Vector3 angle;
    private AudioSource audioS;

    void Start()
    {
        // Turret�̍ŏ��̊p�x���擾����B
        angle = transform.eulerAngles;

        audioS = GetComponent<AudioSource>();
    }

    void Update()
    {
        // ���蓖�Ă�{�^���i�L�[�j�͎��R�ɕύX�\
        if (Input.GetKey(KeyCode.Q))
        {
            audioS.enabled = true;

            angle.y -= 0.5f;

            // �i�|�C���g�j�e�́u����p�x�v�ɍ��킹��̂��utransform.root.eulerAngles.y�v�̕���
            transform.eulerAngles = new Vector3(angle.y, transform.root.eulerAngles.y, 0);

            // �ړ��ł���p�x�ɐ�����������B
            if (angle.y < 70)
            {
                angle.y = 70;
            }
        }
        else if (Input.GetKey(KeyCode.E))
        {
            audioS.enabled = true;
            angle.y += 0.5f;
            transform.eulerAngles = new Vector3(angle.y, transform.root.eulerAngles.y, 0);

            if (angle.y > 90)
            {
                angle.y = 90;
            }
        }
        else
        {
            audioS.enabled = false;
        }
    }
}