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
    // Start is called before the first frame update
    void Start()
    {
        waveParticle=wave.GetComponent<Wave>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {

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
        }
        //missile �̏ꍇ
    }
    private void PlayWave(Vector3 vec)
    {
        //�A�j���[�V�����Đ�
        waveParticle.PlayWave(vec);
    }
}
