using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Smoke : MonoBehaviourPunCallbacks
{
    // �v���C���[
    //private GameObject player;

    private float Smoketime = 5.0f;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // �^�C�}�[�̎��Ԃ𓮂���
        timer += Time.deltaTime;

        // 5�b�o������
        if (timer > Smoketime)
        {
            // �^�C�}�[�̎��Ԃ��O�ɖ߂��B
            timer = 0.0f;

            // �o���A�������B
            gameObject.SetActive(false);
            ////���v���C���[�̉����\���ɂ���
            //this.gameObject.transform.parent.GetComponent<ShotShell>().DisActiveSmoke();
        }
    }
}