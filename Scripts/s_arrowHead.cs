using UnityEngine;
using System.Collections;

public class s_arrowHead : MonoBehaviour
{
    //vectors that will hold the position and rotation of objects
    //private because no other class needs to access them
    private Vector3 arrowSpawnPos;
    private Vector3 bowStartPos;
    private Vector3 camStartPos;
    private Vector3 newPos;

    //bools that look to see whether the coroutine is running or not
    private bool isCoroutineExecuting;
    private bool isCoolDownExecuting;

    //variables that will handle the board hit sound
    private AudioSource arrowAudio;
    public AudioClip boardHit;

    private float startMousePos;
    private float lastMousePosY;

    //object of gameSystem so that this class can access the UpdateArrows function
    private s_gameSystem gameSystem;
    private GameObject gameSystemGameObject;

    //bow object so the start position of it can be found
    public GameObject bow;

    public GameObject wind;

    //properties effecting the arrow 
    public float thrust;
    public float pullBackSpeed;

    //the rigidbody for the arrow head
    public Rigidbody rb;

    //this is used to make sure the arrow is only pulled back once per shot
    public bool pullBack = true;



    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        rb = GetComponent<Rigidbody>();

        //finding the positions of the arrow, bow and camera
        arrowSpawnPos = new Vector3(bow.transform.position.x, bow.transform.position.y, bow.transform.position.z + 1);
        bowStartPos = new Vector3(bow.transform.position.x, bow.transform.position.y, bow.transform.position.z);
        camStartPos = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);

        //finds the object that has the gameSystem script attached
        gameSystemGameObject = GameObject.FindGameObjectWithTag("GameSystem");
        gameSystem = gameSystemGameObject.GetComponent<s_gameSystem>();

        //gets the audio source component so that the audio will play when called
        arrowAudio = GetComponent<AudioSource>();

        pullBack = true;

    }

    //update is called once per frame
    void Update()
    {
        //Vector3 arrowPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);

        //checks each update to see if the player has clicked the screen to pull the arrow back
        StartCoroutine( pullArrowBack());

        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

        setThrust();
    }

    public void setThrust()
    {
        //reads the player pref set int he powerBar script to calculate the thrust
        thrust = PlayerPrefs.GetFloat("Power") * 10;

    }

    IEnumerator pullArrowBack()
    {
        //if the player hasnt alerady pulled the arrow back then run the code
        if (pullBack)
        {
                if (Input.GetMouseButtonDown(0) && Input.mousePosition.y < 1600)
                {

                    transform.position = Vector3.Lerp(transform.position, newPos, pullBackSpeed);


                }

                //if the user has removed their finger from the screen fire the arrow
                if (Input.GetMouseButtonUp(0) && Input.mousePosition.y < 1600)
                {
                    //allows the arrow to have force applied
                    rb.isKinematic = false;
                    //stops the arrow being a child of the bow
                    transform.parent = null;
                    //adds a large amount of force to the arrow once like a bow would
                    rb.AddForce(0, 0, thrust);
                    
                    //stops player from being able to pull the arrow back in the air
                    pullBack = false;

                    //starts a delay that will add gravity to the arrow so that it starts to dip
                    StartCoroutine(ExecuteAfterTime(0.6f));
                }
            
            
           
        }
        yield return new WaitForSeconds(1);
    }

    //time delay for gravity
    IEnumerator ExecuteAfterTime(float time)
    {

        if (isCoroutineExecuting)
            yield break;

        isCoroutineExecuting = true;

        yield return new WaitForSeconds(time);

        rb.useGravity = true;


        isCoroutineExecuting = false;

    }

    //time delay to spawn the arrow back at the bow
    IEnumerator coolDown(float time)
    {

        if (isCoolDownExecuting)
            yield break;

        isCoolDownExecuting = true;

        yield return new WaitForSeconds(time);

        gameSystem.UpdateArrows(1);
        resetCamera();
        spawnNewArrow();

        isCoolDownExecuting = false;

    }

    //check collision between the arrow and target
    void OnTriggerEnter(Collider col)
    {

        //stops forces from acting on the arrow
        rb.isKinematic = true;
        //sets thrust back to 0
        thrust = 0;
        //freezes the rotation and position of the arrow so that it look slike it sticks in the target
        rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;

        //plays a sound when the arrow hits the target
        arrowAudio.PlayOneShot(boardHit);

        //finds the game object that the arrow collides with
        GameObject target = col.gameObject;

        //sets that game object to the arrows parent
        //this is so that when it hits the moving target it moves with the target
        //and not floating in mid air
        transform.SetParent(target.transform);

        //starts the cool down delay for respawning the arrow
        StartCoroutine(coolDown(1f));
       
    }

    void spawnNewArrow()
    {
        //moves the arrow back to the starting position
        transform.position = arrowSpawnPos;
        //sets the rotation of the arrow back to the starting rotation
        transform.rotation = Quaternion.identity;
        transform.Rotate(-180, 90, -180);
        //sets the arrow back to a child of the bow
        transform.SetParent(bow.transform);

        //allows the player to take the next shot
        pullBack = true;

        rb.useGravity = false;
        //removes the freeze constraints from the arrow
        rb.constraints = RigidbodyConstraints.None;

    }

    void resetCamera()
    {
        //resets the camera position
        Camera.main.transform.position = camStartPos;

    }

    void resetBow()
    {
        //resets the bow position
        bow.transform.position = bowStartPos;

    }
}
