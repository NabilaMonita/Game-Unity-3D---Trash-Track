using UnityEngine;

public class CameraFollowOpenMaps : MonoBehaviour {

    private GameObject player;
    private Vector3 offset;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - player.transform.position;
    }

    void LateUpdate() {
        Vector3 newPosition = player.transform.position + offset;
        transform.position = new Vector3(newPosition.x, transform.position.y, newPosition.z);
    }
}
