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
    private TankHealth th;

    [SerializeField]
    private GameObject barrierPrefab;

    //private int reward = 5; // 弾数をいくつ回復させるかは自由に決定

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // 「Boat」オブジェクトを探してデータを取得する
            boat = GameObject.Find("Boat(Clone)");

            // バリアのプレハブを実体化（インスタンス化）する。
            GameObject barrier = Instantiate(barrierPrefab, boat.transform.GetChild(1).position, Quaternion.identity);

            //  TankHealthスクリプトの中に記載されている「Barrierメソッド」を呼び出す。
            //th.Barrier();

            // アイテムを画面から削除する。
            Destroy(gameObject);

            // アイテムゲット音を出す。
            //AudioSource.PlayClipAtPoint(getSound, transform.position);

            // アイテムゲット時にエフェクトを発生させる。
            GameObject effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);

            // エフェクトを0.5秒後に消す。
            Destroy(effect, 0.5f);

            // バリアを10秒後に破壊する。
            Destroy(barrier, 10.0f);
        }
    }
}
