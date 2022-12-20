using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BarrierItem : MonoBehaviour
{
    //[SerializeField]
    //private AudioClip getSound;
    [SerializeField]
    private GameObject effectPrefab;

    //private GameObject boat;

    //プレイヤー
    private GameObject player;

    [SerializeField]
    private GameObject barrierPrefab;

    //[SerializeField]
    //private Barrier barriercomponent;

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
        if (other.gameObject.layer == 8)
        {
            //バリアアクティブ
            other.gameObject.transform.GetChild(1).gameObject.SetActive(true);

            // アイテムを画面から削除する。
            PhotonNetwork.Destroy(gameObject);

            // アイテムゲット音を出す。
            //AudioSource.PlayClipAtPoint(getSound, transform.position);

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
}
