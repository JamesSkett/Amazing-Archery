using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class s_powerBar : MonoBehaviour {


    private Vector3 mouseStartPos;
    private Vector3 distance;
    private bool hasClicked = false;

    public Text powerText;
	
	// Update is called once per frame
	void Update ()
    {
        //if finger has touched the screen
        if(Input.GetMouseButton(0))
        {
            if (hasClicked == false)
            {
                // gets the pos of when first touched screen
                mouseStartPos = Input.mousePosition;
                //stops the position from being updated again
                hasClicked = true;
            }

            //calulates the distance between the first touch position and the current posistion
            distance = mouseStartPos - Input.mousePosition;
            //diaplays the power on the screen
            updatePower(distance.y);
            //stores the distance in a player prefab so it can be used to calculate the thrust in another script
            PlayerPrefs.SetFloat("Power", distance.y);

        }

        //if finger has been released 
        if(Input.GetMouseButtonUp(0))
        {
            //reset the distance
            distance = new Vector3(0, 0, 0);
            //updates the power on the screen
            updatePower(distance.y);
            //allow the start finger pos to be found again
            hasClicked = false;
        }

    }

    void updatePower(float distance)
    {
        //displays the power on the game UI
        //distance is cast to an int so that it can only be whole numbers
        powerText.text = "Power: " + (int)distance;

    }
}
