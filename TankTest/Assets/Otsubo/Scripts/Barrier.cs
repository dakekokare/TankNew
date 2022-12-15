using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Barrier : MonoBehaviourPunCallbacks
{
    // プレイヤー
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.GetChild(1).position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Shell")
        return;

        ////敵の弾に当たったら
        if (other.TryGetComponent<BulletNet>(out var shell))
        {
            if (shell.OwnerId != PhotonNetwork.LocalPlayer.ActorNumber)
            {
                // ぶつかってきた相手方（敵の砲弾）を破壊する。
                PhotonView.Destroy(other.gameObject);
            }
        }
    }

    public void GetPlayer(GameObject getPlayer)
    {
        player = getPlayer;
    }

    public void SearchPlayer()
    {
        // ルーム内のネットワークオブジェクト
        foreach (var photonView in PhotonNetwork.PhotonViewCollection)
        {
            //boat かつ　自分
            if (photonView.gameObject.name == "Boat(Clone)")
            {
                if (photonView.IsMine)
                {
                    Debug.Log("[" + GetInstanceID() + "]" + "Player Find");
                    player = PhotonView.Find(photonView.ViewID).gameObject;
                }
            }
        }
    }
}
