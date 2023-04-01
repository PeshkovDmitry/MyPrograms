Triangle tr = new Triangle(30);
tr.Print(true);

public class Triangle
{
    private int[,] data;

    private int maxValueSymbolCount;

    private int size;

    public Triangle(int s = 5)
    {
        size = s;
        data = new int[size, size];
        int maxValue = 0;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j <= i; j++)
            {
                data[i, j] = (j == 0) || (j == i) ? 1 : data[i - 1, j - 1] + data[i - 1, j];
                maxValue = data[i, j] > maxValue ? data[i, j] : maxValue;
            }
        }
        maxValueSymbolCount = SymbolCount(maxValue);
    }

    public void SimplePrint()
    {
        Console.Clear();
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j <= i; j++)
            {
                Console.Write($"{data[i, j]} ");
            }
            Console.WriteLine();
        }
    }

    public void Print(bool onlyOddAsStar = false)
    {
        Console.Clear();
        for (int i = 0; i < size; i++)
        {
            int rowLength = onlyOddAsStar ? 2 * i + 1 : i + (i + 1) * maxValueSymbolCount;
            if (rowLength < Console.BufferWidth && i < Console.BufferHeight)
            {
                for (int j = 0; j <= i; j++)
                {
                    int currentCursorPosition = Console.WindowWidth / 2 - rowLength / 2;
                    currentCursorPosition 
                        += onlyOddAsStar ? 2 * j : j * (maxValueSymbolCount + 1);
                    Console.SetCursorPosition(currentCursorPosition, i);
                    if (onlyOddAsStar) 
                        if (data[i, j]%2 == 1) Console.Write("*");
                        else Console.Write(" ");
                    else Console.Write($"{data[i, j]} ");
                }
                Console.WriteLine();
            }
            else 
            {
                Console.Clear();
                Console.WriteLine("Размер консоли недостаточен для вывода треугольника Паскаля");
            }
        }
    }

    private int SymbolCount(int num)
    {
        int res = (num == 0) ? 1 : 0;
        while (num > 0)
        {
            res++;
            num = num / 10;
        }
        return res;
    }
}