using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerListCount : MonoBehaviourPunCallbacks
{
    //カウントダウン
    [SerializeField]
    private GameObject countDown;


    // Start is called before the first frame update
    void Start()
    {
        
    }

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

                // hpを生成
                photonView.RPC(nameof(CreateHp), RpcTarget.All);
            }
        }
    }

    [PunRPC]
    private void CreateHp()
    {
        GameObject obj = (GameObject)Resources.Load("HpEnemy");
        //生成する
        Instantiate(obj);

        // ルーム内のネットワークオブジェクト
        foreach (var photonView in PhotonNetwork.PhotonViewCollection)
        {
            //boat かつ　自分
            if (photonView.gameObject.name == "Boat(Clone)")
            {
                if (photonView.IsMine)
                {
                    Debug.Log("CreateHP");
                    obj = PhotonView.Find(photonView.ViewID).gameObject;
                    obj.GetComponent<TankHealth>().SetEnemyHpUi();

                }
            }
        }
    }
}
