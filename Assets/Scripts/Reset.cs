using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    private void OnEnable()
    {
        ResetGameState();
    }

    // 게임 상태를 초기화하는 함수
    private void ResetGameState()
    {
        Debug.Log("게임 초기 상태로 시작");
        PlayerInput.state = TouchState.NONE;
        PlayerInput.isBallBlue = true;
        PlayerInput.ballDrop = false;
    }

    // 현재 진행 중인 씬(게임 씬)을 다시 로드하여 처음 상태로 되돌리는 함수
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
