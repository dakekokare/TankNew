using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class UiTurretGuage : MonoBehaviourPunCallbacks
{
    //�^���b�g�Q�[�W
    [SerializeField]
    private Image TurretBar = default;
    //�ˌ��\�t���O
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
                //���N���b�N����Ă�����
                if (Input.GetMouseButton(0))
                {
                    //Debug.Log("[" + TurretBar.fillAmount + "]");

                    if (TurretBar.fillAmount < 1.0f)
                        // �Q�[�W�ɔ��f����
                        TurretBar.fillAmount += 0.01f;
                    else
                        //�ˌ��s�\
                        TurretShotFlag = false;
                }
                //���N���b�N��������Ă�����
                else
                {
                    //Debug.Log("[" + TurretBar.fillAmount + "]");
                    //0���傫��������
                    if (TurretBar.fillAmount > 0.0f)
                        // �Q�[�W�ɔ��f����
                        TurretBar.fillAmount -= 0.01f;
                }
            }
            //�ˌ��s�\�Ȏ�
            if (TurretShotFlag == false)
            {

            }
        }
    }

    private void SearchPlayer()
    {
        // ���[�����̃l�b�g���[�N�I�u�W�F�N�g
        foreach (var photonView in PhotonNetwork.PhotonViewCollection)
        {
            //boat ���@��������Ȃ�������
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
