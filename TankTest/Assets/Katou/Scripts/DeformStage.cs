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
        //毎フレームメッシュコライダーを付けはずしする
        Destroy(GetComponent<MeshCollider>());
        gameObject.AddComponent<MeshCollider>();

        //地形変形
        ripple.Frequency += Time.deltaTime * 0.5f;
    }
}
