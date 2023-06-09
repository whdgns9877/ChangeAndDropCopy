using System;
using UnityEngine;

public enum TouchState { TouchBegan, TouchEnd, SingleTouch, Swipe }

public static class PlayerInput
{
    // �÷��̾��� �Է��� ��Ÿ���� enum
    public static TouchState state = TouchState.TouchEnd;

    public static bool ballDrop = false; // ó�� ��ġ�� ������ ��Ҵ����� ��Ÿ���� bool ����
    public static bool isBallBlue = true; // �� ��� ���� ��ġ�� blue/orange�� ���� ���ϴµ� �̰��� ��Ÿ���� ����

    private static bool isSwiping; // ���� �������� �������� ��Ÿ���� bool����
    private static float swipeThreshold = 50f; // ���������� �����Ǵ� �ּ� �̵� �Ÿ�
    private static float swipeDistance = 0f; // ���������� �Ÿ�
    private static Vector2 swipeStartPosition; // �������� �Ÿ��� ������ Vector2����

    public static event Action OnSingleTouch; // �̱���ġ�� ���� �̺�Ʈ�� ��Ƴ��� Action ����

    // Monobehaviour�� Update�Լ��� �ƴϹǷ� PlayerInput�Լ��� �̿��ϴ� ������ �ش� Update�Լ��� ȣ���ؾ��Ѵ�
    public static void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // ��ġ ���۽�
            if (touch.phase == TouchPhase.Began)
            {
                state = TouchState.TouchBegan;
                isSwiping = false;
                swipeStartPosition = touch.position;
                swipeDistance = 0f;
            }
            // ��ġ ���� ���� ���������� ����������
            else if (touch.phase == TouchPhase.Moved)
            {
                // �������� ���� �ƴϰ� ���������� �Ÿ��� �ּҰŸ��� �Ѿ�����
                if (!isSwiping && Vector2.Distance(touch.position, swipeStartPosition) >= swipeThreshold)
                {
                    isSwiping = true;
                    Vector2 swipeDirection = touch.position - swipeStartPosition;
                    float swipeAngle = Vector2.Angle(Vector2.right, swipeDirection);
                    state = TouchState.Swipe;
                }

                // �������� ���϶�
                if (isSwiping)
                {
                    // ���� Ŀ�� 0.01�� ����
                    swipeDistance = (touch.position.x - swipeStartPosition.x) * 0.01f;
                }
            }
            // ��ġ�� �����ų� �߰��� ���������(�հ����� ��ġ ���������� �����ų� �Ǽ��� ���������� ��)
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                // �������� ���̾��ٸ�
                if (isSwiping)
                {
                    state = TouchState.TouchEnd;                   
                    ballDrop = true;
                }
                // �������� ���� �ƴϰ� �̹� ������ �������� �ִٸ�
                else if(ballDrop == true)
                {
                    state = TouchState.SingleTouch;
                    isBallBlue = !isBallBlue;
                    OnSingleTouch?.Invoke(); //����س��� Event �߻� (������ �� ��ȭ)
                }
            }
        }
    }

    public static float GetSwipeDistance()
    {
        return Mathf.Clamp(swipeDistance, -3.18f, 3.18f);
    }
}
