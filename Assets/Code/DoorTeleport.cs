using UnityEngine;
using UnityEngine.InputSystem;

public class DoorTeleport : MonoBehaviour
{
    public Transform teleportPoint;

    private bool canInteract = false;
    private GameObject player;
    public GameObject pressEText;

    void Start()
    {
        pressEText.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canInteract = true;
            player = collision.gameObject;

            pressEText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canInteract = false;
            player = null;

            pressEText.SetActive(false);
        }

    }

    void Update()
    {
        if (canInteract &&
     Keyboard.current.eKey.wasPressedThisFrame)
        {
            player.transform.position =
                teleportPoint.position;

            pressEText.SetActive(false);
        }
    }
}