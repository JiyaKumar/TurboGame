using UnityEngine;

public class CarController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 100f;

    private string currentCommand = "";
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        Vector2 moveDirection = Vector2.zero;
        float rotation = 0f;

        switch (currentCommand.ToLower())
        {
            case "go":
            case "forward":
                moveDirection = transform.up;
                break;

            case "back":
            case "reverse":
                moveDirection = -transform.up;
                break;

            case "left":
                rotation = turnSpeed * Time.deltaTime;
                break;

            case "right":
                rotation = -turnSpeed * Time.deltaTime;
                break;

            case "stop":
                moveDirection = Vector2.zero;
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
                return;

            default:
                return;
        }

        // Apply movement
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);
        rb.MoveRotation(rb.rotation + rotation);
    }

    // This method will be called from your speech manager
    public void OnVoiceCommandReceived(string command)
    {
        currentCommand = command;
        Debug.Log("Voice Command Received: " + command);
    }
}
