using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class TankHealth : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject effectPrefab1;
    [SerializeField]
    private GameObject effectPrefab2;
    public int tankHP;

    //[SerializeField]
    //private Text HPLabel;

    private GameObject canvas;

    private Text HPLabel;

    void Start()
    {
        canvas = GameObject.Find("CanvasObj(Clone)").transform.GetChild(0).GetChild(0).gameObject;
        //HPLabel = canvas.transform.GetChild(0).GetChild(0).gameObject;
        HPLabel = canvas.GetComponent<Text>();

        HPLabel.text = "HP：" + tankHP;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine)
        {
           if (other.TryGetComponent<BulletNet>(out var shell))
                {
                    if (shell.OwnerId != PhotonNetwork.LocalPlayer.ActorNumber)
                    {
                        photonView.RPC(nameof(HitBullet), RpcTarget.All, shell.Id, shell.OwnerId);
                    }
                }
        }
        else
        {
            // HPを１ずつ減少させる。

            tankHP -= 1;

            HPLabel.text = "HP：" + tankHP;
        }

        // ぶつかってきた相手方（敵の砲弾）を破壊する。
        PhotonView.Destroy(other.gameObject);

            if (tankHP > 0)
            {
                GameObject effect1 = Instantiate(effectPrefab1, transform.position, Quaternion.identity);
                Destroy(effect1, 1.0f);
            }
            else
            {
                GameObject effect2 = Instantiate(effectPrefab2, transform.position, Quaternion.identity);
                Destroy(effect2, 1.0f);

                //Lose UI 追加
                GameObject lose = GameObject.Find("LOSECanvas").gameObject.transform.GetChild(0).gameObject;
                lose.SetActive(true);

            //win Uiをアクティブする
            photonView.RPC(nameof(WinActive), RpcTarget.All);
            

            //// プレーヤーを破壊する。
            PhotonNetwork.Destroy(gameObject);
            //gameObject.SetActive(false);
        }
    }

    [PunRPC]
    private void HitBullet(int id, int ownerId)
    {
        //弾削除
        Debug.Log("弾削除");

        //弾を削除する
        var bullets = FindObjectsOfType<BulletNet>();
        foreach (var bullet in bullets)
        {
            if (bullet.Equals(id, ownerId))
            {
                Destroy(bullet.gameObject);
                break;
            }
        }
    }

    [PunRPC]
    private void WinActive()
    {
        Debug.Log("勝利");
        //win をアクティブする
        GameObject win = GameObject.Find("WINCanvas").gameObject.transform.GetChild(0).gameObject;
        win.SetActive(true);
        if (tankHP == 0)
            win.SetActive(false);
    }

}