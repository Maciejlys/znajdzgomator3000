using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 10f;
    private CharacterController controller;
    private Vector3 moveDir = Vector3.zero;
    private float gravity = 20f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (controller.isGrounded)
        {
            moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDir = transform.TransformDirection(moveDir);

            moveDir *= speed;
        }
    }

    private void FixedUpdate()
    {
        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
    }
}
