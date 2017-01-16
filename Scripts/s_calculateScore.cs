using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class s_calculateScore : MonoBehaviour {

    public GameObject arrowHead;

    private s_gameSystem gameSystem;
    private GameObject gameSystemGameObject;

    Vector3 targetPos;
    Vector3 arrowPos;

    public GameObject PopUpText;
    public Text scorePopupText;
    private int TempScore;

    private bool isCoroutineExecuting;

    // Use this for initialization
    void Start ()
    {
        //finds the object that has the gameSystem script attached
        gameSystemGameObject = GameObject.FindGameObjectWithTag("GameSystem");
        gameSystem = gameSystemGameObject.GetComponent<s_gameSystem>();

        //disables scrore popup UI when the game starts
        PopUpText.SetActive(false);
    }
	

    //when the collision happens get the positions of both the bow and the 
    //target then calculate the distance between them
    void OnTriggerEnter()
    {
        targetPos = new Vector2(transform.position.x, transform.position.y);
        arrowPos = new Vector2(arrowHead.transform.position.x, arrowHead.transform.position.y);

        float distance = Vector2.Distance(targetPos, arrowPos);

        //calls the calculate score function and passes in the distance 
        calculateScore(distance);


    }

    //calculates the score based on the distance away 
    //from the center of the target game object
    void calculateScore(float distance)
    {
        //if the Yellow is hit 50 points
        if(distance < 0.2)
        {
            TempScore = 50;
            PopUpText.SetActive(true);
            scorePopupText.text = TempScore.ToString();
            gameSystem.UpdateScore(50);

            StartCoroutine(ExecuteAfterTime(1));

        }
        //if the red is hit 40 points
        else if(distance < 0.3 && distance > 0.2)
        {
            TempScore = 40;
            PopUpText.SetActive(true);
            scorePopupText.text = TempScore.ToString();
            gameSystem.UpdateScore(40);
        }
        //if the blue is hit 30 points
        else if (distance < 0.53 && distance > 0.3)
        {
            TempScore = 30;
            PopUpText.SetActive(true);
            scorePopupText.text = TempScore.ToString();
            gameSystem.UpdateScore(30);

            StartCoroutine(ExecuteAfterTime(1));

        }
        //if the black is hit 20 points
        else if (distance < 0.74 && distance > 0.53)
        {
            TempScore = 20;
            PopUpText.SetActive(true);
            scorePopupText.text = TempScore.ToString();
            gameSystem.UpdateScore(20);

            StartCoroutine(ExecuteAfterTime(1));

        }
        //if the white is hit 10 points
        else if (distance > 0.74)
        {
            TempScore = 10;
            PopUpText.SetActive(true);
            scorePopupText.text = TempScore.ToString();
            gameSystem.UpdateScore(10);

            StartCoroutine(ExecuteAfterTime(1));

        }
    }


    //disables the pop up text after a small amount of time
    //called after the score is calculated
    IEnumerator ExecuteAfterTime(float time)
    {

        if (isCoroutineExecuting)
            yield break;

        isCoroutineExecuting = true;

        yield return new WaitForSeconds(time);

        PopUpText.SetActive(false);


        isCoroutineExecuting = false;

    }
}
