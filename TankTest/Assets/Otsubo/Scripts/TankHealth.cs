using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
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

        HPLabel.text = "HPÅF" + tankHP;
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
            // HPÇÇPÇ∏Ç¬å∏è≠Ç≥ÇπÇÈÅB

            tankHP -= 1;

            HPLabel.text = "HPÅF" + tankHP;
        }

        // Ç‘Ç¬Ç©Ç¡ÇƒÇ´ÇΩëäéËï˚ÅiìGÇÃñCíeÅjÇîjâÛÇ∑ÇÈÅB
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

                // ÉvÉåÅ[ÉÑÅ[ÇîjâÛÇ∑ÇÈÅB
                //Destroy(gameObject);

                PhotonNetwork.Destroy(gameObject);
            }
        }


    [PunRPC]
    private void HitBullet(int id, int ownerId)
    {
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
}