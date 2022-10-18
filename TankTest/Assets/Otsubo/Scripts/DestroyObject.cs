using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    // �G�t�F�N�g�v���n�u�̃f�[�^�����邽�߂̔������B
    [SerializeField]
    private GameObject effectPrefab;

    [SerializeField]
    private GameObject effectPrefab2; // 2��ޖڂ̃G�t�F�N�g�����邽�߂̔�
    public int objectHP;

    // ���̃��\�b�h�̓R���C�_�[���m���Ԃ������u�ԂɌĂяo�����
    private void OnTriggerEnter(Collider other)
    {
        // �������Ԃ����������Tag��Shell�Ƃ������O�������Ă������Ȃ�΁i�����j
        if (other.CompareTag("Shell"))
        {
            // �I�u�W�F�N�g��HP���P������������B
            objectHP -= 1;

            // ������HP��0�����傫���ꍇ�ɂ́i�����j
            if (objectHP > 0)
            {
                // �Ԃ����Ă����I�u�W�F�N�g��j�󂷂�
                Destroy(other.gameObject);

                // �G�t�F�N�g�����̉��i�C���X�^���X���j����B
                GameObject effect = Instantiate(effectPrefab, other.transform.position, Quaternion.identity);

                // �G�t�F�N�g���Q�b��ɉ�ʂ������
                Destroy(effect, 2.0f);
            }
            else //  �����łȂ��ꍇ�iHP��0�ȉ��ɂȂ����ꍇ�j�ɂ́i�����j
            {
                Destroy(other.gameObject);

                // �����P��ނ̃G�t�F�N�g�𔭐�������B
                GameObject effect2 = Instantiate(effectPrefab2, other.transform.position, Quaternion.identity);
                Destroy(effect2, 2.0f);

                // ���̃X�N���v�g�����Ă���I�u�W�F�N�g��j�󂷂�ithis�͏ȗ����\�j
                Destroy(this.gameObject);
            }
        }
    }
}