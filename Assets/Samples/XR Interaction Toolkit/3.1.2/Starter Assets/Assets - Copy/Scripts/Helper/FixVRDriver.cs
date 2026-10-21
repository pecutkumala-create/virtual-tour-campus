using UnityEngine;
// Perbaikan: Namespace yang benar untuk XR Interaction Toolkit
using UnityEngine.XR.Interaction.Toolkit; 

public class FixVRDriver : MonoBehaviour
{
    // Menggunakan var tipe komponen yang tepat sesuai library Unity terbaru
    private CharacterControllerDriver driver;

    void Start()
    {
        driver = GetComponent<CharacterControllerDriver>();
        if (driver != null)
        {
            driver.enabled = false;
            // Aktifkan kembali setelah 0.1 detik agar posisi kamera stabil dulu
            Invoke("EnableDriver", 0.1f); 
        }
    }

    void EnableDriver()
    {
        if (driver != null)
        {
            driver.enabled = true;
        }
    }
}