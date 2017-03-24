/** Date Created: 3/17/17
 * Last Modified: 3/21/17
 *
 * Used to test
 * -Rishi Parida
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TestingTurret : Enemy {
    [SerializeField]
    private float attackTimeMin, attackTimeMax;


    private GameObject[,] MoveableTiles = new GameObject[3, 3];

    public GameObject FriendlyGrid;

    protected override void OnEnable() {
        base.OnEnable();
        //shoot();

        int childCounter = 0;
        for (int y = 0; y < MoveableTiles.GetLength(1); y++) {
            for (int x = 0; x < MoveableTiles.GetLength(0); x++) {
                MoveableTiles[x, y] = FriendlyGrid.transform.GetChild(childCounter).gameObject;
                childCounter++;
            }
        }
        if(verticalTileCounter < 0)
            verticalTileCounter = Random.Range(0, 2);
        if (willMove)
            moveForce = 1;

        StartCoroutine(BeginMovement(beginMoveDelay));

        Damage(0);
        StartCoroutine(assignStart());
    }

    int moveForce = 0;

    [SerializeField]
    //Whether or not the turret will move
    bool willMove;

    IEnumerator assignStart() {
        yield return new WaitForSeconds(0.01f);
        GameManager.instance.AddEnemy += LogLiving;
        yield break;
    }

    void Move() { StartCoroutine(MoveRandomTime(moveForce)); }

    [SerializeField]
    private float moveTime, beginMoveDelay;

    IEnumerator BeginMovement(float waitTime) {
        MoveVertical(0);
        yield return new WaitForSeconds(waitTime);
        Move();
    }

    protected virtual IEnumerator MoveRandomTime(int moveAmt) {
        yield return new WaitForSeconds(moveTime);
        MoveVertical(moveAmt);
        Move();
    }

    void MoveVertical(int newVTC) {
        verticalTileCounter+= newVTC;
        if (verticalTileCounter >= 3)
            verticalTileCounter = 0;
        transform.position = MoveableTiles[moveX, verticalTileCounter].transform.position + tileOffset;
        setTextPos();
        //if (newVTC > 0)
        //    StartCoroutine(bulletShoot(false));
    }

    /*IEnumerator bulletShoot(bool repeat) {
        yield return new WaitForSeconds(Random.Range(attackTimeMin, attackTimeMax));
        GetComponent<Animator>().SetTrigger("Shoot");
        yield return new WaitForSeconds(0.25f);
        CreateBullet();
        if (repeat)
            shoot();
        if (GetComponent<TileDisablerTimeRandom>() != null)
            GetComponent<TileDisablerTimeRandom>().DisableTile();
    }*/
    
    //protected virtual void CreateBullet() { Instantiate(bullet, transform.position + offset, Quaternion.identity); }

    [SerializeField]
    private Vector3 tileOffset;
    [SerializeField]
    int moveX;

    [SerializeField]
    protected int verticalTileCounter = -1;
    
    public int getVTC() { return verticalTileCounter; }


    //void shoot() { StartCoroutine(bulletShoot(true)); }

    public GameObject bullet;
    public Vector3 offset;

    public Text healthText;
    public Vector3 textOffset;

    void setTextPos() { healthText.transform.position = transform.position + textOffset; }

    public float timeSc = 1;

    void Update() {

        Time.timeScale = timeSc;
    }
    
    public override void Damage(int damage) {
        base.Damage(damage);
        setTextPos();
        healthText.text = string.Format("{0}",health * 10);
    }

    protected override void Die() {
        Debug.Log("Killed");
        Destroy(healthText);
        GameManager.instance.AddEnemy -= LogLiving;
        base.Die();
    }

    void LogLiving() { Debug.Log(name + " is living"); }
}
