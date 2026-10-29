using UnityEngine;
using UnityEngine.InputSystem;

public class ShadowManager : MonoBehaviour
{
    public GameObject shadowPrefab;

    public Player player;
    public CameraFollow cameraFollow;
    public SanityManager sanityManager;

    GameObject currentShadow;

    ShadowController shadowScript;

    void Update()
    {
        // Q เปิด/ปิด Shadow

        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            if (currentShadow == null)
            {
                currentShadow =
                    Instantiate(
                        shadowPrefab,
                        player.transform.position +
                        Vector3.right * 2,
                        Quaternion.identity
                    );

                shadowScript =
                    currentShadow.GetComponent<ShadowController>();
                Collider2D shadowCollider = currentShadow.GetComponent<Collider2D>();

                GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");

                foreach (GameObject wall in walls)
                {
                    Physics2D.IgnoreCollision(
                        shadowCollider,
                        wall.GetComponent<Collider2D>(),
                        true
                    );
                }

                sanityManager.UseShadow();
            }
            else
            {
                cameraFollow.target = player.transform;

                Camera.main.transform.position =
                    new Vector3(
                        player.transform.position.x,
                        player.transform.position.y,
                        Camera.main.transform.position.z
                    );
                Destroy(currentShadow);

                currentShadow = null;

                player.canMove = true;

                sanityManager.ReturnShadow();
            }
        }

        // R สลับควบคุม

        if (Keyboard.current.rKey.wasPressedThisFrame
            && currentShadow != null)
        {
            if (player.canMove)
            {
                player.canMove = false;

                shadowScript.isControlled = true;

                cameraFollow.target = currentShadow.transform;
            }
            else
            {
                cameraFollow.target = player.transform;

                player.canMove = true;

                Destroy(currentShadow);

                currentShadow = null;

                sanityManager.ReturnShadow();
            }

        }
    }
}