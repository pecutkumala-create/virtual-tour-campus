using UnityEngine;

public class MinimapCameraController : MonoBehaviour
{
    [Header("Target Setup")]
    public Transform playerTarget;

    [Header("Minimap Settings")]
    public float mapHeight = 80f;

    void LateUpdate()
    {
        if (playerTarget == null) return;

        // 1. Kamera atas tetap mengikuti posisi X dan Z player
        Vector3 newPosition = new Vector3(playerTarget.position.x, mapHeight, playerTarget.position.z);
        transform.position = newPosition;

        // 2. AKTIFKAN INI: Kamera ikut berputar di sumbu Y mengikuti rotasi pandangan player
        // Rotasi X tetap kuncian 90 derajat agar tetap melihat tegak lurus ke bawah
        transform.rotation = Quaternion.Euler(90f, playerTarget.eulerAngles.y, 0f);
    }
}