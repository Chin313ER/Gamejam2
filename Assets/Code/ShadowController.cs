using UnityEngine;
using UnityEngine.InputSystem;

public class ShadowController : MonoBehaviour
{
    public float speed = 5f;

    public bool isControlled = false;
    public float jumpForce = 8f;

    private int jumpCount = 0;
    public int maxJump = 2;
    private Vector3 originalScale;
    Rigidbody2D rb;
    Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        originalScale = transform.localScale;
        
    }

    void Update()
    {
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

        anim.SetFloat("Speed", Mathf.Abs(move));
        if (move > 0)
        {
            // D = หันขวา
            transform.localScale = new Vector3(
                Mathf.Abs(originalScale.x),
                originalScale.y,
                originalScale.z
            );
        }
        else if (move < 0)
        {
            // A = หันซ้าย
            transform.localScale = new Vector3(
                -Mathf.Abs(originalScale.x),
                originalScale.y,
                originalScale.z
            );
        }

        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);

        // กระโดดเฉพาะตอนคุม Shadow
        if (Keyboard.current.spaceKey.wasPressedThisFrame && jumpCount < maxJump)
        {
            jumpCount++;

            anim.SetBool("jump", true);

            rb.linearVelocity =
                new Vector2(rb.linearVelocity.x, 0);

            rb.AddForce(
                Vector2.up * jumpForce,
                ForceMode2D.Impulse
            );
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;

            anim.SetBool("jump", false);
        }
    }

}