using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SendVariable : MonoBehaviourPunCallbacks, IPunObservable
{
    public int playerNum = 0;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // オーナーの場合
        if (stream.IsWriting)
        {
            stream.SendNext(this.playerNum);
        }
        // オーナー以外の場合
        else
        {
            this.playerNum = (int)stream.ReceiveNext();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
