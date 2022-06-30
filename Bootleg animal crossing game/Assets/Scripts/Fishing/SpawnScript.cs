using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{

    public GameObject[] newFish;
    public Transform[] spawnLocations;
    public GameObject fishingTarget;
    
    public void Fish()
    {
        StartCoroutine(SpawnAfterTime());
    }
    
    IEnumerator SpawnAfterTime()
    {
        yield return new WaitForSeconds(4);
        GameObject nf = Instantiate(newFish[Random.Range(0, newFish.Length)], this.transform) as GameObject;
        nf.transform.localPosition = new Vector3(Random.Range(-1f, 10f), 0.00f, Random.Range(-1f, 10f));
    }

}
