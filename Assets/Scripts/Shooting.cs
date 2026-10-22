 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float baseFiringRate = 0.2f;
    [SerializeField] float projcetileLifetime = 5f;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float fireRateVariance = 0f;
    [SerializeField] float minimumFireRate = 0.1f;

    [HideInInspector]public bool isFiring;

    Coroutine firingCoroutine;
    //AudioPlayer audioPlayer;

   // void Awake()
    //{
       // audioPlayer = FindObjectOfType<AudioPlayer>();
    //}
    void Start()
    {if (useAI)
        {
            isFiring = true;
        } 

    }

 
    void Update()
    {
        Fire();
    }

    void Fire()  
    {     
        if (isFiring && firingCoroutine == null ) 
        { 
         firingCoroutine = StartCoroutine(FireContinuously());

        }
      else if (!isFiring && firingCoroutine != null)
        { 
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }


    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject instance = Instantiate(projectilePrefab,
                                              transform.position, 
                                              Quaternion.identity);
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = transform.up * projectileSpeed;//up is the y axis
            }

            Destroy(instance, projcetileLifetime);

            float timeToNextProjectile = Random.Range(baseFiringRate - fireRateVariance, 
                                                       baseFiringRate + fireRateVariance);
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFireRate, float.MaxValue);

            //audioPlayer.PlayShootingClip();
            MainController.Instance.SoundManager.PlayShootingClip();
            yield return new WaitForSeconds(timeToNextProjectile);

        }
    }
}
