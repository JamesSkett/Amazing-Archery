using UnityEngine;
using System.Collections;

public class s_moveTarget : MonoBehaviour
{

    private bool dirRight = true;
    private float speed = 2.0f;

    void Update()
    {
        //if the target wants to move in the right direction
        if (dirRight)
            //move it right
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        else
            //else move it left
            transform.Translate(-Vector2.right * speed * Time.deltaTime);

        //if the target gets to 1.0f on the x axis
        if (transform.position.x >= 1.0f)
        {
            //stop it from moving right
            dirRight = false;
        }

        //if the target gets to -4 on the x axis
        if (transform.position.x <= -4)
        {
            //stop it from moving left
            dirRight = true;
        }
    }
}
