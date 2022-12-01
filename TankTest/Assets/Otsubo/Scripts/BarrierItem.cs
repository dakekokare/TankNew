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

    [SerializeField]
    private GameObject barrierPrefab;

    //private int reward = 5; // �e���������񕜂����邩�͎��R�Ɍ���

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            boat = GameObject.Find("Boat(Clone)");

            //  ShotShell�X�N���v�g�̒��ɋL�ڂ���Ă���uAddShell���\�b�h�v���Ăяo���B
            // reward�Őݒ肵�����l�������e�����񕜂���B
            //ss.AddShell(reward);

            // �A�C�e������ʂ���폜����B
            Destroy(gameObject);

            // �A�C�e���Q�b�g�����o���B
            //AudioSource.PlayClipAtPoint(getSound, transform.position);

            // �A�C�e���Q�b�g���ɃG�t�F�N�g�𔭐�������B
            GameObject effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);

            // �G�t�F�N�g��0.5�b��ɏ����B
            Destroy(effect, 0.5f);
        }
    }
}
