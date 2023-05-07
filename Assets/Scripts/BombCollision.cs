using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This handles a specific collision not used in all Enemies that explodes upon hitting the ground.
public class BombCollision : MonoBehaviour
{
    [SerializeField] GameObject boom;
    GameObject vfxParentTransform;

    void Start() {
        vfxParentTransform = GameObject.FindWithTag("VFXParent");
    }

    void OnCollisionEnter(Collision other) {
        BlowUp(other.gameObject.tag);
    }

    void BlowUp(string tag) {
        if (tag != "Enemy") {
            GameObject obj = Instantiate(boom, transform);
            obj.transform.parent = vfxParentTransform.transform;
            Destroy(gameObject);
        }
    }
}
