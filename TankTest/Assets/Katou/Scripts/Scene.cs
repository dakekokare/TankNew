using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
//using ExitGames.Client.Photon;

// MonoBehaviourPunCallbacks���p�����āAPUN�̃R�[���o�b�N���󂯎���悤�ɂ���
public class Scene : MonoBehaviourPunCallbacks
{
    //�v���C���[�X�|�[�����W
    [SerializeField]
    private GameObject[] array;

    //�A�C�e�����W
    [SerializeField]
    private GameObject itemPos;

    //�A�C�e�������p�����z��
    [SerializeField]
    private string[] itemString;

    ////�F���
    //private Vector3 pColor;
    //private Vector3 eColor;

    private GameObject colorObj;
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
        //���g�̐F�����Z�b�g����
        Color col = SceneShare.GetColor();
        Vector3 cVec = new Vector3(col.r, col.g, col.b);
        //�}�X�^�[�N���C�A���g
        if (PhotonNetwork.IsMasterClient)
        {
            Vector3 position = Vector3.zero;
            colorObj=PhotonNetwork.Instantiate("Color", position, Quaternion.identity);

            //�F����ǉ�
            colorObj.GetComponent<SaveColor>().AddPlayerColor(cVec);
        }
        else
        {
            //�J���[�I�u�W�F�N�g�擾
            SearchSaveColor();
            //�F����ǉ�
            colorObj.GetComponent<SaveColor>().AddEnemyColor(cVec);

            int num = PhotonNetwork.CurrentRoom.GetSpawn();
            //���v���C���[�̃X�|�[�����W�m�F
            // num ==0 -> �����l�A�}�X�^�[�N���C�A���g���X�|�[�����Ă��Ȃ�
            if (num == 0)
            {
                //�b��ɍċN����
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
            r = GetRandomNum(num, array);
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

    private int GetRandomNum(int num, GameObject[] obj)
    {

        //�����擾
        int r = Random.Range(1, obj.Length);
        //���v���C���[�̃X�|�[�����W��������
        if (r == num)
            //�ċN
            r = GetRandomNum(num, obj);
        return r;
    }
    private int GetItemRandomNum(int num, int[] itemNumber)
    {
        //�����ꏊ���d�Ȃ�Ȃ��p�ɗ����擾
        int r = Random.Range(1, num);

        //�����̍��W�ɑ��A�C�e�����X�|�[�����Ă��Ȃ����`�F�b�N����
        for (int i = 0; i < itemNumber.Length; i++)
        {
            //���A�C�e���̃X�|�[�����W��������
            if (r == itemNumber[i])
            {
                //�ċN
                r = GetItemRandomNum(num, itemNumber);
            }
        }
        return r;
    }

    public void GenerationItem()
    {
        ////�A�C�e�����W�ԍ��z��
        int[] itemSpawnNumber = new int[itemString.Length];

        //�A�C�e���̐��@
        for (int i = 0; i < itemString.Length; i++)
        {
            //�����ŃA�C�e���X�|�[�����W�����߂�
            itemSpawnNumber[i] = GetItemRandomNum(itemPos.transform.childCount, itemSpawnNumber);
        }
        //�A�C�e������
        ItemCreate(itemSpawnNumber, itemPos);

        //�J�X�^���v���p�e�B�Ɏg�p���̍��W�ԍ����Z�b�g����
        PhotonNetwork.CurrentRoom.SetItemSpawn(itemSpawnNumber);



    }
    public Vector3 MoveItem(int index)
    {
        ////�g�p����Ă���A�C�e�����W��Ⴄ
        int[] item = PhotonNetwork.CurrentRoom.GetItemSpawn();
        //�g�p����Ă��Ȃ����W�ԍ��@
        int r = GetItemRandomNum(itemPos.transform.childCount, item);
        //�v�f�ԍ��̒��g������������
        item[index] = r;
        //�J�X�^���v���p�e�B�Ɏg�p���̍��W�ԍ����Z�b�g����
        PhotonNetwork.CurrentRoom.SetItemSpawn(item);

        Debug.Log("[" + itemString[index] + r + "]����");
        return itemPos.transform.GetChild(r).transform.position;
    }

    private void ItemCreate(int[] itemPosNum, GameObject spawnPos)
    {
        for (int i = 0; i < itemString.Length; i++)
        {
            Debug.Log("[" + itemString[i] + "]����");
            //item����
            PhotonNetwork.Instantiate(
                itemString[i],
                spawnPos.transform.GetChild(itemPosNum[i]).transform.position,
                Quaternion.identity
                );
        }
    }

    public string[] GetItemString()
    {
        return itemString;
    }



    //void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        Debug.Log("���M");
    //        Debug.Log("[p" + pColor + "]");


    //        //�f�[�^�̑��M
    //        stream.SendNext(pColor);
    //    }
    //    else
    //    {
    //        Debug.Log("��M");
    //        Debug.Log("[e" + eColor + "]");
    //        //�f�[�^�̎�M
    //        eColor = (Vector3)stream.ReceiveNext();
    //    }
    //}

    //private void Update()
    //{
    //}


    private void SearchSaveColor()
    {
        // ���[�����̃l�b�g���[�N�I�u�W�F�N�g
        foreach (var photonView in PhotonNetwork.PhotonViewCollection)
        {
            //Color �I�u�W�F�N�g
            if (photonView.gameObject.name == "Color")
            {
                colorObj = PhotonView.Find(photonView.ViewID).gameObject;

            }
        }
    }
}