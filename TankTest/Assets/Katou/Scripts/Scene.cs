using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class Scene : MonoBehaviourPunCallbacks, IPunObservable
{
    //プレイヤースポーン座標   
    public GameObject[] array;

    //スポーン座標番号
    private int spawnNum=100;
    //他プレイヤースポーン座標番号
    private int otherSpawnNum=100;

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
        }
        else
        {
            Debug.Log("すでに生成されている");
        }
    }

    // ゲームサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnJoinedRoom()
    {
        //他プレイヤーのスポーン座標確認
        CheckOtherPlayerSpawnNum();
    }
    private void CheckOtherPlayerSpawnNum()
    {
        //マスタークライアントではないとき
        if (!PhotonNetwork.IsMasterClient)
        {
            //他プレイヤーのスポーン座標確認
            if (otherSpawnNum == 100)
            {
                //3秒後に再起処理
                Invoke("CheckOtherPlayerSpawnNum", 3);
                return;
            }
        }
        //オブジェクト生成
        CreateObject();
    }

    private void CreateObject()
    {
        //オブジェクト生成
        //スポーン座標代入
        var position = Vector3.zero;
        //スポーン座標取得
        spawnNum = GetRandomNum();
        position = array[spawnNum].gameObject.transform.position;
        PhotonNetwork.Instantiate("Boat", position, Quaternion.identity);

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

    private int GetRandomNum()
    {
        //乱数取得
        int r = Random.Range(0, 5);
        //他プレイヤーのスポーン座標だったら
        if (r == otherSpawnNum)
            //再起
            GetRandomNum();
        return r;
    }


    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // 送信する
            stream.SendNext(spawnNum);
        }
        else
        {
            // 受信する
            otherSpawnNum = (int)stream.ReceiveNext();
        }
    }
}