using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerListCount : MonoBehaviourPunCallbacks
{
    //カウントダウン
    [SerializeField]
    private GameObject countDown;

    private GameObject hpObj;

    // Start is called before the first frame update
    void Start()
    {
        //hpObj = (GameObject)Resources.Load("HpEnemy");
    }

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


                ////hp生成する
                //Instantiate(hpObj);

                // hpの情報をセットする
                // ルーム内のネットワークオブジェクト
                //foreach (var photonView in PhotonNetwork.PhotonViewCollection)
                //{
                //    //boat かつ　自分
                //    if (photonView.gameObject.name == "Boat(Clone)")
                //    {
                //        if (photonView.IsMine)
                //        {
                //            Debug.Log("CreateHP");
                //            hpObj = PhotonView.Find(photonView.ViewID).gameObject;
                //            hpObj.GetComponent<TankHealth>().SetEnemyHpUi();
                //            break;
                //        }
                //    }
                //}

                //hp Enemyにコンポーネントを追加
                SearchHpEnemyTransform();

            }
        }
    }

    private void SearchHpEnemyTransform()
    {
        hpObj = GameObject.Find("HpEnemy(Clone)");
        //playerを見つける
        hpObj.GetComponent<HpEnemyTransform>().SearchPlayer();
    }
}
