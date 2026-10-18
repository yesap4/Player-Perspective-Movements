using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 4f;
    public float JumpHeight = 3f;
    public float gravity = -20;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isOnGround;

    Vector3 velocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Update is called once per frame
    void Update()
    {

        isOnGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isOnGround && velocity.y < 0)
        {
            velocity.y = -2f;
        } 

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump")&& isOnGround )
        {
            velocity.y = Mathf.Sqrt(JumpHeight * -2f * gravity);
        };


        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);


    }
}
