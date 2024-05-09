using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public float accelerationTime = 0.1f;
    // Move in direction?

    [Header("Movement Controls")]
    public string movementXAxis = "Horizontal";
    public string movementYAxis = "Vertical";
    public float movementDeadzone = 0.1f;
    public bool movementUseRawInput = true;

    [Header("Look Controls")]
    public bool lookUsingMouse = true;
    public string lookXAxis = "Look Horizontal";
    public string lookYAxis = "Look Vertical";
    public float lookDeadzone = 0.1f;
    public bool lookUseRawInput = true;

    [Header("Debug")]
    public bool debugVelocity = false;
    public Color debugVelocityColor = Color.green;
    public bool debugAcceleration = false;
    public Color debugAccelerationColor = Color.red;
    public bool debugLookDirection = false;
    public Color debugLookDirectionColor = Color.white;


    [HideInInspector]
    public Vector2 velocity = Vector2.zero;
    [HideInInspector]
    public Vector2 acceleration = Vector2.zero;
    [HideInInspector]
    public Vector2 lookDirection = Vector2.zero;


    private bool isLookingWithMouse = true;

    private Rigidbody2D rb2d;


    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Move();
        Look();
        DrawDebugLines();
    }

    private void FixedUpdate()
    {
        // rb2d.velocity = velocity;
    }


    private void Move()
    {
        Vector2 input = GetMovementInput();
        Vector2 targetVelocity = input.normalized * speed;
        velocity = Vector2.SmoothDamp(velocity, targetVelocity, ref acceleration, accelerationTime);
    }

    private void Look()
    {
        if (lookUsingMouse && IsMovingMouse())
        {
            lookDirection = MouseDirection();
            isLookingWithMouse = true;
            return;
        }
        if (!isLookingWithMouse || IsMovingNonMouse())
        {
            lookDirection = NonMouseDirection();
            isLookingWithMouse = false;
        }
    }

    private bool IsMovingMouse()
    {
        return Input.mousePositionDelta != Vector3.zero;
    }

    private bool IsMovingNonMouse()
    {
        return GetLookInput() != Vector2.zero;
    }

    private Vector2 MouseDirection()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    private Vector2 GetMovementInput()
    {
        Vector2 input = new(
            GetInputFromAxis(movementXAxis, movementUseRawInput),
            GetInputFromAxis(movementYAxis, movementUseRawInput)
        );
        return ApplyDeadzone(input, movementDeadzone);
    }

    private Vector2 GetLookInput()
    {
        Vector2 input = new(
            GetInputFromAxis(lookXAxis, lookUseRawInput),
            GetInputFromAxis(lookYAxis, lookUseRawInput)
        );
        return ApplyDeadzone(input, lookDeadzone);
    }

    private float GetInputFromAxis(string axisName, bool useRawInput)
    {
        return useRawInput ? Input.GetAxisRaw(axisName) : Input.GetAxis(axisName);
    }

    private Vector2 ApplyDeadzone(Vector2 vector, float deadzone)
    {
        if (Mathf.Abs(vector.x) < deadzone) vector.x = 0f;
        if (Mathf.Abs(vector.y) < deadzone) vector.y = 0f;
        return vector;
    }



    // Oldish
    private Vector2 NonMouseDirection()
    {
        Rect pixelsRect = Camera.main.pixelRect;
        float minHalf = Mathf.Min(pixelsRect.width, pixelsRect.height) / 2f;
        Vector2 pixelPosition = pixelsRect.center + GetLookInput() * minHalf;
        return Camera.main.ScreenToWorldPoint(pixelPosition) - transform.position;
    }


    private void DrawDebugLines()
    {
        if (debugVelocity)
        {
            DrawLineOffset(transform.position, velocity, debugVelocityColor);
            Debug.Log("Velocity: " + velocity);
        }
        if (debugAcceleration)
        {
            DrawLineOffset(transform.position, acceleration, debugAccelerationColor);
            Debug.Log("Acceleration: " + acceleration);
        }
        if (debugLookDirection)
        {
            DrawLineOffset(transform.position, lookDirection, debugLookDirectionColor);
            Debug.Log("Look Direction: " + lookDirection);
        }
    }

    private void DrawLineOffset(Vector2 position, Vector2 offset, Color color)
    {
        Debug.DrawLine(position, position + offset, color);
    }
}
