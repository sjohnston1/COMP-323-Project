using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    //Variables
    public Transform[] backgrounds; //stores backgrounds for parallaxing
    private float[] parallaxScales; //proportion of the cameras movement to move the backgrounds by
    public float smoothing = 1f;    //how smooth the parallax is going to be, set above zero

    private Transform cam;          //reference to main cameras transform
    private Vector3 previousCamPos; //stores position of the camera in the previous frame

    //is called before Start(). Good for references
    void Awake()
    {
        cam = Camera.main.transform; //set up the camera reference

    }

    // Start is called before the first frame update
    void Start()
    {
        previousCamPos = cam.position; //the previous frame had the current frame's camera position
       
        parallaxScales = new float[backgrounds.Length]; //assigning corresponding parallaxScales
        for(int i = 0; i < backgrounds.Length; i++) {
            parallaxScales[i] = backgrounds[i].position.z*-1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < backgrounds.Length; i++) { //for each background
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i]; //the parallax is the opposite of the camera movement because the previous frame multiplied by the scale

            float backgroundTargetPosX = backgrounds[i].position.x + parallax; //set a target x position which is the current position plus the parallax

            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z); //create a target position which is the background's current position with it's target x position

            //fade between current position and the target position using lerp
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }
        // set the previousCamPos to the camera's position at the end of the frame
        previousCamPos = cam.position;
    }
}
