/** Date Created: 3/17/17
 * Last Modified: 3/20/17
 *
 * Used to test
 * -Rishi Parida
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageTester : MonoBehaviour {
    [SerializeField]
    private Vector2 bulletVelocity;
    public Vector3 rotation;

    void OnEnable() {
        GetComponent<Rigidbody2D>().velocity = bulletVelocity;
        transform.rotation = Quaternion.Euler(rotation);
    }

    public int damage;
    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Wall")
            Destroy(gameObject);
        Enemy enemy = col.GetComponent<Enemy>();
        if (enemy != null) {
            enemy.Damage(damage);
            Destroy(gameObject);
        }
    }
}
