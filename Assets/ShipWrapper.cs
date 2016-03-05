using UnityEngine;
using System.Collections;

/// <summary>
/// Wraps the player around the screen using false duplicates placed evenly outside of view of the camera.
/// I came up with the idea to use the false players during the paralax lab and search for an example.
/// Thus I used this tutorial to employ my idea http://gamedevelopment.tutsplus.com/articles/create-an-asteroids-like-screen-wrapping-effect-with-unity--gamedev-15055
/// </summary>
public class ShipWrapper : MonoBehaviour {
    //the copyplayer prefabs made
    public GameObject shipCopy;
    //the widtha and height of the screen
    private float _screenWidth;
    private float _screenHeight;
    // bottom left and top right corners of screen
    private Vector3 _sBottomLeft;
    private Vector3 _sTopRight;
    //number of copies to make
    private static int _numCopies = 8;
    //array of copies
    private GameObject[] _copies = new GameObject[_numCopies];
    
	// Use this for initialization
	void Start () {
        //get he main camera
        var _camera = Camera.main;
        //set the points of the camera
        _sBottomLeft = _camera.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z));
        _sTopRight = _camera.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z));
        //set the screen width/height
        _screenWidth = _sTopRight.x - _sBottomLeft.x;
        _screenHeight = _sTopRight.y - _sBottomLeft.y;
        //create the player copies
        CreateShipCopies();
    }

    // Update is called once per frame
    void Update () {
        //switch ships if needed
        SwitchShips();
    }

    //no longer used
    void OnBecameInvisible()
    {
        //SwitchShips();
    }

    //crate 8 cop[es of the player
    void CreateShipCopies()
    {
        //create 8 copies
        for(int i = 0; i < _numCopies; ++i)
        {
            //create copies and place in the array
            _copies[i] = Instantiate(shipCopy, Vector3.zero, Quaternion.identity) as GameObject;
        }
        //position the copies
        PositionCopyShips();
    }

    /// <summary>
    /// Positions the copies evenly outside the view of the camera
    /// </summary>
    void PositionCopyShips()
    {
        //used for each copy position
        var _copyPosition = transform.position;

        //far right
        _copyPosition.x = transform.position.x + _screenWidth;
        _copyPosition.y = transform.position.y;
        _copies[0].transform.position = _copyPosition;

        //bottom right
        _copyPosition.x = transform.position.x + _screenWidth;
        _copyPosition.y = transform.position.y - _screenHeight;
        _copies[1].transform.position = _copyPosition;

        //bottom
        _copyPosition.x = transform.position.x;
        _copyPosition.y = transform.position.y - _screenHeight;
        _copies[2].transform.position = _copyPosition;

        //bottom left
        _copyPosition.x = transform.position.x - _screenWidth;
        _copyPosition.y = transform.position.y - _screenHeight;
        _copies[3].transform.position = _copyPosition;

        //left
        _copyPosition.x = transform.position.x - _screenWidth;
        _copyPosition.y = transform.position.y;
        _copies[4].transform.position = _copyPosition;

        //top left
        _copyPosition.x = transform.position.x - _screenWidth;
        _copyPosition.y = transform.position.y + _screenHeight;
        _copies[5].transform.position = _copyPosition;

        //top
        _copyPosition.x = transform.position.x;
        _copyPosition.y = transform.position.y + _screenHeight;
        _copies[6].transform.position = _copyPosition;

        //top right
        _copyPosition.x = transform.position.x + _screenWidth;
        _copyPosition.y = transform.position.y + _screenHeight;
        _copies[7].transform.position = _copyPosition;

        //match their rotation with the player
        for(int i = 0; i < 8; ++i)
        {
            _copies[i].transform.rotation = transform.rotation;
        }
    }

    /// <summary>
    /// Switches the player with the visible copy and repositions the copies around its new position
    /// </summary>
    void SwitchShips()
    {
        //check each copy in array
        foreach(var copy in _copies)
        {
            // the copy is within the camera
            if (copy.transform.position.x < _sTopRight.x &&
            copy.transform.position.x > _sBottomLeft.x &&
            copy.transform.position.y < _sTopRight.y &&
            copy.transform.position.y > _sBottomLeft.y)
            {
                //move the player their
                transform.position = copy.transform.position;
                break;
            }
        }
        //position the copy ships around players new position
        PositionCopyShips();
    }
}
