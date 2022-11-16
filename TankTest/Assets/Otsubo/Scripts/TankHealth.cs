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

        HPLabel.text = "HP：" + tankHP;
    }

    private void OnTriggerEnter(Collider other)
    {
        // もしもぶつかってきた相手のTagが”EnemyShell”であったならば（条件）
        if (other.gameObject.tag == "EnemyShell")
        {
            // HPを１ずつ減少させる。
            tankHP -= 1;

            HPLabel.text = "HP：" + tankHP;

            // ぶつかってきた相手方（敵の砲弾）を破壊する。
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

                // プレーヤーを破壊する。
                Destroy(gameObject);
            }
        }
    }
}