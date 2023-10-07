/*
 * VultureMain Class
 * Movement of each vulture droid
 * 
 * 
 * 
 * IMPORTANT INFORMATION:
 * Shooting is still in development due to its overflow of data from the instantiation of numerous lasers
 * 
 * 
 * 
 */




using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class VultureMain : MonoBehaviour
{

    [Header("Set in Inspector")]
    public GameObject vultureLaser;

    public GameObject mainShip;

    [Header("Set Dynamically")]

    public float vultureSpeed = -100;



    public float shootRate = 3f;

    public float nextShoot;






    // Start is called before the first frame update
    void Start()
    {

        nextShoot = 0f;
    }

    // Update is called once per frame
    void Update()
    {

        //Moves ship
        transform.position = new Vector3(transform.position.x + vultureSpeed * Time.deltaTime, transform.position.y, transform.position.z);





        //Kills object after it reaches end
        if (transform.position.x < 0)
        {
            Destroy(gameObject);
        }


        //Shoots every 3s and if it is infront of ship

        //CURRENTLY NOT WORKING
        if (Time.time > nextShoot && transform.position.x > mainShip.transform.position.x && transform.position.x < mainShip.transform.position.x + 1000)
        {
            nextShoot = Time.time + shootRate;

            var position = new Vector3(transform.position.x - 100, transform.position.y, transform.position.z);
            Quaternion rotation = Quaternion.Euler(0, 0, 90);

            Instantiate(vultureLaser, position, rotation);
        }



    }



    //Collision destruction
    void OnTriggerEnter(Collider other)
    {


        Destroy(gameObject);

    }


}
