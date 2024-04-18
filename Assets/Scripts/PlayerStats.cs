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

    private bool canVaccum;
    public int pullPower;
    public int pullBattery;
    public int powerLevel;
    public int pullBatteryReloadSpeed;

    public bool canDash;

    // Objects
    public Image healthbar;
    public Transform firePoint;
    public Image powerbar;

    void Start()
    {
        canVaccum = true;
        
        healthbar.fillAmount = (float) health / maxHealth;
        powerbar.fillAmount = (float) powerLevel / pullBattery;
    }
     

    // HEALTH BAR
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
    //


    // POWER BAR
    public void usePower(int amount)
    {
        this.powerLevel -= amount;

        if (this.powerLevel <= 0)
        {
            this.powerLevel = 0;
            powerbar.fillAmount = 0;
            StartCoroutine(delayUseVaccum());
        }
        else
        {
            powerbar.fillAmount = (float) powerLevel / pullBattery;
        }
    }

    public void addPower(int amount)
    {
        this.powerLevel += amount;

        if (this.powerLevel > pullBattery)
        {
            this.powerLevel = pullBattery;
            powerbar.fillAmount = 100f;
        }
        else
        {
            powerbar.fillAmount = (float) powerLevel / pullBattery;
        }
    }

    IEnumerator delayUseVaccum()
    {
        this.canVaccum = false;
        yield return new WaitForSeconds(1f);
        this.canVaccum = true;
    }

    public void setFullHealth()
    {
        this.health = this.maxHealth;
        healthbar.fillAmount = 1;
    }

    public int getHealth()
    {
        return health;
    }

    public int getMaxHealth()
    {
        return maxHealth;
    }

    public float getSpeed()
    {
        return speed;
    }

    public int getPullPower()
    {
        return pullPower;
    }

    public int getBattery()
    {
        return pullBattery;
    }

    public int getReloadSpeed()
    {
        return pullBatteryReloadSpeed;
    }

    public bool getCanVaccum()
    {
        return canVaccum;
    }
}
