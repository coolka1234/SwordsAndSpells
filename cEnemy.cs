using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;
using System.Runtime;

namespace GraRPG
{
    [Serializable]
    public class cEnemy:cPlayer
    {
        public string ability;
        //public cConsumable dropCon = new cConsumable();
        //public cEquipment dropEq = new cEquipment();
        public Bitmap Bitmap = new Bitmap(Properties.Resources.Image1);
        public void save()
        {
            string path = null;
            path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string path2;
            path2 = Path.Combine(path, "GraRPG", "Enemy");
            Directory.CreateDirectory(path2);
            string filePath = Path.Combine(path2, ID + ".txt");
            Stream stream = File.Open(filePath, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
            stream.Close();
        }
        public cEnemy load(cEnemy enemy, int Id)
        {
            string path = null;
            path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string path2;
            path2 = Path.Combine(path, "GraRPG", "Enemy");
            string filePath = Path.Combine(path2, Id + ".txt");
            try
            {
                Stream stream = File.Open(filePath, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                stream.Position = 0;
                enemy = (cEnemy)formatter.Deserialize(stream);
                stream.Close();
                enemy.loadImage();
            }
            catch (Exception ex)
            {

                ex.StackTrace.ToString();
            }
            return enemy;
        }
        public void loadImage()
        {
            if(ID==1)
            {
                Bitmap = Properties.Resources.skeleton;
                ability = "Reduces any non ability damage by 10%";
            }
            else if(ID==2)
            {
                Bitmap = Properties.Resources.Blob;
                ability = "Absorbs some of non-ability damage received";
            }
            else if(ID==3)
            {
                Bitmap = Properties.Resources.Wizard;
                ability = "Has a chance to block a spell";
            }
            else if(ID==4)
            {
                Bitmap = Properties.Resources.Golem;
                ability = "Every attack slightly hardens him, increasing his armor";
            }
            else if(ID ==5)
            {
                Bitmap = Properties.Resources.Cyborg;
                ability = "Its electric aura hurts the player everytime he receives damage";
            }
            else if(ID==6)
            {
                Bitmap = Properties.Resources.Vampire;
                ability = "Every attack steals some health from you";
            }
            else if(ID==7)
            {
                Bitmap = Properties.Resources.Dracula;
                ability = "Heals 5% missing health every turn";
            }
            else if(ID==8)
            {
                Bitmap = Properties.Resources.Zombie;
                ability = "After getting killed, gets resurected with 50% HP";
            }
        }
        public void setLevel(int level1)
        {
            if (playerClass == 1)
            {
                health += level1 * 100;
                increaseDMG(level1 * 40);
                inteligence += level1 * 30;
            }
            else if (playerClass == 2)
            {
                health += level1*200;
                increaseDMG(level1 * 30);
                inteligence += level1 * 15;
            }
            else if (playerClass == 3)
            {
                health += level1 * 350;
                increaseDMG(level1 * 15);
                inteligence += level1 * 15;
            }
            else if (playerClass == 4)
            {
                health += level1 * 120;
                increaseDMG(level1 * 50);
                inteligence += level1 * 20;
            }
            level=level1;
        }
        public new string createTip()
        {
            string tip;
            tip = $"Name: " + name + $"{Environment.NewLine}"+"Ability: "+ability+Environment.NewLine + "Max health: " + maxHealth + $"{Environment.NewLine}" + "Average damage: " + (maxDamage + minDamage) / 2 + $"{Environment.NewLine}" + "Inteligence: " + inteligence + Environment.NewLine + "Class: " + playerClass + $"{Environment.NewLine}" + "Luck: " + luck + $"{Environment.NewLine}" + "Armor: " + armor;
            return tip;
        }
    }
}
