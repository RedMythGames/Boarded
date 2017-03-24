/** Date Created: 3/20/17
 * Last Modified: 3/20/17
 *
 * The purpose of this class is to contain the information that is needed to manage the game and have it run 
 * -Rishi Parida
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {
    public delegate void CountEnemies();

    public event CountEnemies AddEnemy;

    public static GameManager instance = null;

    void Start() {
        if (instance == null) {
            instance = this;
            Debug.Log("Instance assigned");
        }
        else
            Destroy(gameObject);
    }

    void Update() {
        if (AddEnemy != null)
            AddEnemy();
    }

}
