using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject destroyVfx;
    [SerializeField] GameObject hitVfx;
    [SerializeField] int scoreOnHit = 10;
    [SerializeField] int scoreOnDefeat = 20;
    [SerializeField] int hitPoints = 3;

    ScoreTracker scoreTracker;
    GameObject vfxParentGameObject;

    void Start()
    {
        scoreTracker = FindObjectOfType<ScoreTracker>();
        AddRigidbody();
        vfxParentGameObject = GameObject.FindWithTag("VFXParent");
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
            InstantiateVfx(destroyVfx);
        }
    }

    void ProcessHit(GameObject other) {
        hitPoints--;
        scoreTracker.UpdateScore(scoreOnHit);
        InstantiateVfx(hitVfx);
    }

    void InstantiateVfx(GameObject vfx) {
        GameObject obj = Instantiate(vfx, transform.position, Quaternion.identity);
        obj.transform.parent = vfxParentGameObject.transform;
    }
}
