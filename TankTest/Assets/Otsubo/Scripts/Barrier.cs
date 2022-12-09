using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Barrier : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
}
