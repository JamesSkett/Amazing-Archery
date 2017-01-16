using UnityEngine;
using UnityEngine.SceneManagement;

public class s_chooseRange : MonoBehaviour {


    public void setEasyRange()
    {
        //when the easy button is pressed a 1 is stored in a player pref
        //so when the number of arrows is chosen this can be accessed to knows to load the easy level
        PlayerPrefs.SetInt("Range", 1);

        //load the choose arrow scene
        SceneManager.LoadScene("ChooseArrows");
    }

    public void setHardRange()
    {
        //when the easy button is pressed a 2 is stored in a player pref
        //so when the number of arrows is chosen this can be accessed to knows to load the hard level
        PlayerPrefs.SetInt("Range", 2);

        //load the choose arrow scene
        SceneManager.LoadScene("ChooseArrows");
    }
}
