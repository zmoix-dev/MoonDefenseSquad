using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCannon : MonoBehaviour
{

    [SerializeField] int range = 300;

    [SerializeField] ParticleSystem[] shipLasers;

    [SerializeField] Transform playerLoc;

    void Start() {
        playerLoc = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        ShootPlayer();
    }

    void ShootPlayer() {
        float distance = CalcDistance(playerLoc);
        if (distance < range) {
            RotateLaser2(playerLoc);
            ProcessFire(distance);
        } else {
            StopFiring();
        }
    }

    float CalcDistance(Transform other) {
        float xDist = transform.parent.transform.position.x * transform.parent.transform.position.x - other.transform.position.x * other.transform.position.x;
        float yDist = transform.parent.transform.position.y * transform.parent.transform.position.y - other.transform.position.y * other.transform.position.y;
        float zDist = transform.parent.transform.position.z * transform.parent.transform.position.z - other.transform.position.z * other.transform.position.z;
        float distance = Mathf.Sqrt(Mathf.Abs(xDist + yDist + zDist));
        return distance;
    }
    
    void RotateLaser2(Transform other ) {
        foreach (ParticleSystem laser in shipLasers) {
            laser.gameObject.transform.LookAt(other);
        }
    }

    void ProcessFire(float distance) {
        foreach (ParticleSystem laser in shipLasers) {
            if (!laser.isEmitting) {
                laser.Play();
            }
        }
    }

    void StopFiring() {
        foreach (ParticleSystem laser in shipLasers) {
            if (laser.isEmitting) {
                laser.Stop();
            }
        }
    }

}
