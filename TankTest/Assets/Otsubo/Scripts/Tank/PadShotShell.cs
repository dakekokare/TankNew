using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PadShotShell : MonoBehaviour
{
    public float shotSpeed;

    // private�̏�Ԃł�Inspector�ォ��ݒ�ł���悤�ɂ���e�N�j�b�N�B
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
            // �^�C�}�[�̎��Ԃ��O�ɖ߂��B
            timer = 0.0f;

            // �C�e�̃v���n�u�����̉��i�C���X�^���X���j����B
            GameObject shell = Instantiate(shellPrefab, transform.position, Quaternion.identity);

            // �C�e�ɕt���Ă���Rigidbody�R���|�[�l���g�ɃA�N�Z�X����B
            Rigidbody shellRb = shell.GetComponent<Rigidbody>();

            // forward�i����Z���j�̕����ɗ͂�������B
            shellRb.AddForce(transform.forward * shotSpeed);

            // ���˂����C�e���R�b��ɔj�󂷂�B
            // �i�d�v�ȍl�����j�s�v�ɂȂ����C�e�̓������[�ォ��폜���邱�ƁB
            Destroy(shell, 3.0f);

            // �C�e�̔��ˉ����o���B
            AudioSource.PlayClipAtPoint(shotSound, transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // �^�C�}�[�̎��Ԃ𓮂���
        timer += Time.deltaTime;
    }
}