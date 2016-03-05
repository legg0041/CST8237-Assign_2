using UnityEngine;
using System.Collections;

/// <summary>
/// Handles collisions on the smaller asteroids
/// </summary>
public class MiniAsteroidCollider : MonoBehaviour {

    /// <summary>
    /// Triggerd on the event of a collision
    /// </summary>
    /// <param name="coll">
    /// The collision that has taken place
    /// </param>
    void OnTriggerEnter2D(Collider2D coll)
    {
        //if it collids with the player or a missile delete the object
        if (coll.name.StartsWith("Missile") || coll.name.StartsWith("PlayerShip"))
        {
            //destroy large asteroid
            Destroy(gameObject);
        }
    }
}
