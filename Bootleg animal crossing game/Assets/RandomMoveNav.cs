using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class RandomMoveNav : MonoBehaviour
{
    NavMeshAgent navMesh;
    // Start is called before the first frame update
    private void Awake()
    {
        navMesh = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        navMesh.Move(transform.forward * Time.deltaTime);
    }
}
