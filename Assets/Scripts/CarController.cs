using UnityEngine;

public class CarController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float boostedSpeed = 10f;
    public float turnSpeed = 100f;

    private Rigidbody2D rb;
    private float currentSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = moveSpeed;
    }

    void Update()
    {
        Vector2 moveDir = transform.up;
        rb.MovePosition(rb.position + moveDir * currentSpeed * Time.deltaTime);
    }

    public void BoostSpeed()
    {
        currentSpeed = boostedSpeed;
    }
    
    public void ResetSpeed()
{
    currentSpeed = moveSpeed;
}

    public void SlowDown()
    {
        currentSpeed = moveSpeed / 2f;
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed * 10f; // Scale it for UI
    }
}
