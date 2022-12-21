using System.Collections;
using UnityEngine;
using Photon.Pun;
public sealed class Homing : MonoBehaviourPunCallbacks
{
    private GameObject target;
    [SerializeField, Min(0)]
    float time;
    //弾生存時間
    [SerializeField]
    float lifeTime;
    //加速制限
    [SerializeField]
    bool limitAcceleration;
    //最大加速
    [SerializeField, Min(0)]
    float maxAcceleration;
    //最小速度　
    [SerializeField]
    Vector3 minInitVelocity;
    //最大速度
    [SerializeField]
    Vector3 maxInitVelocity;
    //座標
    Vector3 position;
    //速度
    Vector3 velocity;
    //加速度
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

            //現在の座標を格納
            thisTransform = transform;
            position = thisTransform.position;
            //最小速度～最大速度の間でランダムで速度を決める
            velocity = new Vector3(
                Random.Range(minInitVelocity.x, maxInitVelocity.x),
                Random.Range(minInitVelocity.y, maxInitVelocity.y),
                Random.Range(minInitVelocity.z, maxInitVelocity.z)
                );

            //加速度計算
            acceleration = 2f / (time * time) * (target.transform.position - position - time * velocity);

            // lifeTime 後に消す
            StartCoroutine(nameof(Timer));
        }
    }
    public void Update()
    {
        if (photonView.IsMine)
        {

            //敵がいなかったら return 
            if (target == null)
                return;


            //加速度制限がtrue の場合
            //加速度のベクトルの大きさ取得
            if (limitAcceleration && acceleration.sqrMagnitude > maxAcceleration * maxAcceleration)
            {
                //加速度を制限
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
        PhotonNetwork.Destroy(gameObject);
    }
    void OnTriggerEnter(Collider t)
    {
        if (photonView.IsMine)
        {
            //自分にミサイルが当たらないようにする処理
            //player かつ　自分だったら
            if (t.gameObject.layer == 8)
            {
                if (t.gameObject.TryGetComponent<PhotonView>(out var other))
                    if(other.gameObject.GetComponent<PhotonView>().IsMine)
                        return;
            }
            PhotonNetwork.Destroy(gameObject);

        }
    }
}