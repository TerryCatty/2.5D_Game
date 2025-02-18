using TMPro;
using UnityEngine;

public class ResetCodeButton : MonoBehaviour
{
    public CodeButton codeButton;
    public SafeCode safe;

    private void Start()
    {
        codeButton.pressedActions += ResetCode;
    }

    public void ResetCode()
    {
        safe.ResetCode();
    }
}
