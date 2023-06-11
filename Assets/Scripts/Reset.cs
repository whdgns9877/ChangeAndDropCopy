using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    private void OnEnable()
    {
        ResetGameState();
    }

    // ���� ���¸� �ʱ�ȭ�ϴ� �Լ�
    private void ResetGameState()
    {
        PlayerInput.state = TouchState.NONE;
        PlayerInput.isBallBlue = true;
        PlayerInput.ballDrop = false;
        PlayerInput.RunEvent();
    }

    // ���� ���� ���� ��(���� ��)�� �ٽ� �ε��Ͽ� ó�� ���·� �ǵ����� �Լ�
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
