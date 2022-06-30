using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseEnter : MonoBehaviour
{
    [SerializeField]
    GameObject[] objects;
    private void OnMouseEnter()
    {
        objects[0].SetActive(false);
        objects[1].SetActive(true);
        print("moi");
    }
}
