using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class PlayerListCount : MonoBehaviourPunCallbacks
{
    //�J�E���g�_�E��
    [SerializeField]
    private GameObject countDown;
    //hp
    private GameObject hpObj;
    //Item Spawn �ꏊ
    [SerializeField]
    private GameObject firstAidKitPosition; 
    // Update is called once per frame
    void Update()
    {
        int count = 0;
        foreach (var p in PhotonNetwork.PlayerList)
        {
            count++;
            //2�l������
            if (count==1)
            {
                //// �J�E���g�_�E����������
                Instantiate(countDown);
                //�A�N�e�B�u��Ԃ��I�t�ɂ���
                this.gameObject.SetActive(false);

                //�v���C���[�T��
                Invoke("SearchHpEnemyTransform", 2);

                //�}�X�^�[�N���C�A���g�Ȃ�Ȃ�@�A�C�e������
                if(PhotonNetwork.IsMasterClient)
                    //�A�C�e������
                    Invoke("GenerationItem", 2);

             
            }
        }
    }

    private void SearchHpEnemyTransform()
    {
        hpObj = GameObject.Find("HpEnemy(Clone)");
        //player��������
        hpObj.GetComponent<HpEnemyTransform>().SearchPlayer();
    }
    private void GenerationItem()
    {
        Debug.Log("firstaidkit ����");
        //�A�C�e������
        PhotonNetwork.Instantiate("FirstAidKit", firstAidKitPosition.transform.position, Quaternion.identity);
    }
}
