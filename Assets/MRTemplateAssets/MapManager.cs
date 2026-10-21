using UnityEngine;

public class MapManager : MonoBehaviour
{
    // Tarik GameObject UI Minimap kamu yang sekarang ke sini
    public GameObject minimapUI;

    // Tarik GameObject UI Maximap yang baru dibuat ke sini
    public GameObject maximapUI;

    // Fungsi untuk membuka map besar
    public void OpenMaximap()
    {
        maximapUI.SetActive(true);   // Munculkan map besar
        minimapUI.SetActive(false);  // Sembunyikan minimap di pojok
    }

    // Fungsi untuk menutup map besar dan kembali ke game
    public void CloseMaximap()
    {
        maximapUI.SetActive(false);  // Sembunyikan map besar
        minimapUI.SetActive(true);   // Munculkan kembali minimap
    }
}