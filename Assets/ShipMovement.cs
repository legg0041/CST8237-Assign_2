using UnityEngine;
using System.Collections;

//used to control the ship
public class ShipMovement : MonoBehaviour {

    //no longer used
    public float moveSpeed = 10.0f;
    //turn speed
    public float turnSpeed = 5.0f;
    //rigid body
    public Rigidbody2D rigidBody;

    //jet 2D renderers
    public SpriteRenderer bottomLeftJet;
    public SpriteRenderer bottomRightJet;
    public SpriteRenderer leftJet;
    public SpriteRenderer rightJet;
    public SpriteRenderer topLeftJet;
    public SpriteRenderer topRightJet;
	
	// Update is called once per frame
	void Update () {
        //use wasd/arrows to get the direction being pushed
        var _rotation = -Input.GetAxis("Horizontal");
        var _thrust = Input.GetAxis("Vertical");
        //if the player is turning to the right
        if(_rotation < 0)
        {
            //make the jet visible
            leftJet.enabled = true;
        }
        else
        {
            //if the player is turning left
            if (_rotation > 0)
            {
                //make the right jet visible
                rightJet.enabled = true;
            }
            else
            {
                //if nothing is being pressed make all invisible
                rightJet.enabled = false;
                leftJet.enabled = false;
            }
        }
        //if player is thrusting up
        if (_thrust > 0)
        {
            //make bottom jets visible
            bottomLeftJet.enabled = true;
            bottomRightJet.enabled = true;
        }
        else
        {
            //if player is thrusting down
            if (_thrust < 0)
            {
                //make top jets visible
                topLeftJet.enabled = true;
                topRightJet.enabled = true;
            }
            else
            {
                //otherwise make all jets invisible
                topLeftJet.enabled = false;
                topRightJet.enabled = false;
                bottomLeftJet.enabled = false;
                bottomRightJet.enabled = false;
            }
        }
        //add the the rotation to the player
        transform.Rotate(0, 0, _rotation * turnSpeed);
        //add the force to the player
        rigidBody.AddForce(transform.up * _thrust);
    }
}
