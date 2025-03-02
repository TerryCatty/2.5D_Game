using UnityEngine;

public class ConfirmCodeButton : MonoBehaviour
{
    public CodeButton codeButton;
    public SafeCode safe;

    private void Start()
    {
        codeButton.pressedActions += ConfirmCode;
    }

   public void ConfirmCode()
    {
        safe.CheckCode();
    }
}
