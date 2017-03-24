/**Date Created: 3/14/17
 *Last Modified: 3/18/17
 * 
 * A base class which, when overridden, will disable a tile, usually after a certain condition is met (time, collision, etc)
 * -Rishi Parida
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileDisablerBase : MonoBehaviour {

    //The tile which will be disabled
    public Tile TileToDisable;

    //The method which will disable the tile itself
    public abstract void DisableTile();

    //Whether or not the game object will destroy after the tile is destroyed
    public bool destroyImmediately;
}
