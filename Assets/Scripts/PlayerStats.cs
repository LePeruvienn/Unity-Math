using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    
    // Stats
    public int health;
    public int maxHealth;
    public int healthRegen;

    public float speed;

    public int pullPower;
    public int pullBattery;
    public int pullBatteryReloadSpeed;

    public bool canDash;

    // Objects
    public Image healthbar;
    public Transform firePoint;

    void Start()
    {
        healthbar.fillAmount = (float) health / maxHealth;
    }

    public void takeDamage(int damage)
    {
        this.health -= damage;

        if (this.health <= 0)
        {
            this.health = 0;
            healthbar.fillAmount = 0;
            Destroy(this.gameObject);
        }
        else
        {
            healthbar.fillAmount = (float) health / maxHealth;
        }
    }

    public void heal(int healvalue)
    {
        this.health += healvalue;

        if (this.health > maxHealth)
        {
            this.health = maxHealth;
            healthbar.fillAmount = 100f;
        }
        else
        {
            healthbar.fillAmount = (float) health / maxHealth;
        }
    }
}
