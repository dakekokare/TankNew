using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerListCount : MonoBehaviourPunCallbacks
{
    //�J�E���g�_�E��
    [SerializeField]
    private GameObject countDown;


    // Start is called before the first frame update
    void Start()
    {
        
    }

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

                // hp�𐶐�
                photonView.RPC(nameof(CreateHp), RpcTarget.All);
            }
        }
    }

    [PunRPC]
    private void CreateHp()
    {
        GameObject obj = (GameObject)Resources.Load("HpEnemy");
        //��������
        Instantiate(obj);

        // ���[�����̃l�b�g���[�N�I�u�W�F�N�g
        foreach (var photonView in PhotonNetwork.PhotonViewCollection)
        {
            //boat ���@����
            if (photonView.gameObject.name == "Boat(Clone)")
            {
                if (photonView.IsMine)
                {
                    Debug.Log("CreateHP");
                    obj = PhotonView.Find(photonView.ViewID).gameObject;
                    obj.GetComponent<TankHealth>().SetEnemyHpUi();

                }
            }
        }
    }
}
