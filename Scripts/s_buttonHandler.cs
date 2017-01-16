using UnityEngine;
using UnityEngine.SceneManagement;

public class s_buttonHandler : MonoBehaviour {

    public GameObject pauseMenu;
    public GameObject gameUI;

    private s_arrowHead arrowHead;
    private GameObject arrowHeadGameObject;

	// Use this for initialization
	void Start ()
    {
        //disables the pause menu UI so only game UI can be seen
        pauseMenu.SetActive(false);
        gameUI.SetActive(true);

        //finds the object that has the gameSystem script attached
        arrowHeadGameObject = GameObject.FindGameObjectWithTag("ArrowHead");
        arrowHead = arrowHeadGameObject.GetComponent<s_arrowHead>();
    }


    
    //when the pause button is pressed set the time scale to 0 so everything stops
    public void pauseGame()
    {
        Time.timeScale = 0;
        //activates the pause menu UI so it can be seen 
        pauseMenu.SetActive(true);
        //disables the GameUI so it doesnt get in the way of the pause menu
        gameUI.SetActive(false);
        //stops the player from being able to pull the bow back while paused
        arrowHead.pullBack = false;

    }

    //sets the time scale back to 1 when the resume button is pressed
    //game can then be played again
    public void unPause()
    {
        Time.timeScale = 1;
        //stops the pause menu from being seen
        pauseMenu.SetActive(false);
        //activates the game UI when the game gets going again
        gameUI.SetActive(true);
        //allows the user to shoot arrow again
        arrowHead.pullBack = true;

    }

    //if the main menu button is pressed load the main menu screen
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

}
