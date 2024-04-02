using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forceMagnitude;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float rotateionSpeed;

    private Rigidbody rb;
    private Camera mainCamera;

    private Vector3 movementDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        ProccessInput();

        RotateToFacelVelocity();

        KeepPlayerOnScreen();
    }

    private void FixedUpdate()
    {
        if(movementDirection == Vector3.zero)
            return;

        rb.AddForce(movementDirection * forceMagnitude,ForceMode.Force);

        rb.velocity = Vector3.ClampMagnitude(rb.velocity,maxVelocity);
    }

    private void ProccessInput()
    {
        if(Touchscreen.current.primaryTouch.isInProgress)
        {
            Vector3 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector3 wolrdPosition = mainCamera.ScreenToWorldPoint(touchPosition);

            movementDirection = transform.position - wolrdPosition;
            movementDirection.z = 0;
            movementDirection.Normalize();
        }
        else
        {
            movementDirection = Vector3.zero;
        }
    }

    private void KeepPlayerOnScreen()
    {
        Vector3 newPosition = transform.position;

        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

        if(viewportPosition.x > 1)
        {
            newPosition.x = -newPosition.x + 0.1f;
        }
        else if(viewportPosition.x < 0)
        {
            newPosition.x = -newPosition.x - 0.1f;
        }

        if(viewportPosition.y > 1)
        {
            newPosition.y = -newPosition.y + 0.1f;
        }
        else if(viewportPosition.y < 0)
        {
            newPosition.y = -newPosition.y - 0.1f;
        }

        transform.position = newPosition;
    }

    private void RotateToFacelVelocity()
    {
        if(rb.velocity == Vector3.zero)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(rb.velocity,Vector3.back);

        transform.rotation = Quaternion.Lerp(transform.rotation,targetRotation,rotateionSpeed * Time.deltaTime);
    }
}