using System.Collections;
using UnityEngine;
using Photon.Pun;
public sealed class Homing : MonoBehaviourPunCallbacks
{
    private GameObject target;
    [SerializeField, Min(0)]
    float time = 1;
    //�e��������
    [SerializeField]
    float lifeTime = 2;
    [SerializeField]
    bool limitAcceleration = false;
    [SerializeField, Min(0)]
    float maxAcceleration = 100;
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
        //���݂̍��W���i�[
        thisTransform = transform;
        position = thisTransform.position;
        //�ŏ����x�`�ő呬�x�̊ԂŃ����_���ő��x�����߂�
        velocity = new Vector3(
            Random.Range(minInitVelocity.x, maxInitVelocity.x), 
            Random.Range(minInitVelocity.y, maxInitVelocity.y), 
            Random.Range(minInitVelocity.z, maxInitVelocity.z)
            );
        // lifeTime ��ɏ���
        StartCoroutine(nameof(Timer));
    }
    public void Update()
    {
        //�G�����Ȃ������� return 
        if (target == null)
        {
            return;
        }
        //�����x�v�Z
        acceleration = 2f / (time * time) * (target.transform.position - position - time * velocity);
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
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(lifeTime);
        PhotonNetwork.Destroy(gameObject);
    }
}