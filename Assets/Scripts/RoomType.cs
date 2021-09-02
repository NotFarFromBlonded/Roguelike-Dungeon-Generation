using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomType : MonoBehaviour
{
    public int type;
    // Start is called before the first frame update
    public void RoomDestruction()
    {
        Destroy(gameObject);
    }
}
