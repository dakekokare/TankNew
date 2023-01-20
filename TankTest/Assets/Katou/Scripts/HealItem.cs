using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class HealItem : /*MonoBehaviour*/ MonoBehaviourPunCallbacks
{
    //�v���C���[
    private GameObject player;
    private float heal;
    // Start is called before the first frame update
    void Start()
    {
        //�񕜗�
        heal = 20.0f;
        //�v���C���[�T��
        SearchPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        float sin = Mathf.Sin(Time.time) + transform.position.y;
        this.transform.position = new Vector3(
            transform.position.x,
            sin * 0.5f,
            transform.position.z);
    }
    void OnTriggerEnter(Collider t)
    {
        //�v���C���[�ƐڐG������
        if (t.gameObject.layer == 8)
        {
            //�v���C���[ ismine
            if(t.gameObject.GetComponent<PhotonView>().IsMine)
              //�ڐG�������
                HealPlayer();

            //�A�C�e����\��
            gameObject.SetActive(false);
            //3�b��ɃA�C�e���A�N�e�B�u
            Invoke("ActiveHealItem", 3);

            //�폜
            //PhotonNetwork.Destroy(this.gameObject);
        }
    }

    // �񕜂���
    private void HealPlayer()
    {
        //�v���C���[�ɉ�
        player.GetComponent<TankHealth>().HealHP(heal);
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

    private void ActiveHealItem()
    {
        // scene object
        Scene scene = GameObject.Find("Scene").GetComponent<Scene>();
        //�X�|�[���p�����z��
        string[] s = scene.GetItemString();
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == "FirstAidKit")
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
