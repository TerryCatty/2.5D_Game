using UnityEngine;

public class EnterItem : GameAction
{
    public GameObject obj;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == obj)
        {
            m_OnComplete.Invoke();
        }
    }
}
