using System.ComponentModel.Design;
using System.Runtime.InteropServices.JavaScript;

namespace SeaBattle.Classes
{
    public class Game
    {
        private string winner = "";
        Random random = new Random();
        Field field1 = new Field("pc");
        Field field2 = new Field("pc");
        bool prevHit = false;
        bool prevHit1 = false;
        bool prevHit2 = false;
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
                prevHit1 = field.Shoot(x, y);
                if (!prevHit1) turn++;
            }

            if (field.checkShootPossibility(x, y) && field.Shoot(x, y)) prevHit1 = true;
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
                    switch(field.owner)
                    {
                        case "PC1":prevHit1 = field.Shoot(x, y); ; break;
                        case "PC2":prevHit2 = field.Shoot(x, y); break;
                        default: prevHit = field.Shoot(x, y); break;
                    }
                }

                if (field.checkShootPossibility(x, y) && field.Shoot(x, y)) prevHit = true;
                return new int[] { x, y };
            }
            return new int[] { 0 };
        }

        public void PvE()
        {
            field1 = new Field("player");
            List<int[]> ShootCoordinates = Special.CoordinateList(field1);
            List<int[]> defaults = Special.CoordinateList(field1);
            field2.RandomFill();
            FieldFillStage();
            Console.Clear();
            bool changedMoves2 = false;
            List<int[]> ShootCoordinates2 = Special.CoordinateList(field1);
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
                    if (!changedMoves2)
                        ShootCoordinates2 = Special.CoordinateList(field1);
                    else
                    {
                        for (int i = ShootCoordinates2.Count - 1; i >= 0; i--)
                        {
                            if (i < ShootCoordinates2.Count && ShootCoordinates2[i] != null) if( field1.field[ShootCoordinates2[i][0], ShootCoordinates2[i][1]] == "[ ]" || field1.field[ShootCoordinates2[i][0], ShootCoordinates2[i][1]] == "[■]")
                            {
                                break;
                            }
                            else
                            {
                                ShootCoordinates2.Remove(ShootCoordinates2[i]);
                                ShootCoordinates2.Remove(null);
                                if (ShootCoordinates2.Count == 0)
                                {
                                    ShootCoordinates2 = Special.CoordinateList(field1);
                                }
                            }
                        }
                    }
                    int rngIndex = random.Next(ShootCoordinates2.Count);
                    while (ShootCoordinates2[rngIndex] == null)
                    {
                        rngIndex = random.Next(ShootCoordinates2.Count);
                    }
                    int[] rngCoord = ShootCoordinates2[rngIndex];

                    if (field1.field[rngCoord[0], rngCoord[1]] == "[■]")
                    {
                        ShootCoordinates2 = Special.NewCoordinateList(rngCoord);
                        prevHit2 = true;
                        changedMoves2 = true;
                        turn--;
                    }

                    Turn(field1, rngCoord);

                    turn++;

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
            List<int[]> ShootCoordinates1 = Special.CoordinateList(field2);
            List<int[]> ShootCoordinates2 = Special.CoordinateList(field1);
            bool changedMoves1 = false;
            bool changedMoves2 = false;
            while (!winCheck())
            {
                System.Threading.Thread.Sleep(100);
                Console.Clear();
                Console.WriteLine($"\t\t\t\tПоле {field1.owner}");
                field1.PrintField(true);
                Console.WriteLine($"Поле {field2.owner}");
                field2.PrintField(true);

                int[] temp = { };

                if (turn % 2 == 1)
                {

                    if (!changedMoves1)
                        ShootCoordinates1 = Special.CoordinateList(field2);
                    else
                    {
                        for (int i = ShootCoordinates1.Count - 1; i >= 0; i--)
                        {
                            if (i < ShootCoordinates1.Count && ShootCoordinates1[i] != null) if(field2.field[ShootCoordinates1[i][0], ShootCoordinates1[i][1]] == "[ ]" || field2.field[ShootCoordinates1[i][0], ShootCoordinates1[i][1]] == "[■]")
                            {
                                break;
                            }
                            else
                            {
                                ShootCoordinates1.Remove(ShootCoordinates1[i]);
                                ShootCoordinates1.Remove(null);
                                if (ShootCoordinates1.Count == 0)
                                {
                                    ShootCoordinates1 = Special.CoordinateList(field2);
                                }
                            }
                        }
                    }
                    int rngIndex = random.Next(ShootCoordinates1.Count);
                    while (ShootCoordinates1[rngIndex] == null)
                    {
                        rngIndex = random.Next(ShootCoordinates1.Count);
                    }
                    int[] rngCoord = ShootCoordinates1[rngIndex];

                    if (field2.field[rngCoord[0], rngCoord[1]] == "[■]")
                    {
                        ShootCoordinates1 = Special.NewCoordinateList(rngCoord);
                        prevHit1 = true;
                        changedMoves1 = true;
                        turn--;
                    }

                    Turn(field2, rngCoord);

                    turn++;

                }
                else
                {
                    if (!changedMoves2)
                        ShootCoordinates2 = Special.CoordinateList(field1);
                    else
                    {
                        for (int i = ShootCoordinates2.Count-1; i >= 0; i--)
                        {
                            if (i < ShootCoordinates2.Count && ShootCoordinates2[i] != null) if(field1.field[ShootCoordinates2[i][0],   ShootCoordinates2[i][1]] == "[ ]" || field1.field[ShootCoordinates2[i][0], ShootCoordinates2[i][1]] == "[■]")
                            {
                                break;
                            }
                            else
                            {
                                ShootCoordinates2.Remove(ShootCoordinates2[i]);
                                ShootCoordinates2.Remove(null);
                                if (ShootCoordinates2.Count == 0)
                                {
                                    ShootCoordinates2 = Special.CoordinateList(field1);
                                }
                            }
                        }
                    }
                    int rngIndex = random.Next(ShootCoordinates2.Count);
                    while (ShootCoordinates2[rngIndex] == null)
                    {
                        rngIndex = random.Next(ShootCoordinates2.Count);
                    }
                    int[] rngCoord = ShootCoordinates2[rngIndex];

                    if (field1.field[rngCoord[0], rngCoord[1]] == "[■]")
                    {
                        ShootCoordinates2 = Special.NewCoordinateList(rngCoord);
                        prevHit2 = true;
                        changedMoves2 = true;
                        turn--;
                    }

                    Turn(field1, rngCoord);

                    turn++;
                    
                }
            }
            winGame();
        }

        public void winGame()
        {
            Console.Clear();
            Console.WriteLine($"\t\t\t\tПобедил {winner}");
            Console.WriteLine($"\t\t\t\tПоле {field1.owner}");
            field1.PrintField(true);
            Console.WriteLine($"Поле {field2.owner}");
            field2.PrintField(false);
        }



    }
}
