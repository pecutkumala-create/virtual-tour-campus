using UnityEngine;

public class LockIconRotation : MonoBehaviour
{
    void LateUpdate()
    {
        if (transform.parent == null) return;

        // 1. KUNCI POSISI GLOBAL (WORLD SPACE)
        // Kita paksa posisi horizontal (X dan Z) DOTY TEPAT sama dengan posisi kamera di dunia game.
        // Tapi posisi vertikal (Y) selalu ditambah secara absolut ke atas langit, tidak peduli kamera miring.
        float targetGlobalY = transform.parent.position.y + 30.089f;
        transform.position = new Vector3(transform.parent.position.x, targetGlobalY, transform.parent.position.z);

        // 2. KUNCI ROTASI GLOBAL (Menggunakan rumus Anda yang sudah berhasil)
        transform.rotation = Quaternion.Euler(-90f, transform.parent.eulerAngles.y, 90f);
    }
}
