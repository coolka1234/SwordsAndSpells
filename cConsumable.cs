using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GraRPG
{
    [Serializable]
    public class cConsumable:cItem
    {
        /// <summary>
        /// Number of uses
        /// </summary>
        public int numOfUses;
        /// <summary>
        /// 1-Buff 2-Heal 3-Increases statistic 4-Debuff
        /// </summary>
        public int effect;
        /// <summary>
        /// 1-Health 2-Damage 3-Armor 4-Int
        /// </summary>
        public int WhichStat;
        /// <summary>
        /// Strenght of buff
        /// </summary>
        public int strenght;
        public void save()
        {
            string path = null;
            path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string path2;
            path2 = Path.Combine(path, "GraRPG", "Consumables");
            Directory.CreateDirectory(path2);
            string filePath = Path.Combine(path2, itemID + ".txt");
            Stream stream = File.Open(filePath, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
            stream.Close();
        }
        public cConsumable load(cConsumable consumable, int id)
        {
            string path = null;
            path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string path2;
            path2 = Path.Combine(path, "GraRPG", "Consumables");
            string filePath = Path.Combine(path2, id + ".txt");
            try
            {
                Stream stream = File.Open(filePath, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                stream.Position = 0;
                consumable = (cConsumable)formatter.Deserialize(stream);
                stream.Close();
                consumable.loadImage();
            }
            catch
            {

            }
            return consumable;
        }
        public string createTip()
        {
            string tip;
            if(itemID==1000)
            {
                tip = itemName + $"{Environment.NewLine}" + "Value: " + cost + $"{Environment.NewLine}" + "Uses left: " + numOfUses + Environment.NewLine + "If used, the next death will be avoided and the player gets healed to half HP";
                return tip;
            }
            else if (itemID == 1001)
            {
                tip = itemName + $"{Environment.NewLine}" + "Value: " + cost + $"{Environment.NewLine}" + "Uses left: " + numOfUses + Environment.NewLine + "Instakills the enemy";
                return tip;
            }
            else if(itemID==1002)
            {
                tip = itemName + $"{Environment.NewLine}" + "Value: " + cost + $"{Environment.NewLine}" + "Uses left: " + numOfUses + Environment.NewLine + "Heals you fully.";
                return tip;

            }
            tip = itemName + $"{Environment.NewLine}" + "Value: " + cost + $"{Environment.NewLine}" + "Uses left: " + numOfUses + $"{Environment.NewLine}" + "Strenght: "+strenght+Environment.NewLine;
            if(itemID==5)
            {
                tip += "Effect: Heals and Buffs armor";
                return tip;
            }
            if(effect==1)
            {
                tip += "Effect: Buff";
                tip += Environment.NewLine;
                if(WhichStat==1)
                {
                    tip += "Buffs: Health";
                    tip += Environment.NewLine;
                }
                else if(WhichStat==2)
                {
                    tip += "Buffs: Damage";
                    tip += Environment.NewLine;
                }
                else if(WhichStat==3)
                {
                    tip += "Buffs: Armor";
                    tip += Environment.NewLine;
                }
                else if(WhichStat==4)
                {
                    tip += "Buffs: Inteligence";
                    tip += Environment.NewLine;
                }
            }
            else if(effect==2)
            {
                tip += "Effect: Heal";
                tip += Environment.NewLine;
            }
            else if(effect==4)
            {
                tip += "Effect:  Enemy Debuff";
                tip += Environment.NewLine;
                if (WhichStat == 1)
                {
                    tip += "Debuffs: Health";
                    tip += Environment.NewLine;
                }
                else if (WhichStat == 2)
                {
                    tip += "Debuffs: Damage";
                    tip += Environment.NewLine;
                }
                else if (WhichStat == 3)
                {
                    tip += "Debuffs: Armor";
                    tip += Environment.NewLine;
                }
            }
            return tip;
        }
        public void loadImage()
        {
            if(itemID==1)
            {
                bitmap = Properties.Resources.PotionHP;
            }
            else if(itemID==2)
            {
                bitmap = Properties.Resources.PotionDamage;
            }
            else if(itemID==3)
            {
                bitmap = Properties.Resources.PotionDebuff;
            }
            else if(itemID==4)
            {
                bitmap = Properties.Resources.ConcetrationPot;
            }
            else if(itemID==5)
            {
                bitmap = Properties.Resources.HoneyPotion;
            }
            else if(itemID==6)
            {
                bitmap = Properties.Resources.BookOfKnowledge;
            }
            else if(itemID ==7)
            {
                bitmap=Properties.Resources.Small_Potion;
            }
            else if(itemID==8)
            {
                bitmap = Properties.Resources.Poison;
            }
            else if (itemID == 9)
            {
                bitmap = Properties.Resources.ThKnife;
            }
            else if(itemID==1000)
            {
                bitmap = Properties.Resources.ResPotion; //add working
            }
            else if (itemID == 1001)
            {
                bitmap = Properties.Resources.InstaKill; //add working
            }
            else if(itemID == 1002)
            {
                bitmap = Properties.Resources.FullHeal;
            }

        }
        public void useItem(ref cPlayer player,ref cEnemy enemy)
        {
            if(itemID == 1000)
            {
                player.ifResUsed = true;
                numOfUses--;
                return;
            }
            else if(itemID==1001)
            {
                enemy.health = 0;
                numOfUses--;
                return;
            }
            else if (itemID == 1002)
            {
                player.health = player.maxHealth;
                numOfUses--;
                return;
            }
            if (itemID == 5)
            {
                if (player.health + this.strenght >= player.maxHealth)
                {
                    player.health = player.maxHealth;
                }
                else
                {
                    player.health += 200;
                }
                player.armor += strenght;
                player.gotBuffed(this);
                numOfUses--;
                return;
            }
            if(effect==1)
            {
                if(WhichStat<5)
                {
                    if(WhichStat==1)
                    {
                        player.maxHealth += strenght;
                    }
                    else if(WhichStat==2)
                    {
                        player.maxDamage += strenght;
                        player.minDamage += strenght;
                        player.gotBuffed(this);
                    }
                    else if(WhichStat==3)
                    {
                        player.armor += strenght;
                        player.gotBuffed(this);
                    }
                    else if(WhichStat==4)
                    {
                        player.inteligence+=strenght;
                        player.gotBuffed(this);
                    }
                    
                }
            }
            else if(effect==2)
            {
                if(player.health+this.strenght>=player.maxHealth)
                {
                    player.health = player.maxHealth;
                }
                else
                {
                    player.health+=this.strenght;
                }
            }
            else if(effect==4)
            {                
                player.debuff(ref enemy, this);
            }
            numOfUses--;
        }
    }
}
