using UnityEngine;
using Unity.XR.CoreUtils;
using System.Collections;

public class VRPhysicalTeleport : MonoBehaviour
{
    [Header("Player Setup")]
    public XROrigin xrOrigin;
    public CharacterController playerController;
    public Transform vrCamera;

    [Header("Target Location")]
    public Transform targetWaypoint;
    public Transform objectToFace;

    [Header("Lock Settings")]
    public int lockFrames = 15;

    private int _lockCounter = 0;
    private Vector3 _lockTargetPosition;

    public void TeleportPlayer()
    {
        if (xrOrigin != null && targetWaypoint != null && vrCamera != null && objectToFace != null)
        {
            StartCoroutine(TeleportRoutine());
        }
        else
        {
            Debug.LogWarning("Periksa Inspector! Ada komponen yang kosong.");
        }
    }

    private IEnumerator TeleportRoutine()
    {
        if (playerController != null) playerController.enabled = false;

        xrOrigin.MoveCameraToWorldLocation(targetWaypoint.position);

        yield return new WaitForEndOfFrame();

        _lockTargetPosition = objectToFace.position;
        _lockCounter = lockFrames;

        // Tunggu sampai semua frame koreksi di LateUpdate selesai
        while (_lockCounter > 0)
            yield return null;

        if (playerController != null) playerController.enabled = true;

        Debug.Log("Teleport dan Rotasi ke Object Selesai!");
    }

    private void LateUpdate()
    {
        if (_lockCounter <= 0) return;

        Vector3 directionToTarget = _lockTargetPosition - vrCamera.position;
        directionToTarget.y = 0;

        // LOG TANPA SYARAT — taruh SEBELUM if
        Debug.Log($"[Frame {Time.frameCount}] lockCounter={_lockCounter}, camPos={vrCamera.position}, targetPos={_lockTargetPosition}, sqrMag={directionToTarget.sqrMagnitude:F6}");

        if (directionToTarget.sqrMagnitude > 0.0001f)
        {
            float angleDifference = Vector3.SignedAngle(vrCamera.forward, directionToTarget, Vector3.up);
            xrOrigin.transform.RotateAround(vrCamera.position, Vector3.up, angleDifference);
            Debug.Log($"  -> ROTATED by {angleDifference:F2} degrees. New camForward={vrCamera.forward}");
        }
        else
        {
            Debug.LogWarning("  -> SKIP ROTATE: target terlalu dekat / sqrMagnitude nyaris 0!");
        }

        _lockCounter--;
    }
}