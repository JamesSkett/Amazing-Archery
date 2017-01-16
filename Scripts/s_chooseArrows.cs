using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class s_chooseArrows : MonoBehaviour {

    //gets the toggle object in scene
    public Toggle Endless;

    public void fiveArrows()
    {
        //stores the amount of arrows in a playerPref so that it can be accessed by the gameSystem class
        PlayerPrefs.SetInt("Arrows", 5);

        
        if (Endless.isOn == true)
        {
            //if the endless toggle is checked the pass a 1 into the playerPref
            //when this player pref is accessed it can tell the level to be endless
            PlayerPrefs.SetInt("Endless", 1);
        }
        else
            //else pass a 1 into the playerPref
            //when this player pref is accessed it can tell the level to be times
            PlayerPrefs.SetInt("Endless", 0);


        //if the player pref "Range" is 1 then it will load up the easy level
        if (PlayerPrefs.GetInt("Range") == 1)
        {
            SceneManager.LoadScene("Game");
        }
        else
            //else it will load the hard level
            SceneManager.LoadScene("HardLevel");
    }

    //the rest of the functions work in the same way as the first just with different arrow amounts
    public void tenArrows()
    {
        PlayerPrefs.SetInt("Arrows", 10);

        if (Endless.isOn == true)
        {
            PlayerPrefs.SetInt("Endless", 1);
        }
        else
            PlayerPrefs.SetInt("Endless", 0);

        if (PlayerPrefs.GetInt("Range") == 1)
        {
            SceneManager.LoadScene("Game");
        }
        else
            SceneManager.LoadScene("HardLevel");

    }

    public void fifteenArrows()
    {
        PlayerPrefs.SetInt("Arrows", 15);

        if (Endless.isOn == true)
        {
            PlayerPrefs.SetInt("Endless", 1);
        }
        else
            PlayerPrefs.SetInt("Endless", 0);

        if (PlayerPrefs.GetInt("Range") == 1)
        {
            SceneManager.LoadScene("Game");
        }
        else
            SceneManager.LoadScene("HardLevel");

    }

    public void twentyArrows()
    {
        PlayerPrefs.SetInt("Arrows", 20);

        if (Endless.isOn == true)
        {
            PlayerPrefs.SetInt("Endless", 1);
        }
        else
            PlayerPrefs.SetInt("Endless", 0);

        if (PlayerPrefs.GetInt("Range") == 1)
        {
            SceneManager.LoadScene("Game");
        }
        else
            SceneManager.LoadScene("HardLevel");

    }
}
