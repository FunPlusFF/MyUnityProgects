using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class selectplane : MonoBehaviour
{[Header("тут маркер для установки башни")]
    [SerializeField]private GameObject Planemarkerprefab;
    private ARRaycastManager ARRaycastMAnagerScript;
    public static Vector3 position;
    [Header("Дистанция атаки, для ближников 1, для дальников как пожелаешь")]
   
    public static   bool plate = false;
    public GameObject spawnNewCastle;
    public GameObject spawnNewPlate;
    public GameObject spawnNewSpawner;
    public GameObject interface1;
    public GameObject Castle1;
    public GameObject Castle2;
    public GameObject Castle3;
    public GameObject Castle4;
    public GameObject Castle5;
    public static int a = 0;
    private TowerScript tp;
    private HealthBarScript HealthBars;
    public static Vector3 positionTower { get; set; } //Строчка для того чтобы замерить позицию башни чтобы на нее наподали крипы
    
    
    void Start()
    {
       
        ARRaycastMAnagerScript = FindObjectOfType<ARRaycastManager>();
        Planemarkerprefab.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        showMarker();
        
    }
    void showMarker() {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        ARRaycastMAnagerScript.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);
        if (hits.Count > 0)
        {if (plate == true)
            { Planemarkerprefab.SetActive(false); }
            else {
                Planemarkerprefab.SetActive(true); }
            Planemarkerprefab.transform.position = hits[0].pose.position;

        }
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began && plate==false) //Когда ставим
        {
            Destroy(spawnNewCastle);

            Planemarkerprefab.SetActive(false);
            switch (a = Random.Range(1, 5))
            {
                case 1: Instantiate(Castle1, hits[0].pose.position, Castle1.transform.rotation); break;
                case 2: Instantiate(Castle2, hits[0].pose.position, Castle2.transform.rotation); break;
                case 3: Instantiate(Castle3, hits[0].pose.position, Castle3.transform.rotation); break;
                case 4: Instantiate(Castle4, hits[0].pose.position, Castle4.transform.rotation); break;
                case 5: Instantiate(Castle5, hits[0].pose.position, Castle5.transform.rotation); break;

            }


            //  spawnNewCastle.transform.position = hits[0].pose.position;
            Instantiate(spawnNewPlate, hits[0].pose.position, spawnNewPlate.transform.rotation);
            //  spawnNewPlate.transform.position = hits[0].pose.position;

            plate = true;
        
            spawnNewSpawner.transform.position = hits[0].pose.position;
            position = hits[0].pose.position;
            positionTower = hits[0].pose.position; //Строчка Лисёнка! это для крипа

     

        }
        //if (GameObject.FindWithTag("Enemy") == hits[0].pose.position)
        //{
        //    GameObject.FindWithTag("Enemy").GetComponent<IntelectCreeps>().DealDamage(10);
        //}
    }
}
