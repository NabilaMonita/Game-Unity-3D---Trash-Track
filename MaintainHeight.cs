using UnityEngine;

public class MaintainHeight : MonoBehaviour
{
    public float desiredHeight = 70f;

    void Update()
    {
        Vector3 position = transform.position;
        position.y = desiredHeight;
        transform.position = position;
    }
}
