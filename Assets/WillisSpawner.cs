using UnityEngine;
using System.Collections;

/// <summary>
/// Handles the spawning of Bruceteroids
/// </summary>
public class WillisSpawner : MonoBehaviour {


    //bottom left point of the screen space
    private Vector3 _sBottomLeft;
    //top right crner of the screen space
    private Vector3 _sTopRight;

    //time from start until it runs again
    public float untilRun = 0.1f;
    //time in seconds to repeat function call
    public float repeatEvery = 3f;
    public GameObject sadWillis;

    // Use this for initialization
    void Start () {
        //get main camera
        var _camera = Camera.main;
        //set bottom and top corner of screens
        _sBottomLeft = _camera.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z));
        _sTopRight = _camera.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z));
        //repeat function every set seconds
        InvokeRepeating("SpawnWillis", untilRun, repeatEvery);
    }

    /// <summary>
    /// Spawns the Bruceteroids outside of the screen
    /// Screen is thought of as center position of a 3x3 grid
    /// </summary>
    void SpawnWillis()
    {
        //clear out any stray bruceteroids
        ClearStrayWillis();
        //generate random number between 0-2
        int _randX = Random.Range(0, 3);
        // to be used to generate y
        int _randY = 0;
        //if the x is center column
        if (_randX == 1)
        {
            //random 0 or 1
            if (Random.Range(0, 2) == 0)
            {
                //set first row
                _randY = 0;
            }
            else
            {
                //set as third row
                _randY = 2;
            }
        }
        else
        {
            //otherwise set it to be column 0-2
            _randY = Random.Range(0, 3);
        }

        //check x value to then generate value accordingly in each column
        switch (_randX)
        {
            case 0:
                _randX = Random.Range((int)(_sBottomLeft.x - 5), (int)(_sBottomLeft.x - 1));
                break;
            case 1:
                _randX = Random.Range((int)(_sBottomLeft.x), (int)(_sTopRight.x));
                break;
            case 2:
                _randX = Random.Range((int)(_sTopRight.x + 1), (int)(_sTopRight.x + 5));
                break;
        }
        //check y value to then generate value accordingly in each row
        switch (_randY)
        {
            case 0:
                _randY = Random.Range((int)(_sTopRight.y + 1), (int)(_sTopRight.y + 5));
                break;
            case 1:
                _randY = Random.Range((int)(_sBottomLeft.y), (int)(_sTopRight.y));
                break;
            case 2:
                _randY = Random.Range((int)(_sBottomLeft.y - 5), (int)(_sBottomLeft.y - 1));
                break;
        }
        //use the generated values to create the new vector
        Vector2 randomVector = new Vector2(_randX, _randY);
        //instantiate bruceteroid
        Instantiate(sadWillis, randomVector, Quaternion.identity);
    }

    /// <summary>
    /// clears the stray bruceteroids that go outside of a certain area off screen
    /// </summary>
    void ClearStrayWillis()
    {
        //create a list of all bruceteroids
        var tempLargeAsteroidList = GameObject.FindGameObjectsWithTag("Bruceteroid");
        //loop through each gameobject
        foreach (GameObject ast in tempLargeAsteroidList)
        {
            //if its outside the the screen too far
            if (ast.transform.position.x > (_sTopRight.x + 5) ||
                ast.transform.position.x < (_sBottomLeft.x - 5) ||
                ast.transform.position.y > (_sTopRight.y + 5) ||
                ast.transform.position.y < (_sBottomLeft.y - 5))
            {
                //destroy the bruceteroid
                Destroy(ast);
            }
        }
    }
}
