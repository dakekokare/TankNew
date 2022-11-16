using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    public float moveSpeed;
    public float turnSpeed;
    private float movementInputValue;
    private float turnInputValue;
    private Rigidbody rb;
    private Vector3 move;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<Rigidbody>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        TankMove();
        TankTurn();
    }

    // 前進・後退のメソッド
    public void TankMove()
    {

        movementInputValue = move.y;
        Vector3 movement = transform.forward * movementInputValue * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }

    // 旋回のメソッド
    public void TankTurn()
    {
        turnInputValue = move.x;
        float turn = turnInputValue * turnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0, turn, 0);
        rb.MoveRotation(rb.rotation * turnRotation);
    }
    public void SetRb(Rigidbody r)
    {
        rb = r;
    }
    public void SetVec(Vector3 v)
    {
        move = v;
    }
}
