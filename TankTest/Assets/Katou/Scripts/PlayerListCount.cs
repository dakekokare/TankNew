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
    //体力回復
    [SerializeField]
    private GameObject firstAidKitPosition;

    //Item Spawn 場所
    //誘導弾
    [SerializeField]
    private GameObject missileTurretPosition;
    //barrier
    [SerializeField]
    private GameObject barrierPosition;

    // Update is called once per frame
    void Update()
    {
        int count = 0;
        foreach (var p in PhotonNetwork.PlayerList)
        {
            count++;
            //2人いたら
            if (count==2)
            {
                //// カウントダウン生成する
                Instantiate(countDown);
                //アクティブ状態をオフにする
                this.gameObject.SetActive(false);

                //プレイヤー探索
                Invoke("SearchHpEnemyTransform", 3);

                //マスタークライアントならなら　アイテム生成
                if(PhotonNetwork.IsMasterClient)
                    //アイテム生成
                    Invoke("GenerationItem",3);

             
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

        Debug.Log("MissileTurret 生成");
        //アイテム生成
        PhotonNetwork.Instantiate("MissileTurret", missileTurretPosition.transform.position, Quaternion.identity);
        
        Debug.Log("Barrier 生成");
        //アイテム生成
        PhotonNetwork.Instantiate("BarrierItem", barrierPosition.transform.position, Quaternion.identity);

    }
    
}
