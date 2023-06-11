using TMPro;
using UnityEngine;

public class ObstacleBreakable : Obstacle
{
    [SerializeField] private float maxWeight = 0f; // �ִ� ��� ����
    [SerializeField] private float squashForce = 0f; // �־����� ����
    [SerializeField] private GameObject explosionCubePrefab; // ���� ȿ���� ���� ť�� ������

    private GameObject[] childCubes = null;
    private float totalWeight = 0f;

    private new void Start()
    {
        base.Start();
        myText.text = $"X {maxWeight}";
        // �ڽ� ť����� ���� �迭�� ����
        int childCount = transform.childCount;
        childCubes = new GameObject[childCount];
        for (int i = 0; i < childCount; i++)
        {
            childCubes[i] = transform.GetChild(i).gameObject;
        }
    }

    private new void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        // �浹�� ��� ��ü�� ���Ը� ���
        Rigidbody otherRb = other.attachedRigidbody;
        if (otherRb != null)
        {
            totalWeight += otherRb.mass;
        }

        // �ִ� ���Ը� �ʰ��ϴ� ���
        if (totalWeight > maxWeight)
        {
            // ���� ť��� �����Ͽ� ������ ȿ�� ����
            ExplodeCubes();
            Destroy(gameObject);
        }
        else
        {
            // �浹�� ������Ʈ�� ���Կ� ���� ť����� �ְ��ϴ� ȿ�� ����
            SquashCubes(totalWeight);
        }
    }

    private void ExplodeCubes()
    {
        foreach (GameObject cube in childCubes)
        {
            for (int i = 0; i < 5; i++)
            {
                // ���� ť����� �����Ͽ� ���� ť���� ��ġ�� ȸ������ �°� ��ġ
                GameObject smallCube = Instantiate(explosionCubePrefab, cube.transform.position, cube.transform.rotation);
                // ���� ť�꿡 ���� ���� ������ ȿ���� �� ���� �ֽ��ϴ�.
                Rigidbody smallCubeRb = smallCube.GetComponent<Rigidbody>();
                if (smallCubeRb != null)
                {
                    smallCubeRb.AddForce(Random.insideUnitSphere * 20f, ForceMode.Impulse);
                } 
            }
        }
    }

    private void SquashCubes(float collisionWeight)
    {
        float squashAmount = collisionWeight / maxWeight * squashForce; // �־��� ���� ���

        for (int i = 0; i < childCubes.Length; i++)
        {
            GameObject cube = childCubes[i];

            // ť�긶�� �ٸ� ȸ������ ��ġ���� �����Ͽ� �־����� ȿ�� ����
            float rotationAmount = 0f;
            float positionOffset = 0f;

            // ť���� ��ȣ�� ���� ȸ������ ��ġ�� ����
            if (i == 0 || i == 4)
            {
                rotationAmount = squashAmount * 10f * (i == 0 ? -1f : 1f);
            }
            else if (i == 1 || i == 3)
            {
                rotationAmount = squashAmount * 20f * (i == 1 ? -1f : 1f);
                positionOffset = squashAmount * -0.1f;
            }
            else if (i == 2)
            {
                positionOffset = squashAmount * -0.2f;
            }

            // ť���� ȸ���� ��ġ �����Ͽ� �־����� ȿ�� ����
            cube.transform.rotation = Quaternion.Euler(0f, 0f, rotationAmount);
            cube.transform.localPosition += new Vector3(0f, positionOffset, 0f);
        }
    }

    public void Process()
    {
        throw new System.NotImplementedException();
    }
}
