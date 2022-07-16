using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Spawnr : MonoBehaviour
{
    public GameObject[] enemiPrefab;    
    public float time = 0f, interval = 2f;
    public int enemiCauntInBatel = 0, enemiCaunt = 0 ;   
    public float radius = 0.5f;
    public float mobSceal = 0.03f;  
    public int[] spawnPercent;
    public static Vector3 post;
    public bool isGameStart;
    GameObject Tawer;
    GameObject pla;
    public TextMeshProUGUI waweText;
    private int wawe = 0;
    public GameObject newWaweBtn;

    public List<GameObject> SpawnedEnemies = new List<GameObject>(); // Tracking of spawned enemies @Serega
    // Start is called before the first frame update
    void Start()
    {
        if (selectplane.plate == true)
        {
            isGameStart = true;
            NewWawe();
        }
        isGameStart = false;
        spawnPercent = new int[enemiPrefab.Length];
        spawnPercent[0] = 20;
        spawnPercent[1] = 20;
        spawnPercent[2] = 20;
        spawnPercent[3] = 20;
        spawnPercent[4] = 20;
        if (selectplane.plate == true)
        {
            Tawer = GameObject.FindGameObjectWithTag("Tawer");
            post = Tawer.transform.position;
            enemiCauntInBatel = 0;
          
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Tawer = GameObject.FindGameObjectWithTag("Tawer");
      //  pla = GameObject.FindGameObjectWithTag("pla");
      
      // pla.transform.position = post;
        if (selectplane.plate == true)
        {
            SpawnCrip();
        }
       
        newWaweBtn.SetActive(!isGameStart);

        
        
           
        
    }
    private Vector3 RandomPointOnCircleEdge(float radius)//случайная точка на окружности
    {
        var vector2 = Random.insideUnitCircle.normalized * (radius - mobSceal);
        return new Vector3(vector2.x, 0, vector2.y);
    }

    private int MobSpawn(int mobChance)//шанс спавна мобов
    {
        int i = 0;
       while(i < spawnPercent.Length)
        {
            if(mobChance <= spawnPercent[i])
            {
                return i;
            }
            else
            {
                mobChance -= spawnPercent[i];
                i++;
            }
        }
        return -1;
    }

    private void SpawnCrip()//спав крипов
    {
        time += Time.deltaTime;

        if ((enemiCauntInBatel < enemiCaunt) && isGameStart)
        {
            if((time >= interval))
            {
                SpawnedEnemies.Add(Instantiate
                (
                enemiPrefab[MobSpawn(Random.Range(1, 100))],
                RandomPointOnCircleEdge(radius) + post,
                enemiPrefab[0].transform.rotation
                )); // Added to the list of spawned enemies @Serega
                enemiCauntInBatel++;
                time = 0f;
            }
            
        }else if(SpawnedEnemies.Count == 0)
        {
            isGameStart = false;
        }
    }

    private void NewWawe()//следущая волна
    {
       
           for (int i = 0; i < spawnPercent.Length - 1; i++)
           {
               if (spawnPercent[i] > 0)
               {
                   if (spawnPercent[i + 1] != 95)
                   {
                        spawnPercent[i] -= 5;
                        spawnPercent[i + 1] += 5;
                   }
               }
           }

           enemiCaunt += 3;
           enemiCauntInBatel = 0;
        wawe++;
        waweText.text = "Wave: " + wawe;

    }  
   

    public void StartGame()
    {
        isGameStart = true;
        NewWawe();
    }
}
