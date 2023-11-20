using System.Runtime.InteropServices.JavaScript;

namespace SeaBattle.Classes
{
    public class Game
    {
        private string winner = "";
        Random random = new Random();
        Field field1 = new Field("pc");
        Field field2 = new Field("pc");
        bool prevHit = true;
        int turn = 1;
        public void FieldFillStage()
        {
            if (field1.WannaRead())
            {
                field1 = field1.ReadFieldFromFile();
            }
            else
            {
                field1.FieldFill();
                if (field1.WannaSave())
                    field1.WriteFieldToFile();
            }
            field1.PrintField(true);
        }

        public bool winCheck()
        {
            if (field1.checkShips() && field2.checkShips()) { return false; }
            if (!field1.checkShips())
                winner = field2.owner;
            else if (!field2.checkShips())
                winner = field1.owner;

            return true;
        }

        public int[] Turn(Field field, int x, int y)
        {
            if (field.checkShootPossibility(x, y))
            {
                prevHit = field.Shoot(x, y);
                if (!prevHit) turn++;
            }

            if (field.checkShootPossibility(x, y) && field.Shoot(x, y)) prevHit = true;
            return new int[] { x, y };
        }
        public int[] Turn(Field field, int[] coords)
        {
            if (coords != null)
            {
                int x = coords[0];
                int y = coords[1];
                if (field.checkShootPossibility(x, y))
                {
                    prevHit = field.Shoot(x, y);
                    if (!prevHit) turn++;
                }

                if (field.checkShootPossibility(x, y) && field.Shoot(x, y)) prevHit = true;
                return new int[] { x, y };
            }
            return new int[] { 0 };
        }

        public void PvE()
        {
            field1 = new Field("player");
            List<int[]> ShootCoordinates = Special.CoordinateList();
            List<int[]> defaults = Special.CoordinateList();
            field2.RandomFill();
            FieldFillStage();
            Console.Clear();
            int[] firstShoot = { 0, 0 };
            while (!winCheck())
            {
                int[] temp = { };
                int[] shoot = { };
                Console.Clear();
                Console.WriteLine("\t\t\t\t\tВаше поле");
                field1.PrintField(true);
                Console.WriteLine("\tПоле Врага");
                field2.PrintField(false);
                if (turn % 2 == 1)
                {
                    Console.Write("Введите координату клетки, в которую хотите выстрелить\n\n>>");
                    int[] coordinates = Special.CoordinateRequest();
                    Turn(field2, coordinates[0], coordinates[1]);
                }
                else
                {
                    if (ShootCoordinates.Count == 0)
                        ShootCoordinates = Special.CoordinateList();
                    if (prevHit) temp = shoot;
                    shoot = Turn(field1, ShootCoordinates[random.Next(ShootCoordinates.Count)]);
                    if (field1.field[shoot[0], shoot[1]] == "[#]")
                        ShootCoordinates = Special.CoordinateList();


                    if (prevHit)
                    {
                        if (ShootCoordinates.Count == 100)
                        {
                            firstShoot = shoot;
                            ShootCoordinates = Special.NewCoordinateList(shoot);
                        }
                        else
                        {
                            if (firstShoot.Length != 0)
                                ShootCoordinates = Special.NewCoordinateListWithFirstShoot(ShootCoordinates, firstShoot, shoot);
                            else
                                ShootCoordinates = Special.CoordinateList();
                            if (field1.field[shoot[0], shoot[1]] == "[X]" && !field1.checkShootPossibility(ShootCoordinates[0][0], ShootCoordinates[0][1]))
                            {
                                firstShoot = shoot;
                                ShootCoordinates = Special.NewCoordinateList(shoot);
                            }
                            else if (!field1.checkShootPossibility(ShootCoordinates[0][0], ShootCoordinates[0][1]) && ShootCoordinates.Count < 4)
                            {
                                firstShoot = temp;
                            }
                        }
                    }

                    else if (!prevHit && ShootCoordinates.Count == 0 || ShootCoordinates.Count == 0)
                        ShootCoordinates = Special.CoordinateList();
                    else if (!prevHit && ShootCoordinates.Count != 100)
                        ShootCoordinates.RemoveAt(Special.FindIndex(ShootCoordinates, shoot));
                }


            }
            winGame();
        }

        public void PCvsPC()
        {
            field1.RandomFill();
            field2.RandomFill();
            field1.owner = "PC1";
            field2.owner = "PC2";
            Console.WriteLine($"\t\t\t\tПоле {field1.owner}");
            field1.PrintField(true);
            Console.WriteLine($"\t\t\t\tПоле {field2.owner}");
            field2.PrintField(true);
            List<int[]> ShootCoordinates = Special.CoordinateList();
            List<int[]> defaults = Special.CoordinateList();
            int[] firstShoot = { 0, 0 };

            while (!winCheck())
            {
                System.Threading.Thread.Sleep(10);
                Console.Clear();
                Console.WriteLine($"\t\t\t\tПоле {field1.owner}");
                field1.PrintField(true);
                Console.WriteLine($"Поле {field2.owner}");
                field2.PrintField(true);

                int[] temp = { };
                int[] shoot = { };

                if (turn % 2 == 1)
                {
                    turn++;
                }
                else
                {
                tryagain:
                    
                    
                    if (ShootCoordinates.Count == 0)
                        ShootCoordinates = Special.CoordinateList();
                    int[] rngCoords = ShootCoordinates[random.Next(ShootCoordinates.Count)];
                    if (prevHit) temp = shoot;

                    shoot = Turn(field2, rngCoords);
                    if (Turn(field2, rngCoords)[0] == 0) { goto tryagain; }
                    if (field2.field[shoot[0], shoot[1]] == "[#]")
                        ShootCoordinates = Special.CoordinateList();

                    if (prevHit)
                    {
                        if (ShootCoordinates.Count == 100)
                        {
                            firstShoot = shoot;
                            ShootCoordinates = Special.NewCoordinateList(shoot);
                        }
                        else
                        {
                            if (firstShoot.Length != 0)
                                ShootCoordinates = Special.NewCoordinateListWithFirstShoot(ShootCoordinates, firstShoot, shoot);
                            else
                                ShootCoordinates = Special.CoordinateList();
                            if (field2.field[shoot[0], shoot[1]] == "[X]" && !field2.checkShootPossibility(ShootCoordinates[0][0], ShootCoordinates[0][1]))
                            {
                                firstShoot = shoot;
                                ShootCoordinates = Special.NewCoordinateList(shoot);
                            }
                            else if (!field2.checkShootPossibility(ShootCoordinates[0][0], ShootCoordinates[0][1]) && ShootCoordinates.Count < 4)
                            {
                                firstShoot = temp;
                            }
                        }
                    }
                    
                    else if (!prevHit && ShootCoordinates.Count == 0)
                        ShootCoordinates = Special.CoordinateList();
                    else if (!prevHit && ShootCoordinates.Count != 100)
                        ShootCoordinates.RemoveAt(Special.FindIndex(ShootCoordinates, shoot));
                }
            }
            winGame();
        }

        public void winGame()
        {
            Console.Clear();
            Console.WriteLine($"\t\t\t\tПобедил {winner}");
            Console.WriteLine("\t\t\t\tВаше поле");
            field1.PrintField(true);
            Console.WriteLine("Вражеское поле");
            field2.PrintField(false);
        }



    }
}
