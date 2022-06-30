using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public float[] position;
    

    public PlayerData (NavMeshMovement player)
    {
        
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
        Debug.Log(position[0]);
        Debug.Log(position[1]);
        Debug.Log(position[2]);
    }

    


}
