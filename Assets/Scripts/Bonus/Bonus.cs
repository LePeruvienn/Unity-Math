using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace bonus
{
    public abstract class Bonus
    {
        private string name;
        private string description;

        private string imagePath;

        public Bonus(string name, string description, string image)
        {
            this.name = name;
            this.description = description;
            this.imagePath = image;
        }

        public virtual void apply(PlayerStats player) {

            Debug.Log("Base apply");

        }
        public string getName() { return name; }
        public string getDescription() { return description; }
        public string getImagePath() { return imagePath; }
    }

    public class BonusSpeed : Bonus
    {

        private float speedAmount;
        
        public BonusSpeed(float speedAmount) : base("Speed", "Give you more speed", "Bonus/magic-boot")
        {
            this.speedAmount = speedAmount;
        }

        public override void apply(PlayerStats player)
        {
            player.speed += speedAmount;
        }
    }

    public class BonusMaxhealth : Bonus
    {

        private int healthamount;

        public BonusMaxhealth(int healthamount) : base("Maxhealth", ("Give you more max health ( +" + healthamount + ")"), "Bonus/maxhealth")
        {
            this.healthamount = healthamount;
        }

        public override void apply(PlayerStats player)
        {
            player.addMaxHealth(healthamount);
            player.setFullHealth();
        }
    }

    public class BonusRegen : Bonus
    {

        private int amount;

        public BonusRegen(int amount) : base("Health Regen", ("Give you more health regen ( +" + amount + ")"), "Bonus/regen")
        {
            this.amount = amount;
        }

        public override void apply(PlayerStats player)
        {
            player.addRegen(amount);
            player.setFullHealth();
        }
    }

    public class BonusDash : Bonus
    {

        public BonusDash() : base("Dash", "Give you the ability to dash", "Bonus/dash"){ }

        public override void apply(PlayerStats player)
        {
            player.setCanDash(true);
        }
    }

    public class BonusPower : Bonus
    {

        private int amount;

        public BonusPower(int amount) : base("Power", "Give you more pull power to youre vaccum (" + amount + ")", "Bonus/energetic")
        {
            this.amount = amount;
        }

        public override void apply(PlayerStats player)
        {
            player.pullPower += amount;
        }
    }

    public class BonusPowerBattery : Bonus
    {

        private int amount;

        public BonusPowerBattery(int amount) : base("Battery", "Give you more Battery to your vaccum", "Bonus/battery")
        {
            this.amount = amount;
        }

        public override void apply(PlayerStats player)
        {
            player.addBattery(this.amount);
        }
    }

    public class BonusPowerReloadSpeed : Bonus
    {

        private int amount;

        public BonusPowerReloadSpeed(int amount) : base("Realod", "Make your vaccum reload faster", "Bonus/batteryFast")
        {
            this.amount = amount;
        }

        public override void apply(PlayerStats player)
        {
            player.pullBatteryReloadSpeed += amount;
        }
    }
}
