using TMPro;
using UnityEngine;

public class ObstacleBreakable : MonoBehaviour, IObstacle
{
    [SerializeField] 
    private float maxWeight = 10f; // 최대 무게
    private float totalMass = 0f;
    private TextMeshProUGUI myText = null;

    private void Start()
    {
        myText = GetComponentInChildren<TextMeshProUGUI>();
        myText.text = $"X {maxWeight}";
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 모든 물체의 무게를 계산
        foreach (ContactPoint contact in collision.contacts)
        {
            Rigidbody otherRb = contact.otherCollider.attachedRigidbody;
            if (otherRb != null)
            {
                totalMass += otherRb.mass;
            }
        }

        // 최대 무게를 초과하는 경우
        if (totalMass > maxWeight)
        {
            // 원하는 효과를 추가로 구현
            // 예: 오브젝트 파괴, 이벤트 발생 등
            Destroy(gameObject);
        }
    }

    public void Process()
    {
        throw new System.NotImplementedException();
    }
}
