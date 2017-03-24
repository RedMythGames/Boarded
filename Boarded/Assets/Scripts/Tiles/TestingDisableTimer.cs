/**Date Created: 3/12/17
 *Last Modified: 3/12/17
 * 
 * Every timerDisable seconds (specified in Unity editor) the tile this is attatched to becomes inactive
 * -Rishi Parida
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingDisableTimer : MonoBehaviour {
    
	void Start () {
        StartCoroutine(startDisabling());
    }

    public float disableTimer;

    IEnumerator startDisabling() {
        gameObject.GetComponent<Tile>().disable();
        yield return new WaitForSeconds(disableTimer);
        StartCoroutine(startDisabling());
    }
}
