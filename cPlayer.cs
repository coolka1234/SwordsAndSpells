using System;
using System.Runtime.Serialization;

namespace GraRPG
{
    [Serializable]
    public class cPlayer : cEntity
    {
        public cPlayer() { }
        /// <summary>
        /// If potion of Resurection has been used
        /// </summary>
        public bool ifResUsed=false;
        /// <summary>
        /// Increases ability strenght
        /// </summary>
        public int inteligence;
        /// <summary>
        /// 1-Mage,2-Hunter,3-Warrior,4-Assasin
        /// </summary>
        public int playerClass;
        /// <summary>
        /// How long will ability last?
        /// </summary>
        public int[] abilityCD = new int[5];
        public int[,] abilityCD2=new int[5,4];
        /// <summary>
        /// Eq slots, 0-boots,1-leggins,2-armor,3-helmet,4-weapon
        /// </summary>
        public bool[] ifEqSpotTaken = new bool[5];
        /// <summary>
        /// Eq of player
        /// </summary>
        public cEquipment[] equipment = new cEquipment[5];
        public cConsumable[] consumables = new cConsumable[10];
        /// <summary>
        /// If the character is hardcore, true
        /// </summary>
        public bool ifHardcore;
        /// <summary>
        /// Amount of gold carried
        /// </summary>
        public int numOfGold;
        /// <summary>
        /// Abilities
        /// </summary>
        public cAbility[] abilities = new cAbility[5];
        /// <summary>
        /// If Crited
        /// </summary>
        public bool ifCrited = false;
        /// <summary>
        /// If triple crited
        /// </summary>
        public bool ifCritedT = false;
        /// <summary>
        /// If disintegrated
        /// </summary>
        public bool ifDis = false;
        public new void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", name);
            info.AddValue("Health", health);
            info.AddValue("Armor", armor);
            info.AddValue("Damage", maxDamage);
            info.AddValue("Luck", luck);
            info.AddValue("Class", playerClass);
            info.AddValue("Eq slots", ifEqSpotTaken);
            info.AddValue("Hardcore", ifHardcore);
            info.AddValue("Equipment", equipment);
            info.AddValue("Consumables", consumables);
            info.AddValue("Gold", numOfGold);
            info.AddValue("MaxHealth", maxHealth);
            info.AddValue("Abilities", abilities);
        }
        public cPlayer(SerializationInfo info, StreamingContext context)
        {
            name = (string)info.GetValue("Name", typeof(string));
            health = (int)info.GetValue("Health", typeof(int));
            armor = (int)info.GetValue("Armor", typeof(int));
            maxDamage = (int)info.GetValue("Damage", typeof(int));
            luck = (int)info.GetValue("Luck", typeof(int));
            playerClass = (int)info.GetValue("Class", typeof(int));
            numOfGold = (int)info.GetValue("Gold", typeof(int));
            ifEqSpotTaken = (bool[])info.GetValue("Eq slots", typeof(bool[]));
            equipment = (cEquipment[])info.GetValue("Equipment", typeof(cEquipment[]));
            consumables = (cConsumable[])info.GetValue("Consumables", typeof(cConsumable[]));
            ifHardcore = (bool)info.GetValue("Hardcore", typeof(bool));
            maxHealth = (int)info.GetValue("MaxHealth", typeof(int));
            abilities = (cAbility[])info.GetValue("Abilities", typeof(cAbility[]));
        }
        /// <summary>
        /// add to a free slot
        /// </summary>
        /// <param name="ability"></param>
        public void addAbility(cAbility ability)
        {
            for (int i = 0; i < 5; i++)
            {
                if (abilities[i] is null)
                {
                    abilities[i] = ability;
                    break;
                }
            }
        }
        /// <summary>
        /// add to a selected slot
        /// </summary>
        /// <param name="ability"></param>
        /// <param name="slot"></param>
        public void addAbility(cAbility ability, int slot)
        {
            
            abilities[slot] = ability;
        }
        public string createTip()
        {
            string tip;
            tip = $"Name: " + name + $"{Environment.NewLine}" + "Max health: " + maxHealth + $"{Environment.NewLine}" + "Average damage: " + (maxDamage + minDamage) / 2 + $"{Environment.NewLine}"+"Inteligence: "+inteligence+Environment.NewLine+ "Class: " +playerClass + $"{Environment.NewLine}" + "Luck: " + luck + $"{Environment.NewLine}" + "Armor: " + armor;
            return tip;
        }
        public int attack(cEnemy enemy)
        {
            ifDis = false;
            ifCritedT=false;
            ifCrited = false;
            int damage = 0;
            Random random = new Random();
            int critChance = random.Next(30 - luck);
            if (!(equipment[1] is null))
            {
                if(equipment[1].itemID==1006)
                {
                    health += (maxHealth - health) / 33;
                }
            }
            int disChance = random.Next(100);
            if (!(enemy.avoidAttack()))
            {
                damage = random.Next(maxDamage - minDamage) + minDamage;
                damage /= 4;
                if (!(equipment[4] is null))
                {
                    damage += equipment[4].damage;
                }
                double armory = 100;
                if (enemy.armor >= 0)
                {
                    armory = (100 + enemy.armor);
                }
                else
                {
                    armory = 100;
                }
                if (!(equipment[4] is null))
                {
                    if(equipment[4].itemID==1002)
                    {
                        armory *= 0.8;
                    }
                }
                double  mitigations= (100 / armory);
                double postArmor = (double)damage * mitigations;
                if (disChance<5&&playerClass==1)
                {
                    postArmor = -1;
                    ifDis = true;
                    return damage;
                }
                if(critChance<5&&playerClass==1)
                {
                    ifCritedT = true;
                    postArmor *= 3;
                }
                else if(critChance<10)
                {
                    postArmor *= 2;
                    ifCrited = true;
                }
                //if (damage - enemy.armor>0)
                //{
                //    enemy.health -= damage - enemy.armor;
                //}
                //else
                //{
                //    return 0;
                //}
                if(enemy.ID==1)
                {
                    postArmor -= postArmor / 10;
                }
                else if(enemy.ID==2)
                {
                    enemy.health += (int)postArmor / 10;
                }
                enemy.health-= (int)postArmor;
                if (!(equipment[4] is null))
                {
                    if (equipment[4].itemName=="Bloodforge")
                    {
                        health += (int)postArmor / 20;
                    }
                }
                return (int)postArmor;
            }
            else
            {
                return 0;
            }
        }
        public int attack(cPlayer enemy)
        {
            int damage = 0;
            Random random = new Random();
            int critChance=random.Next(5);
            if (!(enemy.avoidAttack()))
            {
                if (maxDamage - minDamage > 0)
                {

                    damage = random.Next(maxDamage - minDamage) + minDamage;
                    double armory = (100 + enemy.armor);
                    double mitigations = (100 / armory);
                    double postArmor = (double)damage * mitigations;
                    if(critChance<2&&enemy.playerClass==1)
                    {
                        postArmor *= 2;
                        enemy.ifCrited = true;
                    }
                    enemy.health -= (int)postArmor;
                    if (ID == 6)
                    {
                        health += (int)postArmor / 3;
                    }
                    if (!(enemy.equipment[4] is null))
                    {
                        if (enemy.equipment[4].itemID==1004)
                        {
                            health -= (int)postArmor / 10;
                        }
                    }
                    return (int)postArmor;
                }
                else if(minDamage>0)
                {
                    damage = minDamage;
                    double armory = (100 + enemy.armor);
                    double mitigations = (100 / armory);
                    double postArmor = (double)damage * mitigations;
                    if (critChance < 2 && enemy.playerClass == 1)
                    {
                        postArmor *= 2;
                        enemy.ifCrited = true;
                    }
                    enemy.health -= (int)postArmor;
                    if (ID == 6)
                    {
                        health += (int)postArmor / 3;
                    }
                    if (!(enemy.equipment[4] is null))
                    {
                        if (enemy.equipment[4].itemID == 1004)
                        {
                            health -= (int)postArmor / 10;
                        }
                    }
                    return (int)postArmor;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
        public void debuff(cEnemy player, ref cAbility ability)
        {
            if(ability.type==3&&ability.currentCooldown==0)
            {
                if(ability.whatBuff==2)
                {
                    player.maxDamage-=ability.healing;
                    player.minDamage-=ability.healing;
                    player.gotDebuffed(ability);
                }
                else if(ability.whatBuff==3)
                {
                    player.armor -= ability.healing;
                    player.gotDebuffed(ability);
                }
                ability.currentCooldown = ability.cooldown;
            }
        } //enemy debuff
        public void debuff(ref cEnemy player,cConsumable consumable)
        {
            if(consumable.effect==4)
            {
                if (consumable.WhichStat == 2)
                {
                    player.maxDamage -= consumable.strenght;
                    player.minDamage -= consumable.strenght;
                    player.gotDebuffed(consumable);
                }
                else if (consumable.WhichStat == 3)
                {
                    player.armor -= consumable.strenght;
                    player.gotDebuffed(consumable);
                }
                else if(consumable.WhichStat == 1)
                {
                    if(player.health-consumable.strenght<0)
                    {
                        player.health = 0;
                    }
                    else
                    {
                        player.health -= consumable.strenght;
                    }
                }
            }
        } //debuff
        public void debuff(ref cPlayer player, cAbility ability)
        {
            if (ability.type == 3 && ability.currentCooldown == 0)
            {
                if (ability.whatBuff == 2)
                {
                    player.maxDamage -= ability.healing;
                    player.minDamage -= ability.healing;
                    player.gotDebuffed(ability);
                    ability.currentCooldown = ability.cooldown;
                }
                else if (ability.whatBuff == 3)
                {
                    player.armor -= ability.healing;
                    player.gotDebuffed(ability);
                    ability.currentCooldown = ability.cooldown;
                }
            }
        }  // player debuff
        public bool avoidAttack()
        {
            bool ifAvoided = false;
            Random rand = new Random();
            if (playerClass == 2 || playerClass == 4)
            {
                if (rand.Next(30) < luck)
                {
                    ifAvoided = true;
                }
            }
            else if (playerClass == 3)
            {
                if (armor < 100)
                {
                    if (rand.Next(300) < armor + luck)
                    {
                        ifAvoided = true;
                    }
                }
                else
                {
                    if(rand.Next(3)==1)
                    {
                        ifAvoided = true;
                    }
                }
            }
            return ifAvoided;
        }
        public void addConsumable(cConsumable consumable)
        {
            for(int i=0;i<10;i++)
            {
                if (consumables[i] is null)
                {
                    consumables[i] = consumable;
                    break;
                }
            }
        }
        public void equip(cEquipment equipment1)
        {
            if (equipment1.whichPiece < 5)
            {
                if(equipment1.whichPiece==4)
                {
                    if (equipment[4] is null)
                    {

                    }
                    else
                    {
                        maxDamage -= equipment[4].damage;
                        minDamage -= equipment[4].damage;
                    }
                    maxDamage += equipment1.damage;
                    minDamage += equipment1.damage;
                }
                else
                {
                    if (equipment[equipment1.whichPiece] is null)
                    {

                    }
                    else
                    {
                        armor -= equipment[equipment1.whichPiece].armor;
                    }
                    armor+=equipment1.armor;
                }
                    equipment[equipment1.whichPiece] = equipment1;
            }
        }
        public bool areSlotsTaken()
        {
            bool ifAre = false;
            int counter = 0;
            for(int i=0;i<5;i++)
            {
                if(!(abilities[i] is null))
                {
                    counter++;
                }
            }
            if(counter==5)
            {
                ifAre=true;
            }
            return ifAre;

        }
        public void gotBuffed(cAbility ability)
        {
            for(int i=0;i<5;i++)
            {
                    if (abilityCD2[i,ability.whatBuff-1] == 0)
                    {
                        abilityCD2[i,ability.whatBuff-1] = ability.healing;
                        break;
                    }
               
            }
        } //buff ab
        public void buffOver()
        {
            for(int i=0;i<5;i++)
            {
                for(int k=1;k<4;k++)
                {
                    try
                    {
                        if (k == 1 && abilityCD2[i, k] != 0)
                        {
                            maxDamage -= abilityCD2[i, k];
                            minDamage -= abilityCD2[i, k];
                            abilityCD2[i, k] = 0;
                        }
                        else if (k == 2 && abilityCD2[i, k] != 0)
                        {
                            armor -= abilityCD2[i, k];
                            abilityCD2[i, k] = 0;
                        }
                        else if (k == 3 && abilityCD2[i, k] != 0)
                        {
                            inteligence -= abilityCD2[i, k];
                            abilityCD2[i, k] = 0;
                        }
                    }
                    catch
                    {

                    }
                }
            }
        } // tylko z tego
        public void gotBuffed(cConsumable consumable)
        {
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    if (abilityCD2[i, consumable.WhichStat - 1] == 0)
                    {
                        abilityCD2[i, consumable.WhichStat - 1] = consumable.strenght;
                        break;
                    }
                }
                catch(IndexOutOfRangeException)
                {
                    break;
                }

            }
        } //buff con
        public void gotDebuffed(cAbility ability)
        {
            for (int i = 0; i < 5; i++)
            {
                if (abilityCD2[i, ability.whatBuff - 1] == 0)
                {
                    abilityCD2[i, ability.whatBuff - 1] = -(ability.healing);
                    break;
                }

            }
        }
        public void gotDebuffed(cConsumable ability)
        {
            for (int i = 0; i < 5; i++)
            {
                if (abilityCD2[i, ability.WhichStat - 1] == 0)
                {
                    abilityCD2[i, ability.WhichStat - 1] = -(ability.strenght);
                    break;
                }

            }
        }
        public int equipmentWeight()
        {
            int weight = 0;
            for(int i=0;i<5;i++)
            {
                if (!(equipment[i] is null))
                {
                    weight += equipment[i].weight;
                }
            }
            return weight;
        }
        public void reduceMaxHP(int num)
        {
            if(maxHealth-num<=0)
            {
                maxHealth = 1;
            }
            else
            {
                maxHealth -= num;
            }
            if(health>maxHealth)
            {
                health = maxHealth;
            }
        }
        public void increaseDMG(int num)
        {
            if (sPlayer.player.maxDamage + num > 0)
            {
                sPlayer.player.maxDamage += num;
            }
            if (sPlayer.player.minDamage + num > 0)
            {
                sPlayer.player.minDamage += num;
            }
        }
        public void levelUp()
        {
            if(playerClass==1)
            {
                health += 100;
                increaseDMG(40);
                inteligence += 30;
            }
            else if(playerClass==2)
            {
                health += 200;
                increaseDMG(30);
                inteligence += 15;
            }
            else if(playerClass==3)
            {
                health += 350;
                increaseDMG(15);
                inteligence += 15;
            }
            else if(playerClass==4)
            {
                health += 120;
                increaseDMG(50);
                inteligence += 20;
            }
            level++;
        }
        public cEquipment loseRandomEquipment()
        {
            cEquipment equipment1 = new cEquipment();
            Random random = new Random();
            while(true)
            {
                int chance = random.Next(5);
                if (!(equipment[chance] is null))
                {
                    equipment1 = equipment[chance];
                    equipment[chance] = null;
                    break;
                }
            }
            return equipment1;
        }
    }
}
