using System.Collections;
using UnityEngine;

public class VRSpawnManager : MonoBehaviour
{
    private IEnumerator Start()
    {
        // Jeda sebentar agar komponen VR selesai inisialisasi
        yield return new WaitForSeconds(0.1f);

        // Angka 0 artinya player baru main game (default). Jangan dipindahkan.
        if (VRFadeSceneTransition.globalSpawnID == 0)
        {
            yield break;
        }

        // Kumpulkan semua titik pendaratan di scene ini
        VRSpawnPoint[] semuaTitikSpawn = FindObjectsOfType<VRSpawnPoint>();

        foreach (VRSpawnPoint titik in semuaTitikSpawn)
        {
            // Pengecekan Kondisi: Apakah ID titik ini sama dengan ID kiriman dari pintu sebelumnya?
            if (titik.spawnID == VRFadeSceneTransition.globalSpawnID)
            {
                // Matikan Character Controller sementara
                CharacterController cc = GetComponent<CharacterController>();
                if (cc != null) cc.enabled = false;

                // Pindahkan XR Rig
                transform.position = titik.transform.position;
                transform.rotation = titik.transform.rotation;

                // Nyalakan kembali
                if (cc != null) cc.enabled = true;

                Debug.Log("Sukses mendarat di titik ID: " + titik.spawnID);
                break;
            }
        }

        // Reset variabel ke 0 agar jika player jalan-jalan, dia tidak terus-terusan kena efek teleport
        VRFadeSceneTransition.globalSpawnID = 0;
    }
}