using UnityEngine;
using TMPro;

public class CodeButtonAction : MonoBehaviour
{
    public CodeButton codeButton;
    public TextMeshPro textButton;
    public int number;

    public SafeCode safe;

    private void Start()
    {
        codeButton.pressedActions += AddNumberCode;
        textButton.text = number.ToString();
    }

    public void AddNumberCode()
    {
        safe.AddNumber(number);
    }
}
