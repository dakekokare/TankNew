using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BarrierItem : MonoBehaviour
{
    //[SerializeField]
    //private AudioClip getSound;
    [SerializeField]
    private GameObject effectPrefab;

    //private GameObject boat;

    //�v���C���[
    private GameObject player;

    [SerializeField]
    private GameObject barrierPrefab;

    //[SerializeField]
    //private Barrier barriercomponent;

    // Start is called before the first frame update
    void Start()
    {
        //�v���C���[�T��
        SearchPlayer();
    }

    void Update()
    {
        float sin = Mathf.Sin(Time.time) + transform.position.y;
        this.transform.position = new Vector3(
            transform.position.x,
            sin * 0.5f,
            transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            //�o���A�A�N�e�B�u
            other.gameObject.transform.GetChild(1).gameObject.SetActive(true);

            // �A�C�e������ʂ���폜����B
            PhotonNetwork.Destroy(gameObject);

            // �A�C�e���Q�b�g�����o���B
            //AudioSource.PlayClipAtPoint(getSound, transform.position);

            // �A�C�e���Q�b�g���ɃG�t�F�N�g�𔭐�������B
            GameObject effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);    

            // �G�t�F�N�g��0.5�b��ɏ����B
            Destroy(effect, 0.5f);
        }
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
