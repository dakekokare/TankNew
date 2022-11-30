using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HpEnemyTransform : MonoBehaviourPunCallbacks
{
    //他のプレイヤ―
    private GameObject otherPlayer;

    // Start is called before the first frame update
    void Start()
    {
        // ルーム内のネットワークオブジェクト
        foreach (var photonView in PhotonNetwork.PhotonViewCollection)
        {
            //boat かつ　自分じゃなかったら
            if (photonView.gameObject.name == "Boat(Clone)")
            {
                if (!photonView.IsMine)
                {
                    otherPlayer=PhotonView.Find(photonView.ViewID).gameObject;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (otherPlayer == null)
            return;
        this.transform.position = otherPlayer.transform.position;
        this.transform.rotation = otherPlayer.transform.rotation;

    }
}
