using System;

namespace GraRPG
{
    [Serializable]
    public class cArena
    {
        /// <summary>
        /// Num of image to generate as background
        /// </summary>
        public int arenaImg;
        /// <summary>
        /// Which turn is the fight in
        /// </summary>
        public int turnCounter;
        /// <summary>
        /// 1-Fight 2-Boss fight 3-Treasure 4-Event 5-Shop 6-Rest
        /// </summary>
        public int arenaKind;
        //cItem dropItem;
    }
}
