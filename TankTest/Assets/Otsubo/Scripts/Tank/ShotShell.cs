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

    private float timeBetweenShot = 0.1f;
    private float timer;
    private int nextBulletId = 0;
    [SerializeField]
    private BulletNet bulletPre;

    private bool shotLock = false;

    void Update()
    {

        // �^�C�}�[�̎��Ԃ𓮂���
        timer += Time.deltaTime;

        if (photonView.IsMine)
        {
            // ������Space�L�[���������Ȃ�΁i�����j
            // �uSpace�v�̕�����ύX���邱�Ƃő��̃L�[�ɂ��邱�Ƃ��ł���i�|�C���g�j
            if (Input.GetMouseButton(0) && timer > timeBetweenShot && shotLock == false)
            {
                // �^�C�}�[�̎��Ԃ��O�ɖ߂��B
                timer = 0.0f;

                // �e�𔭎˂��邽�тɒe��ID��1�����₵�Ă���
                photonView.RPC(nameof(FireBullet), RpcTarget.All, nextBulletId++);

            }
        }
    }

     [PunRPC]
    private void FireBullet(int id)
    {
        //var shell = PhotonNetwork.Instantiate(
        //    "Shell", transform.position, Quaternion.identity
        //    ).GetComponent<BulletNet>();
        var shell = Instantiate(bulletPre);

        shell.Init(id, photonView.OwnerActorNr);
        // �C�e�̃v���n�u�����̉��i�C���X�^���X���j����B
        //GameObject shell = PhotonNetwork.Instantiate("Shell", transform.position, Quaternion.identity);

        // �C�e�ɕt���Ă���Rigidbody�R���|�[�l���g�ɃA�N�Z�X����B
        Rigidbody shellRb = shell.GetComponent<Rigidbody>();

        //���W�ړ�
        shellRb.transform.position = transform.position;

        // forward�i����Z���j�̕����ɗ͂�������B
        shellRb.AddForce(transform.forward * shotSpeed);
        //shellRb.AddForce(-transform.up * (shotSpeed * 0.5f));

        // ���˂����C�e���R�b��ɔj�󂷂�B
        // �i�d�v�ȍl�����j�s�v�ɂȂ����C�e�̓������[�ォ��폜���邱�ƁB
        //Destroy(shell, 3.0f);
        PhotonView.Destroy(shell, 3.0f);
        // �C�e�̔��ˉ����o���B
        AudioSource.PlayClipAtPoint(shotSound, transform.position);

    }

    public void ShotUnlock()
    {
        shotLock = false;
    }
}