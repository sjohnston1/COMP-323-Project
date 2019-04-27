using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour
{
    //Variables
    public int offsetX = 2; //offset so we don't through an error

    //used for checking if we need to instantiate stuff
    public bool hasARightBuddy = false;
    public bool hasALeftBuddy = false;
    
    public bool reverseScale = false; //used if the object is not tilable

    private float spriteWidth = 0f; //the width of our element
    private Camera cam;
    private Transform myTransform;

    void Awake()
    {
        cam = Camera.main;
        myTransform = transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = sRenderer.sprite.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        //does it still need buddies, if not do nothing
        if(hasALeftBuddy == false || hasARightBuddy == false) {
            //calculate the cameras extend (half the width) of what the camera can see in the world coordinates
            float camHorizontalExtend = cam.orthographicSize + Screen.width / Screen.height;

            //calculate the x position where the camera can seee the edge of the sprite (element)
            float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth / 2) - camHorizontalExtend;
            float edgeVisiblePositionLeft = (myTransform.position.x - spriteWidth / 2) + camHorizontalExtend;

            //checking if we can see the edge of the element and then calling MakeNewBuddy if we can
            if(cam.transform.position.x >= edgeVisiblePositionRight - offsetX && hasARightBuddy == false) {
                MakeNewBuddy(1);
                hasARightBuddy = true;
            } else if(cam.transform.position.x <= edgeVisiblePositionLeft + offsetX && hasALeftBuddy == false) {
                MakeNewBuddy(-1);
                hasALeftBuddy = true;
            }
        }
    }

    //a function that creates a buddy on the side required
    void MakeNewBuddy(int rightOrLeft) 
    {
        //calculating the new position for our new buddy
        Vector3 newPosition = new Vector3(myTransform.position.x + spriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);
        //instantiating our ned buddy and storing it in a variable
        Transform newBuddy = Instantiate(myTransform, newPosition, myTransform.rotation) as Transform;

        //if not tilable, reverse the x size of the object to get rid of seams
        if(reverseScale == true) {
            newBuddy.localScale = new Vector3(newBuddy.localScale.x * -1, newBuddy.localScale.y, newBuddy.localScale.z);
        }

        newBuddy.parent = myTransform.parent;

        if(rightOrLeft > 0) {
            newBuddy.GetComponent<Tiling>().hasALeftBuddy = true;
        } else {
            newBuddy.GetComponent<Tiling>().hasARightBuddy = true;
        }
    }
}
