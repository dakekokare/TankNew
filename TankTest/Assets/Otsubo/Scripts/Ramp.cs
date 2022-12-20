using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramp : MonoBehaviour
{
    private Vector3 angle;

    // Start is called before the first frame update
    void Start()
    {
        // Ramp‚ÌÅ‰‚ÌŠp“x‚ğæ“¾‚·‚éB
        angle = transform.parent.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 8)
            {
                Vector3 dir = Quaternion.Euler(0, 0, 1) * other.transform.forward;

                Rigidbody rb = other.GetComponent<Rigidbody>();
                rb.AddForce(dir, ForceMode.Impulse);
            }
        }
    }
}
