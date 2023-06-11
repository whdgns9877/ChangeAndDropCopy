using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    // 자주 사용하는 string들을 메모리 복사 방지를위해 모아놓는다.
    public const string BALLTAG = "Ball";
    public const string BLUETAG = "Blue";
    public const string ORANGETAG = "Orange";
    public const string EndLineTag = "EndLine";

    public static readonly Quaternion QI = Quaternion.identity;

    // 공들중 y포지션이 가장 낮은것을 뽑아내는 함수
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