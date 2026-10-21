using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VRFadeSceneTransition : MonoBehaviour
{
    // Variabel global penyimpan ID pintu (Sama seperti 'spawnPoint variable' di video)
    public static int globalSpawnID = 0;

    [Header("UI Reference")]
    public CanvasGroup fadeCanvasGroup;

    [Header("Transition Settings")]
    public float fadeDuration = 1.0f;
    public string sceneName;

    [Header("Target Pendaratan")]
    [Tooltip("Isi dengan angka unik untuk pintu ini (Misal: 1 untuk Gedung A. Jangan pakai angka 0)")]
    public int targetSpawnID = 1;

    public bool teleportOnTriggerEnter = true;
    private bool isTransitioning = false;

    private void Start()
    {
        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.alpha = 1;
            StartCoroutine(Fade(1, 0));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (teleportOnTriggerEnter && !isTransitioning)
        {
            if (other.CompareTag("Player") || other.GetComponentInParent<Unity.XR.CoreUtils.XROrigin>() != null)
            {
                StartTransition();
            }
        }
    }

    public void StartTransition()
    {
        if (!isTransitioning)
        {
            StartCoroutine(TransitionRoutine());
        }
    }

    private IEnumerator TransitionRoutine()
    {
        isTransitioning = true;
        if (fadeCanvasGroup != null) yield return StartCoroutine(Fade(0, 1));

        if (!string.IsNullOrEmpty(sceneName))
        {
            // 👇 Ganti nilai variabel global sebelum Load Scene (seperti menit 00:04:23 di video)
            globalSpawnID = targetSpawnID;

            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            while (!operation.isDone) yield return null;
        }
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            yield return null;
        }
        fadeCanvasGroup.alpha = endAlpha;
    }
}