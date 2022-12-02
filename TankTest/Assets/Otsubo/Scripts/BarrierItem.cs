using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierItem : MonoBehaviour
{
    //[SerializeField]
    //private AudioClip getSound;
    [SerializeField]
    private GameObject effectPrefab;
    private GameObject boat;
    private TankHealth th;

    [SerializeField]
    private GameObject barrierPrefab;

    //private int reward = 5; // �e���������񕜂����邩�͎��R�Ɍ���

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // �uBoat�v�I�u�W�F�N�g��T���ăf�[�^���擾����
            boat = GameObject.Find("Boat(Clone)");

            // �o���A�̃v���n�u�����̉��i�C���X�^���X���j����B
            GameObject barrier = Instantiate(barrierPrefab, boat.transform.GetChild(1).position, Quaternion.identity);

            //  TankHealth�X�N���v�g�̒��ɋL�ڂ���Ă���uBarrier���\�b�h�v���Ăяo���B
            //th.Barrier();

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
}
