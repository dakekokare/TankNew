using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// MonoBehaviourPunCallbacks���p�����āAphotonView�v���p�e�B���g����悤�ɂ���
public class TankMovement : MonoBehaviourPunCallbacks
{
    public float moveSpeed;
    public float turnSpeed;
    private Rigidbody rb;
    private float movementInputValue;
    private float turnInputValue;
    //�ʐM�p
    private PhotonView pview;

    private void Start()
    {
        pview = GetComponent<PhotonView>();


        // ���g�����������I�u�W�F�N�g�����Ɉړ��������s��
        if (/*gameObject.GetPhotonView().IsMine*/pview.IsMine)
        {

            rb = GetComponent<Rigidbody>();


        }
    }

    private void Update()
    {
        // ���g�����������I�u�W�F�N�g�����Ɉړ��������s��
        if (/*gameObject.GetPhotonView().IsMine*/pview.IsMine)
        {
            TankMove();
            TankTurn();
        }
    }

    // �O�i�E��ނ̃��\�b�h
    void TankMove()
    {
           movementInputValue = Input.GetAxis("Vertical");
           Vector3 movement = transform.forward * movementInputValue * moveSpeed * Time.deltaTime;
           rb.MovePosition(rb.position + movement);
    }

    // ����̃��\�b�h
    void TankTurn()
    {
       
            turnInputValue = Input.GetAxis("Horizontal");
            float turn = turnInputValue * turnSpeed * Time.deltaTime;
            Quaternion turnRotation = Quaternion.Euler(0, turn, 0);
            rb.MoveRotation(rb.rotation * turnRotation);
        
    }
}