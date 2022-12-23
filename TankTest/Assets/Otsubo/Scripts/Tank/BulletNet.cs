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
            ////�G�̃o���A�ɓ���������
            //else 
            //{
            //    //�G �̒e�̎�
            //    if (this.gameObject.tag == "EnemyShell")
            //    {
            //        return;
            //    }
            //    //�e�̎�
            //    else if (this.gameObject.tag == "Shell")
            //    {
            //        //�����Ƒ���̒e������
            //        DestroyShellOtherPlayer(other.gameObject);
            //        Destroy(this.gameObject);
            //    }
            //}
        }


        //�v���C���[��������
        if (other.gameObject.layer == 8)
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
            //    else if (gameObject.tag == "Shell")
            //    {
            //        //�����ɂ���������
            //        if (other.GetComponent<PhotonView>().IsMine)
            //            return;
            //        else
            //        {
            //            //�D�ƐڐG������,�_���[�W����
            //            other.gameObject.GetComponent<TankHealth>().HitBullet();
            //            //�����Ƒ���̒e������
            //            DestroyShellOtherPlayer(other.gameObject);
            //            Destroy(this.gameObject);
            //        }
            //    }
        }
    }
    public void ChengeMaterial()
    {
        // ���������v���n�u�̃}�e���A����ݒ�
        this.gameObject.GetComponent<MeshRenderer>().material = mat;
    }
    private void DestroyShellOtherPlayer()
    {
        player.transform.
        GetChild(0).
        GetChild(0).
        GetChild(2).
        GetChild(0).
        GetComponent<ShotShell>().DeleteShellOther(Id, OwnerId);

        //if (obj.gameObject.layer == 8)
        //{
        //    //����̒e���폜����
        //    obj.transform.
        //        GetChild(0).
        //        GetChild(0).
        //        GetChild(2).
        //        GetChild(0).
        //        GetComponent<ShotShell>().DeleteShellOther(Id, OwnerId);
        //}
    }

    public void SetPlayer(GameObject obj)
    {
        player = obj;
    }
}
