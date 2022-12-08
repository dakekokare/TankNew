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
    //Item Spawn 場所
    [SerializeField]
    private GameObject firstAidKitPosition; 
    // Update is called once per frame
    void Update()
    {
        int count = 0;
        foreach (var p in PhotonNetwork.PlayerList)
        {
            count++;
            //2人いたら
            if (count==1)
            {
                //// カウントダウン生成する
                Instantiate(countDown);
                //アクティブ状態をオフにする
                this.gameObject.SetActive(false);

                //プレイヤー探索
                Invoke("SearchHpEnemyTransform", 2);

                //マスタークライアントならなら　アイテム生成
                if(PhotonNetwork.IsMasterClient)
                    //アイテム生成
                    Invoke("GenerationItem", 2);

             
            }
        }
    }

    private void SearchHpEnemyTransform()
    {
        hpObj = GameObject.Find("HpEnemy(Clone)");
        //playerを見つける
        hpObj.GetComponent<HpEnemyTransform>().SearchPlayer();
    }
    private void GenerationItem()
    {
        Debug.Log("firstaidkit 生成");
        //アイテム生成
        PhotonNetwork.Instantiate("FirstAidKit", firstAidKitPosition.transform.position, Quaternion.identity);
    }
}
