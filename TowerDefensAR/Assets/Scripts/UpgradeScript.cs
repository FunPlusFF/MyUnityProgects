using UnityEngine;
using UnityEngine.UI;

public class UpgradeScript : MonoBehaviour
{
    [SerializeField] private Button btnHealthUpgrade, btnAttackUpgrade, btnAttackSpeedUpgrade;
    private TowerScript tower;

    void Start()
    {
        tower = FindObjectOfType<TowerScript>();

        btnHealthUpgrade.onClick.AddListener(tower.UpgradeHealth);
        btnAttackUpgrade.onClick.AddListener(tower.UpgradeAttack);
        btnAttackSpeedUpgrade.onClick.AddListener(tower.UpgradeAttackSpeed);
    }
}