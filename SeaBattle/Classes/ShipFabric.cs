using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Classes
{
    public static class ShipFabric
    {

        public static Ship Create(int len)
        {
            switch (len)
            {
                case 1: return new ShipOne();
                case 2: return new ShipTwo();
                case 3: return new ShipThree();
                case 4: return new ShipFour();
            }
            return null;
        }
    }
}
