using UnityEngine;

public class FightZone : MonoBehaviour
{
    public bool isRotatingByMovement;
    public bool isRotatingCursor;
    public bool attackActive;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Movement movement))
        {
            movement.isRotatingByMovement = isRotatingByMovement;
            movement.isRotatingCursor = isRotatingCursor;
        }

        if (other.TryGetComponent(out AttackSystem attack))
        {
            attack.isActive = attackActive;
        }
    }
}
