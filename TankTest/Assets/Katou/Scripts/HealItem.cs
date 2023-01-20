using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class HealItem : /*MonoBehaviour*/ MonoBehaviourPunCallbacks
{
    //プレイヤー
    private GameObject player;
    private float heal;
    // Start is called before the first frame update
    void Start()
    {
        //回復量
        heal = 20.0f;
        //プレイヤー探索
        SearchPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        float sin = Mathf.Sin(Time.time) + transform.position.y;
        this.transform.position = new Vector3(
            transform.position.x,
            sin * 0.5f,
            transform.position.z);
    }
    void OnTriggerEnter(Collider t)
    {
        //プレイヤーと接触したら
        if (t.gameObject.layer == 8)
        {
            //プレイヤー ismine
            if(t.gameObject.GetComponent<PhotonView>().IsMine)
              //接触したら回復
                HealPlayer();

            //アイテム非表示
            gameObject.SetActive(false);
            //3秒後にアイテムアクティブ
            Invoke("ActiveHealItem", 3);

            //削除
            //PhotonNetwork.Destroy(this.gameObject);
        }
    }

    // 回復する
    private void HealPlayer()
    {
        //プレイヤーに回復
        player.GetComponent<TankHealth>().HealHP(heal);
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

    private void ActiveHealItem()
    {
        // scene object
        Scene scene = GameObject.Find("Scene").GetComponent<Scene>();
        //スポーン用文字配列
        string[] s = scene.GetItemString();
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == "FirstAidKit")
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
