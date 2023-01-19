using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
//using ExitGames.Client.Photon;

// MonoBehaviourPunCallbacks���p�����āAPUN�̃R�[���o�b�N���󂯎���悤�ɂ���
public class Scene : MonoBehaviourPunCallbacks
{
    //�v���C���[�X�|�[�����W   
    public GameObject[] array;
    
    private void Start()
    {
        // �v���C���[���g�̖��O��"Player"�ɐݒ肷��
        PhotonNetwork.NickName = "Player";
        // PhotonServerSettings�̐ݒ���e���g���ă}�X�^�[�T�[�o�[�֐ڑ�����
        PhotonNetwork.ConnectUsingSettings();
    }



    // �}�X�^�[�T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnConnectedToMaster()
    {
        // "Room"�Ƃ������O�̃��[���ɎQ������i���[�������݂��Ȃ���΍쐬���ĎQ������j
        if (PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default))
        {
            Debug.Log("����");
        }
        else
        {
            Debug.Log("���łɐ�������Ă���");
        }
    }

    // �Q�[���T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnJoinedRoom()
    {


        //���v���C���[�̃X�|�[�����W�m�F
        CheckOtherPlayerSpawnNum();
    }
    private void CheckOtherPlayerSpawnNum()
    {
        //�}�X�^�[�N���C�A���g�ł͂Ȃ��Ƃ�
        if (!PhotonNetwork.IsMasterClient)
        {
            int num = PhotonNetwork.CurrentRoom.GetSpawn();
            //���v���C���[�̃X�|�[�����W�m�F
            // num ==0 -> �����l�A�}�X�^�[�N���C�A���g���X�|�[�����Ă��Ȃ�
            if (num == 0)
            {
                //3�b��ɍċN����
                Invoke("CheckOtherPlayerSpawnNum", 1);
                return;
            }
        }
        //�I�u�W�F�N�g����
        CreateObject();
    }

    private void CreateObject()
    {

        //�I�u�W�F�N�g����
        //�X�|�[�����W���
        var position = Vector3.zero;
        int r;
        //�}�X�^�[�N���C�A���g
        if (PhotonNetwork.IsMasterClient)
        {
            //�X�|�[�����W�擾
            r = Random.Range(1, array.Length);
            PhotonNetwork.CurrentRoom.SetSpawn(r);
        }
        else
        {
            //���v���C���[�̃X�|�[�����W
            int num = PhotonNetwork.CurrentRoom.GetSpawn();
            r = GetRandomNum(num);
        }
        position = array[r].gameObject.transform.position;
        PhotonNetwork.Instantiate("Boat", position, Quaternion.identity);



        GameObject obj = (GameObject)Resources.Load("CanvasObj");
        //��������
        Instantiate(obj);
        //playerHp����
        obj = (GameObject)Resources.Load("HpPlayer");
        //��������
        Instantiate(obj);
        //EnemyHp����
        obj = (GameObject)Resources.Load("HpEnemy");
        //��������
        Instantiate(obj);
    }

    private int GetRandomNum(int num)
    {
        
        //�����擾
        int r = Random.Range(1, array.Length);
        //���v���C���[�̃X�|�[�����W��������
        if (r == num)
            //�ċN
            GetRandomNum(num);
        return r;
    }

}