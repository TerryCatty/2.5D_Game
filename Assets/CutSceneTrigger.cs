using UnityEngine;

public class CutSceneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        FindAnyObjectByType<VisualNovelle>().OpenPanel();
        Destroy(gameObject);
    }
}
