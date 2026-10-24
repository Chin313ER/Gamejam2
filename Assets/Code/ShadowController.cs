using UnityEngine;
using UnityEngine.InputSystem;

public class ShadowController : MonoBehaviour
{
    public float speed = 5f;

    public bool isControlled = false;
    public float jumpForce = 8f;

    private int jumpCount = 0;
    public int maxJump = 2;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame
    && jumpCount < maxJump)
        {
            jumpCount++;

            rb.linearVelocity =
                new Vector2(rb.linearVelocity.x, 0);

            rb.AddForce(
                Vector2.up * jumpForce,
                ForceMode2D.Impulse
            );
        }
        if (!isControlled)
        {
            rb.linearVelocity =
                new Vector2(0, rb.linearVelocity.y);

            return;
        }

        float move = 0;

        if (Keyboard.current.aKey.isPressed)
            move = -1;

        if (Keyboard.current.dKey.isPressed)
            move = 1;

        rb.linearVelocity =
            new Vector2(
                move * speed,
                rb.linearVelocity.y
            );
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
        }
    }

}