using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    [SerializeField]
    [Tooltip("追従させたいターゲット")]
    private GameObject target;
    //private Camera TPScamera;
    private Vector3 offset;
    private Vector3 targetAngle = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        //target = GameObject.Find("BoatBody");
        //TPScamera = GameObject.Find("TPSCamera").GetComponent<Camera>();
        // ゲーム開始時点のカメラとターゲットの距離（オフセット）を取得
        //offset = TPScamera.gameObject.transform.position - target.transform.position;
        offset = gameObject.transform.position - target.transform.position;
    }

    /// <summary>
    /// プレイヤーが移動した後にカメラが移動するようにするためにLateUpdateにする。
    /// </summary>
    void LateUpdate()
    {
        // カメラの位置をターゲットの位置にオフセットを足した場所にする。
        //TPScamera.gameObject.transform.position = target.transform.position + offset;
        gameObject.transform.position = target.transform.position + offset;

        targetAngle.y = target.transform.eulerAngles.y + 90.0f ;
        targetAngle.z = target.transform.eulerAngles.x;
        //TPScamera.gameObject.transform.localEulerAngles = targetAngle;
        gameObject.transform.eulerAngles = targetAngle;
    }
}
