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
    

    //�҂�����
    private int waitSecond=3;
    // Update is called once per frame
    void Update()
    {
        //�v���C���[��2�l����Ȃ����
        if (PhotonNetwork.PlayerList.Length != 1)
            return;
        //�Q�[���J�n�O�J�E���g�_�E��������
        InitializeGame();
    }

    private void InitializeGame()
    {
        //// �J�E���g�_�E����������
        Instantiate(countDown);

        //�v���C���[�T��
        Invoke("SearchHpEnemyTransform", waitSecond);

        //�}�X�^�[�N���C�A���g�Ȃ�Ȃ�@�A�C�e������
        if (PhotonNetwork.IsMasterClient)
            //�A�C�e������
            Invoke("CreateItem", waitSecond);

        //�A�N�e�B�u��Ԃ��I�t�ɂ���
        this.gameObject.GetComponent<PlayerListCount>().enabled=false;
    }

    private void SearchHpEnemyTransform()
    {
        hpObj = GameObject.Find("HpEnemy(Clone)");
        //player��������
        hpObj.GetComponent<HpEnemyTransform>().SearchPlayer();
    }
    private void CreateItem()
    {
        // scene object
        Scene scene = GameObject.Find("Scene").GetComponent<Scene>();
        //�A�C�e������
        scene.GenerationItem();
    }

}
