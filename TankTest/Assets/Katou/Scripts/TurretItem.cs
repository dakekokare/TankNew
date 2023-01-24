using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TurretItem : MonoBehaviour
{

    void OnTriggerEnter(Collider t)
    {
        //プレイヤーと接触したら
        if (t.gameObject.layer == 8)
        {
            //誘導弾タレットアクティブ
            t.gameObject.transform.GetChild(0).GetChild(0).GetChild(3)
                .gameObject.SetActive(true);
            //デフォルトタレットfalse
            t.gameObject.transform.GetChild(0).GetChild(0).GetChild(2)
                .gameObject.SetActive(false);

            //アイテム非表示
            gameObject.SetActive(false);

            //数秒後にアイテム表示
            Invoke("ActiveTurretItem", 3);
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
                //移動
                this.gameObject.transform.position = vec;
            }

        }
        //アイテム表示
        gameObject.SetActive(true);
    }
}
