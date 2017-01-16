using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class s_HighScores : MonoBehaviour
{

    private const string PlayerScore = "PlayerScore";
    private const string PlayerName = "PlayerName";
    private const string TotalScoreCount = "TotalScoreCount";
    //dictionary to store the high scores in
    public Dictionary<string, int> ScoreTable = new Dictionary<string, int>();


    //adds scores to the scoreTable dictionary
    public void AddPlayerScore(string playerName)
    {
        if(PlayerPrefs.GetInt(PlayerName + 1) == 0)
        {
            ScoreTable.Add("Player 1", 100);
            ScoreTable.Add("Player 2", 20);
            ScoreTable.Add("Player 3", 150);
            ScoreTable.Add("Player 4", 50);
            ScoreTable.Add("Player 5", 40);
            ScoreTable.Add("Player 6", 80);
        }

        ScoreTable.Add(playerName, PlayerPrefs.GetInt("Score"));

        Debug.Log("Score Added");
        AddScoresToPlayerPrefs();
    }

    //adds the records from the scoreTable to the player prefabs
    public void AddScoresToPlayerPrefs()
    {
        

        int i = 0;
        //loops through the key value pairs
        foreach (KeyValuePair<string, int> record in ScoreTable)
        {
            PlayerPrefs.SetInt(PlayerScore + i, record.Value);
            PlayerPrefs.SetString(PlayerName + i, record.Key);

            i++;
        }

        //sets the totalScoreCount to the amount of record there are
        PlayerPrefs.SetInt(TotalScoreCount, i);

        Debug.Log("Score Added to prefs");


        ReadScoresFromPlayerPrefs();
    }

    //reads the scores from the player prefs so that they can be ordered
    void ReadScoresFromPlayerPrefs()
    {
        ScoreTable.Clear();

        int numScores = PlayerPrefs.GetInt(TotalScoreCount);

        int j = 0;

        for (int i = 0; i < numScores; i++)
        {
            int score = PlayerPrefs.GetInt(PlayerScore + i);
            string name = PlayerPrefs.GetString(PlayerName + i);

            ScoreTable.Add(name, score);
            j++;
            

        }
        Debug.Log("Scores read");

        //sorts the record in decending order usning LINQ
        IOrderedEnumerable<KeyValuePair<string, int>> list;

        list = from pair in ScoreTable
               orderby pair.Value descending,
               pair.Key
               select pair;

        //adds the ordered records back into the scoreTable
        ScoreTable = list.ToDictionary(x => x.Key, x => x.Value);

        Debug.Log("Scores Ordered");

        RemoveLowScores();

        
    }

    

    string lowestScoreKey;
    int lowestScore = 0;

    //removes the low scores so new ones can be added
    void RemoveLowScores( )
    {
        if (ScoreTable.Count < 5) return;
        foreach (KeyValuePair<string, int> record in ScoreTable)
        {
            //if the lowest score is 0 or less than the previous record
            if ((lowestScore == 0) || (lowestScore > record.Value))
            {
                //set the lowestScore to the value of the record its looking at
                lowestScore = record.Value;
                //set the low score key to the key of the record its looking at
                lowestScoreKey = record.Key;
            }
        }

        //remove the low score from the table
        ScoreTable.Remove(lowestScoreKey);

        Debug.Log("Low Score removed");

    }


    public GUIStyle customGUIStyle;

    void OnGUI()
    {
        int boxYPos2 = 1;
        foreach (KeyValuePair<string, int> record in ScoreTable)
        {
            //loop through the records and display the high scores in GUI boxes
            GUI.Box(new Rect(Screen.width / 2, 100 * boxYPos2 / 2, Screen.width / 2, 50), record.Key + " " + record.Value, customGUIStyle);
            boxYPos2++;
        }
    }
}
