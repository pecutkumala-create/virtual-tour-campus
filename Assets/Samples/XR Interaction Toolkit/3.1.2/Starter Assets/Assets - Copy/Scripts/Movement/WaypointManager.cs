using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    // Variabel static ini nilainya bertahan antar scene
    public static string targetSpawnId;

    [Header("Player Setup")]
    [Tooltip("Masukkan XR Origin (atau objek player utama) ke sini")]
    public Transform xrOrigin;

    // Struct untuk menyimpan data Waypoint di Inspector
    [System.Serializable]
    public struct Waypoint
    {
        public string waypointId; // Harus sama persis dengan 'targetWaypointId' di scene sebelumnya
        public Transform waypointTransform; // Posisi fisik (Empty GameObject) di scene
    }

    [Header("Waypoints List")]
    public Waypoint[] waypoints;

    private void Start()
    {
        // Jika tidak ada pesan tujuan atau XR Origin kosong, batalkan
        if (string.IsNullOrEmpty(targetSpawnId) || xrOrigin == null)
        {
            return;
        }

        // Cari waypoint yang ID-nya cocok dengan pesan dari scene sebelumnya
        foreach (Waypoint wp in waypoints)
        {
            if (wp.waypointId == targetSpawnId)
            {
                // Pindahkan XR Origin ke posisi dan rotasi waypoint tersebut
                xrOrigin.position = wp.waypointTransform.position;
                xrOrigin.rotation = wp.waypointTransform.rotation;

                Debug.Log("Berhasil memindahkan player ke: " + targetSpawnId);

                // Reset nilai agar tidak terbawa jika pindah scene secara normal nanti
                targetSpawnId = "";
                break;
            }
        }
    }
}