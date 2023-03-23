// Тетрис в консольном исполнении

// Двухмерный массив с текущим полем. Будет заполняться по мере появления фигур
int[,] field = new int[30,25];

// Счет игры
int score = 0;

// Скорость игры 
int speed = 1;

// Метод, произвольно выбирающий одну из нескольких заданных фигур
int[,] GetFigure() {
    // Возможные комбинации фигур
    // ***
    //  *       
    int[,] f0 = {{1,1,1},{0,1,0}};
    // ***
    //   *       
    int[,] f1 = {{1,1,1},{0,0,1}};
    // ***
    // *         
    int[,] f2 = {{1,1,1},{1,0,0}};
    // ****        
    int[,] f3 = {{1,1,1,1}};   
    // ***        
    int[,] f4 = {{1,1,1}};
    // **        
    int[,] f5 = {{1,1}};
    // **
    // **        
    int[,] f6 = {{1,1},{1,1}};
    // Произвольный номер фигуры
    int num = new Random().Next(0,7); // [0,6] 
    switch (num) {
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
int[,] RotateFigure(int[,] f) {
    int[,] result = new int[f.GetLength(1),f.GetLength(0)];
    for(int i = 0; i < f.GetLength(0); i++) {
        for(int j = 0; j < f.GetLength(1); j++) {
            result[j,f.GetLength(0) - i - 1] = f[i,j]; 
        }
    }
    return result;
}

// Метод, выводящий рабочее поле на экран
void Print(int[,] fld, int[,] fg, int xPos, int yPos, int scr, int spd) {
    // Символ для вывода на экран вместо единички 
    char c = '☺';
    // Для вывода на экран создаем новый массив, 
    // объединяющий данные fld и fg 
    int[,] screen = new int[fld.GetLength(0),fld.GetLength(1)];
    // Копируем в него массив fld
    for(int i = 0; i < fld.GetLength(0); i++) {
        for(int j = 0; j < fld.GetLength(1); j++) {
            screen[i,j] = fld[i,j];
        }
    }
    // Накладываем на него фигуру
    // Т.к. массивы содержат 0 или 1, можно использовать логику ИЛИ 
    for(int i = 0; i < fg.GetLength(0); i++) {
        for(int j = 0; j < fg.GetLength(1); j++) {
            screen[yPos + i, xPos + j] = screen[yPos + i, xPos + j] | fg[i, j];
        }
    }
    // Очищаем консоль
    Console.Clear();
    // Выводим первую строку 
    string str = "┌";
    for(int i = 0; i < fld.GetLength(1); i++) str = str + "─";
    str = str + "┐" + $" Счет: {scr} Скорость: {spd}";
    Console.WriteLine(str);
    // Выводим основное поле
    for(int i = 0; i < screen.GetLength(0); i++) {
        Console.Write("│");
        for(int j = 0; j < screen.GetLength(1); j++) {
            if (screen[i,j] == 1) Console.Write(c);
            else Console.Write(" ");
        }
        Console.WriteLine("│");
    }
    // Выводим последнюю строку
    str = "└";
    for(int i = 0; i < fld.GetLength(1); i++) str = str + "─";
    str = str + "┘";
    Console.WriteLine(str);
}

// Метод, показывающий, что фигура легла на дно
bool OnTheFloor(int[,] fld, int[,] fg, int xPos, int yPos) {
    bool result = false;
    // Просматриваем фигуру построчно, начиная с нижней строки
    for (int i = fg.GetLength(0) - 1; i >= 0 ;i--) {
        // Просматриваем каждую точку этой строки
        for (int j = 0; j < fg.GetLength(1);j++) {
            // Если эта точка фигуры не пуста, проверяем ее
            if (fg[i,j] == 1) {
                // Получаем координату этой точки на общем поле
                int xOnField = xPos + j;
                int yOnField = yPos + i;             
                if (yOnField == fld.GetLength(0) - 1) {
                    // За этой точкой - нижняя граница, фигура достигла дна
                    result = true;
                    break;
                } 
                else if (fld[yOnField + 1, xOnField] == 1) {
                    // За этой точкой - существующая точка на поле
                    result = true;
                    break;
                }
            }
        }
        // Если точка достигла дна, дальнейшая проверка не нужна
        if (result) break;            
    }
    return result;
}

// Метод, убирающий лишние строки и меняющий счет
void DeleteReadyLines(int[,] fld, ref int scr) {
    // Просматриваем построчно снизу
    bool isReady = true;
    for(int i = field.GetLength(0) - 1; i >= 0; i--) {
        for(int j = 0; j < field.GetLength(1); j++) {
            if (field[i, j] == 0) {
                isReady = false;
                break;
            }
        }
        // Если данная строка собрана, ее надо убрать
        if (isReady) {
            // Увеличиваем счет
            scr++;
            // Смещаем содержимое рабочего поля вниз
            for(int i1 = i; i1 > 0; i1--) {
                for(int j1 = 0; j1 < field.GetLength(1); j1++) {
                    fld[i1,j1] = fld[i1 - 1,j1];    
                }        
            } 
        }
    }    
}

// Запускаем основной цикл программы
while (true) {
    // Счетчик циклов - для плавности работы программы
    int count = 0;
    // Формируем фигуру
    int[,] figure = GetFigure();
    // Задаем ей начальное значение вверху экрана
    int y = 0;
    int x = new Random().Next(0,field.GetLength(1) - figure.GetLength(1));
    // Если она достигла дна уже сейчас, значит, игра проиграна
    if (OnTheFloor(field, figure, x, y)) return;
    // Запускаем цикл перемещения фигуры вниз
    bool isDown = false;
    while (!isDown) {
        // Выводим на экран текущее состояние (через каждые 10 циклов, чтобы экран не так сильно мелькал)
        if (count%10 == 0) Print(field, figure, x, y, score, speed);        
        // Выполняем действия в зависимости от нажатой клавиши
        if (Console.KeyAvailable) {
            ConsoleKey ck = Console.ReadKey(false).Key;
            switch (ck) {
                case ConsoleKey.UpArrow:        // Стрелка вверх - переворачиваем фигуру        
                    figure = RotateFigure(figure);
                    break;
                case ConsoleKey.DownArrow:      // Стрелка вниз - спускаемся
                    if (y < field.GetLength(0) - figure.GetLength(0)) y++;
                    break;        
                case ConsoleKey.LeftArrow:      // Стрелка влево - перемещение влево
                    if (x != 0) x--;
                    break;   
                case ConsoleKey.RightArrow:     // Стрелка вправо - перемещение вправо
                    if (x < field.GetLength(1) - figure.GetLength(1)) x++;
                    break;        
                case ConsoleKey.Escape:         // Esc - выход из программы
                    return; 
            }
        } else {
            // Никакие клавиши не нажимались
            // Делаем задержку времени, зависящую от скорости
            await Task.Delay(25/speed);
            // Увеличиваем счетчик циклов
            count++;
            // Снижаем позицию фигуры на одну строку (каждый десятый цикл)
            if (y < field.GetLength(0) - figure.GetLength(0)) {
                if (count%10 == 0) y++;
            }    
        }
        // Проверяем, достигнуто ли дно
        isDown = OnTheFloor(field, figure, x, y);
        // Если дно достигнуто
        if (isDown) {
            // То фигуру надо оставить на дне
            for(int i = 0; i < figure.GetLength(0); i++) {
                for(int j = 0; j < figure.GetLength(1); j++) {
                    field[y + i, x + j] = field[y + i, x + j] | figure[i, j];
                }
            }
            // Проверить, а не появились ли какие готовые строки
            DeleteReadyLines(field, ref score);
            // Изменяем скорость в зависимости от результата
            speed = 1 + score/10; 
        }
    }
}
