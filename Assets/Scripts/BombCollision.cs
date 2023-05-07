using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void OnParticleCollision(GameObject other) {
        BlowUp(other.tag);
    }

    void BlowUp(string tag) {
        if (tag != "Enemy") {
            GameObject obj = Instantiate(boom, transform);
            obj.transform.parent = vfxParentTransform.transform;
            Destroy(gameObject);
        }
    }
}
