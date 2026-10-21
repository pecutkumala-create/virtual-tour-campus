using UnityEngine;

public class BuildingInfoTrigger : MonoBehaviour
{
    [Header("Object yang muncul saat player mendekat")]
    [SerializeField] private GameObject infoObjectToShow;

    [Header("Tag player (biasanya XR Rig)")]
    [SerializeField] private string playerTag = "Player";

    private void Start()
    {
        if (infoObjectToShow != null)
            infoObjectToShow.SetActive(false); // pastikan tersembunyi di awal
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag) && infoObjectToShow != null)
        {
            infoObjectToShow.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag) && infoObjectToShow != null)
        {
            infoObjectToShow.SetActive(false);
        }
    }
}