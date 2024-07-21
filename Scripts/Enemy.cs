using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float kecepatan = 5f;
    public float jangkauanDeteksi = 10f;
    private Transform pemain;
    private Rigidbody rb;

    void Start()
    {
        pemain = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, pemain.position) < jangkauanDeteksi)
        {
            Vector3 arah = (pemain.position - transform.position).normalized;
            rb.velocity = new Vector3(arah.x * kecepatan, rb.velocity.y, arah.z * kecepatan);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }


}
