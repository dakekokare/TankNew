using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    //�G�t�F�N�g
    [SerializeField]
    private ParticleSystem[] particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        //��~
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
        //��~
        foreach (ParticleSystem p in particleSystem)
        {
            //�Đ����ł͂Ȃ�������
            if(p.isPlaying==false)
            {
                //�ʒu���ړ����čĐ�
                p.gameObject.transform.position 
                    = new Vector3(vec.x,vec.y+2.0f,vec.z);
                p.Play();
                break;
            }
        }
    }
}
