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
            //2�l������
            if(count==2)
            {
                Vector3 position = Vector3.zero;

                //// �i�l�b�g���[�N�I�u�W�F�N�g�j�𐶐�����
                //PhotonNetwork.Instantiate("PlayerCount", position, Quaternion.identity);

            }
        }
    }
}
