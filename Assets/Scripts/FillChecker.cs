using UnityEngine;
using UnityEngine.UI;

public class FillChecker : MonoBehaviour
{
    [SerializeField] private Slider fillSlider = null;
    [SerializeField] private float maxWeight = 60f; // 최대 무게
    [SerializeField] private CameraMove camMove = null;
    private float increaseAmount = 0f;

    private void Start()
    {
        camMove = FindObjectOfType<CameraMove>();
        increaseAmount = 1 / maxWeight;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Utils.BALLTAG))
        {
            if(camMove.enabled == true) camMove.enabled = false;
            SetSliderValue();
        }
    }

    private void SetSliderValue()
    {
        fillSlider.value += increaseAmount;
    }
}
