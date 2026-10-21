using UnityEngine;

public class FixWebGLRender : MonoBehaviour
{
    private MeshRenderer[] allRenderers;

    void Start()
    {
        // Mengambil semua komponen Mesh Renderer yang ada di objek ini dan anaknya
        allRenderers = GetComponentsInChildren<MeshRenderer>();

        // Memaksa browser WebGL memperbarui status render objek di awal game
        ForceRenderUpdate();
    }

    void ForceRenderUpdate()
    {
        foreach (MeshRenderer renderer in allRenderers)
        {
            if (renderer != null)
            {
                // Trik WebGL: Matikan sebentar lalu nyalakan lagi untuk memicu render ulang di browser
                renderer.enabled = false;
                renderer.enabled = true;
            }
        }
    }
}
