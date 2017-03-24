/** Date Created: 3/20/17
 * Last Modified: 3/21/17
 *
 * Used to test
 * -Rishi Parida
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingBombThrower : TestingTurret {

    [SerializeField]
    GameObject PlayerGrid;

    GameObject[,] PlayerTiles = new GameObject[3, 3];

    protected override void OnEnable() {
        base.OnEnable();
        int childCounter = 0;
        for (int y = 0; y < PlayerTiles.GetLength(1); y++) {
            for (int x = 0; x < PlayerTiles.GetLength(0); x++) {
                PlayerTiles[x, y] = PlayerGrid.transform.GetChild(childCounter).gameObject;
                childCounter++;
            }
        }
    }
    

    /*protected override void CreateBullet() {
        GameObject newBullet = Instantiate(bullet, transform.position + offset, Quaternion.identity);
        newBullet.GetComponent<RaycastTester>().setTile(PlayerTiles[Player.instance.getX(), verticalTileCounter]);
    }*/

}
