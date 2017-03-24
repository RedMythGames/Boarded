/**Date Created: 3/12/17
 *Last Modified: 3/18/17
 * 
 * The script for the tiles that a player or enemy will move on. The purpose (as of now) is simply to allow the character to move, or not move, depending on whether or not the tile is active.
 * -Rishi Parida
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    //Whether or not the tile is active (can be stood on by a character)
    [SerializeField]
    private bool isActive = true;
    //Returns the value of is active
    public bool getActive() { return isActive; }

    //How long the tile is disabled for and the transparency it turns to when it is disabled
    [SerializeField]
    private float disableTime = 5, tileDisableAlpha = 0.4f;

    //Whether or not the tile is undergoing the process of becoming active
    private bool enabling = false;

    //Disables the tile, then after disableTime seconds enables itself
    public void disable() {
        if(!enabling)
            StartCoroutine(enableTimed(disableTime));
    }

    //The tile will flash, showing the disabled effect for disableFlashTime seconds, then showing the normal tile for disableFlashRevoery seconds before disabling
    //[SerializeField]
    //float disableFlashTime, disableFlashRecovery;
    
    IEnumerator enableTimed(float enableTime) {
        //Makes sure that the coroutine can't be called while it's already running
        enabling = true;

        //The transparency of the tile before it's disabled, will be used to return the tile to normal
        float tempTileAlpha = gameObject.GetComponent<SpriteRenderer>().color.a;

        /*setTileAlpha(tileDisableAlpha);
        yield return new WaitForSeconds(disableFlashTime);
        setTileAlpha(tempTileAlpha);
        yield return new WaitForSeconds(disableFlashRecovery);*/

        //Makes the player unable to access the tile anymore
        isActive = false;

        //Sets the tile's sprite to be more transparent, then wait for enableTime seconds before returning the tile to its original state
        setTileAlpha(tileDisableAlpha);
        yield return new WaitForSeconds(enableTime);

        isActive = true;
        setTileAlpha(tempTileAlpha);
        enabling = false;
    }

    void setTileAlpha(float newAlpha) {
        Color GOColor = gameObject.GetComponent<SpriteRenderer>().color;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(GOColor.r, GOColor.g, GOColor.b, newAlpha);
    }

}
