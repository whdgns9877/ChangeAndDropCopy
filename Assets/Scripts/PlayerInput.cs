using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public enum TouchState { TouchBegan, TouchEnd, SingleTouch, Swipe }

public static class PlayerInput
{
    public static TouchState state = TouchState.TouchEnd;

    private static bool isSwiping;
    private static Vector2 swipeStartPosition;
    private static float swipeThreshold = 50f; // 스와이프로 감지되는 최소 이동 거리
    private static float swipeDistance = 0f; // 스와이프된 거리

    public static void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                state = TouchState.TouchBegan;
                isSwiping = false;
                swipeStartPosition = touch.position;
                swipeDistance = 0f;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                if (!isSwiping && Vector2.Distance(touch.position, swipeStartPosition) >= swipeThreshold)
                {
                    isSwiping = true;
                    Vector2 swipeDirection = touch.position - swipeStartPosition;
                    float swipeAngle = Vector2.Angle(Vector2.right, swipeDirection);
                    state = TouchState.Swipe;
                }

                if (isSwiping)
                {
                    swipeDistance = (touch.position.x - swipeStartPosition.x) * 0.01f;
                }
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                if (isSwiping)
                {
                    state = TouchState.TouchEnd;
                }
                else
                {
                    state = TouchState.SingleTouch;
                }
            }
        }
    }

    public static float GetSwipeDistance()
    {
        return Mathf.Clamp(swipeDistance, -3.18f, 3.18f);
    }
}
