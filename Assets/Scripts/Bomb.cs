using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Header("Bomb type")]
    public bool isFireBomb = false;
    public bool isFreezeBomb = false;
    public bool isSleepBomb = false;

    [Header("Explosion PS")]
    public GameObject boomPS;
    public GameObject frostyPS;
    public GameObject sleepPS;
    GameObject explosionEffect;


    public float radius = 5f;
    public float explosionForce = 500f;

    public float delay = 3f;
    public float countdown;

    //camera shake properties
    public float CameraShakeIntensity = 2f;
    public float CameraShakeTimer = 1f;

    bool hasExploded = false;

    // Start is called before the first frame update
    void Start()
    {
        if (isFireBomb)
        {
            explosionEffect = boomPS;
        }
        else if (isFreezeBomb)
        {
            explosionEffect = frostyPS;
        }
        else if (isSleepBomb)
        {
            explosionEffect = sleepPS;
        }
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    void Explode()
    {
        //Show effect
        Instantiate(explosionEffect, new Vector3(transform.position.x, transform.position.y+3f, transform.position.z), transform.rotation);

        //get nearby objects
        Collider[] nearbyObjectsColliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider nearbyObject in nearbyObjectsColliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                //add force
                rb.AddExplosionForce(explosionForce, transform.position, radius);
                //damage
                Health healthScript = nearbyObject.GetComponent<Health>();
                if (healthScript != null)
                {
                    healthScript.ChangeHealth(-2);
                }

                //TO BREAK THE WOODENBOXES
                if (nearbyObject.CompareTag("Woodenbox")) 
                {
                    WoodenBoxHandler box = nearbyObject.GetComponent<WoodenBoxHandler>();
                    if (box) box.Break();
                }

                //shake the camera
                CameraShaker.instance.ShakeCamera(1,2);
            }
        }

        //destroy grenade
        Destroy(gameObject);
    }
}
