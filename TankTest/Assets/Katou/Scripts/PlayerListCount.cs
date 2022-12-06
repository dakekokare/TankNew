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

    // Start is called before the first frame update
    void Start()
    {
        //hpObj = (GameObject)Resources.Load("HpEnemy");
    }

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


                ////hp��������
                //Instantiate(hpObj);

                // hp�̏����Z�b�g����
                // ���[�����̃l�b�g���[�N�I�u�W�F�N�g
                //foreach (var photonView in PhotonNetwork.PhotonViewCollection)
                //{
                //    //boat ���@����
                //    if (photonView.gameObject.name == "Boat(Clone)")
                //    {
                //        if (photonView.IsMine)
                //        {
                //            Debug.Log("CreateHP");
                //            hpObj = PhotonView.Find(photonView.ViewID).gameObject;
                //            hpObj.GetComponent<TankHealth>().SetEnemyHpUi();
                //            break;
                //        }
                //    }
                //}

                //hp Enemy�ɃR���|�[�l���g��ǉ�
                SearchHpEnemyTransform();

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
