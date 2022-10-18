using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    private Vector3 angle;
    private AudioSource audioS;

    void Start()
    {
        // Turretの最初の角度を取得する。
        angle = transform.eulerAngles;

        audioS = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 割り当てるボタン（キー）は自由に変更可能
        if (Input.GetKey(KeyCode.P))
        {
            audioS.enabled = true;

            angle.x -= 0.5f;

            // （ポイント）親の「旋回角度」に合わせるのが「transform.root.eulerAngles.y」の部分
            transform.eulerAngles = new Vector3(angle.x, transform.root.eulerAngles.y, 0);

            // 移動できる角度に制限を加える。
            if (angle.x < 70)
            {
                angle.x = 70;
            }
        }
        else if (Input.GetKey(KeyCode.L))
        {
            audioS.enabled = true;
            angle.x += 0.5f;
            transform.eulerAngles = new Vector3(angle.x, transform.root.eulerAngles.y, 0);

            if (angle.x > 90)
            {
                angle.x = 90;
            }
        }
        else
        {
            audioS.enabled = false;
        }
    }
}