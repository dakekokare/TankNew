using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PadTurret : MonoBehaviour
{
    Vector3 look;
    private Vector3 angle;
    private AudioSource audioS;

    // Start is called before the first frame update
    void Start()
    {
        // Turret�̍ŏ��̊p�x���擾����B
        angle = transform.eulerAngles;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        float sensitiveRotate = 1.0f;

        float rotateX = look.x * sensitiveRotate;

        transform.parent.parent.Rotate(0.0f, rotateX, 0.0f);

        //angle.x -= look.y * sensitiveRotate;
        angle.x -= look.y;

        transform.eulerAngles = new Vector3(angle.x, transform.parent.parent.eulerAngles.y, 0);

        // �ړ��ł���p�x�ɐ�����������B
        if (angle.x < 80)
        {
            angle.x = 80;
        }

        if (angle.x > 95)
        {
            angle.x = 95;
        }
    }
}
