using System;
using UnityEngine;

public enum TouchState { TouchBegan, TouchEnd, SingleTouch, Swipe, NONE }

public static class PlayerInput
{
    // �÷��̾��� �Է��� ��Ÿ���� enum
    public static TouchState state = TouchState.NONE;

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
#if UNITY_EDITOR
        // �����Ϳ��� ��ġ �Է� �ùķ��̼�
        if (Input.GetMouseButtonDown(0))
        {
            InputStart();
        }
        else if (Input.GetMouseButton(0))
        {
            InputMove();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            InputEnd();
        }
        else
        {
            state = TouchState.NONE;
        }
#else
        // ����� ����̽����� ��ġ �Է�
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                InputStart();
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                InputMove();
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                InputEnd();
            }
        }
        else
        {
            state = TouchState.NONE;
        }
#endif
    }

    // �Է��� ���۵Ǿ����� ������ �Լ�
    private static void InputStart()
    {
        state = TouchState.TouchBegan;
        isSwiping = false;
        swipeStartPosition = Input.mousePosition;
        swipeDistance = 0f;
    }

    // �Է��� �߻��ϰ� �����϶� ������ �Լ�
    private static void InputMove()
    {
        if (!isSwiping && Vector2.Distance(Input.mousePosition, swipeStartPosition) >= swipeThreshold)
        {
            isSwiping = true;
            Vector2 swipeDirection = (Vector2)Input.mousePosition - swipeStartPosition;
            float swipeAngle = Vector2.Angle(Vector2.right, swipeDirection);
            state = TouchState.Swipe;
        }

        if (isSwiping)
        {
            swipeDistance = (Input.mousePosition.x - swipeStartPosition.x) * 0.01f;
        }
    }

    // �Է��� �������� ������ �Լ�
    private static void InputEnd()
    {
        if (isSwiping)
        {
            state = TouchState.TouchEnd;
            ballDrop = true;
        }
        else if (ballDrop == true)
        {
            state = TouchState.SingleTouch;
            isBallBlue = !isBallBlue;
            OnSingleTouch?.Invoke();
        }
    }
    
    // �������� �Ÿ��� Clamping�Ͽ� ��ȯ�ϴ� �Լ�
    public static float GetSwipeDistance()
    {
        return Mathf.Clamp(swipeDistance, -3.18f, 3.18f);
    }
}
