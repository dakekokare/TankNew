using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TurretItem : MonoBehaviourPunCallbacks
{
    //音
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip sound;
    void Start()
    {
        //音
        audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        float sin = Mathf.Sin(Time.time);
        this.transform.position = new Vector3(
            transform.position.x,
            (sin * 0.03f) + transform.position.y,
            transform.position.z
            );
    }
    void OnTriggerEnter(Collider t)
    {
        if (photonView.IsMine)
        {
            //プレイヤーと接触したら
            if (t.gameObject.layer == 8)
            {
                //タレットをアクティブにする
                photonView.RPC(nameof(ActiveObj), RpcTarget.All, t.gameObject.GetComponent<PhotonView>().ViewID);
                //取得再生
                audioSource.PlayOneShot(sound);


                //数秒後にアイテム表示
                Invoke("ActiveTurretItem", 3);
            }
        }
    }

    private void ActiveTurretItem()
    {
        // scene object
        Scene scene = GameObject.Find("Scene").GetComponent<Scene>();
        //スポーン用文字配列
        string[] s = scene.GetItemString();
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == "MissileTurret")
            {
                ////アイテムがスポーンしていない座標の乱数に座標を貰う
                Vector3 vec = scene.MoveItem(i);
                //アイテム移動
               photonView.RPC(nameof(Move), RpcTarget.All, vec);
            }

        }
    }

    [PunRPC]

    private void Move(Vector3 vec)
    {
        //移動
        this.gameObject.transform.position = vec;
        //アイテム表示
        gameObject.SetActive(true);

    }

    [PunRPC]
    private void ActiveObj(int id)
    {
        GameObject t = PhotonView.Find(id).gameObject;


        //誘導弾タレットアクティブ
        t.gameObject.transform.GetChild(0).GetChild(0).GetChild(2)
            .gameObject.SetActive(true);
        //デフォルトタレットfalse
        t.gameObject.transform.GetChild(0).GetChild(0).GetChild(1)
            .gameObject.SetActive(false);

        //アイテム非表示
        gameObject.SetActive(false);

    }
}
