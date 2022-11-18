using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class NextScene : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        //3秒後にメソッドを実行する
        Invoke("Next", 3);

    }

    // Update is called once per frame
    void Update()
    {
    }
    public void Next()
    {
        if(PhotonNetwork.IsMasterClient)
              PhotonNetwork.DestroyAll();
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Title");
    }
}
