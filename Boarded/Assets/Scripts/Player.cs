/**Date Created: 3/10/17
 *Last Modified: 3/23/17
 * 
 * The script for the player object. The purpose (as of now) is simply to allow the character to move and shoot.
 * -Rishi Parida
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : CharacterBase {

    //How fast the player can move between the vertical tiles
    [SerializeField]
    private float speed;

    //Player's position will be pos, used for modification before application
    private Vector3 pos;

    //How big each tile is
    public float tileSize = 1;

    //Stores how long player has been on the same row
    [SerializeField]
    float moveTime = 0;

    //How far down and to the right the player can go
    //public int xLim, yLim;

    //A 2D array of tiles that the player can move on, as well as the gameobject which all of the tiles are stored in the editor
    [SerializeField]
    private GameObject[,] PlayerTiles = new GameObject[3,3], EnemyTiles = new GameObject[3, 3];
    public GameObject FriendlyGrid, EnemyGrid;

    //The public player variable that can be accessed anywhere, created more for simplicity than anything else
    [HideInInspector]
    public static Player instance;

    //When the scene first starts, the static instance Player is assigned to this (there will only be one Player script)
    void Start() { instance = this; }

    //When the player is enabled, assigns the tiles in Grid to be stored in PlayerTiles, thensets the player to be on the rop left corner
    protected override void OnEnable() {

        //Sets the health to be the maximum health to start the game
        base.OnEnable();


        debugText2.text = "Y height" + Screen.height;

        //Sets the pos variable to the player's position so it can be modified before applying to the Game Object
        pos = transform.position;

        //This variable is incremented to track which tile (in the FriendlyGrid gameobject) is assigned to PlayerTiles
        int childCounter = 0;
        //Assigns the PlayerTiles 2D array to be the tile grid in order
	//(Note: Look for more efficient way to accomplish this)
        for(int y = 0; y < PlayerTiles.GetLength(1); y++) {
            for (int x = 0; x < PlayerTiles.GetLength(0); x++) {
                PlayerTiles[x, y] = FriendlyGrid.transform.GetChild(childCounter).gameObject;
                childCounter++;
            }
        }
	    
        //Resets the child counter (to be used again) then assigns the EnemyGrid in the same way that FriendlyGrid was assigned
        childCounter = 0;
        for (int y = 0; y < EnemyTiles.GetLength(1); y++) {
            for (int x = 0; x < EnemyTiles.GetLength(0); x++) {
                EnemyTiles[x, y] = EnemyGrid.transform.GetChild(childCounter).gameObject;
                childCounter++;
            }
        }
	    
	//Sets the Game Object's position to be the top left of the graph at the start    
        transform.position = PlayerTiles[0, 0].transform.position + offset;
    }

    //The two variables that will track the x and y position of the tile that the player is on
    //(ex: If the player is in the top left corner,
	//xTIle and yTile will both be 0, but if the player moves to the left, xTile will be 1 and yTile will still be 0)
    [SerializeField]
    int xTile = 0, yTile = 0;
	
	//Public accessor methods for xTile and yTile
    public int getX() { return xTile; }
    public int getY() { return yTile; }

    //A bullet which can be shot by the player, and the offset relative to the player
    public GameObject bullet;
    public Vector3 bulletOffset;

    //The health bar whose fill amount will be manipulated, and the health text which will be shown to the user
    [SerializeField]
    Image healthBar;

	//The text that will display the current health of the player
    [SerializeField]
    Text healthText;

	//Used for debugging purposes
    [SerializeField]
    Text debugText, debugText2;

	//Every frame change, this function will handle movement and attacks
    void Update() {

        //Increases the moveTime variable with time (moveTime is used to keep the player from moving too fast
        moveTime += Time.deltaTime;

        //Moves the character in the direction inputted on the keyboard
		//(Note: The player can move right and left every frame, but not vertical.
		//			This is simply a design choice and can be manipulated)
        if (Input.GetButtonDown("Right")) {
            setPosToTile(1, true);
        }
        else if (Input.GetButtonDown("Left")) {
            setPosToTile(-1, true);
        }
        if (Input.GetButtonDown("Up") && moveTime > speed) {
            setPosToTile(-1, false);
        }
        else if(Input.GetButtonDown("Down") && moveTime > speed) {
            setPosToTile(1, false);
        }
		
		//This starts the section for touch screen movement. It's messy and it's inefficient but it's the best I could do :D
		
		//If the player is not touching the screen, the player can move again.
		if(Input.touchCount == 0) {
		 swipe = true;
		 //oldTouch.position = Vector2.zero;
		 }
		
		//If the player has touched the screen
		if (Input.touchCount == 1) {
			//If the player's finger has moved in any direction, and they haven't moved with that touch
			//(Essentially, if the player's finger has swiped the screen, and they haven't moved without taking off their finger)
			if (Input.touches [0].phase == TouchPhase.Moved && swipe) {
				//The current touch of the player, currTouch isn't necessary but makes the code easier to see
				var currTouch = Input.touches [0];
				
				//If the current touch is farther to the right (compared to the last touch) by at least touchOffset.x pixels, moves the Player to the right
				if (currTouch.position.x > oldTouch.position.x + touchOffset.x) {
					setPosToTile (1, true);
				//Otherwise, if the current touch is farther to the left, moves the Player to the left
				} else if (currTouch.position.x < oldTouch.position.x - touchOffset.x) {
					setPosToTile (-1, true);
				}

				if (currTouch.position.y > oldTouch.position.y + touchOffset.y) {
					setPosToTile (1, false);
				} else if (currTouch.position.y < oldTouch.position.y - touchOffset.y) {
					setPosToTile (1, false);
				}

				/*if (currTouch.position.x < Screen.width / 2) {
	                if(currTouch.position.y < Screen.height / 2)
	                    setPosToTile(-1, false);
	                else
	                    setPosToTile(-1, true);
	            }
	            else if (currTouch.position.x > Screen.width / 2) {
	                if (currTouch.position.y < Screen.height / 2)
	                    setPosToTile(1, false);
	                else
	                    setPosToTile(1, true);
	                //debugText.text = "Y po: " + touch.position.y + " X po: " + touch.position.x;
	            }

	            debugText.text = "Y po: " + currTouch.position.y + " X po: " + currTouch.position.x;*/
        	
				oldTouch = currTouch;
			}
		}

        //Debug.Log("XTile: " + xTile + "YTile: " + yTile);
        transform.position = pos;

        //When the shoot button is pressed, instantiates a bullet game object at the player's position with respect to the bullet offset, after shootTime seconds
        if (Input.GetButtonDown("Shoot")) {
            if(!shooting)
                StartCoroutine(Shoot(shootTime));
            
        }

        if (Input.GetButtonDown("Sword")) {
            GameObject tempSword = GameObject.Instantiate(PlayerSword, transform.position + swordOffset, Quaternion.identity);
            tempSword.GetComponent<Sword>().StartDisable();
        }

    }
	Touch oldTouch;
	bool swipe = true;

	[SerializeField]
	Vector2 touchOffset;

    [SerializeField]
    GameObject PlayerSword;
    [SerializeField]
    Vector3 swordOffset;

    //The delay between when the player presses the shoot button and when the bullet is instantiated
    [SerializeField]
    float shootTime;

    //Whether or not the player is currently trying to shoot
    bool shooting;

    //Instantiates the bullet with respect to the bullet offset
    IEnumerator Shoot(float shootDelay) {
        shooting = true;
        yield return new WaitForSeconds(shootDelay);
        GameObject newBullet = GameObject.Instantiate(bullet, transform.position + bulletOffset, Quaternion.identity);
        Debug.Log("Player bullet's name " + newBullet.name);
        shooting = false;
    }

    void DestroyTileInFront() {
        if (xTile == PlayerTiles.GetLength(0) - 1)
            EnemyTiles[0, yTile].GetComponent<Tile>().disable();
        else
            PlayerTiles[xTile + 1, yTile].GetComponent<Tile>().disable();
    }

    public override void Damage(int damage) {
        if (health == 0) {
            health = maxHealth;
            Color GOColor = gameObject.GetComponent<SpriteRenderer>().color;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(GOColor.r, GOColor.g, GOColor.b, 0.3f);
        }
        else {
            health -= damage;
            healthBar.fillAmount = (float)health / maxHealth;
            healthText.text = string.Format("{0}", health * 100);
        }
    }

    int GridAdd(int input, int value) {
        if (input + value >= 0 && input + value < PlayerTiles.GetLength(0))
            input += value;
        return input;
    }

    public Vector3 offset;
    //Returns the position of the tile at the index of xRef, yRef (in PlayerTiles) if the tile is active, and returns the player's position if not
    void setPos2Tile(int xRef, int yRef) {
        if (PlayerTiles[xRef, yRef].GetComponent<Tile>().getActive())
            pos = PlayerTiles[xRef, yRef].transform.position + offset;
        else
            pos = transform.position;
    }

    void setPosToTile(int change, bool horizChange) {
		swipe = false;
        if (horizChange)
            xTile = GridAdd(xTile, change);
        else {
            yTile = GridAdd(yTile, change);
            moveTime = 0;
        }

        //If the tile the player is trying to move to is active, the player moves there
        if (PlayerTiles[xTile, yTile].GetComponent<Tile>().getActive()) { pos = PlayerTiles[xTile, yTile].transform.position + offset; }
        else {
            if (horizChange)
                xTile -= change;
            else {
                yTile -= change;
            }
        }
    }

}
