using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TurretItem : MonoBehaviourPunCallbacks
{
    //��
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip sound;
    void Start()
    {
        //��
        audioSource = GetComponent<AudioSource>();

    }

    private void Update()
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
                //�^���b�g���A�N�e�B�u�ɂ���
                photonView.RPC(nameof(ActiveObj), RpcTarget.All, t.gameObject.GetComponent<PhotonView>().ViewID);
                //�擾�Đ�
                audioSource.PlayOneShot(sound);


                //���b��ɃA�C�e���\��
                Invoke("ActiveTurretItem", 3);
            }
        }
    }

    private void ActiveTurretItem()
    {
        // scene object
        Scene scene = GameObject.Find("Scene").GetComponent<Scene>();
        //�X�|�[���p�����z��
        string[] s = scene.GetItemString();
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == "MissileTurret")
            {
                ////�A�C�e�����X�|�[�����Ă��Ȃ����W�̗����ɍ��W��Ⴄ
                Vector3 vec = scene.MoveItem(i);
                //�A�C�e���ړ�
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
    private void ActiveObj(int id)
    {
        GameObject t = PhotonView.Find(id).gameObject;


        //�U���e�^���b�g�A�N�e�B�u
        t.gameObject.transform.GetChild(0).GetChild(0).GetChild(2)
            .gameObject.SetActive(true);
        //�f�t�H���g�^���b�gfalse
        t.gameObject.transform.GetChild(0).GetChild(0).GetChild(1)
            .gameObject.SetActive(false);

        //�A�C�e����\��
        gameObject.SetActive(false);

    }
}
