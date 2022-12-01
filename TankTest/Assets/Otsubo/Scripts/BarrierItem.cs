using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierItem : MonoBehaviour
{
    //[SerializeField]
    //private AudioClip getSound;
    [SerializeField]
    private GameObject effectPrefab;
    private GameObject boat;

    [SerializeField]
    private GameObject barrierPrefab;

    //private int reward = 5; // 弾数をいくつ回復させるかは自由に決定

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // 「Boat」オブジェクトを探してデータを取得する
            boat = GameObject.Find("Boat(Clone)");

            // 砲弾のプレハブを実体化（インスタンス化）する。
            GameObject barrier = Instantiate(barrierPrefab, boat.transform.GetChild(1).position, Quaternion.identity);

            //  ShotShellスクリプトの中に記載されている「AddShellメソッド」を呼び出す。
            // rewardで設定した数値分だけ弾数が回復する。
            //ss.AddShell(reward);

            // アイテムを画面から削除する。
            Destroy(gameObject);

            // アイテムゲット音を出す。
            //AudioSource.PlayClipAtPoint(getSound, transform.position);

            // アイテムゲット時にエフェクトを発生させる。
            GameObject effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);

            // エフェクトを0.5秒後に消す。
            Destroy(effect, 0.5f);
        }
    }
}
