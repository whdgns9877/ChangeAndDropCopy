using UnityEngine;

public class BallObserver : MonoBehaviour
{
    // ���鿡 �پ��ִ� Material
    [SerializeField] private Material ballMat = null;
    // ���� ����� struct�� Color ���� ĳ��
    private Color orangeColor = new Color(1f, 0.5f, 0f); //(�ش� RGB ���� ��Ȳ��)

    private void OnEnable() => PlayerInput.OnSingleTouch += ChangeColorOfAllBalls; // ����� �Է��� �̱���ġ�϶� �߻���ų Event ���

    // Player�� �Է¿� ���� ������ ���� ��ȭ��Ų��
    private void ChangeColorOfAllBalls() => ballMat.color = PlayerInput.isBallBlue == true ? Color.blue : orangeColor;

    private void OnDisable() => PlayerInput.OnSingleTouch -= ChangeColorOfAllBalls; // ����� Event�� �ν��Ͻ� ��Ȱ��ȭ �ɶ� ����

    // ���α׷��� ����ɶ� ��Ȳ�÷��� material�� color���� ����ɼ� �����Ƿ� �⺻���� �Ķ������� �������´�.
    private void OnApplicationQuit() => ballMat.color = Color.blue;
}
