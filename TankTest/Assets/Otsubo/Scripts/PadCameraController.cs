using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PadCameraController : MonoBehaviour
{
    //[SerializeField]
    private Camera FPScamera;
    //[SerializeField]
    private Camera TPScamera;
    //[SerializeField]
    private GameObject canvas;

    private GameObject aimImage;
    private bool mainCameraON = true;

    // Start is called before the first frame update
    void Start()
    {
        FPScamera = GameObject.Find("FPSCamera").GetComponent<Camera>();
        TPScamera = GameObject.Find("TPSCamera").GetComponent<Camera>();
        //aimImage  = GameObject.Find("AimImage");
        //canvas = GameObject.Find("Canvas");

        canvas = (GameObject)Resources.Load("Canvas");


        aimImage = canvas.transform.GetChild(1).gameObject;

        FPScamera.enabled = false;
        TPScamera.enabled = true;

        // 主観カメラ（FPSカメラ）がオンの時だけ、照準器もオンにする。
        aimImage.SetActive(false);
    }

    public void OnChange(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && mainCameraON == true)
        {
            FPScamera.enabled = true;
            TPScamera.enabled = false;

            mainCameraON = false;

            aimImage.SetActive(true);
        }
        else if (context.phase == InputActionPhase.Performed && mainCameraON == false)
        {
            FPScamera.enabled = false;
            TPScamera.enabled = true;

            mainCameraON = true;

            aimImage.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
