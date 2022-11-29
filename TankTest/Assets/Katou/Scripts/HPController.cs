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
                // ���͂���������A�X�^�~�i������������
                //currentHp = Mathf.Max(0f, currentHp - Time.deltaTime);
            }
            else
            {
                // ���͂��Ȃ�������A�X�^�~�i���񕜂�����
                //currentHp = Mathf.Min(currentHp + Time.deltaTime * 2, MaxHp);
            }
        }
        // ���͂���������A�X�^�~�i������������
        currentHp = Mathf.Max(0f, currentHp - Time.deltaTime);

        // �X�^�~�i���Q�[�W�ɔ��f����
        HpBar.fillAmount = currentHp / MaxHp;
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // ���g�̃A�o�^�[�̃X�^�~�i�𑗐M����
            //stream.SendNext(currentHp);
        }
        else
        {
            // ���v���C���[�̃A�o�^�[�̃X�^�~�i����M����
            //currentHp = (float)stream.ReceiveNext();
        }
    }
}