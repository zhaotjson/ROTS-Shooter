/*
 *      AP Computer Science Principles
 *      Create Project
 *      
 *      Jason Jiang
 *      
 *      Using UNITY
 *      
 *      
 *      Models created by Jason Jiang in Blender
 *      Based on Star Wars Episode III Revenge of the Sith
 *      
 *      In Alpha Stage
 *      Expect minor bugs and lack of victory
 *      
 *      Current goal is to reach the Invisible Hand (A Space Ship)
 *      
 * 
 */







/* 
 * Starship Class
 * Main code of the game
 * Features all of the movement and combat
 * 
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Starship : MonoBehaviour
{
    


    

    //Variables
    [Header("Set in Inspector")]
    public GameObject camera;
    public GameObject ship;
    public GameObject laser;


    
    
    

    



    [Header("Set Dynamically")]
    public Vector3 shipPosition;

    public float speed = 0;

    public List<Guns> allGuns;


    public int gunPosition = 0;
    public double gunTimeElapsed;






    // Start is called before the first frame update
    void Start()
    {

        //Sets the camera to match the position of the ship
        shipPosition = transform.position;

        camera.transform.position = new Vector3(transform.position.x - 7, transform.position.y + 1, transform.position.z);

        allGuns = new List<Guns>();


        //Initiating guns available

        //central fire
        int gun1Lasers = 1;
        List<float> positions1 = new List<float>();
        positions1.Add(0f);
        float cooldown1 = 0.25f;

        Guns gun1 = new Guns(gun1Lasers, positions1, cooldown1);

        //Double Fire
        int gun2Lasers = 2;
        List<float> positions2 = new List<float>();
        positions2.Add(0.5f);
        positions2.Add(-0.5f);
        float cooldown2 = 0.5f;

        Guns gun2 = new Guns(gun2Lasers, positions2, cooldown2);


        //Spread Fire
        int gun3Lasers = 4;
        List<float> positions3 = new List<float>();
        positions3.Add(0.5f);
        positions3.Add(-0.5f);
        positions3.Add(1.5f);
        positions3.Add(-1.5f);


        float cooldown3 = 1f;

        Guns gun3 = new Guns(gun3Lasers, positions3, cooldown3);


        //Creating List
        allGuns.Add(gun1);
        allGuns.Add(gun2);
        allGuns.Add(gun3);
       

        gunTimeElapsed = 0;



    }

    









    /*
     * MOVEMENTS
     * 
     * 
     * 
     * 
     */


    // Update is called once per frame
    void Update()
    {


        //Update for cooldowns
        gunTimeElapsed += Time.deltaTime;


        //Acceleration depending on userInput
        if (Input.GetKey("w"))
        {
            speed += 1;


            //Makes sure that the maximum speed is 100
            if (speed >= 100)
            {
                speed = 100;
            }
            
        }

        if (Input.GetKey("s"))
        {
            speed -= 1;

            //Makes sure that the minimum speed is -50
            if (speed <= -50)
            {
                speed = -50;
            }
        }





        //Turning if a button is pressed
        //Starting rotations for object 0, 90, 90

        // Creation of object required different rotations :l

        //Turning Right
        if (Input.GetKey("d")){

            //Values
            Quaternion from = Quaternion.Euler(0, 90, 90);
            Quaternion to = Quaternion.Euler(0, 90, 60);



            //Run turn function
            StartCoroutine(rotate(from, to));
            //Smooth Move
            //Movement depends on speed
            StartCoroutine(moveZ(-Mathf.Abs(speed) / 5));
            
            


        }


        //Turning Left
        if (Input.GetKey("a"))
        {
            //Values
            Quaternion from = transform.rotation;
            Quaternion to = Quaternion.Euler(0, 90, 120);



            //Run Turn function
            StartCoroutine(rotate(from, to));
            //Smooth Move
            //Movement depends on speed
            StartCoroutine(moveZ(Mathf.Abs(speed) / 5));
            
            
        }



        //Going Down
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Quaternion from = transform.rotation;
            Quaternion to = Quaternion.Euler(20, 90, 90);




            StartCoroutine(rotate(from, to));
            StartCoroutine(moveY(-Mathf.Abs(speed) / 5));

           
        }

        if (Input.GetKey(KeyCode.Tab))
        {
            Quaternion from = transform.rotation;
            Quaternion to = Quaternion.Euler(-20, 90, 90);



            StartCoroutine(rotate(from, to));
            StartCoroutine(moveY(Mathf.Abs(speed) / 5));
        }





        //Sets rotation back to normal
        //Delay of 1 second to accustom the continual movement after release
        if (Input.GetKeyUp("d"))
        {
            Invoke("delayedReturn", 1);

        }
        if (Input.GetKeyUp("a"))
        {
            Invoke("delayedReturn", 1);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Invoke("delayedReturn", 1);
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            Invoke("delayedReturn", 1);
        }





        





        //Changes position according to speed
        shipPosition.x += speed * Time.deltaTime;
        transform.position = shipPosition;
        //Changes camera to match ship position
        camera.transform.position = new Vector3(transform.position.x - 7, transform.position.y + 1, transform.position.z);









        /* 
         * COMBAT
         * 
         * 
         * 
         * 
         */



        //Switch Guns

        if (Input.GetKeyDown("q"))
        {
            //Change Guns
            gunPosition += 1;

            //Switch back to first one if it is greater than 2
            if (gunPosition > 2)
            {
                gunPosition = 0;
            }
        }



        //Instantiate laser when pressing space bar
        if (Input.GetKeyDown(KeyCode.Space))
        {

            //Find data of chosen gun
            Guns chosenGun = allGuns[gunPosition];



            
            //Utilize data to instantiate lasers
            shoot(chosenGun);
            


     
        }
        

    }
    void shoot(Guns selectedGun)
    {

        //Take out data
        int numberOfLasers = selectedGun.NumLasers;
        List<float> allPositions = selectedGun.Positions;
        float gunCooldown = selectedGun.Cooldown;
        Quaternion rotation = Quaternion.Euler(0, 0, 90);



        //Check if it is on cooldown
        if (gunTimeElapsed >= gunCooldown)
        {
            for (int i = 0; i < numberOfLasers; i++)
            {

                Instantiate(laser, new Vector3(transform.position.x + 5, transform.position.y, transform.position.z + allPositions[i]), rotation);
            }

            //Set on cooldown
            gunTimeElapsed = 0f;
        }
        else {
            print("On Cooldown!!");
        
        }
        



    }















    /* 
     * COLLISIONS!!!
     * Don't get hit :)
     * 
     * 
     */

    void OnTriggerEnter(Collider other)
    {


        Destroy(gameObject);

        print("GG you died :l  ");
    }





    /*
     * USEFUL FUNCTIONS
     *
     * 
     * 
     * 
     * 
     */



    //Rotation function
    IEnumerator rotate(Quaternion start, Quaternion end)
    {

        float timeElapsed = 0;

        while (timeElapsed < 1)
        {

            //Smooth turn
            transform.rotation = Quaternion.Lerp(start, end, timeElapsed);


            //Update time
            timeElapsed += Time.deltaTime;
            yield return null;
        }


        //Snap back to rotation
        transform.rotation = end;


    }


    //This is called at a delay with Invoke
    void delayedReturn()
    {
        
        Quaternion current = transform.rotation;
        Quaternion orig = Quaternion.Euler(0, 90, 90);


        //Calls the rotate to set back to normal
        StartCoroutine(rotate(current, orig));


    }


    //Z-axis move function
    IEnumerator moveZ(float halfDistance)
    {
        float timeElapsed = 0;
        Vector3 origPosition = shipPosition;
        while (timeElapsed < 2)
        {
            //Change position
            shipPosition.z = origPosition.z + halfDistance * timeElapsed;


            //Keeps it inside game area
            if (shipPosition.z < -200)
            {
                shipPosition.z = -200;
            }
            if (shipPosition.z > 200)
            {
                shipPosition.z = 200;
            }


            camera.transform.position = new Vector3(transform.position.x - 7, transform.position.y + 1, transform.position.z);

            //Update time
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        
    }

    IEnumerator moveY(float halfDistance)
    {
        float timeElapsed = 0;
        Vector3 origPosition = shipPosition;
        while (timeElapsed < 2)
        {
            shipPosition.y = origPosition.y + halfDistance * timeElapsed;
            
            //Keeps it inside game area
            if (shipPosition.y < 0)
            {
                shipPosition.y = 0;
            }
            if (shipPosition.y > 200)
            {
                shipPosition.y = 200;
            }


            //Change camera to new ship position
            camera.transform.position = new Vector3(transform.position.x - 7, transform.position.y + 1, transform.position.z);

            timeElapsed += Time.deltaTime;

            yield return null;
        }
    }


    

}
