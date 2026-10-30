using UnityEngine;

public class PickUp : MonoBehaviour
{
    public AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(
                pickupSound,
                transform.position
            );

            Destroy(gameObject);
        }
    }
}