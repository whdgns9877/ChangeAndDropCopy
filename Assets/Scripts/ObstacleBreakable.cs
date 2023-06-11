using TMPro;
using UnityEngine;

public class ObstacleBreakable : Obstacle
{
    [SerializeField] private float maxWeight = 0f; // 최대 허용 무게
    [SerializeField] private float squashForce = 0f; // 휘어짐의 세기
    [SerializeField] private GameObject explosionCubePrefab; // 폭발 효과를 위한 큐브 프리팹

    private GameObject[] childCubes = null;
    private float totalWeight = 0f;

    private new void Start()
    {
        base.Start();
        myText.text = $"X {maxWeight}";
        // 자식 큐브들을 얻어와 배열에 저장
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
        // 충돌한 모든 물체의 무게를 계산
        Rigidbody otherRb = other.attachedRigidbody;
        if (otherRb != null)
        {
            totalWeight += otherRb.mass;
        }

        // 최대 무게를 초과하는 경우
        if (totalWeight > maxWeight)
        {
            // 작은 큐브들 생성하여 터지는 효과 구현
            ExplodeCubes();
            Destroy(gameObject);
        }
        else
        {
            // 충돌한 오브젝트의 무게에 따라 큐브들을 휘게하는 효과 적용
            SquashCubes(totalWeight);
        }
    }

    private void ExplodeCubes()
    {
        foreach (GameObject cube in childCubes)
        {
            for (int i = 0; i < 5; i++)
            {
                // 작은 큐브들을 생성하여 기존 큐브의 위치와 회전값에 맞게 배치
                GameObject smallCube = Instantiate(explosionCubePrefab, cube.transform.position, cube.transform.rotation);
                // 작은 큐브에 힘을 가해 터지는 효과를 줄 수도 있습니다.
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
        float squashAmount = collisionWeight / maxWeight * squashForce; // 휘어짐 정도 계산

        for (int i = 0; i < childCubes.Length; i++)
        {
            GameObject cube = childCubes[i];

            // 큐브마다 다른 회전값과 위치값을 적용하여 휘어지는 효과 구현
            float rotationAmount = 0f;
            float positionOffset = 0f;

            // 큐브의 번호에 따라 회전값과 위치값 조정
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

            // 큐브의 회전과 위치 조정하여 휘어지는 효과 적용
            cube.transform.rotation = Quaternion.Euler(0f, 0f, rotationAmount);
            cube.transform.localPosition += new Vector3(0f, positionOffset, 0f);
        }
    }

    public void Process()
    {
        throw new System.NotImplementedException();
    }
}
