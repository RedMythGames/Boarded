/**Date Created: 3/14/17
 *Last Modified: 3/14/17
 * 
 * Disables a tile, optionally after a certain condition is reached (time pass,  collision, etc)
 * -Rishi Parida
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDisablerTime : TileDisablerBase {

    //The tile which will be disabled after a certain time
    //public Tile TileToDisable;

    public override void DisableTile() {
        Debug.Log("Disabled tiles");
        StartCoroutine(delayDisable());
    }

    public float disableTime = 0;

    IEnumerator delayDisable() {
        Debug.Log("Delay disabled");
        yield return new WaitForSeconds(disableTime);
        TileToDisable.disable();

        if(destroyImmediately)
            Destroy(gameObject);
    }

}
