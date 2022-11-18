using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerListCount : MonoBehaviourPunCallbacks
{
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
            if(count==2)
            {
                Vector3 position = Vector3.zero;

                //// （ネットワークオブジェクト）を生成する
                //PhotonNetwork.Instantiate("PlayerCount", position, Quaternion.identity);

            }
        }
    }
}
