using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
public class PadTankMovement : MonoBehaviourPunCallbacks
{
    Vector3 move;
    public float moveSpeed;
    public float turnSpeed;
    private Rigidbody rb;
    private float movementInputValue;
    private float turnInputValue;

    // コントローラ設定
    private TestInputActions m_input = null;

    private MoveManager moveManager;
    void Start()
    {


        m_input = new TestInputActions();
        m_input.Enable();

        //if(photonView.IsMine)
        rb = GetComponent<Rigidbody>();

        moveManager = GameObject.Find("MoveManager").GetComponent<MoveManager>();

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        //if (photonView.IsMine)
        move = context.ReadValue<Vector2>();
        moveManager.SetVec(move);
        moveManager.SetRb(rb);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        moveManager.SetVec(move);
        moveManager.SetRb(rb);

        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log("Fire");
        }
    }

    void Update()
    {
        //const float Speed = 100f;
        //transform.Translate(move * Speed * Time.deltaTime);
        //if (photonView.IsMine)
        
        //TankMove();
        //TankTurn();
    }

    // 前進・後退のメソッド
    void TankMove()
    {
        movementInputValue = move.y;
        Vector3 movement = transform.forward * movementInputValue * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }

    // 旋回のメソッド
    void TankTurn()
    {
        turnInputValue = move.x;
        float turn = turnInputValue * turnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0, turn, 0);
        rb.MoveRotation(rb.rotation * turnRotation);
    }
}