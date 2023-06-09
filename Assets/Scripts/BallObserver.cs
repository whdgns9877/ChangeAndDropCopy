using UnityEngine;

public class BallObserver : MonoBehaviour
{
    // 공들에 붙어있는 Material
    [SerializeField] private Material ballMat = null;
    // 자주 사용할 struct인 Color 값을 캐싱
    private Color orangeColor = new Color(1f, 0.5f, 0f); //(해당 RGB 값은 주황색)

    private void OnEnable() => PlayerInput.OnSingleTouch += ChangeColorOfAllBalls; // 사용자 입력이 싱글터치일때 발생시킬 Event 등록

    // Player의 입력에 따라서 공들의 색을 변화시킨다
    private void ChangeColorOfAllBalls() => ballMat.color = PlayerInput.isBallBlue == true ? Color.blue : orangeColor;

    private void OnDisable() => PlayerInput.OnSingleTouch -= ChangeColorOfAllBalls; // 등록한 Event를 인스턴스 비활성화 될때 해제

    // 프로그램이 종료될때 주황컬러로 material의 color값이 저장될수 있으므로 기본색인 파란색으로 돌려놓는다.
    private void OnApplicationQuit() => ballMat.color = Color.blue;
}
