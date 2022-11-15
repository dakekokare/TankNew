using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //[SerializeField]
    private Camera FPScamera;
    //[SerializeField]
    private Camera TPScamera;
    // �ubool�v�́utrue�v���ufalse�v�̓���̏����������Ƃ��ł��܂��i�|�C���g�j
    private bool mainCameraON = true;

    //[SerializeField]
    private GameObject canvas;
    private GameObject aimImage;

    void Start()
    {
        FPScamera = GameObject.Find("FPSCamera").GetComponent<Camera>();
        TPScamera = GameObject.Find("TPSCamera").GetComponent<Camera>();
        //aimImage  = GameObject.Find("AimImage");
        canvas = GameObject.Find("CanvasObj(Clone)");

        //canvas = (GameObject)Resources.Load("CanvasObj");


        aimImage = canvas.transform.GetChild(0).GetChild(1).gameObject;

        FPScamera.enabled = false;
        TPScamera.enabled = true;

        // �i���z�j��σJ�����iFPS�J�����j���I���̎������A�Ə�����I���ɂ���B
        aimImage.gameObject.SetActive(false);
    }

    void Update()
    {
        // �i�d�v�|�C���g�j�u&&�v�͘_���֌W�́u���v���Ӗ�����B
        // �uA && B�v�́uA ���� B�v�i����A��B�̗��������������Ƃ����Ӗ��j
        // �u==�v�́u���E���������v�Ƃ����Ӗ�
        // �������uC�{�^���v�����������A�u���v�A�umainCameraON�v�̃X�e�[�^�X���utrue�v�̎��i�����j
        if (Input.GetKeyDown(KeyCode.C) && mainCameraON == true)
        {
            FPScamera.enabled = true;
            TPScamera.enabled = false;

            mainCameraON = false;

            aimImage.gameObject.SetActive(true);

        } // �������uC�{�^���v�����������A�u���v�A�umainCameraON�v�̃X�e�[�^�X���ufalse�v�̎��i�����j
        else if (Input.GetKeyDown(KeyCode.C) && mainCameraON == false)
        {
            FPScamera.enabled = false;
            TPScamera.enabled = true;

            mainCameraON = true;

            aimImage.gameObject.SetActive(false);
        }
    }
}