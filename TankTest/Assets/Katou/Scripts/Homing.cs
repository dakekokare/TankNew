using System.Collections;
using UnityEngine;
using Photon.Pun;
public sealed class Homing : MonoBehaviourPunCallbacks
{
    private GameObject target;
    [SerializeField, Min(0)]
    float time;
    //�e��������
    [SerializeField]
    float lifeTime;
    //��������
    [SerializeField]
    bool limitAcceleration;
    //�ő����
    [SerializeField, Min(0)]
    float maxAcceleration;
    //�ŏ����x�@
    [SerializeField]
    Vector3 minInitVelocity;
    //�ő呬�x
    [SerializeField]
    Vector3 maxInitVelocity;
    //���W
    Vector3 position;
    //���x
    Vector3 velocity;
    //�����x
    Vector3 acceleration;
    Transform thisTransform;
    public GameObject Target
    {
        set
        {
            target = value;
        }
        get
        {
            return target;
        }
    }
    void Start()
    {
        if (photonView.IsMine)
        {

            //���݂̍��W���i�[
            thisTransform = transform;
            position = thisTransform.position;
            //�ŏ����x�`�ő呬�x�̊ԂŃ����_���ő��x�����߂�
            velocity = new Vector3(
                Random.Range(minInitVelocity.x, maxInitVelocity.x),
                Random.Range(minInitVelocity.y, maxInitVelocity.y),
                Random.Range(minInitVelocity.z, maxInitVelocity.z)
                );

            //�����x�v�Z
            acceleration = 2f / (time * time) * (target.transform.position - position - time * velocity);

            // lifeTime ��ɏ���
            StartCoroutine(nameof(Timer));
        }
    }
    public void Update()
    {
        if (photonView.IsMine)
        {

            //�G�����Ȃ������� return 
            if (target == null)
                return;


            //�����x������true �̏ꍇ
            //�����x�̃x�N�g���̑傫���擾
            if (limitAcceleration && acceleration.sqrMagnitude > maxAcceleration * maxAcceleration)
            {
                //�����x�𐧌�
                acceleration = acceleration.normalized * maxAcceleration;
            }
            time -= Time.deltaTime;
            if (time < 0f)
            {
                return;
            }
            velocity += acceleration * Time.deltaTime;
            position += velocity * Time.deltaTime;
            thisTransform.position = position;
            thisTransform.rotation = Quaternion.LookRotation(velocity);
        }
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(lifeTime);
        Debug.Log("Missile LifeLimit");
        PhotonNetwork.Destroy(gameObject);
    }
    void OnTriggerEnter(Collider t)
    {
        if (photonView.IsMine)
        {
            //�����Ƀ~�T�C����������Ȃ��悤�ɂ��鏈��
            //player��������
            if (t.gameObject.layer == 8)
            {
                if (t.gameObject.TryGetComponent<PhotonView>(out var other))
                    //�����̑D��������
                    if (other.IsMine)
                        return;
                    else
                    {
                        //�G��������q�b�g����
                        photonView.RPC(nameof(HitBoatMissile), RpcTarget.Others,other.ViewID);
                    }
            }
            //�o���A�ɓ���������
            //if(t.gameObject.tag== "Barrier")
            //{

            //}

            Debug.Log("[ Missile�폜" + t.gameObject.layer + "&" + t.gameObject.tag + "]");
            PhotonNetwork.Destroy(gameObject);

        }
    }
    [PunRPC]
    private void HitBoatMissile(int id)
    {
        //�G�ɓ���������q�b�g������������
        GameObject boat = PhotonView.Find(id).gameObject;
        //boat.gameObject.transform.Find("BoatBody").
        //    gameObject.GetComponent<TankHealth>().HitBullet();
        
        boat.gameObject.
        gameObject.GetComponent<TankHealth>().HitBullet();

    }
}