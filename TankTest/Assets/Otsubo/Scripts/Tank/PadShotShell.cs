using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PadShotShell : MonoBehaviour
{
    public float shotSpeed;

    // privateの状態でもInspector上から設定できるようにするテクニック。
    [SerializeField]
    private GameObject shellPrefab;

    [SerializeField]
    private AudioClip shotSound;

    private float timeBetweenShot = 0.75f;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && timer > timeBetweenShot)
        {
            // タイマーの時間を０に戻す。
            timer = 0.0f;

            // 砲弾のプレハブを実体化（インスタンス化）する。
            GameObject shell = Instantiate(shellPrefab, transform.position, Quaternion.identity);

            // 砲弾に付いているRigidbodyコンポーネントにアクセスする。
            Rigidbody shellRb = shell.GetComponent<Rigidbody>();

            // forward（青軸＝Z軸）の方向に力を加える。
            shellRb.AddForce(transform.forward * shotSpeed);

            // 発射した砲弾を３秒後に破壊する。
            // （重要な考え方）不要になった砲弾はメモリー上から削除すること。
            Destroy(shell, 3.0f);

            // 砲弾の発射音を出す。
            AudioSource.PlayClipAtPoint(shotSound, transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // タイマーの時間を動かす
        timer += Time.deltaTime;
    }
}
