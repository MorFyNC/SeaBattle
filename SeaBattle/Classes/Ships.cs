using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Classes
{
    public class Ship
    {
        public int len;
        public Ship() { }
    }
    
    public class ShipOne : Ship
    {
        public int len = 1;


        public ShipOne() 
        { 
            
        }
    }

    public class ShipTwo : Ship
    {
        public int len = 2;

        public ShipTwo() { }
    }

    public class ShipThree : Ship
    {
        public int len = 3;

        public ShipThree() { }
    }

    public class ShipFour : Ship
    {
        public int len = 4;

        public ShipFour() { }
    }
}
