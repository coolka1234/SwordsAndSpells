using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;

namespace GraRPG
{
    [Serializable]
    public class cAbility
    {
        public bool ifBlockedAbility=false;
        /// <summary>
        /// Name of ability
        /// </summary>
        public string name="";
        /// <summary>
        /// Cooldown of the ability
        /// </summary>
        public int cooldown;
        /// <summary>
        /// 1. Buff 2. Damage 3. Enemy debuff
        /// </summary>
        public int type;
        /// <summary>
        /// Damage, if not type 2=0
        /// </summary>
        public int damage;
        /// <summary>
        /// Amount of healing
        /// </summary>
        public int healing;
        /// <summary>
        /// 1- Health, 2-Damage 3-Armor
        /// </summary>
        public int whatBuff;
        /// <summary>
        /// image of ability
        /// </summary>
        public Bitmap image = new Bitmap(Properties.Resources.Image1);
        public int ID;
        public int currentCooldown=0;
        public void save()
        {
            string path = null;
            path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string path2;
            path2 = Path.Combine(path, "GraRPG", "Abilities");
            Directory.CreateDirectory(path2);
            string filePath = Path.Combine(path2, ID + ".txt");
            Stream stream = File.Open(filePath, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
            stream.Close();
        }
        public cAbility load(cAbility ability,int id)
        {
            string path = null;
            path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string path2;
            path2 = Path.Combine(path, "GraRPG", "Abilities");
            string filePath = Path.Combine(path2, id + ".txt");
            try
            {
                Stream stream = File.Open(filePath, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                stream.Position = 0;
                ability = (cAbility)formatter.Deserialize(stream);
                stream.Close();
                ability.setImage();
            }
            catch (Exception ex)
            {
                ex.Data.ToString().Trim();
            }
            return ability;
        }
        public cAbility()
        {
            this.name = "";
            this.cooldown = 0;
            this.type = 1;
            this.damage = 0;
            this.healing = 0;
            this.whatBuff = 1;
            this.image = Properties.Resources.Image1;
            ID = 0;
        }
        public string createTip(cPlayer player)
        {
            string tip;
            double realHealing = healing;
            if (!(player.equipment[0] is null))
            {
                if (player.equipment[0].itemID==1005)
                {
                    realHealing *= 1.15;
                }
            }
            tip = name + $"{Environment.NewLine}" + "Cooldown: " + currentCooldown + "/" + cooldown + $"{Environment.NewLine}" + "Damage: " + (damage+calculateBonusDmg(player)) + $"{Environment.NewLine}" + "Strenght of heal(if applies): " + (realHealing+calculateBonusHealing(player)) + $"{Environment.NewLine}"+"Strenght of buff/debuff(if applies): "+(healing+calculateBonusBuff(player))+Environment.NewLine;
            if(ID==7||ID==10)
            {
                tip += "Effect: Lifesteal";
                tip += $"{Environment.NewLine}";
                return tip;
            }
            if(type == 1)
            {
                tip += "Effect: Self Buff";
                tip += $"{Environment.NewLine}";
                if (whatBuff == 0)
                {
                    tip += "Buffs: Nothing";
                    tip += $"{Environment.NewLine}";
                }
                else if (whatBuff == 1)
                {
                    tip += "Buffs: Health";
                    tip += $"{Environment.NewLine}";
                }
                else if (whatBuff == 3)
                {
                    tip += "Buffs: Armor";
                    tip += $"{Environment.NewLine}";
                }
                else if (whatBuff == 2)
                {
                    tip += "Buffs: Damage";
                    tip += $"{Environment.NewLine}";
                }
            }
            else if(type == 2)
            {
                tip += "Effect: Damage";
                tip += $"{Environment.NewLine}";
            }
            else if(type==3)
            {
                tip += "Effect: Enemy debuff";
                tip += $"{Environment.NewLine}";
                if (whatBuff == 0)
                {
                    tip += "Debuffs: Nothing";
                    tip += $"{Environment.NewLine}";
                }
                else if (whatBuff == 1)
                {
                    tip += "Debuffs: Health";
                    tip += $"{Environment.NewLine}";
                }
                else if (whatBuff == 3)
                {
                    tip += "Debuffs: Armor";
                    tip += $"{Environment.NewLine}";
                }
                else if (whatBuff == 2)
                {
                    tip += "Debuffs: Damage";
                    tip += $"{Environment.NewLine}";
                }
            }
            return tip;
        }
        public void setImage()
        {
            if(ID==1)
            {
                image = Properties.Resources.fireball;
            }
            if(ID==2)
            {
                image = Properties.Resources.Lightning;
            }
            if(ID==3)
            {
                image = Properties.Resources.Healing;
            }
            if(ID==4)
            {
                image = Properties.Resources.IronSkin;
            }
            if(ID==5)
            {
                image = Properties.Resources.Barricade;
            }
            if(ID==6)
            {
                image = Properties.Resources.Weakness;
            }
            else if(ID==7)
            {
                image = Properties.Resources.Backstab;
            }
            else if(ID==8)
            {
                image = Properties.Resources.ArrowShot;
            }
            else if (ID == 9)
            {
                image = Properties.Resources.Shield;
            }
            else if (ID == 10)
            {
                image = Properties.Resources.Lifesteal;
            }
        }
        
        public void useAbility(cPlayer player, cEnemy enemy)
        {
            if (currentCooldown == 0)
            {
                Random random = new Random();
                int chance = random.Next(5);
                double realHealing = healing;
                if (!(player.equipment[0] is null))
                {
                    if (player.equipment[0].itemID == 1005)
                    {
                        realHealing *= 1.15;
                    }
                }
                if (chance == 0 && enemy.ID == 3)
                {
                    currentCooldown = cooldown;
                    ifBlockedAbility = true;
                    return;
                }
                if (ID == 7||ID==10)
                {
                    enemy.health -= damage+calculateBonusDmg(player);
                    player.health += (int)realHealing+calculateBonusHealing(player);
                    currentCooldown = cooldown;
                    return;
                }
                if (type == 1)
                {
                    if (whatBuff == 1)
                    {
                        if (player.health + (int)realHealing + calculateBonusHealing(player) <= player.maxHealth)
                        {
                            player.health = player.health + (int)realHealing + calculateBonusHealing(player);
                        }
                        else
                        {
                            player.health = player.maxHealth;
                        }
                    }
                    else if (whatBuff == 2)
                    {
                        
                        player.maxDamage = player.maxDamage + healing+calculateBonusBuff(player);
                    }
                    else if (whatBuff == 3)
                    {
                        player.armor += healing+calculateBonusBuff(player);
                    }
                }
                else if (type == 2)
                {
                    enemy.health -= damage+calculateBonusBuff(player);
                }
                else if (type == 3)
                {
                    if (whatBuff == 2)
                    {
                        enemy.maxDamage = enemy.maxDamage - healing-calculateBonusBuff(player);
                    }
                    else if (whatBuff == 3)
                    {
                        enemy.armor -= healing+calculateBonusBuff(player);
                    }
                }
                currentCooldown = cooldown;
            }
        }
        public bool isOnCD()
        {
            if(currentCooldown==0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private int calculateBonusDmg(cPlayer player)
        {
            int bonus = 0;
            if (damage != 0)
            {
                bonus = damage / 10;
            }
            bonus = (bonus * player.inteligence) / 100;
            return bonus;
        }
        private int calculateBonusBuff(cPlayer player)
        {
            int bonus = 0;
            if(healing!=0)
            {
                bonus = healing / 40;
            }
            bonus=(bonus * player.inteligence)/100;
            return bonus;
        }
        private int calculateBonusHealing(cPlayer player)
        {
            int bonus = 0;
            if (healing != 0)
            {
                bonus = healing / 20;
            }
            bonus = (bonus * player.inteligence) / 100;
            return bonus;
        }
    }
}
