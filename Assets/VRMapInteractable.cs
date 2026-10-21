using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(MeshCollider))]
public class VRMapInteractable : MonoBehaviour
{
    [Header("Referensi Komponen Map")]
    [Tooltip("Masukkan Kamera Top-Down yang merender map ini")]
    public Camera minimapCamera;

    [Tooltip("Pilih layer 'WaypointLayer' yang sudah dibuat")]
    public LayerMask waypointLayer;

    // Fungsi ini akan dipanggil saat pemain menekan pelatuk (trigger) ke arah map
    public void OnMapActivated(ActivateEventArgs args)
    {
        // 1. Cek apakah yang menembak adalah laser dari controller (XR Ray Interactor)
        if (args.interactorObject is UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor rayInteractor)
        {
            // 2. Ambil titik pasti di mana ujung laser mengenai Quad Maximap
            if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
            {
                // 3. Ambil koordinat UV (koordinat gambar) dari titik tersebut
                // Wajib pakai Mesh Collider agar hit.textureCoord tidak bernilai (0,0)
                Vector2 hitUV = hit.textureCoord;

                // 4. Ubah titik gambar tersebut menjadi koordinat layar untuk Kamera Minimap
                Vector3 viewportClickPos = new Vector3(hitUV.x, hitUV.y, 0);
                Ray minimapRay = minimapCamera.ViewportPointToRay(viewportClickPos);

                // 5. Kamera Minimap menembakkan raycast lurus ke bawah untuk mencari Waypoint
                if (Physics.Raycast(minimapRay, out RaycastHit mapHit, Mathf.Infinity, waypointLayer))
                {
                    // Berhasil! Raycast mengenai Waypoint di dunia 3D
                    Debug.Log("🎯 Waypoint terpilih: " + mapHit.collider.gameObject.name);

                    // TULIS LOGIC KAMU DI SINI
                    // Contoh: Teleportasi pemain ke lokasi waypoint
                    // Transform playerRig = ...
                    // playerRig.position = mapHit.transform.position;
                }
                else
                {
                    Debug.Log("Melenceng, tidak ada waypoint di titik peta tersebut.");
                }
            }
        }
    }
}