using UnityEngine;

public class CodeDoorTarget : MonoBehaviour
{
    public int code;

    private string codeTrying;

    public void AddCodeValue(int value)
    {
        codeTrying += value.ToString();
        CheckCode();
    }

    private void CheckCode()
    {
        if(codeTrying.Contains(code.ToString()))
        {
            Debug.Log("Code");
            codeTrying = "";

            GetComponent<ActionAnimation>().Play();
        }
    }
}
