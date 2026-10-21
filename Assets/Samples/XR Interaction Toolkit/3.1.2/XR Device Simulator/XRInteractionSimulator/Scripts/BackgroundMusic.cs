using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic instance;

    void Awake()
    {
        // Mencegah musik mendobel jika kita kembali lagi ke scene pertama
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        // Perintah keramat agar musik terus lanjut saat pindah scene
        DontDestroyOnLoad(gameObject);
    }
}
