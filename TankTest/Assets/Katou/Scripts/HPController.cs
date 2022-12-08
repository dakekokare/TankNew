using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviour
{
    [SerializeField]
    private Image HpBar = default;
    [SerializeField]
    private Image DamageBar = default;
    //hp
    private float greenHp;
    //redHp
    private float redHp;
    //最大ho
    private float MaxHp;
    //赤ダメージ減少フラグ
    private bool damageFlag = false;

    private void Update()
    {
        if (damageFlag)
        {
            //hpがダメージHPより小さい場合
            if (greenHp < redHp)
                redHp = Mathf.Max(0f, redHp - Time.deltaTime * 20.0f);
            else
            {
                damageFlag = false;
            }
            // ゲージに反映する
            DamageBar.fillAmount = redHp / MaxHp;
        }

    }

    public void Damage(float damage)
    {
        // 入力があったら減少させる
        greenHp -= damage;
        // ゲージに反映する
        HpBar.fillAmount = greenHp / MaxHp;
        //　一定時間後にHPバーを減らすフラグを設定
        Invoke("StartRedHP", 0.3f);
    }
    public void StartRedHP()
    {
        damageFlag = true;
    }
    //hp セッター
    public void SetHp(float hp)
    {
        greenHp = hp;
        redHp = hp;
        MaxHp = hp;
    }
    public void HealHp(float heal)
    {
        //回復
        greenHp += heal;
        redHp += heal;
        //最大hpより大きくなったら
        if(greenHp>=MaxHp)
        {
            greenHp = MaxHp;
            redHp = MaxHp;
        }
        // ゲージに反映する
        HpBar.fillAmount = greenHp / MaxHp;
        // ゲージに反映する
        DamageBar.fillAmount = redHp / MaxHp;

    }
}