using UnityEngine;
using UnityEngine.SceneManagement;

public class VRSceneTeleporter : MonoBehaviour
{
    [Header("Scene Settings")]
    [Tooltip("Nama scene tujuan yang ingin dibuka (pastikan sudah didaftarkan di Build Settings)")]
    public string sceneName;

    [Header("Trigger Settings")]
    [Tooltip("Jika dicentang, scene akan berpindah otomatis saat player menginjak plane ini.")]
    public bool teleportOnTriggerEnter = true;
    
    [Tooltip("Tag dari GameObject Player (biasanya 'Player' atau 'MainCamera')")]
    public string playerTag = "Player";

    // Fungsi ini dipanggil otomatis jika Player menginjak Plane (Trigger)
    private void OnTriggerEnter(Collider other)
    {
        if (teleportOnTriggerEnter)
        {
            // Cek apakah yang menginjak adalah Player berdasarkan Tag
            if (other.CompareTag(playerTag) || other.GetComponentInParent<Unity.XR.CoreUtils.XROrigin>() != null)
            {
                ChangeScene();
            }
        }
    }

    // Fungsi ini bisa dipanggil dari Event XR Simple Interactable (Interaksi Pointer/Ray)
    public void OnXRInteract()
    {
        ChangeScene();
    }

    private void ChangeScene()
{
    if (!string.IsNullOrEmpty(sceneName))
    {
        Debug.Log("Berpindah ke scene: " + sceneName);
        
        // PASTIKAN ditulis lengkap seperti di bawah ini:
        SceneManager.LoadSceneAsync(sceneName);
    }
    else
    {
        Debug.LogWarning("Nama scene belum diisi di Inspector!");
    }
}
}