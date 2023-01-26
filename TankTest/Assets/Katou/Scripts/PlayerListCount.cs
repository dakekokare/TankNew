using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class PlayerListCount : MonoBehaviourPunCallbacks
{
    //カウントダウン
    [SerializeField]
    private GameObject countDown;
    //hp
    private GameObject hpObj;
    

    //待ち時間
    private int waitSecond=3;
    // Update is called once per frame
    void Update()
    {
        //プレイヤーが2人じゃなければ
        if (PhotonNetwork.PlayerList.Length != 1)
            return;
        //ゲーム開始前カウントダウン時処理
        InitializeGame();
    }

    private void InitializeGame()
    {
        //// カウントダウン生成する
        Instantiate(countDown);

        //プレイヤー探索
        Invoke("SearchHpEnemyTransform", waitSecond);

        //マスタークライアントならなら　アイテム生成
        if (PhotonNetwork.IsMasterClient)
            //アイテム生成
            Invoke("CreateItem", waitSecond);

        //アクティブ状態をオフにする
        this.gameObject.GetComponent<PlayerListCount>().enabled=false;
    }

    private void SearchHpEnemyTransform()
    {
        hpObj = GameObject.Find("HpEnemy(Clone)");
        //playerを見つける
        hpObj.GetComponent<HpEnemyTransform>().SearchPlayer();
    }
    private void CreateItem()
    {
        // scene object
        Scene scene = GameObject.Find("Scene").GetComponent<Scene>();
        //アイテム生成
        scene.GenerationItem();
    }

}
