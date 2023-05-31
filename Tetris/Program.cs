// Тетрис в консольном исполнении

// Двухмерный массив с текущим полем. Будет заполняться по мере появления фигур
int[,] workField = new int[30, 25];

// Счет игры
int currentScore = 0;

// Скорость игры 
int currentSpeed = 1;

// Базовая скорость игры - время такта в мс при speed = 1
int baseSpeed = 25;

// Смещение игрового поля от левого края - нужен для качественно прорисовки поля
string leftEmptyString = "          ";  

// Метод, произвольно выбирающий одну из нескольких заданных фигур
int[,] GetFigure()
{
    // Возможные комбинации фигур. Фигуры можно произвольно менять, в том числе и по размеру
    // ***
    //  *       
    int[,] f0 = { { 1, 1, 1 }, { 0, 1, 0 } };
    // ***
    //   *       
    int[,] f1 = { { 1, 1, 1 }, { 0, 0, 1 } };
    // ***
    // *         
    int[,] f2 = { { 1, 1, 1 }, { 1, 0, 0 } };
    // ****        
    int[,] f3 = { { 1, 1, 1, 1 } };
    // ***        
    int[,] f4 = { { 1, 1, 1 } };
    // **        
    int[,] f5 = { { 1, 1 } };
    // **
    // **        
    int[,] f6 = { { 1, 1 }, { 1, 1 } };
    // Произвольный номер фигуры
    int num = new Random().Next(0, 7); // [0,6] 
    switch (num)
    {
        case 0: return f0;
        case 1: return f1;
        case 2: return f2;
        case 3: return f3;
        case 4: return f4;
        case 5: return f5;
        case 6: return f6;
    }
    // Для корректности программы
    return f0;
}

// Метод, поворачивающий фигуру
int[,] RotateFigure(int[,] figure)
{
    int[,] result = new int[figure.GetLength(1), figure.GetLength(0)];
    for (int i = 0; i < figure.GetLength(0); i++)
    {
        for (int j = 0; j < figure.GetLength(1); j++)
        {
            result[j, figure.GetLength(0) - i - 1] = figure[i, j];
        }
    }
    return result;
}

// Метод, выводящий рабочее поле на экран
void PrintAllScreen(int[,] field, int screen, int speed)
{
    // Символ для вывода на экран вместо единички 
    char c = '☺';
    // Очищаем консоль
    Console.Clear();
    // Формируем первую строку 
    string outString = leftEmptyString + "┌";
    for (int i = 0; i < field.GetLength(1); i++) outString = outString + "─";
    outString = outString + "┐" + $" Счет: {screen} Скорость: {speed} \n";
    // Формируем основное поле
    for (int i = 0; i < field.GetLength(0); i++)
    {
        outString = outString + leftEmptyString + '│';
        for (int j = 0; j < field.GetLength(1); j++)
        {
            if (field[i, j] == 1) outString = outString + c;
            else outString = outString + ' ';
        }
        outString = outString + '│' + "\n";
    }
    // Выводим последнюю строку
    outString = outString + leftEmptyString + "└";
    for (int i = 0; i < field.GetLength(1); i++) outString = outString + "─";
    outString = outString + "┘";
    Console.WriteLine(outString);
}

// Метод, выводящий/убирающий фигуру в заданной позиции экрана
// show показывает, рисовать фигуру или стирать с экрана
void PrintFigure(int[,] figure, int xPos, int yPos, bool show)
{
    // Символ для вывода на экран вместо единички 
    char c = '☺';
    // Просматриваем каждый элемент фигуры
    for (int i = 0; i < figure.GetLength(0); i++)
    {
        for (int j = 0; j < figure.GetLength(1); j++)
        {
            // Если элемент существует, в зависимости от show его надо или показать, или закрасить 
            if (figure[i, j] == 1)
            {
                // Устанавливаем курсор с учетом толщины полей и отступа слева
                Console.SetCursorPosition(xPos + j + leftEmptyString.Length + 1, yPos + i + 1);
                if (show) Console.WriteLine(c);
                else Console.WriteLine(" ");
            }
        }
    }
}

// Метод, показывающий, что может ли фигура двигаться в указанном направлении
// Если rotation = 0 - проверяем снизу
// Если rotation = -1 - проверяем слева
// Если rotation = 1 - проверяем справа
bool HasFigureNear(int[,] field, int[,] figure, int xPos, int yPos, int rotation = 0)
{
    // Просматриваем фигуру построчно, начиная с нижней строки
    for (int i = figure.GetLength(0) - 1; i >= 0; i--)
    {
        // Просматриваем каждую точку этой строки
        for (int j = 0; j < figure.GetLength(1); j++)
        {
            // Если эта точка фигуры не пуста, проверяем ее
            if (figure[i, j] == 1)
            {
                // Получаем координату этой точки на общем поле
                int xOnField = xPos + j;
                int yOnField = yPos + i;
                // Получаем координаты интресующей соседней точки
                int secondPointX = xOnField + rotation;
                int secondPointY = yOnField + (rotation == 0 ? 1 : 0);
                // Проверяем, не нарушила ли точка границу
                bool onBorder = (rotation == 0 && secondPointY == field.GetLength(0))
                                || (rotation == -1 && secondPointX == -1)
                                || (rotation == 1 && secondPointX == field.GetLength(1));
                // Если нарушена граница или данная точка в рабочем поле знанята, выдаем true
                if (onBorder || (!onBorder && field[secondPointY, secondPointX] == 1)) return true;
            }
        }
    }
    return false;
}

// Метод, убирающий лишние строки и меняющий счет
void DeleteReadyLines(int[,] field, ref int score)
{
    // Просматриваем построчно снизу
    for (int i = 0; i < workField.GetLength(0); i++)
    {
        // Определям, собрана ли данная строка
        bool isReady = true;    
        for (int j = 0; j < workField.GetLength(1); j++)
        {
            if (workField[i, j] == 0)
            {
                isReady = false;
                break;
            }
        }
        // Если данная строка собрана, ее надо убрать
        if (isReady)
        {
            // Увеличиваем счет
            score++;
            // Смещаем содержимое рабочего поля вниз
            for (int i1 = i; i1 > 0; i1--)
            {
                for (int j1 = 0; j1 < workField.GetLength(1); j1++)
                {
                    field[i1, j1] = field[i1 - 1, j1];
                }
            }
        }
    }
}

// ОСНОВНАЯ ПРОГРАММА
Console.CursorVisible = false;
// Выводим первоначальный экран
PrintAllScreen(workField, currentScore, currentSpeed);
// Запускаем основной цикл программы
while (true)
{
    // Счетчик циклов - для плавности работы программы
    int count = 0;
    // Формируем фигуру
    int[,] currentFigure = GetFigure();
    // Задаем ей начальное значение вверху экрана
    int y = 0;
    int x = new Random().Next(0, workField.GetLength(1) - currentFigure.GetLength(1));
    // Выводим на экран 
    PrintFigure(currentFigure, x, y, true);
    // Если она достигла дна уже сейчас, значит, игра проиграна
    if (HasFigureNear(workField, currentFigure, x, y))
    {
        Console.Clear();
        return;
    }
    // Запускаем цикл перемещения фигуры вниз
    bool isDown = false;
    while (!isDown)
    {
        // Выполняем действия в зависимости от нажатой клавиши
        if (Console.KeyAvailable)
        {
            ConsoleKey ck = Console.ReadKey().Key;
            switch (ck)
            {
                case ConsoleKey.UpArrow:        // Стрелка вверх - переворачиваем фигуру        
                    PrintFigure(currentFigure, x, y, false);
                    currentFigure = RotateFigure(currentFigure);
                    PrintFigure(currentFigure, x, y, true);
                    break;
                case ConsoleKey.DownArrow:      // Стрелка вниз - спускаемся
                    if (!HasFigureNear(workField, currentFigure, x, y + 1))
                    {
                        PrintFigure(currentFigure, x, y, false);
                        y++;
                        PrintFigure(currentFigure, x, y, true);
                    }
                    break;
                case ConsoleKey.LeftArrow:      // Стрелка влево - перемещение влево
                    if (!HasFigureNear(workField, currentFigure, x, y, -1))
                    {
                        PrintFigure(currentFigure, x, y, false);
                        x--;
                        PrintFigure(currentFigure, x, y, true);
                    }
                    break;
                case ConsoleKey.RightArrow:     // Стрелка вправо - перемещение вправо
                    if (!HasFigureNear(workField, currentFigure, x, y, 1))
                    {
                        PrintFigure(currentFigure, x, y, false);
                        x++;
                        PrintFigure(currentFigure, x, y, true);
                    } 
                    break;
                case ConsoleKey.Escape:         // Esc - выход из программы
                    Console.Clear();
                    return;
            }
        }
        else
        {
            // Никакие клавиши не нажимались
            // Делаем задержку времени, зависящую от скорости
            await Task.Delay(baseSpeed / currentSpeed);
            // Увеличиваем счетчик циклов
            count++;
            // Снижаем позицию фигуры на одну строку (каждый десятый цикл)
            if (y < workField.GetLength(0) - currentFigure.GetLength(0))
            {
                if (count % 10 == 0)
                {
                    PrintFigure(currentFigure, x, y, false);
                    y++;
                    PrintFigure(currentFigure, x, y, true);
                }
            }
        }
        // Проверяем, достигнуто ли дно
        isDown = HasFigureNear(workField, currentFigure, x, y);
        // Если достигнуто дно
        if (isDown)
        {
            // То фигуру надо оставить на дне
            for (int i = 0; i < currentFigure.GetLength(0); i++)
            {
                for (int j = 0; j < currentFigure.GetLength(1); j++)
                {
                    workField[y + i, x + j] = workField[y + i, x + j] | currentFigure[i, j];
                }
            }
            // Проверить, а не появились ли какие готовые строки
            DeleteReadyLines(workField, ref currentScore);
            // Изменяем скорость в зависимости от результата
            currentSpeed = 1 + currentScore / 10;
            // Перерисоввываем основное поле
            PrintAllScreen(workField, currentScore, currentSpeed);
        }
    }
}