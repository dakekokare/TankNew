using System.Collections;
using UnityEngine;
using Photon.Pun;
public class HomingSpawn : MonoBehaviour
{
    private GameObject target;
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
        //�v���C���[�T��
        SearchPlayer();

    }
    void Update()
    {
        //����
        if (Input.GetMouseButton(0))
        {
            isSpawning = true;
        }
        //�^�[�Q�b�g�����Ȃ�������
        if (target == null)
            return;
        if (!isSpawning)
            return;
        //�ǔ��e����
        StartCoroutine(nameof(SpawnMissile));
    }
    IEnumerator SpawnMissile()
    {
        isSpawning = false;
        Homing homing;

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
            homing = PhotonNetwork.Instantiate("HomingMissile", pos , Quaternion.identity).GetComponent<Homing>();
            
            

            //�^�[�Q�b�g���ݒ肳��Ă��Ȃ��o�O
            homing.Target = target;

        }
        //�^���b�g��A�N�e�B�u��
        this.gameObject.SetActive(false);
        //�f�t�H���g�^���b�g�A�N�e�B�u
        ActiveDefaultTurret();
        //�w�肵���b���҂� 
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
                }
            }
        }
    }

    private void ActiveDefaultTurret()
    {
        //�f�t�H���g�^���b�g�A�N�e�B�u
        gameObject.transform.parent.GetChild(2).gameObject.SetActive(true);
    }
}