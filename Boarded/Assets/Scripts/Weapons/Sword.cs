/* Date Created: 3/22/17
 *Last Modified: 3/22/17
 * 
 * When enabled, strikes the 3 tiles horizontally in front of the character.
 */



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon {

	/*protected overried*/ void OnTriggerEnter2D(Collider2D col) {
        Debug.Log(col.name);
        Enemy collisionEnemey = col.GetComponent<Enemy>();
        if (collisionEnemey != null)
            collisionEnemey.Damage(dmgValue);
    }

    [SerializeField]
    protected int dmgValue;
    [SerializeField]
    protected float activeTime;

    public void StartDisable() { StartCoroutine(Disable()); }

    IEnumerator Disable() {
        yield return new WaitForSeconds(activeTime);
        Destroy(gameObject);
    }

}
