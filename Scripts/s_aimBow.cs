using UnityEngine;

public class s_aimBow : MonoBehaviour {

    //variables
    //how fast the bow will move
    private float speed = 8.0f;

	
	// Update is called once per frame
	void Update ()
    {
        //gets the values given from the phones accelerometer
        Vector3 direction = Input.acceleration;

        direction.Normalize();

        //this moves the bow according to the x and z values of the accelerometer
        //I used z so the phone is upright when using it
        transform.Translate(direction.x * speed * Time.deltaTime, (-direction.z) * speed * Time.deltaTime, 0);

    }
}
