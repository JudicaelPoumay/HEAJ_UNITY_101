using UnityEngine;

public class SphereSpawner : MonoBehaviour
{
    public float spawnInterval = 5f;
    public GameObject spherePrefab;
    private float timer = 0f;

    void Start()
    {
        timer = spawnInterval;
        spawn();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            spawn();
            timer = spawnInterval;
        }
    }

    void spawn()
    {
        var sphere = Instantiate(spherePrefab, transform.position, transform.rotation);
        Destroy(sphere, spawnInterval); 
    }
}
