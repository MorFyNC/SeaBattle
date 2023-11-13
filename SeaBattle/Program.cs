using SeaBattle.Classes;
using System.Runtime.InteropServices;

Console.WriteLine("\tМорской бой! \n\tВыберите режим игры: \n\t1. Игрок против компьютера. \n\t2. Компьютер против компьютера.");
int choice = Convert.ToInt32(Console.ReadLine());

switch(choice)
{
    case 1:
        FieldFillStage();
        break;
    case 2:
        break;
}

void FieldFillStage()
{
    bool emptyness = false;
    List<Ship> ShipsLeft = new List<Ship> { new ShipOne(), new ShipOne(), new ShipOne(), new ShipOne(), new ShipTwo(), new ShipTwo(), new ShipTwo(), new ShipThree(), new ShipThree(), new ShipFour() };
    int Ship1Count = 0;
    int Ship2Count = 0;
    int Ship3Count = 0;
    int Ship4Count = 0;

    var shipCount = () =>
    {
        foreach(var ship in ShipsLeft)
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

    Field playerField = new Field();
    Field PCField = new Field();
    while (!emptyness)
    {
        Console.Clear();
        shipCount();
        ActionSeparator(92, $"\t\t\tМорской бой /Игрок vs Компьютер/ \n\tЗаполните свое поле! У вас есть еще {Ship1Count} одиночных, " +
            $"{Ship1Count} двойных, {Ship2Count} тройных и {Ship4Count} четверных кораблей");
        PrintField(playerField);
        Separator();
        Console.WriteLine("\t\t\tВыберите длину корабля");
        Console.ReadLine();

    }
    

}

bool FillShip(int xStart, int yStart, int xEnd, int yEnd, Ship ship, Field field)
{
    var Check = (bool possibility, int shiplen, int xStart, int yStart, int xEnd, int yEnd) =>
    {
        if (possibility)
        {
            if (xStart == xEnd)
            {
                for (int i = 0; i < shiplen; i++)
                {
                    field.field[xStart, yStart] = "[🚢]";
                    xStart++;
                }
            }
            else if (yStart == yEnd)
            {
                for (int i = 0; i < shiplen; i++)
                {
                    field.field[xStart, yStart] = "[🚢]";
                    yStart++;
                }
            }
        }
    };

    bool possibility = CheckCellsAround(xStart, yStart, xEnd, yEnd, ship, field);
    switch (ship.len)
    {
        case 1:
            Check(possibility, 1, xStart, yStart, xEnd, yEnd);
            break;
        case 2:
            Check(possibility, 2, xStart, yStart, xEnd, yEnd);
            break;
        case 3:
            Check(possibility, 3, xStart, yStart, xEnd, yEnd);
            break;
        case 4:
            Check(possibility, 4, xStart, yStart, xEnd, yEnd);
            break;
    }

    return false;
}

bool CheckCellsAround(int xStart, int yStart, int xEnd, int yEnd, Ship ship, Field field)
{
    var Check = (bool s) =>
    {
        if (s)
        {
            return true;
        }
        return false;
    };
    bool one = field.field[xStart + 1, yStart + 1] == "[ ]" && field.field[xStart - 1, yStart - 1] == "[ ]"
                && field.field[xStart - 1, yStart + 1] == "[ ]" && field.field[xStart + 1, yStart - 1] == "[ ]"
                && field.field[xStart, yStart + 1] == "[ ]" && field.field[xStart + 1, yStart] == "[ ]"
                && field.field[xStart, yStart - 1] == "[ ]" && field.field[xStart - 1, yStart] == "[ ]";

    bool two = one && field.field[xEnd + 1, yEnd + 1] == "[ ]" && field.field[xEnd - 1, yEnd - 1] == "[ ]"
                && field.field[xEnd - 1, yEnd + 1] == "[ ]" && field.field[xEnd + 1, yEnd - 1] == "[ ]"
                && field.field[xEnd, yEnd + 1] == "[ ]" && field.field[xEnd + 1, yEnd] == "[ ]"
                && field.field[xEnd, yEnd - 1] == "[ ]" && field.field[xEnd - 1, yEnd] == "[ ]";

    bool three = two;

    bool four = two;

    switch (ship.len)
    {
        case 1:
            Check(one);
            break;
        case 2:
            Check(two);
            break;
        case 3:
            Check(three);
            break;
        case 4:
            Check(four);
            break;
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
    Console.WriteLine($"\t{sep}\n" + $"\n\t{action}\n" + $"\t{sep}\n");
}

void Separator()
{
    Console.WriteLine("\n\t\t\t\t\t    *** *** ***\n");
}