using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
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

            //�폜
            //PhotonNetwork.Destroy(this.gameObject);
        }
    }
    public void SearchPlayer()
    {
        //// ���[�����̃l�b�g���[�N�I�u�W�F�N�g
        //foreach (var photonView in PhotonNetwork.PhotonViewCollection)
        //{
        //    //boat ���@����
        //    if (photonView.gameObject.name == "Boat(Clone)")
        //    {
        //        if (photonView.IsMine)
        //        {
        //            Debug.Log("[" + GetInstanceID() + "]" + "Player Find");
        //            player = PhotonView.Find(photonView.ViewID).gameObject;
        //        }
        //    }
        //}
    }

}
