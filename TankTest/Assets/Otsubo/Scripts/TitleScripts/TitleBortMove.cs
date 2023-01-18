using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBortMove : MonoBehaviour
{
    public float moveSpeed;
    public float turnSpeed;
    private Rigidbody rb;
    private float movementInputValue;
    private float turnInputValue;
    private int time;
    private bool move;

    private Transform Initialize;
    private Vector3 InitializePos;
    private Quaternion InitializeRot;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        time = 0;
        move = false;
        Initialize = this.gameObject.transform;
        InitializePos = this.gameObject.transform.position;
        InitializeRot = this.gameObject.transform.rotation;
    }

    void Update()
    {
        BoatMove();
        BoatTurn();
    }

    // 前進・後退のメソッド
    void BoatMove()
    {
        if (move == true)
        {
            movementInputValue = Input.GetAxis("Vertical");
            Vector3 movement = -transform.right * 0.5f * moveSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + movement);

            time++;
            if(time == 150)
            {
                
                move = false;
            }
        }
    }

    // 旋回のメソッド
    void BoatTurn()
    {
        turnInputValue = Input.GetAxis("Horizontal");
        float turn = turnInputValue * turnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0, turn, 0);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    // 前進・後退のメソッド
    public void StartMove()
    {
        time = 0;
        move = true;
    }

    public void BoatReset()
    {
        this.transform.position = InitializePos;
        this.transform.rotation = InitializeRot;
        this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
