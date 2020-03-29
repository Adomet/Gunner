using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoardController : MonoBehaviour
{
    //My player
    public Player player;
    // All Players
    public List<Player> PlayerList = new List<Player>();

    public GameController GC;

    //LeaderBoard elements list 6. My Player

    public List<TextMeshProUGUI> LeaderPlayernames;

    public List<TextMeshProUGUI> LeaderPlayerScores;


    //Player Scores
    public TextMeshProUGUI P_ScoreInt;
    public TextMeshProUGUI P_KillsInt;
    public TextMeshProUGUI P_SurvivalInt;




    private int seconds = 0;




    // Start is called before the first frame update
    void Start()
    {

        GC = GameObject.FindObjectOfType<GameController>();

        InvokeRepeating("CountSeconds",1f, 1f);
    }



    public void UpdateLeaderBoard()
    {
        Player[] Playerarr = GameObject.FindObjectsOfType<Player>();
        PlayerList = new List<Player>();
        foreach (Player player in Playerarr)
        {
            if(player.enabled)
            PlayerList.Add(player);
        }

        PlayerList.Sort(delegate (Player a, Player b) {
            return ((b.KillCount * 100) + (b.pUpCount * 10)).CompareTo((a.KillCount * 100) + (a.pUpCount * 10));
        });

        // set up text

        if (PlayerList.Count >=5)
        {
            for (int i = 0; i < 5; i++)
            {

                LeaderPlayernames[i].SetText((i + 1).ToString() + ". " + PlayerList[i].playerName);
                LeaderPlayerScores[i].SetText(((PlayerList[i].KillCount * 100) + (PlayerList[i].pUpCount * 10)).ToString());


            }
        }

        //update my player text

        if(player !=null)
        {
            LeaderPlayernames[5].SetText(((PlayerList.IndexOf(player) + 1).ToString()) + ". " + player.playerName);
            LeaderPlayerScores[5].SetText(((player.KillCount * 100) + (player.pUpCount * 10)).ToString());
        }

    }


    public void CountSeconds ()
    {
        //Do noting if GameOver
        //if (GC.IsGameOver)
        //    return;


        seconds++;
        //MakeScoreboard Updates here

        UpdateLeaderBoard();

        //Make a score calculation system
        P_ScoreInt.SetText((player.KillCount*100 + player.pUpCount*10).ToString());
        P_KillsInt.SetText(player.KillCount.ToString());
        P_SurvivalInt.SetText(seconds +"s");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
