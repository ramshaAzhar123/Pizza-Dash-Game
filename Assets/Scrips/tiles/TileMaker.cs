using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMaker : MonoBehaviour
{
    public Transform playerT;
    public GameObject blocksPref;
    public float spawnZ = 0f;
    private float blockLen = 18f;
    private int nbrBlockInScreen = 5;

    void Start()
    {
        for (int i = 0; i < nbrBlockInScreen; i++)
        {
            SpawnBlocks();
        }
    }

    void Update()
    {
        if (playerT.position.z > spawnZ - (nbrBlockInScreen * blockLen))
        {
            SpawnBlocks();
        }
    }

    private void SpawnBlocks()
    {
        GameObject go = Instantiate(blocksPref) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += blockLen;

        // Destroy the oldest block if there are more than nbrBlockInScreen blocks
        if (transform.childCount > nbrBlockInScreen)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }

}


