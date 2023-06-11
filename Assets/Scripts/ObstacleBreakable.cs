using TMPro;
using UnityEngine;

public class ObstacleBreakable : MonoBehaviour, IObstacle
{
    [SerializeField] 
    private float maxWeight = 10f; // �ִ� ����
    private float totalMass = 0f;
    private TextMeshProUGUI myText = null;

    private void Start()
    {
        myText = GetComponentInChildren<TextMeshProUGUI>();
        myText.text = $"X {maxWeight}";
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �浹�� ��� ��ü�� ���Ը� ���
        foreach (ContactPoint contact in collision.contacts)
        {
            Rigidbody otherRb = contact.otherCollider.attachedRigidbody;
            if (otherRb != null)
            {
                totalMass += otherRb.mass;
            }
        }

        // �ִ� ���Ը� �ʰ��ϴ� ���
        if (totalMass > maxWeight)
        {
            // ���ϴ� ȿ���� �߰��� ����
            // ��: ������Ʈ �ı�, �̺�Ʈ �߻� ��
            Destroy(gameObject);
        }
    }

    public void Process()
    {
        throw new System.NotImplementedException();
    }
}
