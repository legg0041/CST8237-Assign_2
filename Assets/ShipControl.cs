using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the overall game
/// </summary>
public class ShipControl : MonoBehaviour {

    //the asteroid gameobject
    public GameObject asteroidPrefab;
    //the mini asteroid game object
    public GameObject miniAsteroidPrefab;

    //the three life icons
    public GameObject oneLife;
    public GameObject twoLife;
    public GameObject threeLife;
    //the missile prefab to fire
    public GameObject missilePrefab;
    //when to start calling the spawner
    public float untilRun = 0.1f;
    //how often to call it after starting
    public float repeatEvery = 3f;
    //the bottom left of the screen
    private Vector3 _sBottomLeft;
    //the top right of the screen
    private Vector3 _sTopRight;
    //the length of time between shots
    private float _shotTime = 0.5f;
    //use to check when the next shot can be fired
    private float _nextShotTime = 0.0f;
    //total number of lives
    private int _livesLeft = 3;
    //max number of large asteroids at one time
    private int _maxAsteroids = 5;

    // Use this for initialization
    void Start()
    {
        //get main camera
        var _camera = Camera.main;
        //set corners
        _sBottomLeft = _camera.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z));
        _sTopRight = _camera.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z));
        //startin spawning asteroids
        InvokeRepeating("SpawnAsteroid", untilRun, repeatEvery);
    }
	
	// Update is called once per frame
	void Update ()
    {
        //check for space key as well as if next shot can be fired yet
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= _nextShotTime)
        {
            //create missile
            Instantiate(missilePrefab, transform.position, transform.rotation);
            //set time of next shot
            _nextShotTime = Time.time + _shotTime;
        }
    }
    
    /// <summary>
    /// Spawns the asteroids
    /// </summary>
    void SpawnAsteroid()
    {
        //clear useless asteroids before counting and spawning
        ClearStrayAsteroid();
        //count current asteroids to stop spawning if there are too many
        List<GameObject> tempLargeAsteroidList = new List<GameObject>(GameObject.FindGameObjectsWithTag("L_Asteroid"));
        var counter = tempLargeAsteroidList.Count;
        if (counter >= _maxAsteroids)
        {
            return;
        }
        
        //act as if the camera is the center of a 3x3 grid
        //get random x between 0-2
        int _randX = Random.Range(0, 3);
        int _randY = 0;
        //if its the middle column
        if(_randX == 1)
        {
            // only allow y to be the first row or last randomly
            if (Random.Range(0, 2) == 0)
            {
                _randY = 0;
            }
            else
            {
                _randY = 2;
            }           
        }
        else
        {
            //else it can be any row
            _randY = Random.Range(0, 3);
        }
        //set the x value according to the random column generated above
        switch (_randX)
        {
            case 0: //to the left of the bottom left corner
                _randX = Random.Range((int)(_sBottomLeft.x - 5), (int)(_sBottomLeft.x -1));
                break;
            case 1: //between the left and right sides of the camera
                _randX = Random.Range((int)(_sBottomLeft.x), (int)(_sTopRight.x));
                break;
            case 2: //to the right of the right corner
                _randX = Random.Range((int)(_sTopRight.x + 1), (int)(_sTopRight.x + 5));
                break;
        }
        //set the y value according to the random row generated above
        switch (_randY)
        {
            case 0: //set it above the top of the screen
                _randY = Random.Range((int)(_sTopRight.y + 1 ), (int)(_sTopRight.y + 5));
                break;
            case 1: //between the top and bottom of the camera
                _randY = Random.Range((int)(_sBottomLeft.y), (int)(_sTopRight.y));
                break;
            case 2: //below the bottom fo the camera
                _randY = Random.Range((int)(_sBottomLeft.y - 5), (int)(_sBottomLeft.y - 1));
                break;
        }
        //create the vector using the values generated
        Vector2 randomVector = new Vector2(_randX, _randY);
        //create the object
        Instantiate(asteroidPrefab, randomVector, Quaternion.identity);

    }
    
    /// <summary>
    /// Triggered onthe event of a collision
    /// </summary>
    /// <param name="coll">
    /// The collision tha has occurred
    /// </param>
    void OnTriggerEnter2D(Collider2D coll)
    {
        //if its collided with either type of asteroid
        if (coll.name.StartsWith("Asteroid") || coll.name.StartsWith("MiniAsteroid"))
        {
            //subtract a life
            _livesLeft -= 1;
            //disable the appropraite icon
            switch (_livesLeft)
            {
                case 2:
                    threeLife.SetActive(false);
                    break;
                case 1:
                    twoLife.SetActive(false);
                    break;
                case 0:
                    oneLife.SetActive(false);
                    break;
            }
            //if no lives left
            if(_livesLeft == 0)
            {
                //load the failure screen
                SceneManager.LoadScene("Failure");
            }
        }
    }

    /// <summary>
    /// Used to clear asteroids that were foreced out of the screen and aren't going to come back
    /// </summary>
    void ClearStrayAsteroid()
    {
        //if the large asteroid is outside of a certain area of the camera just destroy it
        var tempLargeAsteroidList = GameObject.FindGameObjectsWithTag("L_Asteroid");
        foreach (GameObject ast in tempLargeAsteroidList)
        {
            if( ast.transform.position.x > (_sTopRight.x + 5) ||
                ast.transform.position.x < (_sBottomLeft.x - 5) ||
                ast.transform.position.y > (_sTopRight.y + 5) ||
                ast.transform.position.y < (_sBottomLeft.y - 5))
            {
                Destroy(ast);
            }
        }
        //if the mini asteroid is outside of a certain area of the camera just destroy it
        var tempMiniAsteroidList = GameObject.FindGameObjectsWithTag("M_Asteroid");
        foreach (GameObject ast in tempMiniAsteroidList)
        {
            if (ast.transform.position.x > (_sTopRight.x + 5) ||
                ast.transform.position.x < (_sBottomLeft.x - 5) ||
                ast.transform.position.y > (_sTopRight.y + 5) ||
                ast.transform.position.y < (_sBottomLeft.y - 5))
            {
                Destroy(ast);
            }
        }
    }
}
