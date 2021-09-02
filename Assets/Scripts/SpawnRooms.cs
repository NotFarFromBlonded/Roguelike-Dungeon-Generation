using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRooms : MonoBehaviour
{
    public LayerMask whatIsRoom;
    public LevelGeneration levelGen;

    public float waitTime;
    bool hasRoom;

    public GameObject closedRoom;
    // Update is called once per frame
    void Update()
    {
        Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom);
        if(roomDetection != null)
        {
            hasRoom = true;
        }
        if(waitTime <= 0)
        {
            if(hasRoom == false)
            {
                Instantiate(closedRoom, transform.position, Quaternion.identity);
            }
        } else
        {
            waitTime -= Time.deltaTime;
        }
    }
}
