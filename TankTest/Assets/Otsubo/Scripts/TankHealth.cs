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

        HPLabel.text = "HP�F" + tankHP;
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
            // HP���P������������B

            tankHP -= 1;

            HPLabel.text = "HP�F" + tankHP;
        }

        // �Ԃ����Ă���������i�G�̖C�e�j��j�󂷂�B
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

                //Lose UI �ǉ�
                GameObject lose = GameObject.Find("LOSECanvas").gameObject.transform.GetChild(0).gameObject;
                lose.SetActive(true);

            //win Ui���A�N�e�B�u����
            photonView.RPC(nameof(WinActive), RpcTarget.All);
            

            //// �v���[���[��j�󂷂�B
            PhotonNetwork.Destroy(gameObject);
            //gameObject.SetActive(false);
        }
    }

    [PunRPC]
    private void HitBullet(int id, int ownerId)
    {
        //�e�폜
        Debug.Log("�e�폜");

        //�e���폜����
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
        Debug.Log("����");
        //win ���A�N�e�B�u����
        GameObject win = GameObject.Find("WINCanvas").gameObject.transform.GetChild(0).gameObject;
        win.SetActive(true);
        if (tankHP == 0)
            win.SetActive(false);
    }

}