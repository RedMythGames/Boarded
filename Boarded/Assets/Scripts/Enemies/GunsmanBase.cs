/**Date Created: 3/21/17
 *Last Modified: 3/22/17
 * 
 * A gunsman enemy that will shoot horizonally across the field every attackTime seconds. This base class doesn't move and attacks until destroyed at a constant rate
 * 
 */



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsmanBase : Enemy {

    [SerializeField]
    //How fast the gunsman attacks
    protected float attackTime;

    //When the gunsman is activated, after all default behavior, they start shooting
    protected override void OnEnable() {
        base.OnEnable();
        Shoot();
    }

    public override void Damage(int damage) {
        base.Damage(damage);
        Debug.Log("OverridenDamage");
    }

    //Calls the ShootDelay Coroutine, used so that the gunsman will shoot until destroyed
    void Shoot() { StartCoroutine(ShootDelay()); }

    //Creates a bullet after attackTime seconds, then calls the shoot method which will call this coroutine again, creating a recursive loop
    IEnumerator ShootDelay() {
        yield return new WaitForSeconds(attackTime);
        //Should start the gunsman's shooting animation, then waits for it to complete before continuing
        GetComponent<Animator>().SetTrigger("Shoot");
        yield return new WaitForSeconds(.25f);

        //Creates the bullet then starts over
        CreateBullet();
        Shoot();
    }

    //Actually instantiates the bullet, is in a seperate function so child classes can override easily
    protected virtual void CreateBullet() { Instantiate(enemyBullet, transform.position + offset, Quaternion.identity); }

    //The bullet that the enemy will fire and the position relative to the enemy it will be fired at
    [SerializeField]
    protected GameObject enemyBullet;
    [SerializeField]
    protected Vector3 offset;

}
