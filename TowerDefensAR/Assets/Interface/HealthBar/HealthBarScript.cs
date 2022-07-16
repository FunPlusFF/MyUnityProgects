using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider HealthBar;
    public Color Low, High;

    public void ChangingHealthBars(float curHP, float maxHP)
    {
        HealthBar.maxValue = maxHP;
        HealthBar.value = curHP;
        
        HealthBar.GetComponentInChildren<Text>().text = curHP + " / " + maxHP;
        HealthBar.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, HealthBar.normalizedValue);
        if (curHP <= 0) { Application.Quit(); }
    }
}