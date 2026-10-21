using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GedungNameNotifier : MonoBehaviour
{
    [Header("UI Reference")]
    [Tooltip("Tarik GedungNameCanvas yang memiliki Canvas Group ke sini")]
    public CanvasGroup canvasGroup;

    [Header("Timing Settings")]
    [Tooltip("Durasi teks memunculkan diri (detik)")]
    public float fadeInDuration = 1.5f;
    
    [Tooltip("Berapa lama teks diam menetap di layar sebelum hilang (detik)")]
    public float displayDuration = 3.0f;
    
    [Tooltip("Durasi teks menghilang (detik)")]
    public float fadeOutDuration = 1.5f;

    void Start()
    {
        if (canvasGroup != null)
        {
            // Pastikan di awal alpha-nya 0 (tidak terlihat)
            canvasGroup.alpha = 0f;
            
            // Jalankan urutan kemunculan teks
            StartCoroutine(ShowGedungNameRoutine());
        }
        else
        {
            Debug.LogWarning("Canvas Group untuk nama gedung belum dimasukkan di Inspector!");
        }
    }

    private IEnumerator ShowGedungNameRoutine()
    {
        // 1. FADE IN (Teks perlahan muncul)
        float elapsedTime = 0f;
        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);
            yield return null;
        }
        canvasGroup.alpha = 1f;

        // 2. DISPLAY TIME (Teks diam di layar selama beberapa detik)
        yield return new WaitForSeconds(displayDuration);

        // 3. FADE OUT (Teks perlahan menghilang)
        elapsedTime = 0f;
        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutDuration);
            yield return null;
        }
        canvasGroup.alpha = 0f;
    }
}