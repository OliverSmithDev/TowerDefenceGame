using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody rb;
    public Transform _camera;
    public GameObject Player;

    public static float moveSpeed = 6f;
    public float CurrentSpeed = 6f;
    public float jumpForce = 10f;
    public float SprintSpeed = 10f;
    public float CrouchSpeed = 4f;

    public LayerMask Ground;

    public bool isGrounded;
    public static bool CanMove = true;
    public Vector3 CrouchVector;
    public Vector3 UnCrouchVector;
    void Start()
    {
        CrouchVector = new Vector3(1f, 0.5f, 1f);
        UnCrouchVector = new Vector3(1f, 1f, 1f);
    }

    void Update()
    {
        //grounding
        isGrounded = Physics.CheckSphere(new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), 0.4f, Ground);


        //facing direction
        Debug.DrawLine(_camera.position, transform.forward * 2.5f);

        //moving
        float x = Input.GetAxisRaw("Horizontal") * CurrentSpeed;
        float y = Input.GetAxisRaw("Vertical") * CurrentSpeed;
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            CurrentSpeed = SprintSpeed;
        }
        else
        {
            CurrentSpeed = moveSpeed;
        }

        //jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);

        //setting movement
        if(CanMove == true)
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            Vector3 move = transform.right * x + transform.forward * y;

            rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);
        }
        else if(CanMove == false)
        {
            rb.constraints = RigidbodyConstraints.FreezePosition;
        }
        //Vector3 move = transform.right * x + transform.forward * y;

        //rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);

        if (Input.GetKeyDown(KeyCode.LeftControl)) // Crouch - half player size/ set speed so lower amount
        {
            Player.transform.localScale = CrouchVector;
            moveSpeed = CrouchSpeed;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl)) // Un crouch and set speed back to normal speed
        {
            Player.transform.localScale = UnCrouchVector;
            moveSpeed = 6f;
        }
    }
}
