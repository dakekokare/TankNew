using System.Collections;
using UnityEngine;
using Photon.Pun;
public class HomingSpawn : MonoBehaviour
{
    private GameObject target;
    //弾
    private GameObject bullet;
    //発射座標
    [SerializeField]
    private GameObject rPos;
    //発射座標
    [SerializeField]
    private GameObject lPos;

    //弾の数
    [SerializeField]
    int iterationCount = 3;
    //発射間隔
    [SerializeField]
    float interval = 0.1f;

    bool isSpawning = false;
    WaitForSeconds intervalWait;
    void Start()
    {
        intervalWait = new WaitForSeconds(interval);
    }
    void Update()
    {
        //発射
        if (Input.GetMouseButton(0))
        {
            //プレイヤー探索
            SearchPlayer();
        }
        //ターゲットがいなかったら
        if (target == null)
            return;
        if (!isSpawning)
        {
            return;
        }
        StartCoroutine(nameof(SpawnMissile));
    }
    IEnumerator SpawnMissile()
    {
        isSpawning = false;
        Homing homing;

        bullet = (GameObject)Resources.Load("HomingBullet");
        //弾の数
        for (int i = 0; i < iterationCount; i++)
        {
            //交互に弾を発射する
            Vector3 pos;
            if (i % 2 == 0)
                pos = lPos.transform.position;
            else
                pos = rPos.transform.position;
            //弾発射
            homing = Instantiate(bullet,pos , Quaternion.identity).GetComponent<Homing>();
            homing.Target = target;
        }
        //タレット非アクティブ化
        this.gameObject.SetActive(false);

        yield return intervalWait;
    }
    public void SearchPlayer()
    {
        // ルーム内のネットワークオブジェクト
        foreach (var photonView in PhotonNetwork.PhotonViewCollection)
        {
            //boat かつ　敵のボート
            if (photonView.gameObject.name == "Boat(Clone)")
            {
                if (!photonView.IsMine)
                {
                    Debug.Log("[" + GetInstanceID() + "]" + "Enemy Find");
                    target = PhotonView.Find(photonView.ViewID).gameObject;
                    isSpawning = true;
                }
            }
        }
    }

}