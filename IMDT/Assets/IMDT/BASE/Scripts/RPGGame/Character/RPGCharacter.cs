using UnityEngine;
using System.Collections;

public class RPGCharacter : MonoBehaviour
{
    public RPGController controller;
    public Rigidbody rigidbody;
    public Animator animator;
    public Vector3 moveDirection;
    public float moveSpeed = 0;
    public bool isMoving = false;
    public Vector3 lastDirection;
    public float coefSpeed = 0.1f;

    // Use this for initialization
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        animator = this.GetComponentInChildren<Animator>();
    }

    public void SetController(RPGController _controller)
    {
        if(this.controller != null)
        {
            this.controller.input.UnBindJoystick("MoveInput", OnMoveInput);
            this.controller.input.UnBindButton("AttackInput", null, OnAttackInput, OnAttackFinish);
        }

        this.controller = _controller;

        if (this.controller != null)
        {
            this.controller.input.BindJoystick("MoveInput", OnMoveInput);
            this.controller.input.BindButton("AttackInput", null, OnAttackInput, OnAttackFinish);
        }
    }

    private void OnMoveInput(Vector2 value)
    {
        isMoving = value.x != 0 || value.y != 0;

        Vector3 forward = this.controller.camera.transform.TransformDirection(Vector3.forward);
        forward.y = 0;
        forward = forward.normalized;

        Vector3 right = new Vector3(forward.z, 0, -forward.x);
        moveDirection = forward * value.y + right * value.x;
        moveSpeed = Vector2.ClampMagnitude(value, 1f).magnitude;

        animator.SetFloat("MoveSpeed", moveSpeed);
    }

    private void OnAttackInput()
    {
        animator.SetBool("Attack", true);
    }

    private void OnAttackFinish()
    {
        animator.SetBool("Attack", false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        UpdateRotating();
        UpdateMoving();
    }


    private void UpdateRotating()
    {
        // 方向插值
        if (isMoving && moveDirection != Vector3.zero)
        {
            rigidbody.isKinematic = false;
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            Quaternion newRotation = Quaternion.Slerp(rigidbody.rotation, targetRotation, 0.06f);
            rigidbody.MoveRotation(newRotation);
            lastDirection = moveDirection;
        }
        else
        {
            rigidbody.isKinematic = true;
        }
    }

    private void UpdateMoving()
    {
        if (isMoving)
        {
            rigidbody.isKinematic = false;
            Vector3 newPosition = rigidbody.position + moveDirection * moveSpeed * coefSpeed;
            newPosition.y = 0;
            rigidbody.MovePosition(newPosition);
        }
        else
        {
            rigidbody.isKinematic = true;
        }
    }
}
