using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GraRPG
{
    [Serializable]
    public class cItem
    {
        /// <summary>
        /// Unique ID of an Item
        /// </summary>
        public int itemID;
        /// <summary>
        /// Name of an item
        /// </summary>
        public string itemName;
        /// <summary>
        /// Cos of an item
        /// </summary>
        public int cost;
        public Bitmap bitmap = new Bitmap(Properties.Resources.Image1);

    }
}
