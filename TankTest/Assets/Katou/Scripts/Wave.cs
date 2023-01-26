using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    //エフェクト
    [SerializeField]
    private ParticleSystem[] particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        //停止
        foreach (ParticleSystem p in particleSystem)
        {
            p.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void PlayWave(Vector3 vec)
    {
        //停止
        foreach (ParticleSystem p in particleSystem)
        {
            //再生中ではなかったら
            if(p.isPlaying==false)
            {
                //位置を移動して再生
                p.gameObject.transform.position 
                    = new Vector3(vec.x,vec.y+2.0f,vec.z);
                p.Play();
                break;
            }
        }
    }
}
