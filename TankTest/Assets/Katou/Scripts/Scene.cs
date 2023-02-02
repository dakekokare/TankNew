using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
//using ExitGames.Client.Photon;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class Scene : MonoBehaviourPunCallbacks
{
    //プレイヤースポーン座標
    [SerializeField]
    private GameObject[] array;

    //アイテム座標
    [SerializeField]
    private GameObject itemPos;

    //アイテム生成用文字配列
    [SerializeField]
    private string[] itemString;

    ////色情報
    //private Vector3 pColor;
    //private Vector3 eColor;

    private GameObject colorObj;
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
        //自身の色情報をセットする
        Color col = SceneShare.GetColor();
        Vector3 cVec = new Vector3(col.r, col.g, col.b);
        //マスタークライアント
        if (PhotonNetwork.IsMasterClient)
        {
            Vector3 position = Vector3.zero;
            colorObj=PhotonNetwork.Instantiate("Color", position, Quaternion.identity);

            //色情報を追加
            colorObj.GetComponent<SaveColor>().AddPlayerColor(cVec);
        }
        else
        {
            //カラーオブジェクト取得
            SearchSaveColor();
            //色情報を追加
            colorObj.GetComponent<SaveColor>().AddEnemyColor(cVec);

            int num = PhotonNetwork.CurrentRoom.GetSpawn();
            //他プレイヤーのスポーン座標確認
            // num ==0 -> 初期値、マスタークライアントがスポーンしていない
            if (num == 0)
            {
                //秒後に再起処理
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
            r = GetRandomNum(num, array);
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

    private int GetRandomNum(int num, GameObject[] obj)
    {

        //乱数取得
        int r = Random.Range(1, obj.Length);
        //他プレイヤーのスポーン座標だったら
        if (r == num)
            //再起
            r = GetRandomNum(num, obj);
        return r;
    }
    private int GetItemRandomNum(int num, int[] itemNumber)
    {
        //生成場所が重ならない用に乱数取得
        int r = Random.Range(1, num);

        //乱数の座標に他アイテムがスポーンしていないかチェックする
        for (int i = 0; i < itemNumber.Length; i++)
        {
            //他アイテムのスポーン座標だったら
            if (r == itemNumber[i])
            {
                //再起
                r = GetItemRandomNum(num, itemNumber);
            }
        }
        return r;
    }

    public void GenerationItem()
    {
        ////アイテム座標番号配列
        int[] itemSpawnNumber = new int[itemString.Length];

        //アイテムの数　
        for (int i = 0; i < itemString.Length; i++)
        {
            //乱数でアイテムスポーン座標を決める
            itemSpawnNumber[i] = GetItemRandomNum(itemPos.transform.childCount, itemSpawnNumber);
        }
        //アイテム生成
        ItemCreate(itemSpawnNumber, itemPos);

        //カスタムプロパティに使用中の座標番号をセットする
        PhotonNetwork.CurrentRoom.SetItemSpawn(itemSpawnNumber);



    }
    public Vector3 MoveItem(int index)
    {
        ////使用されているアイテム座標を貰う
        int[] item = PhotonNetwork.CurrentRoom.GetItemSpawn();
        //使用されていない座標番号　
        int r = GetItemRandomNum(itemPos.transform.childCount, item);
        //要素番号の中身を書き換える
        item[index] = r;
        //カスタムプロパティに使用中の座標番号をセットする
        PhotonNetwork.CurrentRoom.SetItemSpawn(item);

        Debug.Log("[" + itemString[index] + r + "]生成");
        return itemPos.transform.GetChild(r).transform.position;
    }

    private void ItemCreate(int[] itemPosNum, GameObject spawnPos)
    {
        for (int i = 0; i < itemString.Length; i++)
        {
            Debug.Log("[" + itemString[i] + "]生成");
            //item生成
            PhotonNetwork.Instantiate(
                itemString[i],
                spawnPos.transform.GetChild(itemPosNum[i]).transform.position,
                Quaternion.identity
                );
        }
    }

    public string[] GetItemString()
    {
        return itemString;
    }



    //void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        Debug.Log("送信");
    //        Debug.Log("[p" + pColor + "]");


    //        //データの送信
    //        stream.SendNext(pColor);
    //    }
    //    else
    //    {
    //        Debug.Log("受信");
    //        Debug.Log("[e" + eColor + "]");
    //        //データの受信
    //        eColor = (Vector3)stream.ReceiveNext();
    //    }
    //}

    //private void Update()
    //{
    //}


    private void SearchSaveColor()
    {
        // ルーム内のネットワークオブジェクト
        foreach (var photonView in PhotonNetwork.PhotonViewCollection)
        {
            //Color オブジェクト
            if (photonView.gameObject.name == "Color")
            {
                colorObj = PhotonView.Find(photonView.ViewID).gameObject;

            }
        }
    }
}