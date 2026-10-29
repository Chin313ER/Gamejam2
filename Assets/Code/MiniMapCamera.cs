using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    public Transform player;

    void LateUpdate()
    {
        transform.position = new Vector3(
            player.position.x,
            player.position.y,
            transform.position.z
        );
    }
}