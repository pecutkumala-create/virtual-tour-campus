using UnityEngine;
using Unity.XR.CoreUtils; // untuk XROrigin
using System.Collections;

public class ElevatorController : MonoBehaviour
{
    [Header("UI")]
    public GameObject floorSelectionPanel;

    [Header("Floor Targets")]
    public Transform[] floorTargets = new Transform[5];

    [Header("Player")]
    public XROrigin xrOrigin; // drag XR Origin (XR Rig) ke sini, GANTI dari Transform biasa

    [Header("Fade Transition")]
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 0.5f;

    bool isTransitioning = false;
    CharacterController charController;

    void Start()
    {
        floorSelectionPanel.SetActive(false);

        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.alpha = 0f;
            fadeCanvasGroup.blocksRaycasts = false;
        }

        if (xrOrigin != null)
            charController = xrOrigin.GetComponent<CharacterController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            floorSelectionPanel.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            floorSelectionPanel.SetActive(false);
    }

    public void GoToFloor(int floorIndex)
    {
        if (isTransitioning) return;
        if (floorIndex < 0 || floorIndex >= floorTargets.Length || floorTargets[floorIndex] == null)
        {
            Debug.LogWarning("[Elevator] Floor target belum di-set untuk index " + floorIndex);
            return;
        }

        floorSelectionPanel.SetActive(false);
        StartCoroutine(TeleportWithFade(floorTargets[floorIndex]));
    }

    IEnumerator TeleportWithFade(Transform target)
    {
        isTransitioning = true;
        yield return StartCoroutine(Fade(0f, 1f));

        if (charController != null)
            charController.enabled = false;

        // 1. Pindahkan kamera ke posisi target
        xrOrigin.MoveCameraToWorldLocation(target.position);

        // 2. Rotasi HANYA di sumbu Y, supaya arah hadap kamera user
        //    match dengan target.forward, tanpa ganggu 'up' rig
        Vector3 currentCameraForward = xrOrigin.Camera.transform.forward;
        Vector3 flatCurrent = Vector3.ProjectOnPlane(currentCameraForward, Vector3.up).normalized;
        Vector3 flatTarget = Vector3.ProjectOnPlane(target.forward, Vector3.up).normalized;
        float angle = Vector3.SignedAngle(flatCurrent, flatTarget, Vector3.up);

        xrOrigin.Origin.transform.RotateAround(xrOrigin.Camera.transform.position, Vector3.up, angle);

        if (charController != null)
            charController.enabled = true;

        Debug.Log("[Elevator] Player dipindah ke: " + target.position);

        yield return new WaitForSeconds(0.15f);
        yield return StartCoroutine(Fade(1f, 0f));

        isTransitioning = false;
    }

    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        if (fadeCanvasGroup == null) yield break;

        fadeCanvasGroup.blocksRaycasts = true;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, t);
            yield return null;
        }

        fadeCanvasGroup.alpha = endAlpha;
        if (endAlpha <= 0f)
            fadeCanvasGroup.blocksRaycasts = false;
    }
}