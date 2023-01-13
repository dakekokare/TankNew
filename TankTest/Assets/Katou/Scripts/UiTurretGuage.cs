using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class UiTurretGuage : MonoBehaviourPunCallbacks
{
    //タレットゲージ
    [SerializeField]
    private Image TurretBar = default;
    //プレイヤー
    private ShotShell player;

    //ゲージ増加スピード
    private float GuageIncSpeed = 0.01f;
    //ゲージ減少スピード
    private float GuageDecSpeed = 0.01f;

    //射撃停止時間
    private int GuageStop = 5;
    //状態
    enum ShotState
    {
        //射撃状態
        Shot,
        //射撃停止状態
        Stop,
        //ゲージ回復状態
        Recovary
    }

    ShotState state; 
    void Start()
    {
        //初期化
        Invoke("Initialize", 1);

        //射撃状態
        state = ShotState.Shot;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case ShotState.Shot:
                //左クリックされていたら
                if (Input.GetMouseButton(0))
                {
                    //Debug.Log("[" + TurretBar.fillAmount + "]");

                    if (TurretBar.fillAmount < 1.0f)
                        // ゲージに反映する
                        TurretBar.fillAmount += GuageIncSpeed;
                    else
                        //射撃停止状態
                        state = ShotState.Stop;
                }
                //左クリックが離されていたら
                else
                {
                    //Debug.Log("[" + TurretBar.fillAmount + "]");
                    //0より大きかったら
                    if (TurretBar.fillAmount > 0.0f)
                        // ゲージに反映する
                        TurretBar.fillAmount -= GuageDecSpeed;
                }
                break;
            case ShotState.Stop:
                //射撃不可能にする
                player.ShotLock();
                //3秒停止 射撃ゲージ回復状態にする
                Invoke("RecovaryShotGuage", GuageStop);
                break;
            case ShotState.Recovary:
                //0より大きかったら
                if (TurretBar.fillAmount > 0.0f)
                    // ゲージに反映する
                    TurretBar.fillAmount -= GuageDecSpeed;
                else
                {
                    //射撃状態へ
                    state = ShotState.Shot;
                    //射撃可能にする
                    player.ShotUnlock();
                }

                break;
        }
    }

    private void Initialize()
    {
        //プレイヤー探索
        SearchPlayer();
    }

    private void SearchPlayer()
    {
        // ルーム内のネットワークオブジェクト
        foreach (var photonView in PhotonNetwork.PhotonViewCollection)
        {
            //boat かつ　自分じゃなかったら
            if (photonView.gameObject.name == "Boat(Clone)")
            {
                if (photonView.IsMine)
                {

                    Debug.Log("[" + GetInstanceID() + "]" + "Player Find");
                    GameObject obj = PhotonView.Find(photonView.ViewID).gameObject;
                    player= obj.transform.
                        GetChild(0).
                        GetChild(0).
                        GetChild(2).
                        GetChild(0).GetComponent<ShotShell>();
                }

            }
        }
    }

    private void RecovaryShotGuage()
    {
        //射撃ゲージ回復状態へ
        state = ShotState.Recovary;
    }
}
