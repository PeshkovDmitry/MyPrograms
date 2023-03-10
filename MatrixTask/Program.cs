﻿// Программа, которая заполняет матрицу заданного размера целыми числами таким образом,
// что числа не повторяются ни в строке, ни в столбце, ни в малом квадрате 3х3
// Количество строк в матрице
int rows = 9;
// Количество столбцов в матрице
int cols = 9;
// Максимальное допустимое число в ячейке
int max = 9;
// Создаем матрицу
int[] matrix = new int[rows*cols];
// Задаем начальное значение
matrix[0] = 7;
matrix[7] = 1;
matrix[10] = 1;
matrix[11] = 5;
matrix[12] = 6;
matrix[15] = 9;
matrix[18] = 3;
matrix[22] = 5;

matrix[27] = 1;
matrix[37] = 2;
matrix[38] = 4;
matrix[40] = 7;
matrix[43] = 9;
matrix[50] = 8;
matrix[51] = 6;

matrix[55] = 5;
matrix[56] = 9;
matrix[58] = 4;
matrix[61] = 2;
matrix[65] = 3;
matrix[75] = 7;
matrix[80] = 4;

// Формируем массив с номерами неизменных ячеек
int[] exeptions = {0,7,10,11,12,15,18,22,27,37,38,40,43,50,51,55,56,58,61,65,75,80};
// Текущее положение в массиве
int pos = 0;
// Направление работы. Вперед - true
bool direction = true;
// Номер итерации - за итерацию считается или вставка подходящего числа, или 0, если в данную ячейку ничего не подходит
int count = 0;
// Пока текущий индекс не превысил размер массива, производим подбор
while (pos < rows*cols) {
    // Проверяем, можно ли менять данную ячейку
    if (CanChange(pos,exeptions)) {
        // Данную ячейку менять можно
        // Для текущей ячейки подбираем ближайшее наименьшее подходящее значение
        Insert(pos, max);
        count++;
        // Проверяем, какое число было вставлено
        // Если не ноль, то можно двигаться далее
        // Если ноль, то двигаемся назад
        // В начальной позиции всегда направление - вперед
        direction = !(matrix[pos] == 0);    
    }
    // Выводим промежуточный вариант
    // PrintResult();
    // В зависимости от принятого направления увеличиваем или уменьшаем текущую позицию
    if (direction) {
        pos++;
    } 
    else {
        pos--;
    }
}
// Выводим то, что получилось
PrintResult();

   
// Метод для вывода результата на экран.
void PrintResult() {
    Console.Clear();
    Console.Write("Всего итераций - ");
    Console.WriteLine(count);
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
            int n = i*cols + col;
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