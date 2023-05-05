using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject destroyVfx;
    [SerializeField] GameObject hitVfx;
    [SerializeField] Transform xfxParent;
    [SerializeField] int scoreOnHit = 10;
    [SerializeField] int scoreOnDefeat = 20;
    [SerializeField] int hitPoints = 3;

    ScoreTracker scoreTracker;

    void Start()
    {
        scoreTracker = FindObjectOfType<ScoreTracker>();
        AddRigidbody();
    }

    private void AddRigidbody()
    {
        Rigidbody body = gameObject.AddComponent<Rigidbody>();
        body.useGravity = false;
    }

    void OnParticleCollision(GameObject other) {
        ProcessHit(other);
        ProcessKilled();
    }

    private void ProcessKilled()
    {
        if (hitPoints <= 0) {
            Destroy(gameObject);
            scoreTracker.UpdateScore(scoreOnDefeat);
            GameObject vfx = Instantiate(destroyVfx, transform.position, Quaternion.identity);
            vfx.transform.parent = parent;
        }
    }

    void ProcessHit(GameObject other) {
        hitPoints--;
        scoreTracker.UpdateScore(scoreOnHit);
        GameObject vfx = Instantiate(hitVfx, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
    }
}
