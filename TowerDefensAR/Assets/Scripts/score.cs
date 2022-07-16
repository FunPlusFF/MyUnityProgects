using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour
{
    public int Allscore=0;

    public int[] Kills = new int[2];
    public int AllKills=0;

    public Dictionary<int, int> mobsType = new Dictionary<int, int>();

    private TextMeshProUGUI ScoreLabel;

    // Start is called before the first frame update
    void Start()
    {
        ScoreLabel = GameObject.Find("ScoreLabel").GetComponent<TextMeshProUGUI>();

        Kills[0] = 0;
        Kills[1] = 0;

        //mobs dictionary
        mobsType.Add(0, 100); //Simple skeleton
        mobsType.Add(1, 250); //heavy skeleton
        mobsType.Add(2, 400); //AceLich
        mobsType.Add(3, 400); //DartLich
        mobsType.Add(4, 400); //LowLich
        mobsType.Add(5, 1500); //Dragon

    }


    //Adding points from kills

    //start
    public void IncreaseScore(int mob)
    {
        AllKills++;
        Kills[mob]++;
        Allscore += mobsType[mob];
        
    }
    //end

    //Removing points
    //start
    //here must be shit that not done
    //end

    // Update is called once per frame
    void Update()
    {
        ScoreLabel.text = "Score: " + Allscore;
    }
}
