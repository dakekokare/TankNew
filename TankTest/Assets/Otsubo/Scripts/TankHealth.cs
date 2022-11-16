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

    //[SerializeField]
    //private Text HPLabel;

    private GameObject canvas;

    private Text HPLabel;

    void Start()
    {
        canvas = GameObject.Find("CanvasObj(Clone)").transform.GetChild(0).GetChild(0).gameObject;
        //HPLabel = canvas.transform.GetChild(0).GetChild(0).gameObject;
        HPLabel = canvas.GetComponent<Text>();

        HPLabel.text = "HPÅF" + tankHP;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ç‡ÇµÇ‡Ç‘Ç¬Ç©Ç¡ÇƒÇ´ÇΩëäéËÇÃTagÇ™ÅhEnemyShellÅhÇ≈Ç†Ç¡ÇΩÇ»ÇÁÇŒÅièåèÅj
        if (other.gameObject.tag == "EnemyShell")
        {
            // HPÇÇPÇ∏Ç¬å∏è≠Ç≥ÇπÇÈÅB
            tankHP -= 1;

            HPLabel.text = "HPÅF" + tankHP;

            // Ç‘Ç¬Ç©Ç¡ÇƒÇ´ÇΩëäéËï˚ÅiìGÇÃñCíeÅjÇîjâÛÇ∑ÇÈÅB
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

                // ÉvÉåÅ[ÉÑÅ[ÇîjâÛÇ∑ÇÈÅB
                Destroy(gameObject);
            }
        }
    }
}