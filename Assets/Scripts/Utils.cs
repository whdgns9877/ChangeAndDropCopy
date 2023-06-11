using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    // ���� ����ϴ� string���� �޸� ���� ���������� ��Ƴ��´�.
    public const string BALLTAG = "Ball";
    public const string BLUETAG = "Blue";
    public const string ORANGETAG = "Orange";
    public const string EndLineTag = "EndLine";

    public static readonly Quaternion QI = Quaternion.identity;

    // ������ y�������� ���� �������� �̾Ƴ��� �Լ�
    public static Transform FindTarget()
    {
        Transform target = null;
        List<Transform> targets = BallObjectPool.Instance.ActiveBallList;

        if (targets.Count == 0) return null;
        target = targets[0];

        foreach (Transform ball in targets)
        {
            if (target.transform.position.y > ball.transform.position.y)
                target = ball;
        }
        return target;
    }
}