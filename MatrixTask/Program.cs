// Программа, которая заполняет матрицу заданного размера целыми числами таким образом,
// что числа не повторяются ни в строке, ни в столбце, ни в малом квадрате 3х3

// Количество строк в матрице
int rows = 6;
// Количество столбцов в матрице
int cols = 6;
// Максимальное допустимое число в ячейке
int max = 6;
// Создаем матрицу
int[] matrix = new int[rows*cols];
// Задаем начальное значение
matrix[0] = 1;
matrix[7] = 2;
matrix[14] = 3;
matrix[21] = 4;
matrix[28] = 5;
matrix[35] = 6;
// Формируем массив с номерами неизменных ячеек
int[] exeptions = {0,7,14,21,28,35};
// Текущее положение в массиве
int pos = 0;
// Направление работы. Вперед - true
bool direction = true;
// Пока текущий индекс не превысил размер массива, производим подбор
while (pos < rows*cols) {
    // Проверяем, можно ли менять данную ячейку
    if (CanChange(pos,exeptions)) {
        // Данную ячейку менять можно
        // Для текущей ячейки подбираем ближайшее наименьшее подходящее значение
        Insert(pos, max);
        // Проверяем, какое число было вставлено
        // Если не ноль, то можно двигаться далее
        // Если ноль, то двигаемся назад
        // В начальной позиции всегда направление - вперед
        direction = !(matrix[pos] == 0);    
    }
    // Выводим то, что получилось
    PrintResult();
    // В зависимости от принятого направления увеличиваем или уменьшаем текущую позицию
    if (direction) {
        pos++;
    } 
    else {
        pos--;
        // Если вдруг пришли к началу, надо принудительно направить вперед
        if (pos < 0 ) {
            pos = 0;
            direction = true;
        }
    }
}

   
// Метод для вывода результата на экран. В позиции pos рисует Х
void PrintResult() {
    Console.Clear();
    int row = pos/cols;
    int col = pos%cols;
    for (int i = 0; i < rows; i++) {
        for (int j = 0; j < cols; j++) {
            Console.Write(matrix[cols*i + j]);
            Console.Write(" ");
        }
        Console.WriteLine();
    }
}

// Метод, определяющий, можно ли использовать данное число для заданного места массива
bool CanUse(int num, int pos) {
    bool result = true;
    int row = pos/cols;
    int col = pos%cols;
    // Проверяем, есть ли такая переменная в текущей строке
    for (int i = 0; i < cols; i++) {
        int n = row*cols+i;
        result = result & (num != matrix[n]);
    }
    // Проверяем, есть ли такая переменная в текущем столбце
    // Проверку выполняем, если по результатам предыдущей проверки результат еще true
    if (result) {
        for (int i = 0; i < rows; i++) {
            int n = i*cols + row;
            result = result & (num != matrix[n]);
        }
    }
    // Проверяем, есть ли такая переменная в текущем малом квадрате
    // Проверку выполняем, если по результатам предыдущей проверки результат еще true
    if (result) {
        // Получаем номер малого квадрата по горизонтали
        int squareCol = col/3;
        // Получаем номер малого квадрата по вертикали
        int squareRow = row/3;
        // Перебираем элементы малого квадрата
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                int n = squareRow * 3 * cols + squareCol * 3 + i*cols + j;
                result = result & (num != matrix[n]);
            };
        }
    }
    return result;
}

// Метод, вставляющий число в позицию pos
// Если число вставить нельзя, проверяет следующее число
// Если число не более maximum, то вставляет его, иначе вставляет 0
// В любом случае передает полученное число как результат
int Insert(int pos, int maximum) {
    // Увеличиваем текущее значение ячейки на один
    int currentNum = matrix[pos] + 1;
    // И продолжаем увеличивать до тех пор, пока значение ячейки не станет подходящим
    while (!CanUse(currentNum,pos)) {
        currentNum++;
    }
    // Если полученное значение больше maximum, то в данную ячейку ничего ставить нельзя
    // Записываем 0
    if (currentNum > maximum) {
        currentNum = 0;
    }
    matrix[pos] = currentNum;
    return currentNum;
} 

// Метод, определяющий, можно и менять данную ячейку 
bool CanChange(int n, int[] except) {
    bool isExeption = false;
    for (int i = 0; i < except.Length; i++) {
        if (n == except[i]) {
            isExeption = true;       
            break;
        }
    }
    return (!isExeption);
}
