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


    public void HitBullet()
    {
        //bullet �ƐڐG
        DamageBullet();
        //���s����
        VictoryJudgment();
    }
    private void DamageBullet()
    {
        Debug.Log("�_���[�W����");
        // HP������������B
        boatHP -= damage;
        //�_���[�W
        playerHpUi.Damage(damage);
        //���v���C���[�Ƀ_���[�W����
        photonView.RPC(nameof(DamageEnemyHpUi), RpcTarget.Others);
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
    private void WinActive()
    {
        Debug.Log("����");
        //win ���A�N�e�B�u����
        GameObject win = GameObject.Find("WINCanvas").gameObject.transform.GetChild(0).gameObject;
        win.SetActive(true);
        if (boatHP < 0)
            win.SetActive(false);
    }

}