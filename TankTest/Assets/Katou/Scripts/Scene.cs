using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class Scene : MonoBehaviourPunCallbacks, IPunObservable
{
    //プレイヤースポーン座標   
    [SerializeField] private GameObject spawnA;
    [SerializeField] private GameObject spawnB;
    //プレイヤー数
    private int playerNum = 0;
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
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
    }

    // ゲームサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnJoinedRoom()
    {
        //スポーン座標代入
        var position=Vector3.zero;
        if (playerNum == 0)
        {
            position = spawnA.transform.position;
            playerNum++;
        }
        else
            position = spawnB.transform.position;

        // （ネットワークオブジェクト）を生成する
        PhotonNetwork.Instantiate("CanvasObj", position, Quaternion.identity);
        PhotonNetwork.Instantiate("Tank", position, Quaternion.identity);


    }

    //変数同期
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //データの送信
            stream.SendNext(playerNum);
        }
        else
        {
            //データの受信
            this.playerNum = (int)stream.ReceiveNext();
        }
    }

}