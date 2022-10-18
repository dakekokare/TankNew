using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TankCamera : MonoBehaviourPunCallbacks
{
    private Camera camera;

    private void Start()
    {
        // 自身が生成したオブジェクトだけに移動処理を行う
        if (photonView.IsMine)
        {

            // カメラ追加
            gameObject.AddComponent<Camera>();
            camera = GetComponent<Camera>();
            camera.depth = 1;
        }
    }
}
