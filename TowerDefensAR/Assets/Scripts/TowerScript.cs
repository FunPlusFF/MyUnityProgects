using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerScript : MonoBehaviour
{
    public float MaxHealth;

    public float curHP;

    public EffectType EffectType;
    public float EffectStrength;
    public float EffectDuration;

    private IEnumerator HealingProccess;

    [Header("Leveling")]

    [SerializeField]
    private int HealthLevel = 1;
    [SerializeField]
    private int AttackLevel = 1;
    [SerializeField]
    private int AttackSpeedLevel = 1;
    [SerializeField]
    private int HealthUpgradingPrice = 1000, AttackUpgradingPrice = 1000, AttackSpeedUpgradingPrice = 1000;

    private float HealthUpgradeAmount = 100f, AttackUpgradeAmount = 10f, AttackSpeedUpgradeAmount = 0.2f;

    private score Score;
    private Shooting Shooting;
    

    public void DealDamage(float damage)
    {
        if (curHP > 0)
        {
            curHP -= damage;
        }

        if (curHP < 0)
        {
            curHP = 0;
        }
    }

    public void HealTower(float heal)
    {
        if (curHP + heal <= MaxHealth)
        {
            curHP += heal;
        }
        else
        {
            curHP = MaxHealth;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        curHP = MaxHealth;
        Score = FindObjectOfType<score>();
        Shooting = FindObjectOfType<Shooting>();
        

        if (EffectType == EffectType.Mechanical)
        {
            HealingProccess = Healing(EffectStrength, 4);
            StartCoroutine(HealingProccess);
        }
    }

    IEnumerator Healing(float amount, float interval)
    {
        while (gameObject.activeInHierarchy)
        {
            HealTower((int)((MaxHealth - curHP) * (amount / 100)));
            yield return new WaitForSeconds(interval);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpgradesTesting();

        if (curHP <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }

    private void UpgradesTesting()
    {
        if(Input.GetKeyDown(KeyCode.S))
            UpgradeAttackSpeed();
        if(Input.GetKeyDown(KeyCode.A))
            UpgradeAttack();
        if(Input.GetKeyDown(KeyCode.H))
            UpgradeHealth();
    }

    public void UpgradeHealth()
    {
        if (Score.Allscore >= HealthUpgradingPrice && HealthLevel < 10)
        {
            Score.Allscore -= HealthUpgradingPrice;
            MaxHealth += HealthUpgradeAmount;
            HealthLevel++;
            HealthUpgradingPrice = (int)((float)HealthUpgradingPrice * 1.5f);
        }
    }

    public void UpgradeAttack()
    {
        if (Score.Allscore >= AttackUpgradingPrice && AttackLevel < 10)
        {
            Score.Allscore -= AttackUpgradingPrice;
            Shooting.ProjectileDamage += AttackUpgradeAmount;
            AttackLevel++;
            AttackUpgradingPrice = (int)((float)AttackUpgradingPrice * 1.5f);
        }
    }

    public void UpgradeAttackSpeed()
    {
        if (Score.Allscore >= AttackSpeedUpgradingPrice && AttackSpeedLevel < 10)
        {
            Score.Allscore -= AttackSpeedUpgradingPrice;
            Shooting.ShootingRate += AttackSpeedUpgradeAmount;
            AttackSpeedLevel++;
            AttackSpeedUpgradingPrice = (int)((float)AttackSpeedUpgradingPrice * 1.5f);
        }
    }
}
