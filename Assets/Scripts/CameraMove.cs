using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Transform targetTr = null;

    // Update is called once per frame
    private void Update()
    {
        if (PlayerInput.ballDrop == true)
        {
            targetTr = FindTarget();
            transform.position = new Vector3(transform.position.x, targetTr.position.y, transform.position.z);
        }
    }

    // ������ y�������� ���� �������� target���� ����
    private Transform FindTarget()
    {
        Transform target = null;
        List<Transform> targets = ObjectPool.GetAllPools("Ball").ConvertAll(t => t.transform);

        target = targets[0];

        foreach (Transform ball in targets)
        {
            if (target.transform.position.y > ball.transform.position.y)
                target = ball;
        }

        return target;
    }
}
