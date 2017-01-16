using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class s_menuButtons : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //loads the level select scene when the play game button is pressed
    public void playButton()
    {
        SceneManager.LoadScene("Level Select");
    }

    //loads the main menu scene when the back button in the level select menu is pressed
    public void retrunToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    //loads the controls help screen when the controls button is pressed
    public void loadControls()
    {
        SceneManager.LoadScene("Controls");
    }

    //loads the level select screen when the back button is pressed in the choose arrow scene
    //because level select is the previous scene
    public void back()
    {
        SceneManager.LoadScene("Level Select");
    }
}
