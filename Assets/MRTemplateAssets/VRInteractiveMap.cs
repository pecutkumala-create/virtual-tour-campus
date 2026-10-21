using UnityEngine;

public class VRInteractiveMap : MonoBehaviour
{
    [Header("Scale Settings")]
    public Vector3 normalScale = new Vector3(0.4f, 0.4f, 1f);
    public Vector3 enlargedScale = new Vector3(1f, 1f, 1f);
    public float transitionSpeed = 8f;

    [Header("Position Settings (Optional)")]
    [Tooltip("Posisi saat peta ukuran normal")]
    public Vector3 normalLocalPos;
    [Tooltip("Posisi saat peta membesar (misal: dibuat agak maju/mendekat ke mata)")]
    public Vector3 enlargedLocalPos = new Vector3(0f, 0f, 0.8f);

    private Vector3 targetScale;
    private Vector3 targetLocalPos;
    private bool isEnlarged = false;

    void Start()
    {
        // Set posisi awal sesuai posisi objek saat ini
        normalLocalPos = transform.localPosition;

        targetScale = normalScale;
        targetLocalPos = normalLocalPos;
        transform.localScale = normalScale;
    }

    void Update()
    {
        // Transisi membesar/mengecil secara halus
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * transitionSpeed);
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetLocalPos, Time.deltaTime * transitionSpeed);
    }

    // Fungsi ini akan dipanggil saat Peta Utama diklik
    public void ToggleMapSize()
    {
        isEnlarged = !isEnlarged;

        if (isEnlarged)
        {
            targetScale = enlargedScale;
            targetLocalPos = enlargedLocalPos;
        }
        else
        {
            targetScale = normalScale;
            targetLocalPos = normalLocalPos;
        }
    }
}