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
    //�v���C���[
    private ShotShell player;

    //�Q�[�W�����X�s�[�h
    private float GuageIncSpeed = 0.01f;
    //�Q�[�W�����X�s�[�h
    private float GuageDecSpeed = 0.01f;

    //�ˌ���~����
    private int GuageStop = 5;
    //���
    enum ShotState
    {
        //�ˌ����
        Shot,
        //�ˌ���~���
        Stop,
        //�Q�[�W�񕜏��
        Recovary
    }

    ShotState state; 
    void Start()
    {
        //������
        Invoke("Initialize", 1);

        //�ˌ����
        state = ShotState.Shot;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case ShotState.Shot:
                //���N���b�N����Ă�����
                if (Input.GetMouseButton(0))
                {
                    //Debug.Log("[" + TurretBar.fillAmount + "]");

                    if (TurretBar.fillAmount < 1.0f)
                        // �Q�[�W�ɔ��f����
                        TurretBar.fillAmount += GuageIncSpeed;
                    else
                        //�ˌ���~���
                        state = ShotState.Stop;
                }
                //���N���b�N��������Ă�����
                else
                {
                    //Debug.Log("[" + TurretBar.fillAmount + "]");
                    //0���傫��������
                    if (TurretBar.fillAmount > 0.0f)
                        // �Q�[�W�ɔ��f����
                        TurretBar.fillAmount -= GuageDecSpeed;
                }
                break;
            case ShotState.Stop:
                //�ˌ��s�\�ɂ���
                player.ShotLock();
                //3�b��~ �ˌ��Q�[�W�񕜏�Ԃɂ���
                Invoke("RecovaryShotGuage", GuageStop);
                break;
            case ShotState.Recovary:
                //0���傫��������
                if (TurretBar.fillAmount > 0.0f)
                    // �Q�[�W�ɔ��f����
                    TurretBar.fillAmount -= GuageDecSpeed;
                else
                {
                    //�ˌ���Ԃ�
                    state = ShotState.Shot;
                    //�ˌ��\�ɂ���
                    player.ShotUnlock();
                }

                break;
        }
    }

    private void Initialize()
    {
        //�v���C���[�T��
        SearchPlayer();
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
        //�ˌ��Q�[�W�񕜏�Ԃ�
        state = ShotState.Recovary;
    }
}
