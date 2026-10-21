using UnityEngine;

public class VRMapWaypoint : MonoBehaviour
{
    [Header("Teleport Targets")]
    [Tooltip("Masukkan objek induk XR Origin / XR Rig")]
    public Transform xrOrigin;

    [Header("Destination")]
    [Tooltip("Masukkan objek kosong (Empty GameObject) tujuan di dunia nyata")]
    public Transform realWorldDestination;

    [Header("Map Reference")]
    [Tooltip("Masukkan objek Papan Peta Utama agar otomatis mengecil setelah teleport")]
    public VRInteractiveMap interactiveMap;

    // Fungsi ini dipanggil saat objek pin diklik
    public void TeleportToDestination()
    {
        if (xrOrigin != null && realWorldDestination != null)
        {
            // Eksekusi pemindahan player ke lokasi nyata
            xrOrigin.position = realWorldDestination.position;
            xrOrigin.rotation = realWorldDestination.rotation;

            // Otomatis mengecilkan kembali peta setelah sukses pindah tempat
            if (interactiveMap != null)
            {
                interactiveMap.ToggleMapSize();
            }
        }
        else
        {
            Debug.LogWarning("Target Teleport atau XR Origin belum diisi di Pin ini!");
        }
    }
}