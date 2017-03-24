/**Date Created: 3/14/17
 *Last Modified: 3/14/17
 * 
 * Disables a tile, optionally after a certain condition is reached (time pass,  collision, etc)
 * -Rishi Parida
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDisablerTimeRandom : MonoBehaviour {

    //The tile which will be disabled after a certain time
    //public Tile TileToDisable;


    //Whether or not the game object will destroy after the tile is destroyed
    public bool destroyImmediately;
    //The tile which will be disabled
    public GameObject TilesEnemy;

    bool isDisabling = false;

    public void DisableTile() {
        if(!isDisabling)
            StartCoroutine(delayDisable());
    }

    public float disableTime;

    IEnumerator delayDisable() {
        isDisabling = true;
        Debug.Log("Delay disabled");
        yield return new WaitForSeconds(disableTime);
        //TileToDisable.disable();
        TilesEnemy.transform.GetChild(Random.Range(0, TilesEnemy.transform.childCount-1)).GetComponent<Tile>().disable();

        if (destroyImmediately)
            Destroy(gameObject);
        else
            DisableTile();
        isDisabling = false;
    }

}
