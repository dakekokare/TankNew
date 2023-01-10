using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class TankHealth : MonoBehaviourPunCallbacks
{
    //爆発エフェクト
    [SerializeField]
    private GameObject effectPrefab1;
    [SerializeField]
    private GameObject effectPrefab2;
    
    //HP
    private float boatHP;
    //ダメージ
    private float damage;
    //PlayerHP UI
    private HPController playerHpUi;
    //EnemyHP UI
    private HPController enemyHpUi;

    void Start()
    {
        //ダメージ
        damage = 20.0f;
        //hp設定
        boatHP = 100.0f;
        //playerHpUi取得
        playerHpUi= GameObject.Find("HpPlayer(Clone)").GetComponent<HPController>();
        //uiにＨＰをセット
        playerHpUi.SetHp(boatHP);
        //enemyHpUi取得
        enemyHpUi = GameObject.Find("HpEnemy(Clone)").GetComponent<HPController>();
        //uiにＨＰをセット
        enemyHpUi.SetHp(boatHP);

    }
    //private void Update()
    //{
    //    //if (enemyHpUi == null)
    //    //    Debug.Log("[" + photonView.ViewID + "]" + "Nullです");
    //    //else
    //    //    Debug.Log("[" + photonView.ViewID + "]" + "入ってます");

    //}




    public void HealHP(float heal)
    {
        //回復
        Debug.Log("回復");
        boatHP += heal;
        //hp bar に　反映
        playerHpUi.HealHp(heal);
        //他プレイヤーのho barに回復処理
        photonView.RPC(nameof(HealEnemyHpUi), RpcTarget.Others,heal);
    }    
    [PunRPC]
    private void HealEnemyHpUi(float heal)
    {
        //敵プレイヤーの回復ui処理
        Debug.Log("Heal Enemy Hp Ui");
        //nullならリターン
        if (enemyHpUi == null)
        {
            Debug.Log("Return");
            return;
        }
        Debug.Log("Enemy回復処理");
        //敵HpUIにダメージ処理
        enemyHpUi.HealHp(heal);
    }


    public void HitBullet()
    {
        //bullet と接触
        DamageBullet();
        //勝敗判定
        VictoryJudgment();
    }
    private void DamageBullet()
    {
        Debug.Log("ダメージ処理");
        // HPを減少させる。
        boatHP -= damage;
        //ダメージ
        playerHpUi.Damage(damage);
        //他プレイヤーにダメージ処理
        photonView.RPC(nameof(DamageEnemyHpUi), RpcTarget.Others);
    }
    [PunRPC]
    private void DamageEnemyHpUi()
    {
        //nullならリターン
        if (enemyHpUi == null)
        {
            Debug.Log("Return");
            return;
        }
        Debug.Log("Enemyダメージ処理");
        //敵HpUIにダメージ処理
        enemyHpUi.Damage(damage);
    }
    private void VictoryJudgment()
    {
        //勝敗判定
        if (boatHP > 0)
        {
            GameObject effect1 = Instantiate(effectPrefab1, transform.position, Quaternion.identity);
            Destroy(effect1, 1.0f);
        }
        else
        {
            GameObject effect2 = Instantiate(effectPrefab2, transform.position, Quaternion.identity);
            Destroy(effect2, 1.0f);

            //Lose UI 追加
            GameObject lose = GameObject.Find("LOSECanvas").gameObject.transform.GetChild(0).gameObject;
            lose.SetActive(true);

            //win Uiをアクティブする
            photonView.RPC(nameof(WinActive), RpcTarget.All);


            //// プレーヤーを破壊する。
            PhotonNetwork.Destroy(gameObject);
        }
    }


    [PunRPC]
    private void WinActive()
    {
        Debug.Log("勝利");
        //win をアクティブする
        GameObject win = GameObject.Find("WINCanvas").gameObject.transform.GetChild(0).gameObject;
        win.SetActive(true);
        if (boatHP < 0)
            win.SetActive(false);
    }

}