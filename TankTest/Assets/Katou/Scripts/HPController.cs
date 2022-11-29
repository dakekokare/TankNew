using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviourPunCallbacks, IPunObservable
{
    private const float MaxHp = 100f;

    [SerializeField]
    private Image HpBar = default;

    private float currentHp = MaxHp;

    private void Update()
    {
        if (photonView.IsMine)
        {
            var input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
            if (input.sqrMagnitude > 0f)
            {
                // 入力があったら、スタミナを減少させる
                //currentHp = Mathf.Max(0f, currentHp - Time.deltaTime);
            }
            else
            {
                // 入力がなかったら、スタミナを回復させる
                //currentHp = Mathf.Min(currentHp + Time.deltaTime * 2, MaxHp);
            }
        }
        // 入力があったら、スタミナを減少させる
        currentHp = Mathf.Max(0f, currentHp - Time.deltaTime);

        // スタミナをゲージに反映する
        HpBar.fillAmount = currentHp / MaxHp;
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // 自身のアバターのスタミナを送信する
            //stream.SendNext(currentHp);
        }
        else
        {
            // 他プレイヤーのアバターのスタミナを受信する
            //currentHp = (float)stream.ReceiveNext();
        }
    }
}