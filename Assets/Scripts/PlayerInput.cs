using System;
using UnityEngine;

public enum TouchState { TouchBegan, TouchEnd, SingleTouch, Swipe, NONE }

public static class PlayerInput
{
    // 플레이어의 입력을 나타내줄 enum
    public static TouchState state = TouchState.NONE;

    public static bool ballDrop = false; // 처음 터치로 공들을 쏟았는지를 나타내줄 bool 변수
    public static bool isBallBlue = true; // 볼 드랍 이후 터치시 blue/orange로 색이 변하는데 이것을 나타내줄 변수

    private static bool isSwiping; // 현재 스와이프 중인지를 나타내줄 bool변수
    private static float swipeThreshold = 50f; // 스와이프로 감지되는 최소 이동 거리
    private static float swipeDistance = 0f; // 스와이프된 거리
    private static Vector2 swipeStartPosition; // 스와이프 거리를 저장할 Vector2변수

    public static event Action OnSingleTouch; // 싱글터치에 따른 이벤트를 담아놓을 Action 변수

    // Monobehaviour의 Update함수가 아니므로 PlayerInput함수를 이용하는 측에서 해당 Update함수를 호출해야한다
    public static void Update()
    {
#if UNITY_EDITOR
        // 에디터에서 터치 입력 시뮬레이션
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
        // 모바일 디바이스에서 터치 입력
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

    // 입력이 시작되었을때 실행할 함수
    private static void InputStart()
    {
        state = TouchState.TouchBegan;
        isSwiping = false;
        swipeStartPosition = Input.mousePosition;
        swipeDistance = 0f;
    }

    // 입력이 발생하고 움직일때 실행할 함수
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

    // 입력이 끝났을때 실행할 함수
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
    
    // 스와이프 거리를 Clamping하여 반환하는 함수
    public static float GetSwipeDistance()
    {
        return Mathf.Clamp(swipeDistance, -3.18f, 3.18f);
    }
}
