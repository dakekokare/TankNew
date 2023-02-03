using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class ShotShell : MonoBehaviourPunCallbacks
{
    public float shotSpeed;

    //// private�̏�Ԃł�Inspector�ォ��ݒ�ł���悤�ɂ���e�N�j�b�N�B
    //[SerializeField]
    //private GameObject shellPrefab;

    [SerializeField]
    private AudioClip shotSound;
    AudioSource audioSource;

    private float timeBetweenShot = 0.1f;
    private float timer;
    private int nextBulletId = 0;
    [SerializeField]
    private BulletNet bulletPre;

    private bool shotLock = true;
    //�F���ێ��@�I�u�W�F�N�g
    //private SaveColor sColor;
    private Vector3 pColor;
    private Vector3 eColor;

    void Start()
    {
        //Component���擾
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        //if (photonView.IsMine)
        //{

        //    Debug.Log("[p" + pColor + "]");
        //    Debug.Log("[e" + eColor + "]");
        //}

        // �^�C�}�[�̎��Ԃ𓮂���
        timer += Time.deltaTime;

        if (photonView.IsMine)
        {
            // ������Space�L�[���������Ȃ�΁i�����j
            // �uSpace�v�̕�����ύX���邱�Ƃő��̃L�[�ɂ��邱�Ƃ��ł���i�|�C���g�j
            if (Input.GetMouseButton(0) && timer > timeBetweenShot && shotLock == false)
            {
                //�^���b�g����\���Ȃ�ł��Ȃ�
                if (gameObject.transform.parent.gameObject == false)
                    return;
                
                // �^�C�}�[�̎��Ԃ��O�ɖ߂��B
                timer = 0.0f;


                // �e�𔭎˂��邽�тɒe��ID��1�����₵�Ă���
                photonView.RPC(nameof(FireBullet), RpcTarget.All, nextBulletId++,pColor,eColor);


                ////�}�X�^�[�N���C�A���g�Ȃ�
                //if(PhotonNetwork.IsMasterClient)
                //    // �e�𔭎˂��邽�тɒe��ID��1�����₵�Ă���
                //    photonView.RPC(nameof(FireBullet), RpcTarget.All, nextBulletId++,pColor,eColor);
                //else
                //    // �e�𔭎˂��邽�тɒe��ID��1�����₵�Ă���
                //    photonView.RPC(nameof(FireBullet), RpcTarget.All, nextBulletId++, eColor, pColor);
            }
        }
    }

    [PunRPC]
    private void FireBullet(int id,Vector3 p,Vector3 e)
    {

        // �C�e�̃v���n�u�����̉��i�C���X�^���X���j����B
        var shell = Instantiate(bulletPre);

        //id��e�ɓn��
        shell.Init(id, photonView.OwnerActorNr);

        // �C�e�ɕt���Ă���Rigidbody�R���|�[�l���g�ɃA�N�Z�X����B
        Rigidbody shellRb = shell.GetComponent<Rigidbody>();

        //���W�ړ�
        shellRb.transform.position = transform.position;

        // forward�i����Z���j�̕����ɗ͂�������B
        shellRb.AddForce(transform.forward * shotSpeed);

        // �C�e�̔��ˉ����o���B
        audioSource.PlayOneShot(shotSound);

        if (shell.OwnerId == PhotonNetwork.LocalPlayer.ActorNumber)
        {
                //�}�e���A���̕ύX
                ChangeMaterial(shell, p);

        }
        else
        {
            //�G�̒e��������tag��ς���
            shell.tag = "EnemyShell";

                //�}�e���A���̕ύX
                ChangeMaterial(shell, e);

        }
        shell.SetPlayer(gameObject.transform.
            parent.parent.parent.parent.parent.gameObject);
    }
    

    public void ShotUnlock()
    {
        shotLock = false;
    }
    public void ShotLock()
    {
        shotLock = true;
    }
    //�����shell�폜
    public void DeleteShellOther(int id,int ownerId)
    {
        Debug.Log("DeleteShellOther");
        //���v���C���[�Ƀ_���[�W����
        photonView.RPC(nameof(FindAndDeleteShell), RpcTarget.Others,id,ownerId);
    }
    [PunRPC]
    private void FindAndDeleteShell(int id,int ownerId)
    {
        Debug.Log("DeleteShell");
        //�e���폜����
        var bullets = FindObjectsOfType<BulletNet>();
        foreach (var bullet in bullets)
        {
            if (bullet.Equals(id, ownerId))
            {
                Destroy(bullet.gameObject);
                break;
            }
        }
    }

    private void SearchSaveColor()
    {
        //// ���[�����̃l�b�g���[�N�I�u�W�F�N�g
        //foreach (var photonView in PhotonNetwork.PhotonViewCollection)
        //{
        //    //Color �I�u�W�F�N�g
        //    if (photonView.gameObject.name == "Color(Clone)")
        //    {
        //        sColor = PhotonView.Find(photonView.ViewID).gameObject.GetComponent<SaveColor>();

        //    }
        //}
    }

    public void SetColor(Vector3 p,Vector3 e)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            //�F���擾
            pColor = p;
            eColor = e;
        }
        else
        {
            //�F���擾
            pColor = e;
            eColor = p;
        }
    }

    private void ChangeMaterial(BulletNet obj,Vector3 vec)
    {
        //�}�e���A���F�ς�
        obj.ChengeMaterial(vec);

        //�O�Ղ̐F��ς���
        TrailRenderer tr = obj.GetComponent<TrailRenderer>();
        Color color = new Color(vec.x, vec.y, vec.z);
        tr.startColor = color;
        tr.endColor = color;

    }

}