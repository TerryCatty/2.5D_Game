using UnityEngine;
using GamePush;
using TMPro;
using static UnityEngine.EventSystems.EventTrigger;

public class TestSaves : MonoBehaviour
{
    public int energy;
    public TextMeshProUGUI text;

    public void ShowEnergy()
    {
        energy = GP_Player.GetInt("energy");
        text.text = energy.ToString();
    }

    public void SaveEnergy()
    {
        GP_Player.Set("energy", energy);
        GP_Player.Sync();
    }

    public void AddEnergy(int value)
    {
        energy += value;
        text.text = energy.ToString();
    }
}
