/** Date Created: 3/18/17
 * Last Modified: 3/21/17
 *
 * The base for all enemy classes
 * -Rishi Parida
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : CharacterBase {

    //How much life the enemy has, and the position relative to the enemy that it's displayed at
    [SerializeField]
    public Text healthText;
    public Vector3 textOffset;

    //Sets the positiion of the health text and updates the text of health
    void setTextPos() {
        healthText.text = string.Format("{0}", health * 10);
        healthText.transform.position = transform.position + textOffset;
    }

    //In addition to doing what was 
    protected override void OnEnable() {
        base.OnEnable();
        setTextPos();
    }


    //Damages the enemy by whatever is inputted
    //if the enemy's health is 0 or less, calls the die function, which destroys the game object and can change any necessary data if needed in child classes
    public override void Damage(int damage) {
        base.Damage(damage);
        if (health <= 0)
            Die();
        setTextPos();
    }

    protected virtual void Die() {
        Destroy(healthText.gameObject);
        Destroy(gameObject);
    }

    [SerializeField]
    protected bool willFire = true;

    public bool getWillFire() { return willFire; }
}
