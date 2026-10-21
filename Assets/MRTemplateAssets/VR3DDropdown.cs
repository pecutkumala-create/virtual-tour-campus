using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class VR3DDropdown : MonoBehaviour
{
    [Header("Dropdown Elements")]
    public GameObject daftarPilihanGroup;

    private XRSimpleInteractable simpleInteractable;
    private bool isMenuOpen = false;

    void Start()
    {
        // Pastikan di awal game, menu tertutup
        if (daftarPilihanGroup != null)
            daftarPilihanGroup.SetActive(false);

        simpleInteractable = GetComponent<XRSimpleInteractable>();

        if (simpleInteractable != null)
        {
            // Menggunakan selectEntered (Aksi klik Trigger Controller)
            simpleInteractable.selectEntered.AddListener(x => ToggleMenuViaClick());
        }
        else
        {
            Debug.LogError("Waduh, komponen XR Simple Interactable belum dipasang di objek ini!");
        }
    }

    private void ToggleMenuViaClick()
    {
        isMenuOpen = !isMenuOpen;
        Debug.Log("Tombol Header Berhasil Diklik! Status Menu: " + isMenuOpen);

        if (daftarPilihanGroup != null)
        {
            daftarPilihanGroup.SetActive(isMenuOpen);
        }
    }

    // Fungsi pembantu untuk menutup menu dari luar (bisa dipanggil oleh tombol opsi)
    public void ForceCloseMenu()
    {
        isMenuOpen = false;
        if (daftarPilihanGroup != null)
            daftarPilihanGroup.SetActive(false);
    }
}