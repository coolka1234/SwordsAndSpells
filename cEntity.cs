using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace GraRPG
{
    [Serializable]
    public class cEntity
    {
        /// <summary>
        /// Level of entity. Higher the level better the stats
        /// </summary>
        public int level;
        /// <summary>
        /// Total health of entity. If 0, entity dies.
        /// </summary>
        public int health;
        /// <summary>
        /// Maximum health
        /// </summary>
        public int maxHealth;
        /// <summary>
        /// Name of the entity.
        /// </summary>
        public string name;
        /// <summary>
        /// Armor of the entity. Decides about the damage mitigated.
        /// </summary>
        public int armor;
        /// <summary>
        ///  Minimal damage dealt by the entity
        /// </summary>
        public int minDamage;
        /// <summary>
        /// Maximum damage dealt by the entity
        /// </summary>
        public int maxDamage;
        /// <summary>
        /// Unique Id of the entity
        /// </summary>
        public int ID;
        /// <summary>
        /// luck tbh, critc chance gold found etc
        /// </summary>
        public int luck;
        public cEntity() { }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {

        }
        public cEntity(SerializationInfo info, StreamingContext context)
        {

        }

    }
}
