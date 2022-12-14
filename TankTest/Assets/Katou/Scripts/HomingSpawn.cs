using System.Collections;
using UnityEngine;
using Photon.Pun;
public class HomingSpawn : MonoBehaviour
{
    private GameObject target;
    //�e
    private GameObject bullet;
    //���ˍ��W
    [SerializeField]
    private GameObject rPos;
    //���ˍ��W
    [SerializeField]
    private GameObject lPos;

    //�e�̐�
    [SerializeField]
    int iterationCount = 3;
    //���ˊԊu
    [SerializeField]
    float interval = 0.1f;

    bool isSpawning = false;
    WaitForSeconds intervalWait;
    void Start()
    {
        intervalWait = new WaitForSeconds(interval);
    }
    void Update()
    {
        //����
        if (Input.GetMouseButton(0))
        {
            //�v���C���[�T��
            SearchPlayer();
        }
        //�^�[�Q�b�g�����Ȃ�������
        if (target == null)
            return;
        if (!isSpawning)
        {
            return;
        }
        StartCoroutine(nameof(SpawnMissile));
    }
    IEnumerator SpawnMissile()
    {
        isSpawning = false;
        Homing homing;

        bullet = (GameObject)Resources.Load("HomingBullet");
        //�e�̐�
        for (int i = 0; i < iterationCount; i++)
        {
            //���݂ɒe�𔭎˂���
            Vector3 pos;
            if (i % 2 == 0)
                pos = lPos.transform.position;
            else
                pos = rPos.transform.position;
            //�e����
            homing = Instantiate(bullet,pos , Quaternion.identity).GetComponent<Homing>();
            homing.Target = target;
        }
        //�^���b�g��A�N�e�B�u��
        this.gameObject.SetActive(false);

        yield return intervalWait;
    }
    public void SearchPlayer()
    {
        // ���[�����̃l�b�g���[�N�I�u�W�F�N�g
        foreach (var photonView in PhotonNetwork.PhotonViewCollection)
        {
            //boat ���@�G�̃{�[�g
            if (photonView.gameObject.name == "Boat(Clone)")
            {
                if (!photonView.IsMine)
                {
                    Debug.Log("[" + GetInstanceID() + "]" + "Enemy Find");
                    target = PhotonView.Find(photonView.ViewID).gameObject;
                    isSpawning = true;
                }
            }
        }
    }

}