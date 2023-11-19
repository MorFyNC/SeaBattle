using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SeaBattle.Classes
{
    public class Ship
    {
        public int len;
        public bool isDead = false;
        public int x;
        public int y;
        public int xEnd;
        public int yEnd;
        public List<int[]> coordinates = new List<int[]>();
        public Ship() { }
         
        public virtual void ManageCoordinates(int x, int y, int xEnd, int yEnd)
        {
            this.x = x;
            this.y = y;
            this.xEnd = xEnd;
            this.yEnd = yEnd;
        }
    }
    
    public class ShipOne : Ship
    {
        public ShipOne() { }
        public int len = 1;
        public override void ManageCoordinates(int x, int y, int xEnd, int yEnd)
        {
            base.ManageCoordinates(x, y, xEnd, yEnd);
            coordinates = new List<int[]>
            {
                new int[] { x, y },
            };
        }
    }

    public class ShipTwo : Ship
    {
        public int len = 2;
        public ShipTwo() { }

        public override void ManageCoordinates(int x, int y, int xEnd, int yEnd)
        {
            base.ManageCoordinates(x, y, xEnd, yEnd);
            coordinates = new List<int[]> 
            { 
                new int[]{x, y}, 
                new int[]{xEnd, yEnd} 
            };
        }
    }


    public class ShipThree : Ship
    {
        public int len = 3;
        public ShipThree() { }

        public override void ManageCoordinates(int x, int y, int xEnd, int yEnd)
        {
            base.ManageCoordinates(x, y, xEnd, yEnd);
            coordinates = new List<int[]> 
            { 
                new int[] { x, y },
                new int[] {x == xEnd ? x : x + 1, y == yEnd ? y : y + 1},
                new int[] { xEnd, yEnd } 
            };
        }
    }

    public class ShipFour : Ship
    {
        public int len = 4;
        public ShipFour() { }

        public override void ManageCoordinates(int x, int y, int xEnd, int yEnd)
        {
            base.ManageCoordinates(x, y, xEnd, yEnd);
            coordinates = new List<int[]>
            {
                new int[] { x, y },
                new int[] {x == xEnd ? x : x + 1, y == yEnd ? y : y + 1},
                new int[] {x == xEnd ? x : x + 2, y == yEnd ? y : y + 2},
                new int[] { xEnd, yEnd }
            };
        }
    }
}
