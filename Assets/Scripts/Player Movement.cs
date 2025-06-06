using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    [SerializeField] float moveSpeed = 6F;
    [SerializeField] float jumpSpeed = 6F;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }
    void Update()
    {
        run();
        flipSprite();
    }

    private void flipSprite()
    {
        bool playerHasSpeed = Mathf.Abs(myRigidBody.linearVelocity.x) > Mathf.Epsilon;
        if (playerHasSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.linearVelocity.x), 1f);
        }
        //Mathf.Sign(myRigidBody.linearVelocity.x) returns 1 if velocity is +ve (moving right),-1 if velocity is -ve
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            myRigidBody.linearVelocity += new Vector2(0f, jumpSpeed);
            Debug.Log("Jump");
        }
    }

    private void run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, myRigidBody.linearVelocity.y);
        myRigidBody.linearVelocity = playerVelocity;
        bool playerHasSpeed = Mathf.Abs(myRigidBody.linearVelocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasSpeed);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log("blahh");
    }
}
