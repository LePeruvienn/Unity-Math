using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

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
    public Image healthBorder;
    public Image healthBack;

    public Transform firePoint;
    public Image powerbar;
    public Image powerBorder;
    public Image powerBack;

    public Transform modeOrigin;
    private List<Transform> modesTransform = new List<Transform>();
    public TextMeshProUGUI txtMode;

    // Calculable

        //Healthbar
    private float maxfillbar;
    public int maxfillbarHP = 1000;

    private float maxfillPowerbar;
    public int maxFillbarPower = 500;

    //modes
    private Vector2 baseScale;




    void Start()
    {
        canVaccum = true;

        // HEALTH AND POWER BAR
        maxfillbar = (float) maxHealth/maxfillbarHP;

        healthbar.fillAmount = ((float) health / maxHealth) * maxfillbar;
        healthBorder.fillAmount = maxfillbar + 0.01f;
        healthBack.fillAmount = maxfillbar;

        maxfillPowerbar = (float) pullBattery / maxFillbarPower;

        powerbar.fillAmount = ((float)powerLevel / pullBattery) * maxfillPowerbar;
        powerBorder.fillAmount = maxfillPowerbar + 0.02f;
        powerBack.fillAmount = maxfillPowerbar;

        // MODES
        getAllModesTransform();
        this.baseScale = modesTransform[0].localScale;
    }
     

    // MODES

    public void getAllModesTransform()
    {
        foreach (Transform child in modeOrigin)
        {
            this.modesTransform.Add(child);
        }
    }

    public void nextMode()
    {
        setUpModes();
        this.modeOrigin.Rotate(0, 0, 90);
    }

    public void previousMode()
    {
        setUpModes();
        this.modeOrigin.Rotate(0, 0, -90);
    }

    private void setUpModes()
    {

        foreach (Transform child in modesTransform)
        {
            if(child.rotation.z == 0f)
            {
                child.rotation =  Quaternion.Euler(child.eulerAngles.x, child.eulerAngles.y, 90f);
            }
            else if(child.rotation.z == 90f)
            {
                child.rotation = Quaternion.Euler(child.eulerAngles.x, child.eulerAngles.y, 0f);
            }

            child.localScale = this.baseScale / 2;
        }

        int indexMode = GetComponent<PlayerAimWeapon>().getIndexMode();
        modesTransform[indexMode].localScale = this.baseScale;

        showMode();
    }

    private void showMode()
    {
        char mode = GetComponent<PlayerAimWeapon>().getMode();
        
        switch (mode)
        {
            case '+':
                this.txtMode.text = "Additionner";
                break;
            case '-':
                this.txtMode.text = "Soustraire";
                break;
            case '�':
                this.txtMode.text = "Multiplier";
                break;
            case '�':
                this.txtMode.text = "Diviser";
                break;
        }
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
            healthbar.fillAmount = ((float) health / maxHealth) * maxfillbar;
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
            healthbar.fillAmount = ((float) health / maxHealth) * maxfillbar;
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
            powerbar.fillAmount = ((float) powerLevel / pullBattery) * maxfillPowerbar;
        }
    }

    public void addPower(int amount)
    {
        this.powerLevel += amount;

        if (this.powerLevel > pullBattery)
        {
            this.powerLevel = pullBattery;
            powerbar.fillAmount = maxfillPowerbar;
        }
        else
        {
            powerbar.fillAmount = ((float) powerLevel / pullBattery) * maxfillPowerbar;
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
        healthbar.fillAmount = maxfillbar;
    }


    public int getHealth()
    {
        return health;
    }

    public int getMaxHealth()
    {
        return maxHealth;
    }

    public void addMaxHealth(int amount)
    {
        this.maxHealth += amount;

        maxfillbar = (float)maxHealth / maxfillbarHP;
        healthbar.fillAmount = ((float)health / maxHealth) * maxfillbar;

        healthBorder.fillAmount = maxfillbar + 0.01f;
        healthBack.fillAmount = maxfillbar;
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

    public void addBattery(int amount) 
    {
        this.pullBattery += amount;

        maxfillPowerbar = (float)pullBattery / maxFillbarPower;

        powerbar.fillAmount = ((float)powerLevel / pullBattery) * maxfillPowerbar;
        powerBorder.fillAmount = maxfillPowerbar + 0.02f;
        powerBack.fillAmount = maxfillPowerbar;

    }

    public int getReloadSpeed()
    {
        return pullBatteryReloadSpeed;
    }

    public bool getCanVaccum()
    {
        return canVaccum;
    }

    public bool getCanDash()
    {
        return canDash;
    }
}