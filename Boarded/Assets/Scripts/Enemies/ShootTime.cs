/** Date Created: 3/21/17
 * Last Modified: 3/21/17
 *
 * Fires a bullet every shootTime seconds, until the game object this is attatched to is destroyed,
 * or the Enemy this is attatched to changes its willFire variable to false
 * -Rishi Parida
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTime : MonoBehaviour {

    [SerializeField]
    private float shootTime;

    protected virtual void OnEnable() { Invoke("Shoot",shootTime); }

    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Vector3 offset;

    protected GameObject newBullet;

    protected virtual void Shoot() {
        StartCoroutine(CreateBullet());
        if(GetComponent<Enemy>() == null || GetComponent<Enemy>().getWillFire())
            Invoke("Shoot", shootTime);

    }

    IEnumerator CreateBullet() {
        GetComponent<Animator>().SetTrigger("Shoot");
        yield return new WaitForSeconds(.25f);
        newBullet = Instantiate(bullet, transform.position + offset, Quaternion.identity);
        extraShoot();
    }

    protected virtual void extraShoot() { }

}
