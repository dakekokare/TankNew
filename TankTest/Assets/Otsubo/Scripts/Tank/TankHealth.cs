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

    }

    private void OnTriggerEnter(Collider other)
    {
        //敵の弾に当たったら
        if (!photonView.IsMine)
        {
           if (other.TryGetComponent<BulletNet>(out var shell))
           {
               if (shell.OwnerId != PhotonNetwork.LocalPlayer.ActorNumber)
               {
                    photonView.RPC(nameof(HitBullet), RpcTarget.All, shell.Id, shell.OwnerId);
               }
           }
            // HPを減少させる。
            boatHP -= damage;
            //ダメージ
            playerHpUi.Damage(damage);
            //他プレイヤーにダメージ処理
            photonView.RPC(nameof(DamageEnemyHpUi), RpcTarget.Others);
        }
        else
        {
        }

        // ぶつかってきた相手方（敵の砲弾）を破壊する。
        PhotonView.Destroy(other.gameObject);

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
    private void HitBullet(int id, int ownerId)
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
        //敵HpUIにダメージ処理
        enemyHpUi.Damage(damage);
    }

    public void SetEnemyHpUi()
    {
        //EnemyHpUi取得
        enemyHpUi = GameObject.Find("HpEnemy(Clone)").GetComponent<HPController>();
        //uiにＨＰをセット
        enemyHpUi.SetHp(boatHP);
    }
}