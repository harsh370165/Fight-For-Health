using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody RigidBody;
    public Camera Camera;
    public float Xaxis = 0, Zaxis;
    public float Speed = 2, JumpForce = 10;
    public Animator PlayerAnimator;
    public bool IsGrounded = true;
    public Vector3 FollowCamera;

    void Update()
    {
        Xaxis += Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        Zaxis += Input.GetAxis("Vertical") * Speed * Time.deltaTime;
        RigidBody.AddForce(Xaxis,0,Zaxis,ForceMode.Impulse);
        if(Xaxis > 0 || Zaxis > 0 || Xaxis < 0 || Zaxis < 0)
        {
            PlayerAnimator.SetBool("run",true);
        }
        else
        {            
            PlayerAnimator.SetBool("run",false);
            PlayerAnimator.SetBool("jump",false);
        }
       if(IsGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            PlayerAnimator.SetBool("jump",true);
            RigidBody.AddForce(Vector3.up * JumpForce * Time.deltaTime,ForceMode.Impulse);
        }  
        Rotate();
        Xaxis = 0;
        Zaxis = 0;

        CameraMovement();
    }
    void CameraMovement()
    {
        Camera.transform.position = this.transform.position + FollowCamera;
        float CameraX = 0;
        float CameraY = 0;
        CameraX += Input.GetAxis("Mouse X");
        CameraY += Input.GetAxis("Mouse Y");
        
    }
    void Rotate()
    {
        Vector3 direction = RigidBody.velocity;
        direction.y = 0f;

        if (Xaxis > 0 || Xaxis < 0 || Zaxis > 0 || Zaxis < 0 && direction.sqrMagnitude > 0.1f)
            this.RigidBody.rotation = Quaternion.LookRotation(direction, Vector3.up);
        else
            RigidBody.angularVelocity = Vector3.zero;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {   
            IsGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGrounded = false;
            PlayerAnimator.SetBool("jump",true);
        }
    }
}