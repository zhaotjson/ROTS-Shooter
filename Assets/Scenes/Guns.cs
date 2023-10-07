/* 
 * Guns Class
 * Creates a new class to hold data on each gun
 * 
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guns
{
    private int numLasers = 0;
    private List<float> positions = new List<float>();
    private float cooldown = 0;



    public Guns(int lasers, List<float> locations, float timePerShot)
    {
        numLasers = lasers;
        positions = locations;
        cooldown = timePerShot;

    }

    public int NumLasers
    {
        get { return numLasers; }
        set { numLasers = value; }
    }

    public List<float> Positions
    {
        get { return positions; }
        set { positions = value; }
    }

    public float Cooldown
    {
        get { return cooldown; }
        set { cooldown = value; }
    }

}
