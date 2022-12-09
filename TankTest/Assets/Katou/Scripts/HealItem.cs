using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class HealItem : MonoBehaviour
{
    //�v���C���[
    private GameObject player;
    private float heal;
    // Start is called before the first frame update
    void Start()
    {
        //�񕜗�
        heal = 20.0f;
        //�v���C���[�T��
        SearchPlayer();
    }

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
        //�v���C���[�ƐڐG������
        if (t.gameObject.layer == 8)
        {
            //�ڐG�������
            HealPlayer();
            //�폜
            PhotonNetwork.Destroy(this.gameObject);
        }
    }

    // �񕜂���
    private void HealPlayer()
    {
        //�v���C���[�ɉ�
        player.GetComponent<TankHealth>().HealHP(heal);
    }
    public void SearchPlayer()
    {
        // ���[�����̃l�b�g���[�N�I�u�W�F�N�g
        foreach (var photonView in PhotonNetwork.PhotonViewCollection)
        {
            //boat ���@����
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
