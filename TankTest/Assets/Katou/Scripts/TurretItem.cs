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
        //�A�C�e���\��
        gameObject.SetActive(true);
    }
}
