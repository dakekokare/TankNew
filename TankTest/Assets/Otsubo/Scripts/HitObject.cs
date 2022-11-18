using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObject : MonoBehaviour
{
    // �G�t�F�N�g�v���n�u�̃f�[�^�����邽�߂̔������B
    [SerializeField]
    private GameObject effectPrefab;

    // ���̃��\�b�h�̓R���C�_�[���m���Ԃ������u�ԂɌĂяo�����
    private void OnTriggerEnter(Collider other)
    {
        // �������Ԃ����������Tag��Shell�Ƃ������O�������Ă������Ȃ�΁i�����j
        if (other.CompareTag("Shell"))
        {
            // �Ԃ����Ă����I�u�W�F�N�g��j�󂷂�
            Destroy(other.gameObject);

            // �G�t�F�N�g�����̉��i�C���X�^���X���j����B
            GameObject effect = Instantiate(effectPrefab, other.transform.position, Quaternion.identity);

            // �G�t�F�N�g���Q�b��ɉ�ʂ������
            Destroy(effect, 2.0f);
        }
    }
}
