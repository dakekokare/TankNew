using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramp : MonoBehaviour
{
    private Vector3 angle;

    // Start is called before the first frame update
    void Start()
    {
        // Rampの最初の角度を取得する。
        angle = transform.parent.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            Debug.Log("chack");

            //Vector3 dir = Quaternion.Euler(0, 0, 100) * transform.parent.forward;
            Vector3 dir = transform.parent.forward;
            Vector3 force = new Vector3(0.0f, 1000.0f, 0.0f);
            if (other.gameObject.TryGetComponent<Rigidbody>(out var r))
                r.AddForce(dir  * 50.0f, ForceMode.Impulse);
        }
    }
}
