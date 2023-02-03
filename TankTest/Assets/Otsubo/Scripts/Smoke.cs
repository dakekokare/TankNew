using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Smoke : MonoBehaviourPunCallbacks
{
    // プレイヤー
    //private GameObject player;

    private float Smoketime = 5.0f;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // タイマーの時間を動かす
        timer += Time.deltaTime;

        // 5秒経ったら
        if (timer > Smoketime)
        {
            // タイマーの時間を０に戻す。
            timer = 0.0f;

            // バリアを消す。
            gameObject.SetActive(false);
            ////他プレイヤーの煙を非表示にする
            //this.gameObject.transform.parent.GetComponent<ShotShell>().DisActiveSmoke();
        }
    }
}