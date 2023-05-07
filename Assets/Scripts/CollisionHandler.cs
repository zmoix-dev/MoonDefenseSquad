using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] GameObject destroyVfx;
    [SerializeField] float invulnPostDamageTaken = 3f;
    [SerializeField] int lives = 3;
    [SerializeField] ParticleSystem[] lasers;

    PlayerControls controls;
    Rigidbody body;
    GameObject explosionVfx;

    float timeOfLastCollision;
    

    void Start() {
        timeOfLastCollision = 0f;
        controls = gameObject.GetComponent<PlayerControls>();
        body = gameObject.GetComponent<Rigidbody>();
    }

    void OnTriggerExit(Collider other) {
        // 3 second invuln window after colliding
        float time = Time.time;
        if (time - timeOfLastCollision > invulnPostDamageTaken) {
            lives--;
            timeOfLastCollision = time;

            if(lives <= 0) {
                controls.enabled = false;
                foreach (ParticleSystem laser in lasers) {
                    laser.Stop();
                }
                body.useGravity = true;
                Instantiate(destroyVfx, transform.position, Quaternion.identity);
                Invoke("reloadLevel", 2f);
            }
        }
    }

    void OnParticleCollision(GameObject other) {
        lives--;
    }

    void reloadLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
