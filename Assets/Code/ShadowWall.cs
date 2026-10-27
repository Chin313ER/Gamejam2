using UnityEngine;

public class ShadowWall : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Shadow"))
        {
            Physics2D.IgnoreCollision(
                collision.collider,
                GetComponent<Collider2D>(),
                true
            );
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Shadow"))
        {
            Physics2D.IgnoreCollision(
                collision.collider,
                GetComponent<Collider2D>(),
                false
            );
        }
    }
}
