using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// MonoBehaviourPunCallbacks���p�����āAPUN�̃R�[���o�b�N���󂯎���悤�ɂ���
public class Scene : MonoBehaviourPunCallbacks, IPunObservable
{
    //�v���C���[�X�|�[�����W   
    public GameObject[] array;

    //�X�|�[�����W�ԍ�
    private int spawnNum=100;
    //���v���C���[�X�|�[�����W�ԍ�
    private int otherSpawnNum=100;

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
            //���v���C���[�̃X�|�[�����W�m�F
            if (otherSpawnNum == 100)
            {
                //3�b��ɍċN����
                Invoke("CheckOtherPlayerSpawnNum", 3);
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
        //�X�|�[�����W�擾
        spawnNum = GetRandomNum();
        position = array[spawnNum].gameObject.transform.position;
        PhotonNetwork.Instantiate("Boat", position, Quaternion.identity);

        position = Vector3.zero;
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

    private int GetRandomNum()
    {
        //�����擾
        int r = Random.Range(0, 5);
        //���v���C���[�̃X�|�[�����W��������
        if (r == otherSpawnNum)
            //�ċN
            GetRandomNum();
        return r;
    }


    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // ���M����
            stream.SendNext(spawnNum);
        }
        else
        {
            // ��M����
            otherSpawnNum = (int)stream.ReceiveNext();
        }
    }
}