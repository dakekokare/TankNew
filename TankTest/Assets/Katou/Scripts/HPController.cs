using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviour
{
    private const float MaxHp = 100f;

    [SerializeField]
    private Image HpBar = default;
    [SerializeField]
    private Image DamageBar = default;
    //hp
    private float greenHp = MaxHp;
    //redHp
    private float redHp = MaxHp;

    //�ԃ_���[�W�����t���O
    private bool damageFlag = false;

    private void Update()
    {
            if (Input.GetMouseButtonDown(0))
            {
                //�_���[�W����
                Damage(20);
            }
            if(damageFlag)
            {
                //hp���_���[�WHP��菬�����ꍇ
                if(greenHp<redHp)
                    redHp = Mathf.Max(0f, redHp - Time.deltaTime*20.0f);
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
}