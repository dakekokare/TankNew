using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerListCount : MonoBehaviourPunCallbacks
{
    //�J�E���g�_�E��
    [SerializeField]
    private GameObject countDown;

    private GameObject hpObj;

    // Update is called once per frame
    void Update()
    {
        int count = 0;
        foreach (var p in PhotonNetwork.PlayerList)
        {
            count++;
            //2�l������
            if (count==2)
            {
                //// �J�E���g�_�E����������
                Instantiate(countDown);
                //�A�N�e�B�u��Ԃ��I�t�ɂ���
                this.gameObject.SetActive(false);

                Invoke("SearchHpEnemyTransform", 2);
            }
        }
    }

    private void SearchHpEnemyTransform()
    {
        hpObj = GameObject.Find("HpEnemy(Clone)");
        //player��������
        hpObj.GetComponent<HpEnemyTransform>().SearchPlayer();
    }
}
