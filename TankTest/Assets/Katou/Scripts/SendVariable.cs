using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SendVariable : MonoBehaviourPunCallbacks, IPunObservable
{
    public int playerNum = 0;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // �I�[�i�[�̏ꍇ
        if (stream.IsWriting)
        {
            stream.SendNext(this.playerNum);
        }
        // �I�[�i�[�ȊO�̏ꍇ
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
