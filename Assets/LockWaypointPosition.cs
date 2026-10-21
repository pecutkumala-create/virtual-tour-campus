using UnityEngine;

public class FreezeObject : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Membeku semua pergerakan posisi (X, Y, dan Z)
        rb.constraints = RigidbodyConstraints.FreezePosition;
    }
}
