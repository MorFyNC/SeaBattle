using System.ComponentModel.Design;

namespace SeaBattle.Classes
{
    public static class ShipFabric
    {

        public static Ship Create(int len, int x, int y, int xEnd, int yEnd)
        {
            switch (len)
            {
                case 1:
                    Ship ship = new ShipOne();
                    ship.ManageCoordinates(x, y, xEnd, yEnd);
                    return ship;
                case 2:
                    ship = new ShipTwo();
                    ship.ManageCoordinates(x, y, xEnd, yEnd);
                    return ship;
                case 3:
                    ship = new ShipThree();
                    ship.ManageCoordinates(x, y, xEnd, yEnd);
                    return ship;
                case 4:
                    ship = new ShipFour();
                    ship.ManageCoordinates(x, y, xEnd, yEnd);
                    return ship;
            }
            return null;
        }
        public static Ship Empty(int len)
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

    public static class Special
    {
        public static int ConvertCoordinate(char coordinate)
        {
            switch (coordinate)
            {
                case 'а': return 1;
                case 'б': return 2;
                case 'в': return 3;
                case 'г': return 4;
                case 'д': return 5;
                case 'е': return 6;
                case 'ё': return 7;
                case 'ж': return 8;
                case 'з': return 9;
                case 'и': return 10;
            }
            return 0;
        }

        public static string[,] EmptyField()
        {
            return new string[,]
            {
                { "   ", " а ", " б ", " в ", " г ", " д ", " е ", " ё ", " ж ", " з ", " и ", ""},
                { " 1 ", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", ""},
                { " 2 ", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", ""},
                { " 3 ", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", ""},
                { " 4 ", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", ""},
                { " 5 ", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", ""},
                { " 6 ", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", ""},
                { " 7 ", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", ""},
                { " 8 ", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", ""},
                { " 9 ", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", ""},
                { "10 ", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", ""},
                { "","","","","","","","","","", "", ""}
            };
        }

        public static List<int> DefaultCoordinateList()
        {
            return new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        }

        public static List<Ship> DefaultShipsLeft()
        {
            return new List<Ship>
                {
                    ShipFabric.Empty(1),
                    ShipFabric.Empty(1),
                    ShipFabric.Empty(1),
                    ShipFabric.Empty(1),
                    ShipFabric.Empty(2),
                    ShipFabric.Empty(2),
                    ShipFabric.Empty(2),
                    ShipFabric.Empty(3),
                    ShipFabric.Empty(3),
                    ShipFabric.Empty(4),
                };
        }

        public static int[] CoordinateRequest()
        {
            string input = Console.ReadLine();

            return new int[]
            {
                Convert.ToInt32(input.Substring(1)),
                Special.ConvertCoordinate(input[0])
            };
        }

        public static List<int[]> CoordinateList() 
        {
            List<int[]> list = new List<int[]>();

            for(int i = 1; i < 11; i++)
            {
                for(int j = 1; j < 11; j++)
                {
                    list.Add(new int[] { i, j });
                }
            }
            return list;
        }

        public static List<int[]> NewCoordinateList(int[] coordinates)
        {
            if (coordinates.Length != 0)
            {
                int x = coordinates[0];
                int y = coordinates[1];


                List<int[]> NewList = new List<int[]>
            {
                x < 10 ? new int[] { x + 1, y } : null,
                x > 1 ? new int[] { x - 1, y } : null,
                y < 10 ? new int[] { x, y + 1 } : null,
                y > 1 ? new int[] { x, y - 1 } : null,
            };

                NewList.Remove(null);
                return NewList;
            }
            return new List<int[]>();
        }

        public static int FindIndex(List<int[]> list, int[] arr)
        {
            for(int i = 0; i < list.Count; i++)
            {
                if (list[i][0] == arr[0] && list[i][1] == arr[1])
                {
                    return i;
                }
            }
            return -1;
        }

        public static List<int[]> NewCoordinateListWithFirstShoot(List<int[]> list, int[] firstShoot, int[] newShoot)
        {
            int[] constant = new int[] { firstShoot[0] - newShoot[0], firstShoot[1] - newShoot[1] };
            bool check1 = firstShoot[0] + 1 < 12 && firstShoot[1] - 1 > 0 ;
            bool check2 = firstShoot[0] - 1 > 0 && firstShoot[1] + 1 < 11;
            List<int[]> newList = new List<int[]>
            {
                check2 ? new int[] {firstShoot[0] - constant[0], firstShoot[1] - constant[1] } : null,
                check1 ? new int[] {firstShoot[0] + constant[0], firstShoot[1] + constant[1] } : null
            };

            newList.Remove(null);
            return newList;
        }
    }
}
