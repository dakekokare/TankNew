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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            // �uBoat�v�I�u�W�F�N�g��T���ăf�[�^���擾����
            //boat = GameObject.Find("Boat(Clone)");

            // �o���A�̃v���n�u�����̉��i�C���X�^���X���j����B
            //GameObject barrier = Instantiate(barrierPrefab, boat.transform.GetChild(1).position, Quaternion.identity);
            //barrier.transform.parent = boat.transform;

            // �o���A�̃v���n�u�����̉��i�C���X�^���X���j����B
            GameObject barrier = Instantiate(barrierPrefab, player.transform.GetChild(1).position, Quaternion.identity);
            //GameObject barrier = Instantiate(barrierPrefab, other.transform.GetChild(1).position, Quaternion.identity);
            
            // �A�C�e������ʂ���폜����B
            Destroy(gameObject);

            // �A�C�e���Q�b�g�����o���B
            //AudioSource.PlayClipAtPoint(getSound, transform.position);

            // �A�C�e���Q�b�g���ɃG�t�F�N�g�𔭐�������B
            GameObject effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);

            // �G�t�F�N�g��0.5�b��ɏ����B
            Destroy(effect, 0.5f);
          
            // �o���A��10�b��ɔj�󂷂�B
            Destroy(barrier, 10.0f);
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
