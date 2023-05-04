using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other) {
        switch (other.gameObject.tag) {
            case "Enemy": 
                Debug.Log("Collided with enemy.");
                break;
            default: 
                Debug.Log("Collided with... something else.");
                break;
        }
    }

    void OnTriggerEnter(Collider other) {
        // Debug.Log($"{gameObject.name} triggered {other.gameObject.name}");
    }
}
