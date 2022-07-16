using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    [Header("Сюда пихаем префабы врагов")]
    public GameObject[] enemyPrefab;
    [Header("Интервал с которым будут появляться вражина")]
    public float  interval = 2f;
    [Header("Количество крипов которое мы хотим поднять")]
    public int[] countEnemySpawn; //Счетчик всех мобов, номер позиции отражает тип, значение элемента в масиве отражает количество заспауненных манстров



    [Header("Это переменная маштабирования изменять только с калькулятором, начальное значение 0.1")]
    public float scale;

    [Header("Помещаем сюда радиус")]
    public int radius;


    private float time = 0f;
    private int count = 0, //Количество мобов мы хотим призвать
    enemySpawned = 0; // Количество мобов которое мы призвали
    //private int[,] countsCreeep; //Счетчик всех мобов, первая переменная это тип, вторая количество мобов

    

    // Start is called before the first frame update
    void Start()
    {

        foreach (int i in countEnemySpawn)
        {
            count += i;
        }
        if (countEnemySpawn.Length != enemyPrefab.Length)
        {
            Array.Resize<System.Int32>(ref countEnemySpawn, enemyPrefab.Length);
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;


        int[] countSpawned = new int[countEnemySpawn.Length];
        if ((time >= interval) && (enemySpawned < count))
        {
            int rndCreep = UnityEngine.Random.Range(0, countSpawned.Length - 1); //Выбираем рандомного криппа
            SelCreep:
            if (countSpawned[rndCreep] == countEnemySpawn[rndCreep]) //Здесь возможно ошибка, не знаю что поставить что бы в массиве была нужная перерменная
            { 
                rndCreep = UnityEngine.Random.Range(0, enemyPrefab.Length - 1);
                goto SelCreep;
                
            }
            else 
            {
               Instantiate(enemyPrefab[rndCreep], RandomPointOnCircleEdge(radius), enemyPrefab[rndCreep].transform.rotation);
                countSpawned[rndCreep]++;
                enemySpawned++;
            }
            time = 0;
            

        }
    }
    private Vector3 RandomPointOnCircleEdge(float radius)
    {
        var vector2 = UnityEngine.Random.insideUnitCircle.normalized * radius * scale;
        return new Vector3(vector2.x, 0, vector2.y);
    }
}




