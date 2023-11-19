using Newtonsoft.Json;

namespace SeaBattle.Classes
{
    public class Field
    {
        private Random random = new Random();
        public string owner;
        public List<Ship> ShipsLeft = Special.DefaultShipsLeft();
        public List<Ship> ShipsLeftDefault = Special.DefaultShipsLeft();
        public List<Ship> ships = new List<Ship>();
        private List<int> xcoords = Special.DefaultCoordinateList();
        private List<int> ycoords = Special.DefaultCoordinateList();
        private List<int> defaults = Special.DefaultCoordinateList();

        public string[,] field = Special.EmptyField();

        public string[,] emptyField = Special.EmptyField();

        public Field(string owner)
        {
            this.owner = owner;
        }

        public Field() { }
        public override string ToString()
        {
            return $"{field}";
        }

        public void PlaceShip(Ship ship)
        {
            if (CheckCells(ship))
            {
                int temp1 = ship.x;
                int temp2 = ship.y;
                if (ship.x == ship.xEnd && ship.y < ship.yEnd)
                {
                    for (int i = 0; i < ship.coordinates.Count; i++)
                    {
                        field[temp1, temp2] = "[■]";
                        temp2++;
                    }
                }
                else if (ship.y == ship.yEnd && ship.x < ship.xEnd)
                {
                    for (int i = 0; i < ship.coordinates.Count; i++)
                    {
                        field[temp1, temp2] = "[■]";
                        
                        temp1++;
                    }
                }
                else if (ship.y == ship.yEnd && ship.x > ship.xEnd)
                {
                    for (int i = 0; i < ship.coordinates.Count; i++)
                    {
                        field[temp1, temp2] = "[■]";
                        temp1--;
                    }
                }
                else if (ship.x == ship.xEnd && ship.y > ship.yEnd)
                {
                    for (int i = 0; i < ship.coordinates.Count; i++)
                    {
                        field[temp1, temp2] = "[■]";
                        temp2--;
                    }
                }
                else if (ship.x == ship.xEnd && ship.y == ship.yEnd)
                {
                    field[temp1, temp2] = "[■]";
                }
            }
        }

        public void DeleteSharps()
        {
            int rows = field.GetUpperBound(0) + 1;
            int columns = field.Length / rows;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (field[i, j] == "[#]")
                    {
                        field[i, j] = "[ ]";
                    }
                }
            }
        }

        public bool CheckCells(Ship ship)
        {
            switch (ship.coordinates.Count)
            {
                case 1: return (field[ship.x, ship.y] == "[ ]" && ShipsLeft.Any(s => s is ShipOne));
                case 2: return (field[ship.x, ship.y] == "[ ]" && field[ship.xEnd, ship.yEnd] == "[ ]") && ShipsLeft.Any(s => s is ShipTwo) && (Math.Abs(ship.xEnd - ship.x) == 1 && ship.y == ship.yEnd || Math.Abs(ship.y - ship.yEnd) == 1 && ship.x == ship.xEnd);
                case 3: return (field[ship.x, ship.y] == "[ ]" && field[ship.xEnd, ship.yEnd] == "[ ]") && ShipsLeft.Any(s => s is ShipThree) && (Math.Abs(ship.xEnd - ship.x) == 2 && ship.y == ship.yEnd || Math.Abs(ship.y - ship.yEnd) == 2 && ship.x == ship.xEnd);
                case 4: return (field[ship.x, ship.y] == "[ ]" && field[ship.xEnd, ship.yEnd] == "[ ]") && ShipsLeft.Any(s => s is ShipFour) && (Math.Abs(ship.xEnd - ship.x) == 3 && ship.y == ship.yEnd || Math.Abs(ship.y - ship.yEnd) == 3 && ship.x == ship.xEnd);
            }

            return false;
        }

        public void FillSharps(Ship ship)
        {
            if (field[ship.x + 1, ship.y] == "[ ]")
                field[ship.x + 1, ship.y] = "[#]";
            if (field[ship.x + 1, ship.y - 1] == "[ ]")
                field[ship.x + 1, ship.y - 1] = "[#]";
            if (field[ship.x + 1, ship.y + 1] == "[ ]")
                field[ship.x + 1, ship.y + 1] = "[#]";
            if (field[ship.x - 1, ship.y] == "[ ]")
                field[ship.x - 1, ship.y] = "[#]";
            if (field[ship.x - 1, ship.y - 1] == "[ ]")
                field[ship.x - 1, ship.y - 1] = "[#]";
            if (field[ship.x - 1, ship.y + 1] == "[ ]")
                field[ship.x - 1, ship.y + 1] = "[#]";
            if (field[ship.x, ship.y - 1] == "[ ]")
                field[ship.x, ship.y - 1] = "[#]";
            if (field[ship.x, ship.y + 1] == "[ ]")
                field[ship.x, ship.y + 1] = "[#]";

            if (field[ship.xEnd + 1, ship.yEnd] == "[ ]")
                field[ship.xEnd + 1, ship.yEnd] = "[#]";
            if (field[ship.xEnd + 1, ship.yEnd - 1] == "[ ]")
                field[ship.xEnd + 1, ship.yEnd - 1] = "[#]";
            if (field[ship.xEnd + 1, ship.yEnd + 1] == "[ ]")
                field[ship.xEnd + 1, ship.yEnd + 1] = "[#]";
            if (field[ship.xEnd - 1, ship.yEnd] == "[ ]")
                field[ship.xEnd - 1, ship.yEnd] = "[#]";
            if (field[ship.xEnd - 1, ship.yEnd - 1] == "[ ]")
                field[ship.xEnd - 1, ship.yEnd - 1] = "[#]";
            if (field[ship.xEnd - 1, ship.yEnd + 1] == "[ ]")
                field[ship.xEnd - 1, ship.yEnd + 1] = "[#]";
            if (field[ship.xEnd, ship.yEnd - 1] == "[ ]")
                field[ship.xEnd, ship.yEnd - 1] = "[#]";
            if (field[ship.xEnd, ship.yEnd + 1] == "[ ]")
                field[ship.xEnd, ship.yEnd + 1] = "[#]";
        }

        public void PrintField(bool FieldType)
        {
            int rows = field.GetUpperBound(0) + 1;
            int columns = field.Length / rows;


            Console.Write("\n\t\t\t\t");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (FieldType)
                        Console.Write($"{field[i, j]}");
                    else
                        Console.Write($"{emptyField[i, j]}");
                }
                Console.Write("\n\t\t\t\t");
            }
        }

        public bool Shoot(int x, int y)
        {
            if (field[x, y] == "[ ]")
            {
                emptyField[x, y] = "[○]";
                field[x, y] = "[○]";
                return false;
            }
            else
            {
                emptyField[x, y] = "[X]";
                field[x, y] = "[X]";
                foreach (Ship ship in ships)
                {
                    if (CheckShipIsDead(ship)) FillSharps(ship);
                }
                return true;
            }
        }

        public bool Shoot(int[] shootCoords)
        {
            int x = shootCoords[0];
            int y = shootCoords[1];

            if (field[x, y] == "[ ]")
            {
                emptyField[x, y] = "[○]";
                field[x, y] = "[○]";
                return false;
            }
            else
            {
                emptyField[x, y] = "[X]";
                field[x, y] = "[X]";
                foreach (Ship ship in ships)
                {
                    if (CheckShipIsDead(ship)) FillSharps(ship);
                }
                return true;
            }
            
        }

        public bool checkShips()
        {
            bool flag = false;
            for (int r = 0; r < 12; r++)
            {
                int c = 0;
                while (flag == false && c < 12)
                {
                    if (field[r, c] == "[■]")
                    {
                        flag = true;
                    }
                    c++;
                }
            }
            return flag;
        }

        public void RandomFill()
        {
            foreach (Ship ship in ShipsLeft)
            {
            start1:
                int rotateChoice = random.Next(2);
                int x = random.Next(1, 11);
                int y = random.Next(1, 11);
                int xEnd = x;
                int yEnd = y;

                if (rotateChoice == 0 && ship is ShipTwo)
                {
                    x = random.Next(1, 11);
                    y = random.Next(1, 10);
                    xEnd = x;
                    yEnd = y + 1;
                }
                else if (rotateChoice == 1 && ship is ShipTwo)
                {
                    x = random.Next(1, 10);
                    y = random.Next(1, 11);
                    xEnd = x + 1;
                    yEnd = y;
                }

                if (rotateChoice == 0 && ship is ShipThree)
                {
                    x = random.Next(1, 11);
                    y = random.Next(1, 9);
                    xEnd = x;
                    yEnd = y + 2;
                }
                else if (rotateChoice == 1 && ship is ShipThree)
                {
                    x = random.Next(1, 9);
                    y = random.Next(1, 11);
                    xEnd = x + 2;
                    yEnd = y;
                }

                if (rotateChoice == 0 && ship is ShipFour)
                {
                    x = random.Next(1, 11);
                    y = random.Next(1, 8);
                    xEnd = x;
                    yEnd = y + 3;
                }
                else if (rotateChoice == 1 && ship is ShipFour)
                {
                    x = random.Next(1, 8);
                    y = random.Next(1, 11);
                    xEnd = x + 3;
                    yEnd = y;
                }
                ship.ManageCoordinates(x, y, xEnd, yEnd);
                if (checkFillPossibility(ship))
                {
                    ships.Add(ship);
                    PlaceShip(ship);
                }
                else { goto start1; }
                FillSharps(ship);
            }
            DeleteSharps();
        }

        public bool checkFillPossibility(Ship ship)
        {
            foreach (int[] i in ship.coordinates)
            {
                if (field[i[0], i[1]] == "[ ]")
                {
                    continue;
                }
                else return false;
            }
            return true;
        }

        public bool checkShootPossibility(List<int> xcoords, List<int> ycoords)
        {
            bool pos = false;
            foreach (int x in xcoords)
            {
                foreach (int y in ycoords)
                {
                    if (field[x, y] == "[ ]" || field[x, y] == "[■]") return pos = true;
                }
            }
            return pos;
        }
        public bool checkShootPossibility(int x, int y)
        {
            return field[x, y] == "[ ]" || field[x, y] == "[■]";
        }

        public void FieldFill()
        {
        again:
            field = Special.EmptyField();
            ShipsLeft = Special.DefaultShipsLeft();
            ships = new List<Ship>();
            while (ShipsLeft.Count > 0)
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\t\tЗаполнение поля");
                PrintField(true);
                Console.Write("\tВведите длину корабля \n\n>>");
                int len = Convert.ToInt32(Console.ReadLine());
                Console.Write("\t\t\t\tВведите начальную координату корабля \n\n>>");
                string input = Console.ReadLine();
                int x = Convert.ToInt32(input.Substring(1));
                int y = Special.ConvertCoordinate(input[0]);
                int xEnd = x;
                int yEnd = y;
                if (len != 1)
                {
                    Console.Write("\t\t\t\tВведите конечную координату корабя \n\n>>");
                    string end = Console.ReadLine();
                    xEnd = Convert.ToInt32(end.Substring(1));
                    yEnd = Special.ConvertCoordinate(end[0]);
                }

                if (shipCount(len) > 0 && checkFillPossibility(ShipFabric.Create(len, x, y, xEnd, yEnd)))
                {
                    ships.Add(ShipFabric.Create(len, x, y, xEnd, yEnd));
                    PlaceShip(ShipFabric.Create(len, x, y, xEnd, yEnd));
                    FillSharps(ShipFabric.Create(len, x, y, xEnd, yEnd));
                    switch (len)
                    {
                        case 1: ShipsLeft.RemoveAt(0); break;
                        case 2: ShipsLeft.RemoveAt(shipCount(1)); break;
                        case 3: ShipsLeft.RemoveAt(shipCount(1) + shipCount(2)); break;
                        case 4: ShipsLeft.RemoveAt(shipCount(1) + shipCount(2) + shipCount(3)); break;
                    }
                }
            }
            Console.Clear();
            DeleteSharps();
            PrintField(true);
            Console.Write("Вы хотите оставить такую раскладку? \n\t\t\t\t1.Да \t\t\t\t2.Нет \n\n>>");
            if (Convert.ToInt32(Console.ReadLine()) != 1)
                goto again;
        }

        public int shipCount(int len)
        {
            int Ship1Count = 0;
            int Ship2Count = 0;
            int Ship3Count = 0;
            int Ship4Count = 0;

            foreach (Ship ship in ShipsLeft)
            {
                if (ship is ShipOne)
                {
                    Ship1Count++;
                }
                else if (ship is ShipTwo)
                {
                    Ship2Count++;
                }
                else if (ship is ShipThree)
                {
                    Ship3Count++;
                }
                else if (ship is ShipFour)
                {
                    Ship4Count++;
                }
            }

            switch (len)
            {
                case 1: return Ship1Count;
                case 2: return Ship2Count;
                case 3: return Ship3Count;
                case 4: return Ship4Count;
            }

            return 0;
        }

        public void WriteFieldToFile()
        {
            Console.WriteLine("\t\t\t\tВведите путь к файлу \n>>");
            string path = Console.ReadLine();
            StreamWriter sw = new StreamWriter($"{path}");
            sw.WriteLine(JsonConvert.SerializeObject(this));
            sw.Close();
        }

        public Field ReadFieldFromFile()
        {
            Console.Clear();
            Console.Write("\t\t\t\tВведите путь к файлу \n\n>>");
            string path = Console.ReadLine();
            StreamReader sr = new StreamReader($"{path}");
            return JsonConvert.DeserializeObject<Field>(sr.ReadLine());
        }

        public bool WannaRead()
        {
            Console.Write("\t\t\t\tХотите ли вы загрузить раскладку из файла? \n\n \t\t\t\t1.Да \t\t\t\t2.Нет \n\n>>");

            return Convert.ToInt32(Console.ReadLine()) == 1;
        }

        public bool WannaSave()
        {
            Console.Write("\t\t\t\tХотите ли вы сохранить данную раскладку? \n\n \t\t\t\t1.Да \t\t\t\t2.Нет \n\n>>");

            return Convert.ToInt32(Console.ReadLine()) == 1;
        }

        public bool CheckShipIsDead(Ship ship)
        {
            bool flag = true;
            foreach (int[] j in ship.coordinates)
            {
                if (field[j[0], j[1]] == "[■]")
                {
                    flag = false; break;
                }
            }
            ship.isDead = true;
            return flag;
        }
    }
}

