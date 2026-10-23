using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 8f;

    public int maxJump = 2;
    int jumpCount = 0;

    private Vector3 originalScale;

    public bool canMove = true;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (!canMove)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            return;
        }

        float move = 0;

        if (Keyboard.current.aKey.isPressed)
        {
            move = -1;
        }

        if (Keyboard.current.dKey.isPressed)
        {
            move = 1;
        }

        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);

        // หันหน้า
        if (move > 0)
        {
            transform.localScale =
                new Vector3(
                    -Mathf.Abs(originalScale.x),
                    originalScale.y,
                    originalScale.z);
        }
        else if (move < 0)
        {
            transform.localScale =
                new Vector3(
                    Mathf.Abs(originalScale.x),
                    originalScale.y,
                    originalScale.z);
        }

        // กระโดด
        if (Keyboard.current.spaceKey.wasPressedThisFrame && jumpCount < maxJump)
        {
            jumpCount++;

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);

            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    // แตะพื้น = รีเซ็ตจำนวนกระโดด
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
        }
    }
}