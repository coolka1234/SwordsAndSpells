using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraRPG
{
    public static class sEnemy
    {
        public static cEnemy enemy= new cEnemy();
        public static object DropItem()
        {
            Random random = new Random();
            int whatItem = random.Next(3);
            int whatExactly=0;
            if(whatItem == 0)
            {
                cConsumable cConsumable=new cConsumable();
                whatExactly=random.Next(3)+1;
                cConsumable=cConsumable.load(cConsumable, whatExactly);
                return cConsumable;
            }
            else if(whatItem == 1)
            {
                cEquipment cEquipment = new cEquipment();
                whatExactly = random.Next(2)+1;
                cEquipment=cEquipment.load(cEquipment, whatExactly);
                return cEquipment;
            }
            else
            {
                cAbility cAbility= new cAbility();
                whatExactly = random.Next(6)+1;
                cAbility=cAbility.load(cAbility, whatExactly);
                return cAbility;
            }
        }
    }
}
