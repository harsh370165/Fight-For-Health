using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    public Vector3 Direction;
    public InputAction Action;
    public PlayerInput Input;
    public Rigidbody Rigidbody;
    public float speed = 10;
    public Animator PlayerAnimation;
    public Camera Camera;
    // Start is called before the first frame update
    void Awake()
    {
        Input = new PlayerInput();
        Action = new InputAction();
    }
    void OnEnable()
    {
        Action = Input.Player.Move;
        Input.Player.Enable();
    }
    void OnDisable()
    {
        Input.Player.Disable();
    }
    void FixedUpdate()
    {
        Direction += Action.ReadValue<Vector2>().y * speed * CameraForward(Camera);
        Direction += Action.ReadValue<Vector2>().x * speed * CameraRight(Camera);
        Rigidbody.AddForce(Direction,ForceMode.Impulse);
        Direction = Vector3.zero;
        LookAt();
        PlayerAnimation.SetBool("run",false);
        if(Input.Player.Move.IsPressed())
        {
            PlayerAnimation.SetBool("run",true);
        }
    }
    public Vector3 CameraRight(Camera Camera)
    {
        Vector3 right = Camera.transform.right;
        right.y = 0;
        return right.normalized;
    }
    public Vector3 CameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }
    void LookAt()
    {
        Vector3 Rotation = Rigidbody.velocity;
        Rotation.y = 0;
        if(Action.ReadValue<Vector2>().sqrMagnitude > 0.1f && Rotation.sqrMagnitude > 0.1f)
        {
            Quaternion RotationAround = Quaternion.LookRotation(Rotation,Vector3.up);
            Rigidbody.rotation = Quaternion.Slerp(Rigidbody.rotation, RotationAround, 90);
        }
    }
}
