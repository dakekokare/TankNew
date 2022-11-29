using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviourPunCallbacks, IPunObservable
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

    //赤ダメージ減少フラグ
    private bool damageFlag = false;

    //通信用受け取るHP
    private float reciveHp = 0;
    private void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //ダメージ処理
                Damage(20);
            }
            if(damageFlag)
            {
                //hpがダメージHPより小さい場合
                if(greenHp<redHp)
                    redHp = Mathf.Max(0f, redHp - Time.deltaTime*20.0f);
                else
                {
                    damageFlag = false;
                }
                // ゲージに反映する
                DamageBar.fillAmount = redHp / MaxHp;
            }
        }

    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // 自身のアバターのスタミナを送信する
            stream.SendNext(greenHp);
        }
        else
        {
            // 他プレイヤーのアバターのスタミナを受信する
            reciveHp = (float)stream.ReceiveNext();
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
}