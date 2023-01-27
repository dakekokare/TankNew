using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class HealItem : /*MonoBehaviour*/ MonoBehaviourPunCallbacks
{
    //�v���C���[
    private GameObject player;
    private float heal;
    //��
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip sound;

    // Start is called before the first frame update
    void Start()
    {
        //�񕜗�
        heal = 20.0f;
        //�v���C���[�T��
        SearchPlayer();
        //��
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        float sin = Mathf.Sin(Time.time);
        this.transform.position = new Vector3(
            transform.position.x,
            (sin * 0.03f) + transform.position.y,
            transform.position.z
            );
    }
    void OnTriggerEnter(Collider t)
    {
        if (photonView.IsMine)
        {
            //�v���C���[�ƐڐG������
            if (t.gameObject.layer == 8)
            {
                //�v���C���[ ismine
                if (t.gameObject.GetComponent<PhotonView>().IsMine)
                    //�ڐG�������
                    HealPlayer();

                //�擾�Đ�
                audioSource.PlayOneShot(sound);

                //��A�N�e�B�u�ɂ���
                photonView.RPC(nameof(ActiveObj), RpcTarget.All);

                //3�b��ɃA�C�e���A�N�e�B�u
                Invoke("ActiveHealItem", 3);

                //�폜
                //PhotonNetwork.Destroy(this.gameObject);
            }
        }
    }

    // �񕜂���
    private void HealPlayer()
    {
        //�v���C���[�ɉ�
        player.transform.GetChild(0).GetComponent<TankHealth>().HealHP(heal);
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
                //�o���A���A�N�e�B�u�ɂ���
                photonView.RPC(nameof(Move), RpcTarget.All, vec);
            }

        }
    }

    [PunRPC]

    private void Move(Vector3 vec)
    {
        //�ړ�
        this.gameObject.transform.position = vec;
        //�A�C�e���\��
        gameObject.SetActive(true);

    }
    [PunRPC]
    private void ActiveObj()
    {
        //�A�C�e����\��
        gameObject.SetActive(false);
    }
}
