using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class Canvas : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        // 自身が生成したオブジェクトだけに移動処理を行う
        if (photonView.IsMine)
        {
            var position = new Vector3(0.0f, 0.1f, 0.0f);

            // キャンバス追加
            //PhotonNetwork.Instantiate("Canvas", position, Quaternion.identity);
        }
    }
}
