using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public Transform[] startingPositions;
    public GameObject[] rooms;

    private int direction;
    public float moveAmount = 10;

    private float timeBtwRoom;
    public float startTimeBtwRoom = 0.25f;

    public float minX;
    public float maxX;
    public float minY;

    public bool stopGeneration;

    public LayerMask room;

    private int downCounter;

    // Start is called before the first frame update
    void Start()
    {
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;
        Instantiate(rooms[1], transform.position, Quaternion.identity);

        direction = Random.Range(1, 6);
    }

    private void Move()
    {
        if(direction == 1||direction == 2)
        {
            //Move right
            if(transform.position.x < maxX)
            {
                downCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);
                if(direction == 3)
                {
                    direction = 1;
                } else if (direction == 4)
                {
                    direction = 5;
                }
            } else
            {
                direction = 5;
            }
            
        } else if (direction == 3 || direction == 4)
        {
            //Move Left
            if(transform.position.x > minX)
            {
                downCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(1, 4);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(3, 6);
            } else
            {
                direction = 5;
            }
            
        } else if (direction == 5)
        {
            downCounter++;
            //Move down
            if(transform.position.y > minY)
            {
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1f, room);
                if(roomDetection.GetComponent<RoomType>().type != 4 && roomDetection.GetComponent<RoomType>().type != 2)
                {
                    if(downCounter >= 2)
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        Instantiate(rooms[4], transform.position, Quaternion.identity);
                    } else
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        int randBottomRoom = Random.Range(2, 5);
                        if (randBottomRoom == 3)
                        {
                            randBottomRoom = 2;
                        }
                        Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                    }
                    
                }
                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;

                int rand = Random.Range(3, 5);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);
            } else
            {
                stopGeneration = true;
            }
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(timeBtwRoom <= 0 && stopGeneration == false)
        {
            Move();
            timeBtwRoom = startTimeBtwRoom;
        } else
        {
            timeBtwRoom -= Time.deltaTime;
        }
    }
}
