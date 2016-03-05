using UnityEngine;
using System.Collections;

/// <summary>
/// Handles the movement of the asteroids
/// </summary>
public class AsteroidMovement : MonoBehaviour {

    //the asteroids rigidbody
    public Rigidbody2D rigidBody;
    //speed of the asteroid
    public float asteroirdSpeed = 25.0f;
    //vector to hold the random force 
    private Vector2 _randForce;
    //position of the bottom left of the camera
    private Vector3 _sBottomLeft;
    //position of the top right of the camera
    private Vector3 _sTopRight;

    // Use this for initialization
    void Start () {
        //get the main camera
        var _camera = Camera.main;
        //set the two corners of the screen
        _sBottomLeft = _camera.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z));
        _sTopRight = _camera.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z));
        //add the random force and rotation
        RandomRotation();
        RandomForce();
    }

    /// <summary>
    /// Adds random rotation to the asteroid
    /// </summary>
    void RandomRotation()
    {
        // holds the random torque
        int _randTorque;
        //generate a random number
        if (Random.Range(0, 2) == 0)
        {
            //if 0 use this change
            _randTorque = Random.Range(-11, -5);
        }
        else
        {
            //if 1 use this range
            _randTorque = Random.Range(5, 10);
        }
        //apply the random torque
        rigidBody.AddTorque(_randTorque, ForceMode2D.Force);
    }

    /// <summary>
    /// Apply a random force on the asteroid depending on its location. This makes it always spin into view of the camera
    /// </summary>
    void RandomForce()
    {
        //ming nad max x values
        int minX;
        int maxX;
        //min and max y values
        int minY;
        int maxY;
        //if its below the bottom right of the camera use x values that will send it to the right
        if (transform.position.x < _sBottomLeft.x)
        {
            minX = 1;
            maxX = 3;
        }
        else
        {
            //if its to the right of the top right then send it to the left
            if (transform.position.x > _sTopRight.x)
            {
                minX = -2;
                maxX = 0;
            }
            else
            {
                //else send it left or right
                minX = -2;
                maxX = 3;
            }
        }
        //if its below the bottom left point send it upwards
        if (transform.position.y < _sBottomLeft.y)
        {
            minY = 1;
            maxY = 3;
        }
        else
        {
            //if its above the top right send it down
            if (transform.position.y > _sTopRight.y)
            {
                minY = -2;
                maxY = 0;
            }
            else
            {
                //else send it up or down
                minY = -2;
                maxY = 3;
            }
        }

        //generate random x and y values based on the set values
        _randForce.x = Random.Range(minX, maxX);
        _randForce.y = Random.Range(minY, maxY);
        //apply the random force
        rigidBody.AddForce(_randForce * asteroirdSpeed);
    }
}
