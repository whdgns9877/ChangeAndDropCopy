using TMPro;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI myText = null;
    protected bool isEmphasizeOn = false;
    private float originalSize;
    // 몇개를 곱할것인지
    protected int myNum;

    // Start is called before the first frame update
    protected void Start()
    {
        myText = GetComponentInChildren<TextMeshProUGUI>();
        originalSize = myText.fontSize;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Utils.BALLTAG))
        {
            EmphasizeText();
        }
    }

    protected void EmphasizeText()
    {
        if(isEmphasizeOn == false)
        {
            isEmphasizeOn = true;
            myText.fontSize *= 1.2f;
            Invoke(nameof(RevertEmphasize), 0.1f);
        }
    }

    protected void RevertEmphasize()
    {
        isEmphasizeOn = false;
        myText.fontSize = originalSize;
    }
}
