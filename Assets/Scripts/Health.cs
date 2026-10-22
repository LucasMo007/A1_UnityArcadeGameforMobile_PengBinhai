//using System.Collections;
//using System.Collections.Generic;

//using UnityEngine;

//public class Health : MonoBehaviour

//{

//    [SerializeField] bool isPlayer;
//    [SerializeField] int health = 50;
//    [SerializeField] ParticleSystem hitEffect;

//    [SerializeField] GameObject healthPickupPrefab; // Assign via Inspector
//    [SerializeField] GameObject couscousAmmoPickupPrefab; // Assign via Inspector


//    [SerializeField] bool applyCameraShake;
//    CameraShake cameraShake;
//    [SerializeField] int score = 50;
//    //AudioPlayer audioPlayer;

//    ScoreKeeper scoreKeeper;
//    LevelManager levelManager;

//    void Awake()
//    {
//        cameraShake = Camera.main.GetComponent<CameraShake>();
//        //audioPlayer = FindObjectOfType<AudioPlayer>();

//        scoreKeeper = FindObjectOfType<ScoreKeeper>();
//        levelManager = FindObjectOfType<LevelManager>();
//    }
//    //void OnTriggerEnter2D(Collider2D other)
//    //{
//    //    DanmageDealer damageDealer = other.gameObject.GetComponent<DanmageDealer>();
//    //    if (damageDealer != null)


//    //    {
//    //        TakeDamage(damageDealer.GetDamage());
//    //        PlayHitEffect();
//    //        ShakeCamera();
//    //        damageDealer.Hit();
//    //        //audioPlayer.PlayDamageClip();

//    //        MainController.Instance.SoundManager.PlayDamageClip();
//    //    }
//    //}
//    void OnTriggerEnter2D(Collider2D other)
//    {
//        // 如果是玩家战机，不应该受到玩家自身子弹的伤害
//        if (isPlayer && other.CompareTag("PlayerLaser")) return;

//        // 如果是敌机，绝对不能受到敌机自身子弹（EnemyLaser）的伤害！
//        if (!isPlayer && other.CompareTag("EnemyLaser")) return;

//        DanmageDealer damageDealer = other.gameObject.GetComponent<DanmageDealer>();
//        if (damageDealer != null)
//        {
//            TakeDamage(damageDealer.GetDamage());
//            PlayHitEffect();
//            ShakeCamera();
//            damageDealer.Hit();

//            if (MainController.Instance != null && MainController.Instance.SoundManager != null)
//            {
//                MainController.Instance.SoundManager.PlayDamageClip();
//            }
//        }
//    }
//    public int GetHealth()
//        {  return health; }
//    public void TakeDamage(int damage)
//    {
//        health -= damage;
//        if (health <= 0)
//        {
//            Die();
//        }
//    }
//    /*void Die()
//    {
//        if (!isPlayer)
//        {
//            scoreKeeper.ModifyScore(score);
//        }
//        else
//        {

//            levelManager.LoadGameOver();
//        }
//         Destroy(gameObject);
//    }*/
//    void Die()
//    {
//        if (!isPlayer)
//        {
//            // 10–20% drop chance
//            float dropChance = Random.Range(0f, 1f);
//            /* if (dropChance < 0.15f) // e.g., 15% chance
//             {
//                 Instantiate(healthPickupPrefab, transform.position, Quaternion.identity);
//             }*/
//            if (dropChance < 0.25f)
//            {
//                Instantiate(healthPickupPrefab, transform.position, Quaternion.identity);
//            }
//            //else if (dropChance < 0.30f)
//            //{ Instantiate(couscousAmmoPickupPrefab, transform.position, Quaternion.identity);
//            //}

//            scoreKeeper.ModifyScore(score);
//        }
//        else
//        {
//            levelManager.LoadGameOver();
//        }

//        Destroy(gameObject);
//    }
//    void PlayHitEffect()
//    {
//        if (hitEffect != null) 
//        {ParticleSystem instance  =  Instantiate(hitEffect,transform.position,Quaternion.identity);
//            Destroy(instance.gameObject,instance.main.duration + instance.main.startLifetime.constantMax);
//        } 

//    }
//    void ShakeCamera()
//    {
//        if (cameraShake != null && applyCameraShake)
//        {
//            cameraShake.Play();
//        }



//    }
//    public void Heal(int amount)
//    {
//        health += amount;
//        // Optionally clamp to a max health value
//        health = Mathf.Min(health, 100); // change 100 to your max value
//    }
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int health = 50;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] GameObject healthPickupPrefab; // 白色回血胶囊预制体

    [SerializeField] bool applyCameraShake;
    CameraShake cameraShake;
    [SerializeField] int score = 50;

    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    void Awake()
    {
        if (Camera.main != null)
        {
            cameraShake = Camera.main.GetComponent<CameraShake>();
        }
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 友军火力保护：战机不吃玩家激光，敌机不吃敌机激光
        if (isPlayer && other.CompareTag("PlayerLaser")) return;
        if (!isPlayer && other.CompareTag("EnemyLaser")) return;

        DanmageDealer damageDealer = other.gameObject.GetComponent<DanmageDealer>();
        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            ShakeCamera();
            damageDealer.Hit();

            if (MainController.Instance != null && MainController.Instance.SoundManager != null)
            {
                MainController.Instance.SoundManager.PlayDamageClip();
            }
        }
    }

    public int GetHealth()
    {
        return health;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (!isPlayer)
        {
            // 25% 几率掉落白色回血胶囊
            float dropChance = Random.Range(0f, 1f);
            if (dropChance < 0.25f && healthPickupPrefab != null)
            {
                Instantiate(healthPickupPrefab, transform.position, Quaternion.identity);
            }

            if (scoreKeeper != null)
            {
                scoreKeeper.ModifyScore(score);
            }
        }
        else
        {
            if (levelManager != null)
            {
                levelManager.LoadGameOver();
            }
        }

        Destroy(gameObject);
    }

    void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    void ShakeCamera()
    {
        if (cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }

    public void Heal(int amount)
    {
        health += amount;
        health = Mathf.Min(health, 100);
    }
}