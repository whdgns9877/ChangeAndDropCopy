using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleObserver : MonoBehaviour
{
    [SerializeField] private List<GameObject> blueObstacles = new List<GameObject>();
    [SerializeField] private List<GameObject> orangeObstacles = new List<GameObject>();

    private void OnEnable() => PlayerInput.OnSingleTouch += ActiveThorn; // 사용자 입력이 싱글터치일때 발생시킬 Event 등록

    // Start is called before the first frame update
    private void Start()
    {
        blueObstacles = GameObject.FindGameObjectsWithTag(Utils.BLUETAG).ToList();
        orangeObstacles = GameObject.FindGameObjectsWithTag(Utils.ORANGETAG).ToList();

        ActiveThorn();
    }

    private void ActiveThorn()
    {
        foreach (GameObject obstacle in blueObstacles)
        {
            obstacle.GetComponent<ObstacleBlue>().ThornActive(!PlayerInput.isBallBlue);
        }

        foreach (GameObject obstacle in orangeObstacles)
        {
            obstacle.GetComponent<ObstacleOrange>().ThornActive(PlayerInput.isBallBlue);
        }
    }

    private void OnDisable() => PlayerInput.OnSingleTouch -= ActiveThorn; // 사용자 입력이 싱글터치일때 발생시킬 Event 해제
}
