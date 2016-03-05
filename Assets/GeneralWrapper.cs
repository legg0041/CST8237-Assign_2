using UnityEngine;
using System.Collections;

/// <summary>
/// Handles the wrapping of anything other than the player
/// </summary>
public class GeneralWrapper : MonoBehaviour {

    //used to remove a random error
    public Transform thisObject;
    //the collider of this object
    public PolygonCollider2D polyCollider;

    //taken from comment made on wrapping tutorial at http://gamedevelopment.tutsplus.com/articles/create-an-asteroids-like-screen-wrapping-effect-with-unity--gamedev-15055#comment-1507059485
    void OnBecameInvisible()
    {
        //used to disable the collider, this helps the asteroids not collide offscreen and push eachother away from the camera
        polyCollider.enabled = false;
        //get the main camera
        var _camera = Camera.main;
        //get the view of the camera 
        var _view = _camera.WorldToViewportPoint(thisObject.position);
        //get the current position
        var _newPos = transform.position;
        //if its outside of the x bounds
        if(_view.x > 1 || _view.x < 0)
        {
            //set it to the negative of itself
            _newPos.x = -_newPos.x;
        }
        //if its outside of the y bounds
        if(_view.y > 1 || _view.y < 0)
        {
            // set it to negative of itself
            _newPos.y = -_newPos.y;
        }
        //set the new position
        transform.position = _newPos;
    }

    /// <summary>
    /// When it becomes visible again enable the collision again
    /// </summary>
    void OnBecameVisible()
    {
        //turn on collissions
        polyCollider.enabled = true;
    }
}
