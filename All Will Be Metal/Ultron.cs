using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace All_Will_Be_Metal
{
    public enum ATTACKTYPES
    {
        TALLONS = 1,
        RAGE = 2,
        BLAST = 3
    }
    class Ultron
    {
        public int health = 8;
        public int firmware = 4;
        public int firmwareGained = 0;
        public int power = 1;
        public bool injured = false;
        public bool FollyOfMan = false;
        public bool usedMatterTransfer = false;
        public int score = 0;
        public int currAttack = 0;
        public int transferenceCost = 2;
        public int follyCost = 2;
        public int enoughCost = 2;
        public Condition condition;

        public Ultron()
        {
            condition = new Condition();
        }

        public void DamageUltron(int damage)
        {
            if (condition.incinerate == false)
            {
                if (damage > health)
                {
                    power += health;
                }
                else
                {
                    power += damage;
                }
            }
            health -= damage;
            if (health < 1)
            {
                firmware -= 1;
                firmwareGained++;
                enoughCost++;
                injured = true;
                health = 8;
                if (firmware < 0)
                {
                    health = 0;
                    return;
                    //GAME WIN
                }
            }
        }

        public void ReducePower(int amount)
        {
            power -= amount;
            if (power < 1)
            {
                power = 0;
            }
            if (power > 10)
            {
                power = 10;
            }
        }

        public void GainPower(int amount)
        {
            power += amount;
            if (power < 1)
            {
                power = 0;
            }
            if (power > 10)
            {
                power = 10;
            }
        }

        public String getUltronSpeed()
        {
            if (condition.slow == true)
            {
                return "small";
            }
            else
            {
                return "medium";
            }
        }

    }
}
