/** Date Created: 3/17/17
 * Last Modified: 3/18/17
 *
 * Used to test
 * -Rishi Parida
 */



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour {

    [SerializeField]
    protected int maxHealth;

    protected int health;

    public virtual void Damage(int dmgValue) { health -= dmgValue; }

    //Sets the health to be the maximum health when the game object is enabled
    protected virtual void OnEnable() { health = maxHealth; }
}
