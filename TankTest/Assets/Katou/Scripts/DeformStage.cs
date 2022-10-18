using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deform;
public class DeformStage : MonoBehaviour
{
    RippleDeformer ripple;
    // Start is called before the first frame update
    void Start()
    {
        ripple = GetComponent<RippleDeformer>();
    }

    // Update is called once per frame
    void Update()
    {
        //���t���[�����b�V���R���C�_�[��t���͂�������
        Destroy(GetComponent<MeshCollider>());
        gameObject.AddComponent<MeshCollider>();

        //�n�`�ό`
        ripple.Frequency += Time.deltaTime * 0.5f;
    }
}
