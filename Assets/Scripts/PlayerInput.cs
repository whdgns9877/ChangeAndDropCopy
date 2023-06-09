using System;
using UnityEngine;

public enum TouchState { TouchBegan, TouchEnd, SingleTouch, Swipe }

public static class PlayerInput
{
    // 플레이어의 입력을 나타내줄 enum
    public static TouchState state = TouchState.TouchEnd;

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
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // 터치 시작시
            if (touch.phase == TouchPhase.Began)
            {
                state = TouchState.TouchBegan;
                isSwiping = false;
                swipeStartPosition = touch.position;
                swipeDistance = 0f;
            }
            // 터치 시작 이후 스와이프로 움직였을시
            else if (touch.phase == TouchPhase.Moved)
            {
                // 스와이프 중이 아니고 스와이프한 거리가 최소거리를 넘었을때
                if (!isSwiping && Vector2.Distance(touch.position, swipeStartPosition) >= swipeThreshold)
                {
                    isSwiping = true;
                    Vector2 swipeDirection = touch.position - swipeStartPosition;
                    float swipeAngle = Vector2.Angle(Vector2.right, swipeDirection);
                    state = TouchState.Swipe;
                }

                // 스와이프 중일때
                if (isSwiping)
                {
                    // 값이 커서 0.01을 곱함
                    swipeDistance = (touch.position.x - swipeStartPosition.x) * 0.01f;
                }
            }
            // 터치가 끝나거나 중간에 떼졌을경우(손가락이 터치 범위밖으로 나가거나 실수로 떼어졌을때 등)
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                // 스와이프 중이었다면
                if (isSwiping)
                {
                    state = TouchState.TouchEnd;                   
                    ballDrop = true;
                }
                // 스와이프 중이 아니고 이미 공들이 떨어지고 있다면
                else if(ballDrop == true)
                {
                    state = TouchState.SingleTouch;
                    isBallBlue = !isBallBlue;
                    OnSingleTouch?.Invoke(); //등록해놓은 Event 발생 (공들의 색 변화)
                }
            }
        }
    }

    public static float GetSwipeDistance()
    {
        return Mathf.Clamp(swipeDistance, -3.18f, 3.18f);
    }
}
