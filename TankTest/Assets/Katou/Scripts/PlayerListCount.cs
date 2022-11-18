using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerListCount : MonoBehaviourPunCallbacks
{
    public GameObject countDown;

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
                //// （ネットワークオブジェクト）を生成する
                //Instantiate("CountDownCanvas", position, Quaternion.identity);
                Instantiate(countDown);
                //アクティブ状態をオフにする
                this.gameObject.SetActive(false);
            }
        }
    }
}
