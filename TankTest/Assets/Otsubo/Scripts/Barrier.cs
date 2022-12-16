using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Barrier : MonoBehaviourPunCallbacks
{
    // �v���C���[
    private GameObject player;

    private float Barriertime = 10.0f;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // �^�C�}�[�̎��Ԃ𓮂���
        timer += Time.deltaTime;

        // 10�b�o������
        if(timer > Barriertime)
        {
            // �^�C�}�[�̎��Ԃ��O�ɖ߂��B
            timer = 0.0f;

            // �o���A�������B
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Shell")
        return;

        if (!photonView.IsMine)
        {
            //shell �ɐڐG�����ꍇ
            if (other.gameObject.tag == "Shell")
            {
                // �Ԃ����Ă���������i�G�̖C�e�j��j�󂷂�B
                Destroy(other.gameObject);
            }

            ////�~�T�C���ƐڐG������
            if (other.gameObject.tag == "Missile")
            {
                // �Ԃ����Ă���������i�G�̖C�e�j��j�󂷂�B
                photonView.RPC(nameof(DeleteMissile), RpcTarget.Others, other.GetComponent<PhotonView>().ViewID);
            }
        }
    }

    //public void SearchPlayer()
    //{
    //    // ���[�����̃l�b�g���[�N�I�u�W�F�N�g
    //    foreach (var photonView in PhotonNetwork.PhotonViewCollection)
    //    {
    //        //boat ���@����
    //        if (photonView.gameObject.name == "Boat(Clone)")
    //        {
    //            if (photonView.IsMine)
    //            {
    //                Debug.Log("[" + GetInstanceID() + "]" + "Player Find");
    //                player = PhotonView.Find(photonView.ViewID).gameObject;
    //            }
    //        }
    //    }
    //}

    [PunRPC]
    private void DeleteMissile(int obj)
    {
        GameObject boat = PhotonView.Find(obj).gameObject;
        //missile ���폜
        PhotonNetwork.Destroy(boat);
    }
}
