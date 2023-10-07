/*
 * Vultures Class
 * Spawning of each Vulture Droid
 * 
 * 
 * 
 */



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vultures : MonoBehaviour
{

    [Header("Set in Inspector")]
    public GameObject vulture;


    public float spawnRate = 5.0f;


    public float nextSpawn = 0.0f;


    // Update is called once per frame
    void Update()
    {

        


        //Spawns 100 vultures near Invisible Hand every 5 seconds
        if (Time.time > nextSpawn)
        {
            for (int i = 0; i < 100; i++)
            {

                var position = new Vector3(Random.Range(9600f, 97000f), Random.Range(0f, 200f), Random.Range(-200f, 200f));

                Quaternion rotation = Quaternion.Euler(0, 90, 90);

                Instantiate(vulture, position, rotation);
            }
            nextSpawn = Time.time + spawnRate;
        }

    }
}
