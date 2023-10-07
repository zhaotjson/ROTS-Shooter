/* 
 * Laser Class
 * Movement of each lazer
 * and self-destruction
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Set in Inspector")]
    public float laserSpeed = 150;


    [Header("Set Dynamically")]
    
    public float laserTimeElapsed = 0;


    // Movement

    // Update is called once per fram
    void Update()
    {
        Vector3 newLaserPosition = new Vector3(transform.position.x + laserSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        transform.position = newLaserPosition;


        //Destroys laser after 10 seconds
        laserTimeElapsed += Time.deltaTime;
        if (laserTimeElapsed > 10)
        {
            Destroy(gameObject);
        }



    }



}
