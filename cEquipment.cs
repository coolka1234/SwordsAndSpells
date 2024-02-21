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
    public class cEquipment:cItem
    {
        /// <summary>
        /// Damage dealt
        /// </summary>
        public int damage;
        /// <summary>
        /// 0-Boots 1-Leggins 2-Armor 3-Helmet 4-Weapon
        /// </summary>
        public int whichPiece;
        /// <summary>
        /// How much armor it gives
        /// </summary>
        public int armor;
        /// <summary>
        /// What its weight(bigger weight decreases dmg)
        /// </summary>
        public int weight;
        ///// <summary>
        ///// Does it give luck
        ///// </summary>
        //public int luck;
        public void save()
        {
            string path = null;
            path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string path2;
            path2 = Path.Combine(path, "GraRPG", "Equipment");
            Directory.CreateDirectory(path2);
            string filePath = Path.Combine(path2, itemID + ".txt");
            Stream stream = File.Open(filePath, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
            stream.Close();
        }
        public cEquipment load(cEquipment eq, int ID)
        {
            string path = null;
            path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string path2;
            path2 = Path.Combine(path, "GraRPG", "Equipment");
            string filePath = Path.Combine(path2, ID + ".txt");
            try
            {
                Stream stream = File.Open(filePath, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                stream.Position = 0;
                eq = (cEquipment)formatter.Deserialize(stream);
                stream.Close();
                eq.loadImage(eq);
            }
            catch (Exception ex)
            {
                ex.Data.ToString();
            }
            return eq;
        }
        public void loadImage(cEquipment equipment)
        {
            if(itemID==1)
            {
                equipment.bitmap = Properties.Resources.sword;
            }
            else if(itemID==2)
            {
                equipment.bitmap = Properties.Resources.Dagger;
            }
            else if(itemID==3)
            {
                equipment.bitmap = Properties.Resources.Broomstick;
            }
            else if(itemID==4)
            {
                equipment.bitmap = Properties.Resources.FurBoots;
            }
            else if(itemID==5)
            {
                equipment.bitmap = Properties.Resources.SteelLeggins;
            }
            else if(itemID==6)
            {
                equipment.bitmap = Properties.Resources.HideChest;
            }
            else if(itemID==7)
            {
                equipment.bitmap = Properties.Resources.SteelHelmet;
            }
            else if(itemID==8)
            {
                equipment.bitmap = Properties.Resources.DaggerBlade;
            }
            else if(itemID==9)
            {
                equipment.bitmap = Properties.Resources.Bow;
            }
            else if(itemID==10)
            {
                equipment.bitmap = Properties.Resources.FurArmor;
            }
            else if (itemID == 11)
            {
                equipment.bitmap = Properties.Resources.GoodLeggins;
            }
            else if(itemID==12)
            {
                equipment.bitmap = Properties.Resources.SorcerersShoes;
            }
            else if(itemID==13)
            {
                equipment.bitmap = Properties.Resources.Ebony_Leggins;
            }
            else if(itemID==1000) //Legendarne
            {
                equipment.bitmap = Properties.Resources.SwordOfYmir;
            }
            else if(itemID==1001)
            {
                equipment.bitmap = Properties.Resources.Bloodforge;
            }
            else if(itemID==1002)
            {
                equipment.bitmap = Properties.Resources.Serrated;
            }
            else if(itemID==1003)
            {
                equipment.bitmap = Properties.Resources.HelmetOfMercury;
            }
            else if (itemID == 1003)
            {
                equipment.bitmap = Properties.Resources.HelmetOfMercury;
            }
            else if (itemID == 1004)
            {
                equipment.bitmap = Properties.Resources.ChestpieceAres;
            }
            else if (itemID == 1005)
            {
                equipment.bitmap = Properties.Resources.Shoes_of_Healer;
            }
            else if (itemID == 1006)
            {
                equipment.bitmap = Properties.Resources.TechLeggins;
            }


        }
        public string createTip()
        {
            string tip;
            tip = $"Name: " + itemName + $"{Environment.NewLine}" + "Value: " + cost + $"{Environment.NewLine}" + "Damage: " + damage + $"{Environment.NewLine}" + "Armor: " + armor + $"{Environment.NewLine}" + "Weight: " + weight + $"{Environment.NewLine}";
            if(itemID == 1000)
            {
                tip += "FROST: Reduces enemy damage permamently.";
            }
            else if(itemID==1001)
            {
                tip += "LIFESTEAL: Permamently heals you for 5% attack damage dealt.";
            }
            else if(itemID==1002)
            {
                tip += "CRUELTY: Ignores 20% of armor.";
            }
            else if (itemID == 1003)
            {
                tip += "QUICKSILVER: It weights nothing.";
            }
            else if(itemID==1004)
            {
                tip += "THORNS: Deflects 10% of all damage taken.";
            }
            else if(itemID==1005)
            {
                tip += "RESTORATION: Healing spells amplified by 15%.";
            }
            else if (itemID == 1006)
            {
                tip += "TECH: Heals you for 3% of missing health every attack.";
            }
            return tip;
        }
    }
}
