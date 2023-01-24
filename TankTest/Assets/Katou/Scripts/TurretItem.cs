using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TurretItem : MonoBehaviour
{

    void OnTriggerEnter(Collider t)
    {
        //�v���C���[�ƐڐG������
        if (t.gameObject.layer == 8)
        {
            //�U���e�^���b�g�A�N�e�B�u
            t.gameObject.transform.GetChild(0).GetChild(0).GetChild(3)
                .gameObject.SetActive(true);
            //�f�t�H���g�^���b�gfalse
            t.gameObject.transform.GetChild(0).GetChild(0).GetChild(2)
                .gameObject.SetActive(false);

            //�A�C�e����\��
            gameObject.SetActive(false);

            //���b��ɃA�C�e���\��
            Invoke("ActiveTurretItem", 3);
        }
    }

    private void ActiveTurretItem()
    {
        // scene object
        Scene scene = GameObject.Find("Scene").GetComponent<Scene>();
        //�X�|�[���p�����z��
        string[] s = scene.GetItemString();
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == "MissileTurret")
            {
                ////�A�C�e�����X�|�[�����Ă��Ȃ����W�̗����ɍ��W��Ⴄ
                Vector3 vec = scene.MoveItem(i);
                //�ړ�
                this.gameObject.transform.position = vec;
            }

        }
        //�A�C�e���\��
        gameObject.SetActive(true);
    }
}
