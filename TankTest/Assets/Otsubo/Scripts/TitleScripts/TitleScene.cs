using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// MonoBehaviourPunCallbacks���p�����āAPUN�̃R�[���o�b�N���󂯎���悤�ɂ���
public class TitleScene : MonoBehaviourPunCallbacks
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
            ////�X�|�[�����W���
            //var position = Vector3.zero;

        }
        else
        {
            Debug.Log("���łɐ�������Ă���");
        }
    }

    // �Q�[���T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnJoinedRoom()
    {

        //�X�|�[�����W���
        var position = Vector3.zero;

        int r =Random.Range(0,1);
        position=array[r].gameObject.transform.position;
        PhotonNetwork.Instantiate("TitleBoat", position, Quaternion.identity);

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
}