using UnityEngine;

public class DisableSimulatorOnBuild : MonoBehaviour
{
    void Awake()
    {
#if !UNITY_EDITOR
        gameObject.SetActive(false);
#endif
    }
}