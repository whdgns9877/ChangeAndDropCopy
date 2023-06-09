using TMPro;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI myText = null;
    private int myNum;
    public int MyNum { get { return myNum; } }

    // Start is called before the first frame update
    private void Start()
    {
        myText = GetComponentInChildren<TextMeshProUGUI>();
        int rand = Random.Range(2, 5);
        myText.text = $"X {rand}";
        myNum = rand;
    }
}
