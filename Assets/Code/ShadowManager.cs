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

                Collider2D shadowCollider =
                    currentShadow.GetComponent<Collider2D>();

                GameObject[] walls =
                    GameObject.FindGameObjectsWithTag("Wall");

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
                // กลับมาคุม Player ก่อนลบ Shadow

                cameraFollow.target = player.transform;

                Camera.main.transform.position =
                    new Vector3(
                        player.transform.position.x,
                        player.transform.position.y,
                        Camera.main.transform.position.z
                    );

                player.canMove = true;

                if (shadowScript != null)
                {
                    shadowScript.isControlled = false;
                }

                Destroy(currentShadow);

                currentShadow = null;

                sanityManager.ReturnShadow();
            }
        }

        // R สลับการควบคุม

        if (Keyboard.current.rKey.wasPressedThisFrame
            && currentShadow != null)
        {
            if (player.canMove)
            {
                // คุม Shadow

                player.canMove = false;

                shadowScript.isControlled = true;

                cameraFollow.target =
                    currentShadow.transform;
            }
            else
            {
                // กลับมาคุม Player

                player.canMove = true;

                shadowScript.isControlled = false;

                cameraFollow.target =
                    player.transform;
            }
        }
    }
}