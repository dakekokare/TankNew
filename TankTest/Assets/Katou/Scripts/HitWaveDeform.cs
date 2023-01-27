using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitWaveDeform : MonoBehaviour
{
    //アニメーション
    [SerializeField]
    private GameObject wave;

    //パーティクルシステム
    private Wave waveParticle;

    //音
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip sound;
    // Start is called before the first frame update
    void Start()
    {
        //Componentを取得
        waveParticle = wave.GetComponent<Wave>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //wave アニメーション再生
        //boat の場合

        //shell の場合
        if (other.gameObject.tag == "Shell"||
            other.gameObject.tag == "EnemyShell")
        {
            //複数回ヒットするバグ　－＞　弾を削除する

            Debug.Log("Wave");
            //着弾位置にアニメーション再生
            PlayWave(other.gameObject.transform.position);
            //水しぶき音再生
            audioSource.PlayOneShot(sound);
        }
        //missile の場合
    }
    private void PlayWave(Vector3 vec)
    {
        //アニメーション再生
        waveParticle.PlayWave(vec);
    }
}
