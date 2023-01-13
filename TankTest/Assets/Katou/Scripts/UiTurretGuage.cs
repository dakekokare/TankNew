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
    //射撃可能フラグ
    public bool TurretShotFlag = true;

    private GameObject player;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (TurretShotFlag == true)
            {
                //左クリックされていたら
                if (Input.GetMouseButton(0))
                {
                    //Debug.Log("[" + TurretBar.fillAmount + "]");

                    if (TurretBar.fillAmount < 1.0f)
                        // ゲージに反映する
                        TurretBar.fillAmount += 0.01f;
                    else
                        //射撃不可能
                        TurretShotFlag = false;
                }
                //左クリックが離されていたら
                else
                {
                    //Debug.Log("[" + TurretBar.fillAmount + "]");
                    //0より大きかったら
                    if (TurretBar.fillAmount > 0.0f)
                        // ゲージに反映する
                        TurretBar.fillAmount -= 0.01f;
                }
            }
            //射撃不可能な時
            if (TurretShotFlag == false)
            {

            }
        }
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
                    player = PhotonView.Find(photonView.ViewID).gameObject;
                }

            }
        }
    }

}
