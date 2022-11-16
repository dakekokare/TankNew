using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// MonoBehaviourPunCallbacks���p�����āAPUN�̃R�[���o�b�N���󂯎���悤�ɂ���
public class Scene : MonoBehaviourPunCallbacks
{
    //�v���C���[�X�|�[�����W   
    [SerializeField] private GameObject spawnA;
    [SerializeField] private GameObject spawnB;

    //�v���C���[���Ď�
    private SendVariable sendVariable;
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
        if(PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default))
        {
            Debug.Log("����");
            ////�X�|�[�����W���
            //var position = Vector3.zero;

            //// �i�l�b�g���[�N�I�u�W�F�N�g�j�𐶐�����
            //PhotonNetwork.Instantiate("PlayerNumVariable", position, Quaternion.identity);


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

        //int view = 0;
        //view = GameObject.Find("PlayerNumVariable(Clone)").GetComponent<PhotonView>().ViewID;
        //PhotonView obj;
        //if (view!=0)
        //    obj = PhotonView.Find(view);

        GameObject obj = GameObject.Find("PlayerNumVariable(Clone)");
        //null������
        if (obj == null)
        {
            position = Vector3.zero;

            // �i�l�b�g���[�N�I�u�W�F�N�g�j�𐶐�����
            PhotonNetwork.Instantiate("PlayerNumVariable", position, Quaternion.identity);

            //    //�v���C���[���Ǘ��ϐ�
            GameObject gb = GameObject.Find("PlayerNumVariable(Clone)");

            sendVariable = gb.GetComponent<SendVariable>();
        }



            if (sendVariable.playerNum == 0)
            {
                position = spawnA.transform.position;
                sendVariable.playerNum++;
            }
            else
                position = spawnB.transform.position;

            // �i�l�b�g���[�N�I�u�W�F�N�g�j�𐶐�����
            PhotonNetwork.Instantiate("CanvasObj", position, Quaternion.identity);
            PhotonNetwork.Instantiate("Tank", position, Quaternion.identity);

    }
}