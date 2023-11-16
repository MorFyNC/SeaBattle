using SeaBattle.Classes;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using System.Text;
Random random = new Random();
Console.OutputEncoding = Encoding.UTF8;
string gameType;
int opponent = 0;
string prevTurn;
string playerShot = "";
string computerShot = "";
string winner = "";
start:
ActionSeparator(30, "\t   Морской бой! \n\t\t\t      Выберите режим игры: \n\t\t\t1. PvE \t\t      2. PCvPC");

Console.Write(">");
bool readedField = false;
int choice = Convert.ToInt32(Console.ReadLine());
Console.Clear();
List<Ship> ShipsLeft = new List<Ship> { ShipFabric.Create(1), ShipFabric.Create(1), ShipFabric.Create(1), ShipFabric.Create(1), ShipFabric.Create(2), ShipFabric.Create(2), ShipFabric.Create(2), ShipFabric.Create(3), ShipFabric.Create(3), ShipFabric.Create(4) };
Field playerField = new Field("Игрок");
Field PCField = new Field("Компьютер");
Field EmptyField1 = new Field();
Field EmptyField2 = new Field();

switch (choice)
{
    case 1:
        gameType = "PvE";
        WannaRead();
        if (!readedField)
        {
            FieldFillStage();
            WannaSave();
        }
        Console.Clear();
        PrintField(playerField);
        Console.WriteLine("Вы хотите оставить такое поле? \n\t\t\t\t1.Да \t\t\t\t2.Нет");
        Console.Write(">");
        int wannaChange = Convert.ToInt32(Console.ReadLine());

        switch (wannaChange)
        {
            case 2: Console.Clear(); goto start;
            case 1: break;
        }
        Console.Clear();
        deleteSharps(playerField);
        PcFieldCreate();
        deleteSharps(PCField);
        Console.WriteLine("Поле игрока");
        PrintField(playerField);
        PvE();
        break;
    case 2:
        gameType = "PCvPC";
        break;
}

void FieldFillStage()
{
    bool emptyness = false;
    int Ship1Count = 0;
    int Ship2Count = 0;
    int Ship3Count = 0;
    int Ship4Count = 0;

    var shipCount = () =>
    {
        Ship1Count = 0;
        Ship2Count = 0;
        Ship3Count = 0;
        Ship4Count = 0;

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
    };

    
    while (!(ShipsLeft.Count == 0))
    {
        var PrintTitle = () =>
        {
            Console.Clear();
            shipCount();
            ActionSeparator(92, $"\t\t\tМорской бой /Игрок vs Компьютер/ \n\t\t\tЗаполните свое поле! У вас есть еще {Ship1Count} одиночных, " +
                $"{Ship2Count} двойных, {Ship3Count} тройных и {Ship4Count} четверных кораблей");
            PrintField(playerField);
            Separator();
        };
        PrintTitle();
        Console.WriteLine("\t\t\t\t     Выберите длину корабля");
        Console.Write(">");
        int len = Convert.ToInt32(Console.ReadLine());
        int startX = 0;
        int startY = 0;
        int xEnd = 0;
        int yEnd = 0;
        switch(len)
        {
            case 1:
                Console.WriteLine("\t\t\t\t   Введите координату клетки");
                Console.Write(">");
                string input = Console.ReadLine();
                if (input.Count() == 2)
                {
                    startX = Convert.ToInt32(Convert.ToString(input[1]));
                    startY = ConvertCoordinate(input[0]) + 1; 
                }
                else
                {
                    startX = Convert.ToInt32(Convert.ToString(input.Substring(1, 2)));
                    startY = ConvertCoordinate(input[0]) + 1;
                    
                }
                if(Ship1Count != 0 && FillShip(startX, startY, startX, startY, 1, playerField, ShipsLeft))
                    ShipsLeft.RemoveAt(0);
                break; 
            case 2:
                Console.WriteLine("\t\t\t\t   Введите координату начальной клетки");
                Console.Write(">");
                string start = Console.ReadLine();
                if (start.Count() == 2)
                {
                    startX = Convert.ToInt32(Convert.ToString(start[1]));
                    startY = ConvertCoordinate(start[0]) + 1;
                }
                else
                {
                    startX = Convert.ToInt32(Convert.ToString(start.Substring(1, 2)));
                    startY = ConvertCoordinate(start[0]) + 1;
                }
                Console.WriteLine("\t\t\t\t   Введите координату конечной клетки");
                Console.Write(">");
                string end = Console.ReadLine();
                if (end.Count() == 2)
                {
                    xEnd = Convert.ToInt32(Convert.ToString(end[1]));
                    yEnd = ConvertCoordinate(end[0]) + 1;
                }
                else
                {
                    xEnd = Convert.ToInt32(Convert.ToString(end.Substring(1, 2)));
                    yEnd = ConvertCoordinate(end[0]) + 1;
                }
                if (Ship2Count != 0 && FillShip(startX, startY, xEnd, yEnd, 2, playerField, ShipsLeft))
                    ShipsLeft.RemoveAt(Ship1Count);
                break;
            case 3:
                Console.WriteLine("\t\t\t\t   Введите координату начальной клетки");
                Console.Write(">");
                start = Console.ReadLine();
                if (start.Count() == 2)
                {
                    startX = Convert.ToInt32(Convert.ToString(start[1]));
                    startY = ConvertCoordinate(start[0]) + 1;
                }
                else
                {
                    startY = ConvertCoordinate(start[0]) + 1;
                    startX = Convert.ToInt32(Convert.ToString(start.Substring(1, 2)));
                }
                Console.WriteLine("\t\t\t\t   Введите координату конечной клетки");
                Console.Write(">");
                end = Console.ReadLine();
                if (end.Count() == 2)
                {
                    xEnd = Convert.ToInt32(Convert.ToString(end[1]));
                    yEnd = ConvertCoordinate(end[0]) + 1;
                }
                else
                {
                    xEnd = Convert.ToInt32(Convert.ToString(end.Substring(1, 2)));
                    yEnd = ConvertCoordinate(end[0]) + 1;
                }
                if (Ship3Count != 0 && FillShip(startX, startY, xEnd, yEnd, 3, playerField, ShipsLeft))
                    ShipsLeft.RemoveAt(Ship1Count+Ship2Count);
                break;
            case 4:
                Console.WriteLine("\t\t\t\t   Введите координату начальной клетки");
                Console.Write(">");
                start = Console.ReadLine();
                if (start.Count() == 2)
                {
                    startX = Convert.ToInt32(Convert.ToString(start[1]));
                    startY = ConvertCoordinate(start[0]) + 1;
                }
                else
                {
                    startY = ConvertCoordinate(start[0]) + 1;
                    startX = Convert.ToInt32(Convert.ToString(start.Substring(1, 2)));
                }
                Console.WriteLine("\t\t\t\t   Введите координату конечной клетки");
                Console.Write(">");
                end = Console.ReadLine();
                if (end.Count() == 2)
                {
                    xEnd = Convert.ToInt32(Convert.ToString(end[1]));
                    yEnd = ConvertCoordinate(end[0]) + 1;
                }
                else
                {
                    xEnd = Convert.ToInt32(Convert.ToString(end.Substring(1, 2)));
                    yEnd = ConvertCoordinate(end[0]) + 1;
                }
                if (Ship4Count != 0 && FillShip(startX, startY, xEnd, yEnd, 4, playerField, ShipsLeft))
                    ShipsLeft.RemoveAt(Ship1Count + Ship2Count + Ship3Count);
                break;
        }
        PrintTitle();
    }
}

bool FillShip(int xStart, int yStart, int xEnd, int yEnd, int shiplen, Field field, List<Ship> ships)
{
    var FillSharps = (int xStart, int yStart, int xEnd, int yEnd, bool possibility) =>
    {
        if (possibility)
        {
            if (field.field[xStart + 1, yStart] == "[ ]")
                field.field[xStart + 1, yStart] = "[#]";
            if (field.field[xStart + 1, yStart - 1] == "[ ]")
                field.field[xStart + 1, yStart - 1] = "[#]";
            if (field.field[xStart + 1, yStart + 1] == "[ ]")
                field.field[xStart + 1, yStart + 1] = "[#]";
            if (field.field[xStart - 1, yStart] == "[ ]")
                field.field[xStart - 1, yStart] = "[#]";
            if (field.field[xStart - 1, yStart - 1] == "[ ]")
                field.field[xStart - 1, yStart - 1] = "[#]";
            if (field.field[xStart - 1, yStart + 1] == "[ ]")
                field.field[xStart - 1, yStart + 1] = "[#]";
            if (field.field[xStart, yStart - 1] == "[ ]")
                field.field[xStart, yStart - 1] = "[#]";
            if (field.field[xStart, yStart + 1] == "[ ]")
                field.field[xStart, yStart + 1] = "[#]";

            if (field.field[xEnd + 1, yEnd] == "[ ]")
                field.field[xEnd + 1, yEnd] = "[#]";
            if (field.field[xEnd + 1, yEnd - 1] == "[ ]")
                field.field[xEnd + 1, yEnd - 1] = "[#]";
            if (field.field[xEnd + 1, yEnd + 1] == "[ ]")
                field.field[xEnd + 1, yEnd + 1] = "[#]";
            if (field.field[xEnd - 1, yEnd] == "[ ]")
                field.field[xEnd - 1, yEnd] = "[#]";
            if (field.field[xEnd - 1, yEnd - 1] == "[ ]")
                field.field[xEnd - 1, yEnd - 1] = "[#]";
            if (field.field[xEnd - 1, yEnd + 1] == "[ ]")
                field.field[xEnd - 1, yEnd + 1] = "[#]";
            if (field.field[xEnd, yEnd - 1] == "[ ]")
                field.field[xEnd, yEnd - 1] = "[#]";
            if (field.field[xEnd, yEnd + 1] == "[ ]")
                field.field[xEnd, yEnd + 1] = "[#]";
        }
    };

    var Fill = (bool possibility, int shiplen, int xStart, int yStart, int xEnd, int yEnd, List<Ship> ships) =>
    {
        int temp1 = xStart;
        int temp2 = yStart;
        if (possibility)
        {
            if (xStart == xEnd && yStart < yEnd)
            {
                for (int i = 0; i < shiplen; i++)
                {
                    field.field[temp1, temp2] = "[■]";
                    temp2++;
                }
            }
            else if (yStart == yEnd && xStart < xEnd)
            {
                for (int i = 0; i < shiplen; i++)
                {
                    field.field[temp1, temp2] = "[■]";
                    temp1++;
                }
            }
            else if (yStart == yEnd && xStart > xEnd)
            {
                for (int i = 0; i < shiplen; i++)
                {
                    field.field[temp1, temp2] = "[■]";
                    temp1--;
                }
            }
            else if (xStart == xEnd && yStart > yEnd)
            {
                for (int i = 0; i < shiplen; i++)
                {
                    field.field[temp1, temp2] = "[■]";
                    temp2--;
                }
            }
            else if(xStart == xEnd && yStart == yEnd)
            {
                field.field[temp1, temp2] = "[■]";
            }
        }
        FillSharps(xStart, yStart, xEnd, yEnd, possibility);
    };

    bool possibility = CheckCells(xStart, yStart, xEnd, yEnd, shiplen, field, ships);

    switch (shiplen)
    {
        case 1:
            Fill(possibility, 1, xStart, yStart, xEnd, yEnd, ships);
            break;
        case 2:
            Fill(possibility, 2, xStart, yStart, xEnd, yEnd, ships);
            break;
        case 3:
            Fill(possibility, 3, xStart, yStart, xEnd, yEnd, ships);
            break;
        case 4:
            Fill(possibility, 4, xStart, yStart, xEnd, yEnd, ships);
            break;
    }

    return possibility;
}

bool CheckCells(int xStart, int yStart, int xEnd, int yEnd, int shiplen, Field field, List<Ship> ships)
{
    switch(shiplen)
    {
        case 1: return (field.field[xStart, yStart] == "[ ]" && ships.Any(s => s is ShipOne));
        case 2: return (field.field[xStart, yStart] == "[ ]" && field.field[xEnd, yEnd] == "[ ]") && ships.Any(s => s is ShipTwo) && (Math.Abs(xEnd - xStart) == 1 && yStart == yEnd || Math.Abs(yStart - yEnd) == 1 && xStart == xEnd);
        case 3: return (field.field[xStart, yStart] == "[ ]" && field.field[xEnd, yEnd] == "[ ]") && ships.Any(s => s is ShipThree) && (Math.Abs(xEnd - xStart) == 2 && yStart == yEnd || Math.Abs(yStart - yEnd) == 2 && xStart == xEnd);
        case 4: return (field.field[xStart, yStart] == "[ ]" && field.field[xEnd, yEnd] == "[ ]") && ships.Any(s => s is ShipFour) && (Math.Abs(xEnd - xStart) == 3 && yStart == yEnd || Math.Abs(yStart - yEnd) == 3 && xStart == xEnd);
    }
    
    return false;
}

void PrintField(Field field)
{
    int rows = field.field.GetUpperBound(0) + 1;    
    int columns = field.field.Length / rows;


    Console.Write("\n\t\t\t\t");
    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < columns; j++)
        {
            Console.Write($"{field.field[i, j]}");
        }
        Console.Write("\n\t\t\t\t");
    }
}

void ActionSeparator(int num, string action)
{
    string sep = "";
    for (int i = 0; i < num; i++)
    {
        sep += "_";
    }
    Console.WriteLine($"\t\t\t{sep}\n" + $"\n\t\t\t{action}\n" + $"\t\t\t{sep}\n");
}

void Separator()
{
    Console.WriteLine("\n\t\t\t\t\t    *** *** ***\n");
}

int ConvertCoordinate(char coordinate)
{
    switch(coordinate)
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

void WriteFieldToFile(string path, string fileName, Field field)
{
    StreamWriter sw = new StreamWriter($"{path}\\{fileName}.txt");
    sw.WriteLine(JsonConvert.SerializeObject(field.field));
    sw.Close();
}

void WannaSave()
{
    Console.WriteLine("\t\t\t\tХотите ли вы сохранить данную раскладку? \n \t\t\t\t1.Да \t\t\t\t2.Нет");
    Console.Write(">");
    int saveChoice = Convert.ToInt32(Console.ReadLine());

    switch(saveChoice)
    {
        case 1:
            Console.Clear();
            ActionSeparator(59, "Введите путь к папке, в которую будет сохранена расстановка");
            Console.Write(">");
            string path = Console.ReadLine();
            Console.Clear();
            ActionSeparator(60, "Введите название файла, в который будет записана расстановка\n\t\t\t\t(будет автоматически создан файл с этим именем)");
            Console.Write(">");
            string fileName = Console.ReadLine();
            WriteFieldToFile(path, fileName, playerField);
            break;
        case 2: break;
    }
}

void ReadFieldFromFile(string path, string fileName)
{
    StreamReader sr = new StreamReader($"{path}\\{fileName}.txt");
    playerField.field = JsonConvert.DeserializeObject<string[,]>(sr.ReadLine());
    readedField = true;
}
void WannaRead()
{
    ActionSeparator(42, "Хотите ли вы загрузить раскладку из файла? \n \t\t\t\t1.Да \t\t2.Нет");
    Console.Write(">");
    int readChoice = Convert.ToInt32(Console.ReadLine());
    Console.Clear();
    switch (readChoice)
    {
        case 1:
            ActionSeparator(61, "Введите путь к папке, в которуй находится файл с расстановкой");
            Console.Write(">");
            string path = Console.ReadLine();
            Console.Clear();
            ActionSeparator(54, "Введите название файла, в котором записана расстановка");
            Console.Write(">");
            string fileName = Console.ReadLine();
            Console.Clear();
            ReadFieldFromFile(path, fileName);
            break;
        case 2: break;
    }
}

void deleteSharps(Field field)
{
    int rows = field.field.GetUpperBound(0) + 1;
    int columns = field.field.Length / rows;

    for (int i = 0; i < rows; i++)
    {
        for(int j = 0; j < columns; j++)
        {
            if(field.field[i, j] == "[#]")
            {
                field.field[i, j] = "[ ]";
            }
        }
    }
}

void PcFieldCreate()
{
    List<Ship> ShipsLeftPC = new List<Ship> { ShipFabric.Create(1), ShipFabric.Create(1), ShipFabric.Create(1), ShipFabric.Create(1), ShipFabric.Create(2), ShipFabric.Create(2), ShipFabric.Create(2), ShipFabric.Create(3), ShipFabric.Create(3), ShipFabric.Create(4) };

    int Ship1Count = 0;
    int Ship2Count = 0;
    int Ship3Count = 0;
    int Ship4Count = 0;

    var shipCount = () =>
    {
        Ship1Count = 0;
        Ship2Count = 0;
        Ship3Count = 0;
        Ship4Count = 0;

        foreach (Ship ship in ShipsLeftPC)
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
    };

    int startX = random.Next(1, 11);
    int startY = random.Next(1, 11);
    int xEnd = 0;
    int yEnd = 0;
    int rotateChoice = random.Next(1, 3);

    for (int len = 1; len < 5;)
    {
        start1:
        rotateChoice = random.Next(1, 3);
        startX = random.Next(1, 11);
        startY = random.Next(1, 11);

        if (rotateChoice == 1 && len == 2)
        {
            startX = random.Next(1, 11);
            startY = random.Next(1, 10);
            xEnd = startX;
            yEnd = startY+1;
        }
        else if (rotateChoice == 2 && len == 2)
        {
            startX = random.Next(1, 10);
            startY = random.Next(1, 11);
            xEnd = startX+1;
            yEnd = startY;
        }

        if (rotateChoice == 1 && len == 3)
        {
            startX = random.Next(1, 11);
            startY = random.Next(1, 9);
            xEnd = startX;
            yEnd = startY + 2;
        }
        else if(rotateChoice == 2 && len == 3)
        {
            startX = random.Next(1, 9);
            startY = random.Next(1, 11);
            xEnd = startX + 2;
            yEnd = startY;
        }

        if (rotateChoice == 1 && len == 4)
        {
            startX = random.Next(1, 11);
            startY = random.Next(1, 8);
            xEnd = startX;
            yEnd = startY + 3;
        }
        else if (rotateChoice == 2 && len == 4)
        {
            startX = random.Next(1, 8);
            startY = random.Next(1, 11);
            xEnd = startX + 3;
            yEnd = startY;
        }
        shipCount();
        switch (len)
        {
            case 1:
                if (Ship1Count != 0 && FillShip(startX, startY, startX, startY, 1, PCField, ShipsLeftPC))
                {
                    ShipsLeftPC.RemoveAt(0);
                }
                    break;
            case 2:
                if (Ship2Count != 0 && FillShip(startX, startY, xEnd, yEnd, 2, PCField, ShipsLeftPC))
                {   
                    ShipsLeftPC.RemoveAt(0);
                }
                break;
            case 3:
                if (Ship3Count != 0 && FillShip(startX, startY, xEnd, yEnd, 3, PCField, ShipsLeftPC))
                {   
                    ShipsLeftPC.RemoveAt(0);
                }
                break;
            case 4:
                if (Ship4Count != 0 && FillShip(startX, startY, xEnd, yEnd, 4, PCField, ShipsLeftPC))
                {   
                    ShipsLeftPC.RemoveAt(0);
                }
                break;
        }
        if ((Ship1Count == 0 && len == 1) || (Ship2Count == 0 && len == 2) || (Ship3Count == 0 && len == 3) || (Ship4Count == 0 && len == 4)) len++;
        else { goto start1; }
       
    }
}

void PvE()
{
    while(!winCheck(playerField, PCField))
    {
        Turn(playerField, PCField);
    }
    Console.Clear();
    Console.WriteLine($"\t\t\t\tПобедил {winner}");
    Console.WriteLine("\t\t\t\tВаше поле");
    PrintField(playerField);
    Console.WriteLine("Вражеское поле");
    PrintField(PCField);
}

string[] Turn(Field field1, Field field2)
{
    
    if (gameType == "PvE")
    {
        switch (opponent)
        {
            case 0:
            again:
                Console.Clear();
                Console.WriteLine("\t\t\t\tВаше поле");
                PrintField(playerField);
                Console.WriteLine("Вражеское поле");
                PrintField(EmptyField2);
                string output = $"{computerShot} \n {playerShot}\n\t\t\t\tВведите координату клетки, в которую хотите выстрелить";
                Console.WriteLine(output);
                Console.Write(">");
                string shootCoordinate = Console.ReadLine();
                int x = shootCoordinate.Length == 2 ? Convert.ToInt32(Convert.ToString(shootCoordinate[1])) : Convert.ToInt32(shootCoordinate.Substring(1, 2));
                int y = ConvertCoordinate(shootCoordinate[0]);

                if (!Shoot(field2, x, y))
                {
                    playerShot = "\t\t\t\tВы попали";
                    goto again;
                }
                else
                {
                    playerShot = "\t\t\t\tВы не попали";
                    opponent = 1;
                }
                break;
            case 1:
            PC:
                x = random.Next(1, 11);
                y = random.Next(1, 11);

                if(!Shoot(field1, x, y))
                {
                    computerShot = $"{field2.owner} попал";
                    Console.Clear();
                    Console.WriteLine("\t\t\t\tВНИМАНИЕ");
                    PrintField(playerField);
                    Console.WriteLine($"{computerShot}");
                    Console.WriteLine("Нажмите ENTER чтобы продолжить");
                    Console.ReadLine();
                    if (!winCheck(playerField, PCField))
                        goto PC;
                    break;
                }
                else
                {
                    computerShot = $"{field2.owner} не попал";
                    opponent = 0;
                }
                break;
        }    
        return new string[] { playerShot, computerShot };
    }
    return null;
}

bool Shoot(Field field, int x, int y)
{
    if (field.field[x, y] == "[ ]")
    {
        if (opponent == 1) EmptyField1.field[x, y] = "[○]";
        else EmptyField2.field[x, y] = "[○]";
        field.field[x, y] = "[○]";
        return true;
    }
    else if (field.field[x,y]== "[■]")
    {
        if (opponent == 1) EmptyField1.field[x, y] = "[X]";
        else EmptyField2.field[x, y] = "[X]";
        field.field[x, y] = "[X]";
        return false;
    }

    return false;
}

bool winCheck(Field field1, Field field2)
{
    for(int i = 0; i < 12; i++)
    {
        for(int j = 0; j < 12; j++)
        {
            if (checkShips(field1.field) && checkShips(field2.field)) { return false; }
        }
    }
    if (!checkShips(field1.field))
        winner = field2.owner;
    else if (!checkShips(field2.field))
        winner = field1.owner;

    return true;
}

bool checkShips(string[,] arr)
{
    bool flag = false;
    for (int r = 0; r < 12; r++)
    {
        int c = 0;
        while (flag == false && c < 12)
        {
            if (arr[r, c] == "[■]")
            {
                flag = true;
            }
            c++;
        }
    }
    return flag;
}