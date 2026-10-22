using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;

    [Header("UI Reference")]
    [SerializeField] private Slider healthSlider; // 对应底部 HUD 的血条

    [Header("VFX & Camera Shake")]
    [SerializeField] private ParticleSystem hitEffect;       // 受击粒子特效
    [SerializeField] private GameObject explosionEffectPrefab; // 死亡大爆炸特效
    [SerializeField] private bool applyCameraShake = true;    // 是否开启受击震屏
    private CameraShake cameraShake;

    // 核心管理器引用
    private LevelManager levelManager;
    private AudioPlayer audioPlayer;

    private void Awake()
    {
        // 获取主相机上的屏幕震动组件
        if (Camera.main != null)
        {
            cameraShake = Camera.main.GetComponent<CameraShake>();
        }

        // 获取场景单例/全局管理器
        levelManager = FindObjectOfType<LevelManager>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    // 2D 物理受击与碰撞检测（学习 Health.cs，解决碰不到敌机/子弹/石头的问题）
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. 尝试获取子弹或撞击体上的 DamageDealer
        DanmageDealer damageDealer = other.gameObject.GetComponent<DanmageDealer>();
        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            ShakeCamera();
            damageDealer.Hit(); // 销毁击中玩家的敌机子弹

            // 播放受击音效
            PlayDamageSound();
        }
    }

    // 提供给外部（如 UIDisplay.cs）读取当前生命值
    public int GetHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    // 扣血逻辑
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // 回血逻辑（供 HealthPickup / 胶囊道具碰撞时调用）
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
    }

    // 刷新移动端底部血条 UI
    public void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    // 播放受击火花/粒子
    private void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    // 屏幕震动反馈
    private void ShakeCamera()
    {
        if (cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }

    // 受击音效调用
    //private void PlayDamageSound()
    //{
    //    if (audioPlayer != null)
    //    {
    //        audioPlayer.PlayDamageClip();
    //    }
    //    else if (MainController.Instance != null && MainController.Instance.AudioPlayer != null)
    //    {
    //        MainController.Instance.AudioPlayer.PlayDamageClip();
    //    }
    //}
    private void PlayDamageSound()
    {
        if (audioPlayer != null)
        {
            audioPlayer.PlayDamageClip();
        }
        else if (MainController.Instance != null && MainController.Instance.SoundManager != null)
        {
            MainController.Instance.SoundManager.PlayDamageClip();
        }
    }

    // 玩家死亡处理
    private void Die()
    {
        // 生成最终爆炸特效
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }

        // 隐藏战机模型与碰撞（防止死亡后继续受击触发）
        gameObject.SetActive(false);

        // 延迟跳转至 GameOver 结算场景
        if (levelManager != null)
        {
            levelManager.LoadGameOver();
        }
    }
}