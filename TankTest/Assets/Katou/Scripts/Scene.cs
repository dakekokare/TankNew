using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
//using ExitGames.Client.Photon;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class Scene : MonoBehaviourPunCallbacks
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
            int num = PhotonNetwork.CurrentRoom.GetSpawn();
            //他プレイヤーのスポーン座標確認
            // num ==0 -> 初期値、マスタークライアントがスポーンしていない
            if (num == 0)
            {
                //3秒後に再起処理
                Invoke("CheckOtherPlayerSpawnNum", 1);
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
        int r;
        //マスタークライアント
        if (PhotonNetwork.IsMasterClient)
        {
            //スポーン座標取得
            r = Random.Range(1, array.Length);
            PhotonNetwork.CurrentRoom.SetSpawn(r);
        }
        else
        {
            //他プレイヤーのスポーン座標
            int num = PhotonNetwork.CurrentRoom.GetSpawn();
            r = GetRandomNum(num);
        }
        position = array[r].gameObject.transform.position;
        PhotonNetwork.Instantiate("Boat", position, Quaternion.identity);



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

    private int GetRandomNum(int num)
    {
        
        //乱数取得
        int r = Random.Range(1, array.Length);
        //他プレイヤーのスポーン座標だったら
        if (r == num)
            //再起
            GetRandomNum(num);
        return r;
    }

}