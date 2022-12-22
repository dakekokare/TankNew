using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class TankHealth : MonoBehaviourPunCallbacks
{
    //�����G�t�F�N�g
    [SerializeField]
    private GameObject effectPrefab1;
    [SerializeField]
    private GameObject effectPrefab2;
    
    //HP
    private float boatHP;
    //�_���[�W
    private float damage;
    //PlayerHP UI
    private HPController playerHpUi;
    //EnemyHP UI
    private HPController enemyHpUi;

    void Start()
    {
        //�_���[�W
        damage = 20.0f;
        //hp�ݒ�
        boatHP = 100.0f;
        //playerHpUi�擾
        playerHpUi= GameObject.Find("HpPlayer(Clone)").GetComponent<HPController>();
        //ui�ɂg�o���Z�b�g
        playerHpUi.SetHp(boatHP);
        //enemyHpUi�擾
        enemyHpUi = GameObject.Find("HpEnemy(Clone)").GetComponent<HPController>();
        //ui�ɂg�o���Z�b�g
        enemyHpUi.SetHp(boatHP);

    }
    //private void Update()
    //{
    //    //if (enemyHpUi == null)
    //    //    Debug.Log("[" + photonView.ViewID + "]" + "Null�ł�");
    //    //else
    //    //    Debug.Log("[" + photonView.ViewID + "]" + "�����Ă܂�");

    //}
    private void OnTriggerEnter(Collider other)
    {
        if (photonView.IsMine)
        {
            Debug.Log("[ Hit " + other.gameObject.layer + "&" + other.gameObject.tag + "]");
            //shell �ɐڐG�����ꍇ
            if (other.gameObject.tag == "Shell")
            {
                //shell �ƐڐG
                ContactShell(other);
                // �Ԃ����Ă���������i�G�̖C�e�j��j�󂷂�B
                Destroy(other.gameObject);
                //���s����
                VictoryJudgment();
            }

            ////�A�C�e���ƐڐG������
            if (other.gameObject.tag == "Missile")
            {
                //missile �ƐڐG
                ContactMissile(other);
                // �Ԃ����Ă���������i�G�̖C�e�j��j�󂷂�B
                photonView.RPC(nameof(DeleteMissile), RpcTarget.Others, other.GetComponent<PhotonView>().ViewID);
                //���s����
                VictoryJudgment();
            }
        }
    }

    private void ContactShell(Collider other)
    {
        //shell�ƐڐG�����ꍇ
        ////�G�̒e�ɓ���������
        if (other.TryGetComponent<BulletNet>(out var shell))
        {
            if (shell.OwnerId != PhotonNetwork.LocalPlayer.ActorNumber)
            {
                Debug.Log("[" + photonView.ViewID + "]" + "�_���[�W����");

                photonView.RPC(nameof(HitShell), RpcTarget.All, shell.Id, shell.OwnerId);
                // HP������������B
                boatHP -= damage;
                //�_���[�W
                playerHpUi.Damage(damage);
                //���v���C���[�Ƀ_���[�W����
                photonView.RPC(nameof(DamageEnemyHpUi), RpcTarget.Others);

            }
        }
    }
    private void ContactMissile(Collider other)
    {
        if (other.gameObject.TryGetComponent<PhotonView>(out var missile))
        {
            //�����̔��˂����~�T�C����������
            if (missile.IsMine)
            {
                Debug.Log("[" + missile.ViewID + "]" + "Return Missile �_���[�W����");
                return;
            }
            Debug.Log("[" + missile.ViewID + "]" + "Missile �_���[�W����");
            // HP������������B
            boatHP -= damage;
            //�_���[�W
            playerHpUi.Damage(damage);
            //���v���C���[�Ƀ_���[�W����
            photonView.RPC(nameof(DamageEnemyHpUi), RpcTarget.Others);
        }
    }
    private void VictoryJudgment()
    {
        //���s����
        if (boatHP > 0)
        {
            GameObject effect1 = Instantiate(effectPrefab1, transform.position, Quaternion.identity);
            Destroy(effect1, 1.0f);
        }
        else
        {
            GameObject effect2 = Instantiate(effectPrefab2, transform.position, Quaternion.identity);
            Destroy(effect2, 1.0f);

            //Lose UI �ǉ�
            GameObject lose = GameObject.Find("LOSECanvas").gameObject.transform.GetChild(0).gameObject;
            lose.SetActive(true);

            //win Ui���A�N�e�B�u����
            photonView.RPC(nameof(WinActive), RpcTarget.All);


            //// �v���[���[��j�󂷂�B
            PhotonNetwork.Destroy(gameObject);
        }
    }

    [PunRPC]
    private void HitShell(int id, int ownerId)
    {
        //�e�폜
        Debug.Log("�e�폜");

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

    [PunRPC]
    private void WinActive()
    {
        Debug.Log("����");
        //win ���A�N�e�B�u����
        GameObject win = GameObject.Find("WINCanvas").gameObject.transform.GetChild(0).gameObject;
        win.SetActive(true);
        if (boatHP < 0)
            win.SetActive(false);
    }

    [PunRPC]
    private void DamageEnemyHpUi()
    {
        //null�Ȃ烊�^�[��
        if (enemyHpUi == null)
        {
            Debug.Log("Return");
            return;
        }
        Debug.Log("Enemy�_���[�W����");
        //�GHpUI�Ƀ_���[�W����
        enemyHpUi.Damage(damage);
    }

    public void HealHP(float heal)
    {
        //��
        Debug.Log("��");
        boatHP += heal;
        //hp bar �Ɂ@���f
        playerHpUi.HealHp(heal);
        //���v���C���[��ho bar�ɉ񕜏���
        photonView.RPC(nameof(HealEnemyHpUi), RpcTarget.Others,heal);
    }    
    [PunRPC]
    private void HealEnemyHpUi(float heal)
    {
        //�G�v���C���[�̉�ui����
        Debug.Log("Heal Enemy Hp Ui");
        //null�Ȃ烊�^�[��
        if (enemyHpUi == null)
        {
            Debug.Log("Return");
            return;
        }
        Debug.Log("Enemy�񕜏���");
        //�GHpUI�Ƀ_���[�W����
        enemyHpUi.HealHp(heal);
    }
    [PunRPC]
    private void DeleteMissile(int obj)
    {
        GameObject missile= PhotonView.Find(obj).gameObject;
        //missile ���폜
        PhotonNetwork.Destroy(missile);
    }
}