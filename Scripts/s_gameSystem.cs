using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class s_gameSystem : MonoBehaviour {

    //object of s_highScores so that this class can access the Add player score function
    private s_HighScores highScoresObject;
    private GameObject highScoresGameObject;

    //object of s_buttonHandler so that this class can access the UI objects
    private s_buttonHandler buttonHandler;
    private GameObject buttonHandlerGameObject;

    //game object that holds the high scores text
    public GameObject HighScoresUI;
    public Text playerName;


    public Text scoreText;
    public Text arrowsLeftText;
    public Text timeLeftText;

    private float timeLeft; 
    public int arrowsLeft;
    private int score = 0;


	// Use this for initialization
	void Start ()
    {
        //sets the number of arrows to the value stored in the player pref 
        arrowsLeft = PlayerPrefs.GetInt("Arrows");

        HighScoresUI.SetActive(false);

        //chnages the time based on how many arrows you have chosen
        switch (arrowsLeft)
        {
            case 5:
                timeLeft = 30; //30 seconds
                break;
            case 10:
                timeLeft = 60; //60 seconds
                break;
            case 15:
                timeLeft = 90; //90 seconds
                break;
            case 20:
                timeLeft = 120; //120 seconds
                break;
        }

        //starts by displaying the score and arrows left on the screen
        scoreText.text = "Score: " + score.ToString();
        arrowsLeftText.text = "Arrows Left: " + arrowsLeft.ToString();

        //finds the object that has the highScores script attached
        highScoresGameObject = GameObject.FindGameObjectWithTag("HighScores");
        highScoresObject = highScoresGameObject.GetComponent<s_HighScores>();

        //finds the object that has the buttonHandler script attached
        buttonHandlerGameObject = GameObject.FindGameObjectWithTag("Button");
        buttonHandler = buttonHandlerGameObject.GetComponent<s_buttonHandler>();
    }

    void Update()
    {
        //if the value stored in the player pref is 0
        //get the time set in the start function and start to count down
        if (PlayerPrefs.GetInt("Endless") == 0)
        {
            timeLeft -= Time.deltaTime;
            timeLeftText.text = "Time Left:  " + (int)timeLeft;
        }
        else
        //else have unlimited time to shoot arrows
        {
            timeLeftText.text = "Time Left:  " + "UNLIMITED";
        }

        //if you run out of arrow or you run out of time
        if (arrowsLeft == 0 || timeLeft <= 0)
        {
            //send the score to a player pref so it can be added to the high score
            PlayerPrefs.SetInt("Score", score);

            //stop the player from being able to play the game
            Time.timeScale = 0;

            //display the high score UI and disable the other UI elements
            HighScoresUI.SetActive(true);
            buttonHandler.gameUI.SetActive(false);
            buttonHandler.pauseMenu.SetActive(false);
        }
    }

    public void UpdateScore(int newScore)
    {
        //add the new score to the old score
        score += newScore;
        //update the score text on the screen
        UpdateUI();
    }

    public void UpdateArrows(int removeArrow)
    {
        //takes away 1 from the arrowsleft variable
        arrowsLeft -= removeArrow;
        //update the arrows left text on the screen
        UpdateUI();
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score.ToString();
        arrowsLeftText.text = "Arrows Left: " + arrowsLeft.ToString();
    }

    public void sendHighScore()
    {
        string name;
        //gets the player name from the text input box and stores it in name
        name = playerName.text;

        //send the player name to the high scores script
        highScoresObject.AddPlayerScore(name);
    }

    


}
