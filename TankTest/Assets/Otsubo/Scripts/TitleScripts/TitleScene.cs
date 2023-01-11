using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class TitleScene : MonoBehaviourPunCallbacks
{
    //プレイヤースポーン座標   
    public GameObject[] array;

    private void Start()
    {
        // プレイヤー自身の名前を"Player"に設定する
        PhotonNetwork.NickName = "Player";
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
    }



    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster()
    {
        // "Room"という名前のルームに参加する（ルームが存在しなければ作成して参加する）
        if (PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default))
        {
            Debug.Log("生成");
            ////スポーン座標代入
            //var position = Vector3.zero;

        }
        else
        {
            Debug.Log("すでに生成されている");
        }
    }

    // ゲームサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnJoinedRoom()
    {

        //スポーン座標代入
        var position = Vector3.zero;

        int r =Random.Range(0,1);
        position=array[r].gameObject.transform.position;
        PhotonNetwork.Instantiate("TitleBoat", position, Quaternion.identity);

        position = Vector3.zero;
        GameObject obj = (GameObject)Resources.Load("CanvasObj");
        //生成する
        Instantiate(obj);
        //playerHp生成
        obj = (GameObject)Resources.Load("HpPlayer");
        //生成する
        Instantiate(obj);
        //EnemyHp生成
        obj = (GameObject)Resources.Load("HpEnemy");
        //生成する
        Instantiate(obj);

    }
}