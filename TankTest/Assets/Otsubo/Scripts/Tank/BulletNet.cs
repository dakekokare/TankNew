using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class BulletNet : MonoBehaviour
{
    [SerializeField]
    Material mat;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        //3�b��ɔj��
        Destroy(this.gameObject, 3.0f);
    }
    // �e��ID��Ԃ��v���p�e�B
    public int Id { get; private set; }
    // �e�𔭎˂����v���C���[��ID��Ԃ��v���p�e�B
    public int OwnerId { get; private set; }
    // �����e���ǂ�����ID�Ŕ��肷�郁�\�b�h
    public bool Equals(int id, int ownerId) => id == Id && ownerId == OwnerId;

    public void Init(int id, int ownerId)
    {
        Id = id;
        OwnerId = ownerId;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Barrier")
        {
            //�o���A�ڐG����
            HitBarrier(other);
        }
        //�v���C���[��������
        else if (other.gameObject.layer == 8)
        {
            //�v���C���[�ڐG����
            HitPlayer(other);
        }
        else 
        {
            //���̑��̐ڐG����
            HitOthers(other);
        }
    }
    public void ChengeMaterial()
    {
        // ���������v���n�u�̃}�e���A����ݒ�
        this.gameObject.GetComponent<MeshRenderer>().material = mat;
    }
    private void DestroyShellOtherPlayer()
    {
        ////�e�̍폜
        player.transform.
        GetChild(0).
        GetChild(0).
        GetChild(0).
        GetChild(1).
        GetChild(0).
        GetComponent<ShotShell>().DeleteShellOther(Id, OwnerId);
    }

    public void SetPlayer(GameObject obj)
    {
        player = obj;
    }

    private void HitBarrier(Collider other) 
    {
        //�����̃o���A
        if (other.GetComponent<PhotonView>().IsMine)
        {
            //�����̒e�̎�
            if (this.gameObject.tag == "Shell")
            {
                Debug.Log("Barrier shell Hit return");
                return;
            }
            //�G�̒e�̎�
            else if (this.gameObject.tag == "EnemyShell")
            {
                Debug.Log("Barrier enemyShell Hit");
                //�����Ƒ���̒e������
                DestroyShellOtherPlayer();
                Destroy(this.gameObject);
            }
        }
    }

    private void HitPlayer(Collider other)
    {
        //enemyShell��������
        if (gameObject.tag == "EnemyShell")
        {

            /////////////////////////////////////////////
            // 
            ///////////////////////////////////////////
            //��������Ȃ�������
            if (!other.GetComponent<PhotonView>().IsMine)
            {
                Debug.Log("Boat EnemyShell Hit return");
                return;
            }
            else
            {
                Debug.Log("Boat EnemyShell Hit");
                //�D�ƐڐG������,�_���[�W����
                other.gameObject.GetComponent<TankHealth>().HitBullet();

                //�����Ƒ���̒e������
                DestroyShellOtherPlayer();
                Destroy(this.gameObject);
            }
        }
    }
    private void HitOthers(Collider other)
    {
        Debug.Log("Other Hit");
        //�����Ƒ���̒e������
        DestroyShellOtherPlayer();
        Destroy(this.gameObject);
    }
}
