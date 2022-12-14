using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TurretItem : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //float sin = Mathf.Sin(Time.time) + transform.position.y;
        //this.transform.position = new Vector3(
        //    transform.position.x,
        //    sin * 0.5f,
        //    transform.position.z);
    }

    void OnTriggerEnter(Collider t)
    {
        //�v���C���[�ƐڐG������
        if (t.gameObject.layer == 8)
        {
            //�U���e�^���b�g�A�N�e�B�u
            t.gameObject.transform.GetChild(0).GetChild(0).GetChild(1)
                .gameObject.SetActive(true);
            //�f�t�H���g�^���b�gfalse
            t.gameObject.transform.GetChild(0).GetChild(0).GetChild(0)
                .gameObject.SetActive(false);

            //�A�C�e���폜
            PhotonNetwork.Destroy(this.gameObject);
        }
    }
}
