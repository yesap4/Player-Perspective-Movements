
using UnityEngine;

public class PerspectiveController : MonoBehaviour
{
    public Transform playerCamera;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;

    [Header("Camera")]
    public float mouseSensitivity = 2f;
    public float cameraDistance = 4f;
    public float cameraHeight = 1.2f;
    public float eyeHeight = 0.6f;

    [Header("Character Visuals")]
    public GameObject playerVisuals;

    private CharacterController controller;

    private float verticalVelocity;
    private float cameraYaw;
    private float cameraPitch = 15f;

    private bool isFirstPerson = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        cameraYaw = transform.eulerAngles.y;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        UpdateView();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            isFirstPerson = !isFirstPerson;

            if (isFirstPerson)
            {
                cameraYaw = transform.eulerAngles.y;
                cameraPitch = 0f;
            }

            UpdateView();
        }

        LookAround();
        MovePlayer();
    }

    void LookAround()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
            return;

        float mouseX = Input.GetAxis("Mouse X")
                       * mouseSensitivity;

        float mouseY = Input.GetAxis("Mouse Y")
                       * mouseSensitivity;

        cameraYaw += mouseX;
        cameraPitch -= mouseY;

        cameraPitch = Mathf.Clamp(cameraPitch, -20f, 60f);

        if (isFirstPerson)
        {
            transform.rotation =
                Quaternion.Euler(0f, cameraYaw, 0f);
        }
    }

    void MovePlayer()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Quaternion yawRotation =
            Quaternion.Euler(0f, cameraYaw, 0f);

        Vector3 forward =
            yawRotation * Vector3.forward;

        Vector3 right =
            yawRotation * Vector3.right;

        Vector3 move = forward * z + right * x;
        move = Vector3.ClampMagnitude(move, 1f);

        if (!isFirstPerson && move.magnitude > 0.1f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(move);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        if (controller.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -2f;
        }

        if (controller.isGrounded &&
            Input.GetKeyDown(KeyCode.Space))
        {
            verticalVelocity = Mathf.Sqrt(
                jumpHeight * -2f * gravity
            );
        }

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 velocity = move * moveSpeed;
        velocity.y = verticalVelocity;

        controller.Move(velocity * Time.deltaTime);
    }

    void LateUpdate()
    {
        if (playerCamera == null)
            return;

        if (isFirstPerson)
        {
            Vector3 eyePosition =
                transform.position
                + Vector3.up * eyeHeight;

            playerCamera.position = eyePosition;

            playerCamera.rotation = Quaternion.Euler(
                cameraPitch, cameraYaw, 0f
            );
        }
        else
        {
            Quaternion cameraRotation =
                Quaternion.Euler(
                    cameraPitch, cameraYaw, 0f
                );

            Vector3 targetPosition =
                transform.position
                + Vector3.up * cameraHeight;

            Vector3 cameraPosition =
                targetPosition
                - cameraRotation * Vector3.forward
                * cameraDistance;

            playerCamera.position = cameraPosition;
            playerCamera.LookAt(targetPosition);
        }
    }

    void UpdateView()
    {
        if (playerVisuals != null)
        {
            playerVisuals.SetActive(!isFirstPerson);
        }
        else
        {
            Renderer playerRenderer = GetComponent<Renderer>();

            if (playerRenderer != null)
            {
                playerRenderer.enabled = !isFirstPerson;
            }
        }
    }
}
