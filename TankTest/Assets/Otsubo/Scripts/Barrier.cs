using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Barrier : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Shell")
        return;

        ////“G‚Ì’e‚É“–‚½‚Á‚½‚ç
        if (other.TryGetComponent<BulletNet>(out var shell))
        {
            if (shell.OwnerId != PhotonNetwork.LocalPlayer.ActorNumber)
            {
                // ‚Ô‚Â‚©‚Á‚Ä‚«‚½‘Šè•ûi“G‚Ì–C’ej‚ğ”j‰ó‚·‚éB
                PhotonView.Destroy(other.gameObject);
            }
        }
    }
}
