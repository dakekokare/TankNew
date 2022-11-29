using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObject : MonoBehaviour
{
    // エフェクトプレハブのデータを入れるための箱を作る。
    [SerializeField]
    private GameObject effectPrefab;

    // このメソッドはコライダー同士がぶつかった瞬間に呼び出される
    private void OnTriggerEnter(Collider other)
    {
        // もしもぶつかった相手のTagにShellという名前が書いてあったならば（条件）
        if (other.CompareTag("Shell"))
        {
            // ぶつかってきたオブジェクトを破壊する
            Destroy(other.gameObject);

            // エフェクトを実体化（インスタンス化）する。
            GameObject effect = Instantiate(effectPrefab, other.transform.position, Quaternion.identity);

            // エフェクトを２秒後に画面から消す
            Destroy(effect, 2.0f);
        }
    }
}
