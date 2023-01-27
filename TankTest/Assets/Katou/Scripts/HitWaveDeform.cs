using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitWaveDeform : MonoBehaviour
{
    //�A�j���[�V����
    [SerializeField]
    private GameObject wave;

    //�p�[�e�B�N���V�X�e��
    private Wave waveParticle;

    //��
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip sound;
    // Start is called before the first frame update
    void Start()
    {
        //Component���擾
        waveParticle = wave.GetComponent<Wave>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //wave �A�j���[�V�����Đ�
        //boat �̏ꍇ

        //shell �̏ꍇ
        if (other.gameObject.tag == "Shell"||
            other.gameObject.tag == "EnemyShell")
        {
            //������q�b�g����o�O�@�|���@�e���폜����

            Debug.Log("Wave");
            //���e�ʒu�ɃA�j���[�V�����Đ�
            PlayWave(other.gameObject.transform.position);
            //�����Ԃ����Đ�
            audioSource.PlayOneShot(sound);
        }
        //missile �̏ꍇ
    }
    private void PlayWave(Vector3 vec)
    {
        //�A�j���[�V�����Đ�
        waveParticle.PlayWave(vec);
    }
}
