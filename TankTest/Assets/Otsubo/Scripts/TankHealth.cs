using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour
{
    [SerializeField]
    private GameObject effectPrefab1;
    [SerializeField]
    private GameObject effectPrefab2;
    public int tankHP;

    [SerializeField]
    private Text HPLabel;



    void Start()
    {
        HPLabel.text = "HP:" + tankHP;
    }

    private void OnTriggerEnter(Collider other)
    {
        // ‚à‚µ‚à‚Ô‚Â‚©‚Á‚Ä‚«‚½‘Šè‚ÌTag‚ªhEnemyShellh‚Å‚ ‚Á‚½‚È‚ç‚ÎiğŒj
        if (other.gameObject.tag == "EnemyShell")
        {
            // HP‚ğ‚P‚¸‚ÂŒ¸­‚³‚¹‚éB
            tankHP -= 1;

            HPLabel.text = "HP:" + tankHP;

            // ‚Ô‚Â‚©‚Á‚Ä‚«‚½‘Šè•ûi“G‚Ì–C’ej‚ğ”j‰ó‚·‚éB
            Destroy(other.gameObject);

            if (tankHP > 0)
            {
                GameObject effect1 = Instantiate(effectPrefab1, transform.position, Quaternion.identity);
                Destroy(effect1, 1.0f);
            }
            else
            {
                GameObject effect2 = Instantiate(effectPrefab2, transform.position, Quaternion.identity);
                Destroy(effect2, 1.0f);

                // ƒvƒŒ[ƒ„[‚ğ”j‰ó‚·‚éB
                Destroy(gameObject);
            }
        }
    }
}