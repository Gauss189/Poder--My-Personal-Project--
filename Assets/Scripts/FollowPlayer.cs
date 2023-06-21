using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private float cameraHeight = 12f;
    private float cameraDistance = -6f;

    private void Update()
    {
        Vector3 pos = player.transform.position;
        pos.y += cameraHeight;
        pos.z += cameraDistance;
        transform.position = pos;
    }
}
