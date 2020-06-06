using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 movement;
    Quaternion Rotation = Quaternion.identity;
    Animator animator;
    Rigidbody playerRigidbody;
    // Start is called before the first frame update
    
    // 1초간 회전 각도
    public float turnSpeed = 20f;

    void Start()
    {
      animator = GetComponent<Animator>();
      playerRigidbody = GetComponent<Rigidbody>();
    }

    private void OnAnimatorMove()
    {
        playerRigidbody.MovePosition(playerRigidbody.position + movement * animator.deltaPosition.magnitude);
        playerRigidbody.MoveRotation(Rotation);
    }


    // Update is called once per frame
    void Update()
    {
    
        float horizontal = Input.GetAxis("Horizontal"); 
        float vertical = Input.GetAxis("Vertical"); 
        
        movement.Set(horizontal, 0f, vertical);
        movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput= !Mathf.Approximately(vertical, 0f);

        bool IsRunning = hasHorizontalInput || hasVerticalInput;

        animator.SetBool("IsRunning", IsRunning);

        //회전관련
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, movement, turnSpeed * Time.deltaTime, 0f);
        Rotation = Quaternion.LookRotation(desiredForward);
    }
}
