using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Camera FPSCamera;
    [SerializeField]
    private Camera TPSCamera;
    // 「bool」は「true」か「false」の二択の情報を扱うことができます（ポイント）
    private bool mainCameraON = true;

    void Start()
    {
        FPSCamera.enabled = true;
        TPSCamera.enabled = false;
    }

    void Update()
    {
        // （重要ポイント）「&&」は論理関係の「かつ」を意味する。
        // 「A && B」は「A かつ B」（条件AとBの両方が揃った時という意味）
        // 「==」は「左右が等しい」という意味
        // もしも「Cボタン」を押した時、「かつ」、「mainCameraON」のステータスが「true」の時（条件）
        if (Input.GetKeyDown(KeyCode.C) && mainCameraON == true)
        {
            FPSCamera.enabled = false;
            TPSCamera.enabled = true;

            mainCameraON = false;

        } // もしも「Cボタン」を押した時、「かつ」、「mainCameraON」のステータスが「false」の時（条件）
        else if (Input.GetKeyDown(KeyCode.C) && mainCameraON == false)
        {
            FPSCamera.enabled = true;
            TPSCamera.enabled = false;

            mainCameraON = true;
        }
    }
}