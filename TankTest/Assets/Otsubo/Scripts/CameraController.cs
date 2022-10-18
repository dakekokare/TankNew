using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Camera FPSCamera;
    [SerializeField]
    private Camera TPSCamera;
    // �ubool�v�́utrue�v���ufalse�v�̓���̏����������Ƃ��ł��܂��i�|�C���g�j
    private bool mainCameraON = true;

    void Start()
    {
        FPSCamera.enabled = true;
        TPSCamera.enabled = false;
    }

    void Update()
    {
        // �i�d�v�|�C���g�j�u&&�v�͘_���֌W�́u���v���Ӗ�����B
        // �uA && B�v�́uA ���� B�v�i����A��B�̗��������������Ƃ����Ӗ��j
        // �u==�v�́u���E���������v�Ƃ����Ӗ�
        // �������uC�{�^���v�����������A�u���v�A�umainCameraON�v�̃X�e�[�^�X���utrue�v�̎��i�����j
        if (Input.GetKeyDown(KeyCode.C) && mainCameraON == true)
        {
            FPSCamera.enabled = false;
            TPSCamera.enabled = true;

            mainCameraON = false;

        } // �������uC�{�^���v�����������A�u���v�A�umainCameraON�v�̃X�e�[�^�X���ufalse�v�̎��i�����j
        else if (Input.GetKeyDown(KeyCode.C) && mainCameraON == false)
        {
            FPSCamera.enabled = true;
            TPSCamera.enabled = false;

            mainCameraON = true;
        }
    }
}