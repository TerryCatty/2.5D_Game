using UnityEngine;
using TMPro;

public class SafeCode : MonoBehaviour
{
    public int code;
    public int maxLength;
    public string codeTrying;

    public TextMeshPro textCode;

    public ActionAnimation animationDoor;

    private void Start()
    {
        ResetCode();
    }

    public void ResetCode()
    {
        codeTrying = "";
        string msg = codeTrying;

        for (int i = codeTrying.Length; i < maxLength; i++)
        {
            msg += "x";
        }

        UpdateText(msg);
    }

    public void AddNumber(int value)
    {
        if (value.ToString().Length > 1) return;

        if (codeTrying.Length >= maxLength) return;

        codeTrying += value.ToString();


        string msg = codeTrying;

        for(int i = codeTrying.Length; i < maxLength; i++)
        {
            msg += "x";
        }

        UpdateText(msg);
    }

    public void CheckCode()
    {
        if(code.ToString() == codeTrying)
        {
            UpdateText("OK");
            animationDoor.Play();
        }
    }

    private void UpdateText(string msg)
    {
        textCode.text = msg;
    }
}
