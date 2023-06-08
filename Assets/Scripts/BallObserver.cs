using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallObserver : MonoBehaviour
{
    [SerializeField] private Material ballMat = null;
    private Color orangeColor = new Color(1f, 0.5f, 0f);

    private void OnEnable() => PlayerInput.OnSingleTouch += ChangeColorOfAllBalls;

    private void ChangeColorOfAllBalls() => ballMat.color = PlayerInput.isBallBlue == true ? Color.blue : orangeColor;

    private void OnDisable() => PlayerInput.OnSingleTouch -= ChangeColorOfAllBalls;

    private void OnApplicationQuit() => ballMat.color = Color.blue;
}
