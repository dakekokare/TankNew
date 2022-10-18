using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// MonoBehaviourPunCallbacksを継承して、photonViewプロパティを使えるようにする
public class TankMovement : MonoBehaviourPunCallbacks
{
    public float moveSpeed;
    public float turnSpeed;
    private Rigidbody rb;
    private float movementInputValue;
    private float turnInputValue;
    //通信用
    private PhotonView pview;

    private void Start()
    {
        pview = GetComponent<PhotonView>();


        // 自身が生成したオブジェクトだけに移動処理を行う
        if (/*gameObject.GetPhotonView().IsMine*/pview.IsMine)
        {

            rb = GetComponent<Rigidbody>();


        }
    }

    private void Update()
    {
        // 自身が生成したオブジェクトだけに移動処理を行う
        if (/*gameObject.GetPhotonView().IsMine*/pview.IsMine)
        {
            TankMove();
            TankTurn();
        }
    }

    // 前進・後退のメソッド
    void TankMove()
    {
           movementInputValue = Input.GetAxis("Vertical");
           Vector3 movement = transform.forward * movementInputValue * moveSpeed * Time.deltaTime;
           rb.MovePosition(rb.position + movement);
    }

    // 旋回のメソッド
    void TankTurn()
    {
       
            turnInputValue = Input.GetAxis("Horizontal");
            float turn = turnInputValue * turnSpeed * Time.deltaTime;
            Quaternion turnRotation = Quaternion.Euler(0, turn, 0);
            rb.MoveRotation(rb.rotation * turnRotation);
        
    }
}