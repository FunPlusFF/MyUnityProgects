using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeal : MonoBehaviour
{
    [Header("��������� ����� ���������� ��������")]
    public float maxHealthPoint = 5;

    float currentHealthPoint;
    IntelectCreeps intelectCreepsScript;

    public void GetDamage(float damage)
    {
        currentHealthPoint -= damage;
    }


    // Update is called once per frame
    void Update()
    {
        if (currentHealthPoint <= 0)//������ ����� ���� �� ������ 0
        {
          //  intelectCreepsScript.Death();
        }
    }
}
