using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class VRTeleportManager : MonoBehaviour
{
    [Header("Player Setup")]
    [Tooltip("Masukkan objek induk XR Origin / XR Rig yang berpindah tempat")]
    public Transform xrOrigin;

    [Header("Waypoints List")]
    [Tooltip("Masukkan objek-objek Waypoint 3D sesuai urutan Dropdown (mulai dari indeks 1)")]
    public List<Transform> waypoints;

    private TMP_Dropdown dropdown;

    void Start()
    {
        // Otomatis mengambil komponen Dropdown yang ada di objek ini
        dropdown = GetComponent<TMP_Dropdown>();

        if (dropdown != null)
        {
            // Menghubungkan fungsi Teleport saat nilai dropdown berubah
            dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        }
    }

    public void OnDropdownValueChanged(int index)
    {
        // Indeks 0 adalah "-- Pilih Lokasi --", jadi kita abaikan agar tidak teleport
        if (index == 0) return;

        // Mencari indeks list waypoint (index - 1 karena list dimulai dari 0 sedangkan dropdown dari 1)
        int waypointIndex = index - 1;

        // Validasi apakah waypoint tersedia di dalam list
        if (waypointIndex >= 0 && waypointIndex < waypoints.Count)
        {
            Transform targetWaypoint = waypoints[waypointIndex];

            if (targetWaypoint != null && xrOrigin != null)
            {
                // Eksekusi Teleportasi: Pindahkan posisi dan rotasi XR Origin ke Waypoint target
                xrOrigin.position = targetWaypoint.position;

                // Opsional: Ikuti rotasi hadap dari objek waypoint tersebut
                xrOrigin.rotation = targetWaypoint.rotation;

                Debug.Log("Teleport berhasil ke: " + targetWaypoint.name);

                // Kembalikan tampilan dropdown ke opsi "-- Pilih Lokasi --" setelah teleport selesai
                dropdown.value = 0;
            }
        }
        else
        {
            Debug.LogWarning("Waypoint untuk indeks ini belum dimasukkan ke dalam list script!");
        }
    }
}