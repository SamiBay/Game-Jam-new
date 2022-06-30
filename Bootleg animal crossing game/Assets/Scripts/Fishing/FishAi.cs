using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAi : MonoBehaviour
{
    public Transform Target;
    public float speed;

    private void Update()
    {

        Target = GameObject.FindWithTag("bobber").transform;

    }

}
