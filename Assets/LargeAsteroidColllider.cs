using UnityEngine;
using System.Collections;

/// <summary>
/// Handles the collisions on the large asteroid and spawning mini ones in its position
/// </summary>
public class LargeAsteroidColllider : MonoBehaviour {

    //the small asteroid to spawn on destruction
    public GameObject miniAsteroid;

    /// <summary>
    /// Triggered on the event of a collision
    /// </summary>
    /// <param name="coll">
    /// The collision thats taken place
    /// </param>
    void OnTriggerEnter2D(Collider2D coll)
    {
        //if it collided with a missile
        if (coll.name.StartsWith("Missile"))
        {
            //get the position of the asteroid
            var firstPos = transform.position;
            //create new position to the upper left of asteroid
            firstPos.x -= 0.5f;
            firstPos.y += 0.5f;
            //create a second position
            var secondPos = transform.position;
            //create a new position to the bottom right
            secondPos.x += 0.5f;
            secondPos.y -= 0.5f;
            //create two mini asteroids
            Instantiate(miniAsteroid, firstPos, transform.rotation);
            Instantiate(miniAsteroid, secondPos, transform.rotation);
            //destroy large asteroid
            Destroy(gameObject);
        }
        else
        {
            //on collision with player simply delete the asteroid
            if (coll.name.StartsWith("PlayerShip"))
            {
                //destroy large asteroid
                Destroy(gameObject);
            }
        }
    }
}
