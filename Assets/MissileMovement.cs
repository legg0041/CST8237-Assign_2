using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Handles the missiles fired by the player
/// </summary>
public class MissileMovement : MonoBehaviour {

    //the missiles rigidbody
    public Rigidbody2D rigidBody;
    //speed of missile
    public float bulletSpeed = 100.0f;
    //change to test
    public float publicLife = 3.0f;

    //UI of score
    private Text _scoreText;
    //points values
    private int _addPoints = 100;
    private int _totalPoints = 0;
    //time to stay alive
    private float _privateLife;

    // Use this for initialization
    void Start () {
        //how long the missile is alive for
        _privateLife = publicLife;
        //add force to the missile pointing up
        rigidBody.AddForce(transform.up * bulletSpeed);
    }
	
	// Update is called once per frame
	void Update () {
        //life idea taken from https://www.youtube.com/watch?v=WZEC-pYTnTY
        //use the time passed to subtract from its life
        _privateLife -= Time.deltaTime;
        //when it hits zero
        if(_privateLife < 0)
        {
            //delete the missile
            Destroy(gameObject);
        }
	}

    /// <summary>
    /// Triggered on collision with an object
    /// </summary>
    /// <param name="coll">
    /// the collision that has taken place
    /// </param>
    void OnTriggerEnter2D(Collider2D coll)
    {
        //if it collides with an asteroid or a mini asteroid
        if (coll.name.StartsWith("Asteroid") || coll.name.StartsWith("MiniAsteroid"))
        {
            //get the current points and parse them
            _scoreText = GameObject.Find("scoreText").GetComponent<Text>();
            _totalPoints = int.Parse(_scoreText.text);
            //add the new points on
            _totalPoints += _addPoints;
            //set the string
            _scoreText.text = _totalPoints.ToString("000000000000000000000");
            //destroy the missile
            Destroy(gameObject);
        }
    }
}
