using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class HealItem : MonoBehaviour
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
            //接触したら回復
            HealPlayer();
            //削除
            PhotonNetwork.Destroy(this.gameObject);
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

}
