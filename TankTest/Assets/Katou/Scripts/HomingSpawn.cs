using System.Collections;
using UnityEngine;
using Photon.Pun;
public class HomingSpawn : MonoBehaviourPunCallbacks
{
    private GameObject target;
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
        if (photonView.IsMine) 
        { 
        intervalWait = new WaitForSeconds(interval);
        //プレイヤー探索
        SearchPlayer();
        }
    }
    void Update()
    {
        if (photonView.IsMine)
        {

            //発射
            if (Input.GetMouseButton(0))
            {
                isSpawning = true;
            }
            //ターゲットがいなかったら
            if (target == null)
                return;
            if (!isSpawning)
                return;
            //追尾弾生成
            StartCoroutine(nameof(SpawnMissile));
        }
    }
    IEnumerator SpawnMissile()
    {
        isSpawning = false;
        Homing homing;

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
            homing = PhotonNetwork.Instantiate("HomingMissile", pos , Quaternion.identity).GetComponent<Homing>();
            //ターゲット設定
            homing.Target = target;

        }        
        
        //タレット非アクティブ化
        this.gameObject.SetActive(false);
        //デフォルトタレットアクティブ
        ActiveDefaultTurret();

        //指定した秒数待つ 
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
                }
            }
        }
    }

    private void ActiveDefaultTurret()
    {
        //デフォルトタレットアクティブ
        gameObject.transform.parent.GetChild(2).gameObject.SetActive(true);
    }
}