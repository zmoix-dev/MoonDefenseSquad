using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DropBombs : MonoBehaviour
{

    [SerializeField] GameObject[] bombs;
    [SerializeField] float bombReload = 2f;
    [SerializeField] GameObject bombSpawn;
    float lastDrop;
    GameObject parent;
    


    void Start() {
        lastDrop = Time.time;
        parent = GameObject.FindWithTag("VFXParent");
    }

    // Update is called once per frame
    void Update()
    {
        Drop();
    }

    private void Drop()
    {
        float now = Time.time;
        if (now - lastDrop > bombReload) {
            Transform bombSpawnLocation = bombSpawn.transform;
            GameObject bomb = Instantiate(bombs[Random.Range(0,bombs.Length)], bombSpawnLocation);
            bomb.transform.parent = parent.transform;
            lastDrop = now;
        }
    }
}
