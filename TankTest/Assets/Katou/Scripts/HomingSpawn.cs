using System.Collections;
using UnityEngine;
public class HomingSpawn : MonoBehaviour
{
    [SerializeField]
    GameObject target;
    //íe
    private GameObject bullet;
    //íeÇÃêî
    [SerializeField]
    int iterationCount = 3;
    //î≠éÀä‘äu
    [SerializeField]
    float interval = 0.1f;

    bool isSpawning = false;
    Transform thisTransform;
    WaitForSeconds intervalWait;
    void Start()
    {
        thisTransform = transform;
        intervalWait = new WaitForSeconds(interval);
    }
    void Update()
    {
        //î≠éÀ
        if (Input.GetMouseButton(0))
            isSpawning = true;
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
        //íeÇÃêî
        for (int i = 0; i < iterationCount; i++)
        {
            homing = Instantiate(bullet, thisTransform.position, Quaternion.identity).GetComponent<Homing>();
            homing.Target = target;
        }
        this.gameObject.SetActive(false);

        yield return intervalWait;
    }
}