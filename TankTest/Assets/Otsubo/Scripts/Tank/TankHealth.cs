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
    private void OnTriggerEnter(Collider other)
    {
        if (photonView.IsMine)
        {
            Debug.Log("[ Hit " + other.gameObject.layer + "&" + other.gameObject.tag + "]");
            //shell に接触した場合
            if (other.gameObject.tag == "Shell")
            {
                //shell と接触
                ContactShell(other);
                // ぶつかってきた相手方（敵の砲弾）を破壊する。
                Destroy(other.gameObject);
                //勝敗判定
                VictoryJudgment();
            }

            ////アイテムと接触したら
            if (other.gameObject.tag == "Missile")
            {
                //missile と接触
                ContactMissile(other);
                // ぶつかってきた相手方（敵の砲弾）を破壊する。
                photonView.RPC(nameof(DeleteMissile), RpcTarget.Others, other.GetComponent<PhotonView>().ViewID);
                //勝敗判定
                VictoryJudgment();
            }
        }
    }

    private void ContactShell(Collider other)
    {
        //shellと接触した場合
        ////敵の弾に当たったら
        if (other.TryGetComponent<BulletNet>(out var shell))
        {
            if (shell.OwnerId != PhotonNetwork.LocalPlayer.ActorNumber)
            {
                Debug.Log("[" + photonView.ViewID + "]" + "ダメージ処理");

                photonView.RPC(nameof(HitShell), RpcTarget.All, shell.Id, shell.OwnerId);
                // HPを減少させる。
                boatHP -= damage;
                //ダメージ
                playerHpUi.Damage(damage);
                //他プレイヤーにダメージ処理
                photonView.RPC(nameof(DamageEnemyHpUi), RpcTarget.Others);

            }
        }
    }
    private void ContactMissile(Collider other)
    {
        if (other.gameObject.TryGetComponent<PhotonView>(out var missile))
        {
            //自分の発射したミサイルだったら
            if (missile.IsMine)
            {
                Debug.Log("[" + missile.ViewID + "]" + "Return Missile ダメージ処理");
                return;
            }
            Debug.Log("[" + missile.ViewID + "]" + "Missile ダメージ処理");
            // HPを減少させる。
            boatHP -= damage;
            //ダメージ
            playerHpUi.Damage(damage);
            //他プレイヤーにダメージ処理
            photonView.RPC(nameof(DamageEnemyHpUi), RpcTarget.Others);
        }
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
    private void HitShell(int id, int ownerId)
    {
        //弾削除
        Debug.Log("弾削除");

        //弾を削除する
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
        Debug.Log("勝利");
        //win をアクティブする
        GameObject win = GameObject.Find("WINCanvas").gameObject.transform.GetChild(0).gameObject;
        win.SetActive(true);
        if (boatHP < 0)
            win.SetActive(false);
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
    [PunRPC]
    private void DeleteMissile(int obj)
    {
        GameObject missile= PhotonView.Find(obj).gameObject;
        //missile を削除
        PhotonNetwork.Destroy(missile);
    }
}