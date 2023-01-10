using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TurretItem : MonoBehaviour
{
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
        //アイテム表示
        gameObject.SetActive(true);
    }
}
