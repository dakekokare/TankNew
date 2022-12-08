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
    //�ő�ho
    private float MaxHp;
    //�ԃ_���[�W�����t���O
    private bool damageFlag = false;

    private void Update()
    {
        if (damageFlag)
        {
            //hp���_���[�WHP��菬�����ꍇ
            if (greenHp < redHp)
                redHp = Mathf.Max(0f, redHp - Time.deltaTime * 20.0f);
            else
            {
                damageFlag = false;
            }
            // �Q�[�W�ɔ��f����
            DamageBar.fillAmount = redHp / MaxHp;
        }

    }

    public void Damage(float damage)
    {
        // ���͂��������猸��������
        greenHp -= damage;
        // �Q�[�W�ɔ��f����
        HpBar.fillAmount = greenHp / MaxHp;
        //�@��莞�Ԍ��HP�o�[�����炷�t���O��ݒ�
        Invoke("StartRedHP", 0.3f);
    }
    public void StartRedHP()
    {
        damageFlag = true;
    }
    //hp �Z�b�^�[
    public void SetHp(float hp)
    {
        greenHp = hp;
        redHp = hp;
        MaxHp = hp;
    }
    public void HealHp(float heal)
    {
        //��
        greenHp += heal;
        redHp += heal;
        //�ő�hp���傫���Ȃ�����
        if(greenHp>=MaxHp)
        {
            greenHp = MaxHp;
            redHp = MaxHp;
        }
        // �Q�[�W�ɔ��f����
        HpBar.fillAmount = greenHp / MaxHp;
        // �Q�[�W�ɔ��f����
        DamageBar.fillAmount = redHp / MaxHp;

    }
}