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
        // ���[�U�[�iray�j���΂��u�N�_�v�Ɓu�����v
        Ray ray = new Ray(transform.position, transform.forward);

        // ray�̂����蔻��̏������锠�����B
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 60))
        {
            string hitName = hit.transform.gameObject.tag;

            if (hitName == "Enemy")
            {
                // �Ə���̐F���u�ԁv�ɕς���
                aimImage.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            }
            else
            {
                // �Ə���̐F���u���F�v
                aimImage.color = new Color(0.0f, 1.0f, 1.0f, 1.0f);
            }
        }
        else
        {
            // �Ə���̐F���u���F�v
            aimImage.color = new Color(0.0f, 1.0f, 1.0f, 1.0f);
        }
    }
}