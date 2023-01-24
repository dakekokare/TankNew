using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
////追加↓
//using UnityEditor;

////これでEnemyのpublicがインスペクタに表示される
//#if UNITY_EDITOR
//[CustomEditor(typeof(Item))]
//#endif

public class BarrierItem : /*Item*/MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject effectPrefab;
    //プレイヤー
    private GameObject player;
    //バリアオーブ
    [SerializeField]
    private GameObject barrierPrefab;
    // Start is called before the first frame update
    void Start()
    {
        //プレイヤー探索
        SearchPlayer();
    }

    void Update()
    {
        float sin = Mathf.Sin(Time.time) + transform.position.y;
        this.transform.position = new Vector3(
            transform.position.x,
            sin * 0.5f,
            transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        //プレイヤーと接触したら
        if (other.gameObject.layer == 8)
        {

            //バリアをアクティブにする
            photonView.RPC(nameof(ActiveBarrier), RpcTarget.All, other.GetComponent<PhotonView>().ViewID);

            //アイテム非表示
            gameObject.SetActive(false);
            //アイテム表示する
            Invoke("ActiveBarrierItem", 3);


            // アイテムゲット時にエフェクトを発生させる。
            GameObject effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);

            // エフェクトを0.5秒後に消す。
            Destroy(effect, 0.5f);
        }
    }

    public void SearchPlayer()
    {
        // ルーム内のネットワークオブジェクト
        foreach (var photonView in PhotonNetwork.PhotonViewCollection)
        {
            //boat かつ　自分
            if (photonView.gameObject.name == "Boat(Clone)")
            {
                if (photonView.IsMine)
                {
                    Debug.Log("[" + GetInstanceID() + "]" + "Player Find");
                    player = PhotonView.Find(photonView.ViewID).gameObject;
                }
            }
        }
    }
    [PunRPC]
    private void ActiveBarrier(int id)
    {
        GameObject obj = PhotonView.Find(id).gameObject;
        //バリアアクティブ
        obj.gameObject.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
    }

    [PunRPC]
    private void DestroyBarrier(int id)
    {
        GameObject obj = PhotonView.Find(id).gameObject;
        PhotonNetwork.Destroy(obj);
    }

    private void ActiveBarrierItem()
    {
        // scene object
        Scene scene = GameObject.Find("Scene").GetComponent<Scene>();
        //スポーン用文字配列
        string[] s=scene.GetItemString();
        for(int i=0;i<s.Length;i++)
        {
            if(s[i]=="BarrierItem")
            {
                ////アイテムがスポーンしていない座標の乱数に座標を貰う
                Vector3 vec = scene.MoveItem(i);
                //移動
                this.gameObject.transform.position = vec;
            }

        }
        //アイテム表示
        gameObject.SetActive(true);
    }

}
