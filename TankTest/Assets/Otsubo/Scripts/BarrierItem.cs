using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
////�ǉ���
//using UnityEditor;

////�����Enemy��public���C���X�y�N�^�ɕ\�������
//#if UNITY_EDITOR
//[CustomEditor(typeof(Item))]
//#endif

public class BarrierItem : /*Item*/MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject effectPrefab;
    //�v���C���[
    private GameObject player;
    //�o���A�I�[�u
    [SerializeField]
    private GameObject barrierPrefab;
    // Start is called before the first frame update
    void Start()
    {
        //�v���C���[�T��
        SearchPlayer();
    }

    void Update()
    {
        float sin = Mathf.Sin(Time.time) + transform.position.y;
        this.transform.position = new Vector3(
            transform.position.x,
            sin * 0.5f,
            transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        //�v���C���[�ƐڐG������
        if (other.gameObject.layer == 8)
        {

            //�o���A���A�N�e�B�u�ɂ���
            photonView.RPC(nameof(ActiveBarrier), RpcTarget.All, other.GetComponent<PhotonView>().ViewID);

            //�A�C�e����\��
            gameObject.SetActive(false);
            //�A�C�e���\������
            Invoke("ActiveBarrierItem", 3);


            // �A�C�e���Q�b�g���ɃG�t�F�N�g�𔭐�������B
            GameObject effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);

            // �G�t�F�N�g��0.5�b��ɏ����B
            Destroy(effect, 0.5f);
        }
    }

    public void SearchPlayer()
    {
        // ���[�����̃l�b�g���[�N�I�u�W�F�N�g
        foreach (var photonView in PhotonNetwork.PhotonViewCollection)
        {
            //boat ���@����
            if (photonView.gameObject.name == "Boat(Clone)")
            {
                if (photonView.IsMine)
                {
                    Debug.Log("[" + GetInstanceID() + "]" + "Player Find");
                    player = PhotonView.Find(photonView.ViewID).gameObject;
                }
            }
        }
    }
    [PunRPC]
    private void ActiveBarrier(int id)
    {
        GameObject obj = PhotonView.Find(id).gameObject;
        //�o���A�A�N�e�B�u
        obj.gameObject.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
    }

    [PunRPC]
    private void DestroyBarrier(int id)
    {
        GameObject obj = PhotonView.Find(id).gameObject;
        PhotonNetwork.Destroy(obj);
    }

    private void ActiveBarrierItem()
    {
        // scene object
        Scene scene = GameObject.Find("Scene").GetComponent<Scene>();
        //�X�|�[���p�����z��
        string[] s=scene.GetItemString();
        for(int i=0;i<s.Length;i++)
        {
            if(s[i]=="BarrierItem")
            {
                ////�A�C�e�����X�|�[�����Ă��Ȃ����W�̗����ɍ��W��Ⴄ
                Vector3 vec = scene.MoveItem(i);
                //�ړ�
                this.gameObject.transform.position = vec;
            }

        }
        //�A�C�e���\��
        gameObject.SetActive(true);
    }

}
