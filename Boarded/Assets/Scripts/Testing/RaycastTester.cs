/** Date Created: 3/14/17
 * Last Modified: 3/20/17
 *
 * Used to test
 * -Rishi Parida
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTester : MonoBehaviour {
    private GameObject tileToReach;
    public Vector3 rotation;

    public void setTile(GameObject newTile) {
        tileToReach = newTile;
        GetComponent<Rigidbody2D>().velocity = new Vector2(-5f, 0);


        transform.rotation = Quaternion.Euler(rotation);
    }

    void OnTriggerEnter2D(Collider2D col) {
        Tile collisionTile = col.GetComponent<Tile>();
        if (collisionTile != null) {
            if (!collisionTile.getActive())
                Destroy(gameObject);
            if (col.gameObject == tileToReach) {
                collisionTile.disable();
                Destroy(gameObject);
            }
        }

    }

    void OnDestroy() { Debug.Log("DESTROYED"); }
}
