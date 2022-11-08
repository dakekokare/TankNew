using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Aim : MonoBehaviour
{
    [SerializeField]
    private Image aimImage;

    void Update()
    {
        // レーザー（ray）を飛ばす「起点」と「方向」
        Ray ray = new Ray(transform.position, transform.forward);

        // rayのあたり判定の情報を入れる箱を作る。
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 60))
        {
            string hitName = hit.transform.gameObject.tag;

            if (hitName == "Enemy")
            {
                // 照準器の色を「赤」に変える
                aimImage.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            }
            else
            {
                // 照準器の色を「水色」
                aimImage.color = new Color(0.0f, 1.0f, 1.0f, 1.0f);
            }
        }
        else
        {
            // 照準器の色を「水色」
            aimImage.color = new Color(0.0f, 1.0f, 1.0f, 1.0f);
        }
    }
}