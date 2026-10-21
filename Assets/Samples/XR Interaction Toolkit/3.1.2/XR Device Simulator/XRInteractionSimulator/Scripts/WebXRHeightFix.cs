using UnityEngine;

public class WebXRHeightFix : MonoBehaviour
{
    void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
            // Mereset local position Y milik Camera Offset agar kembali dikontrol penuh oleh WebXR
            Vector3 localPos = transform.localPosition;
            localPos.y = 0f; 
            transform.localPosition = localPos;
#endif
    }
}
