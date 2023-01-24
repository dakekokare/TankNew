using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    [SerializeField]
    [Tooltip("�Ǐ]���������^�[�Q�b�g")]
    private GameObject target;
    //private Camera TPScamera;
    private Vector3 offset;
    private Vector3 targetAngle = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        //target = GameObject.Find("BoatBody");
        //TPScamera = GameObject.Find("TPSCamera").GetComponent<Camera>();
        // �Q�[���J�n���_�̃J�����ƃ^�[�Q�b�g�̋����i�I�t�Z�b�g�j���擾
        //offset = TPScamera.gameObject.transform.position - target.transform.position;
        offset = gameObject.transform.position - target.transform.position;
    }

    /// <summary>
    /// �v���C���[���ړ�������ɃJ�������ړ�����悤�ɂ��邽�߂�LateUpdate�ɂ���B
    /// </summary>
    void LateUpdate()
    {
        // �J�����̈ʒu���^�[�Q�b�g�̈ʒu�ɃI�t�Z�b�g�𑫂����ꏊ�ɂ���B
        //TPScamera.gameObject.transform.position = target.transform.position + offset;
        gameObject.transform.position = target.transform.position + offset;

        targetAngle.y = target.transform.eulerAngles.y + 90.0f ;
        targetAngle.z = target.transform.eulerAngles.x;
        //TPScamera.gameObject.transform.localEulerAngles = targetAngle;
        gameObject.transform.eulerAngles = targetAngle;
    }
}
