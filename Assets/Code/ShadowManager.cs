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

                sanityManager.UseShadow();
            }
            else
            {
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
                player.canMove = true;

                shadowScript.isControlled = false;

                cameraFollow.target = player.transform;
            }
        }
    }
}