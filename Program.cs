using System;

namespace lab4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool isArrayFormed = false; //флаг для проверки, создан ли массив

            int[] array = new int[0];
            int answer;

            PrintMenu();
            //цикл, пока пользователь не выйдет из программы
            do
            {
                bool isAnswerCorrect = false; //флаг для проверки правильности ввода
                //ввод пункта меню
                do
                {
                    answer = ReadNumber("Введите номер операции: ", "Ошибка при вводе пункта меню. Пожалуйста, попробуйте еще раз");
                    if (answer > 0 && answer < 11)
                        isAnswerCorrect = true;
                    else
                        Console.WriteLine("\nОшибка при вводе пункта меню. Пожалуйста, попробуйте еще раз\n");
                } while (!isAnswerCorrect);

                PrintMenu();
                //организация меню
                switch (answer)
                {
                    case 1:
                        CreateArray(ref array, ref isArrayFormed);
                        break;
                    case 2:
                        PrintArray(array, isArrayFormed);
                        break;
                    case 3:
                        RemoveOddPositions(ref array, isArrayFormed);
                        break;
                    case 4:
                        AddElement(ref array, isArrayFormed);
                        break;
                    case 5:
                        ReverseArray(array, isArrayFormed);
                        break;
                    case 6:
                        FindElement(array, isArrayFormed);
                        break;
                    case 7:
                        SortArray(array, isArrayFormed);
                        break;
                    case 8:
                        DoShakerSort(array, isArrayFormed);
                        break;
                    case 9:
                        DoBinarySearch(array, isArrayFormed);
                        break;
                    case 10:
                        Console.WriteLine("Нажмите любую кнопку для выхода из программы...");
                        break;
                }
            } while (answer != 10);
        }

        //формирование массива
        #region CreateArray
        static void CreateArray(ref int[] array, ref bool isArrayFormed)
        {
            Random rnd = new Random();
            //инициализация длины массива и флага для проверки правильности ее ввода
            int lengthArray;
            bool isLenCorrect = false;
            //ввод длины массива
            do
            {
                lengthArray = ReadNumber("Введите количество элементов создаваемого массива: ",
                              "Ошибка при вводе длины массива. Пожалуйста, введите целое число от 1 до 100");
                if (lengthArray > 0 && lengthArray < 101)
                    isLenCorrect = true;
                else
                    Console.WriteLine("\nОшибка при вводе длины массива. Пожалуйста, введите целое число от 1 до 100\n");
            } while (!isLenCorrect);
            //формирование массива
            array = new int[lengthArray];

            Console.WriteLine("\n1. Заполнить массив генератором случайных чисел");
            Console.WriteLine("2. Заполнить массив ручным вводом\n");

            int readAnswer;
            bool isReadAnswer = false;
            //ввод пунка меню
            do
            {
                readAnswer = ReadNumber("Введите номер операции: ", "Ошибка при вводе пункта меню. Пожалуйста, попробуйте еще раз");
                if (readAnswer > 0 && readAnswer < 3) //
                    isReadAnswer = true;
                else
                    Console.WriteLine("\nОшибка при вводе пункта меню. Пожалуйста, попробуйте еще раз\n");
            } while (!isReadAnswer);

            //организация меню
            switch (readAnswer)
            {
                case 1:
                    for (int i = 0; i < lengthArray; i++)
                        array[i] = rnd.Next(-200, 200);
                    break;
                case 2:
                    Console.WriteLine();
                    for (int i = 0; i < lengthArray; i++)
                    {
                        try
                        {
                            Console.Write($"Введите {i + 1}-й элемент массива: ");
                            int number = int.Parse(Console.ReadLine()); //ручной ввод элементов массива
                            array[i] = number;
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("\nОшибка при вводе элемента массива. Пожалуйста, введите целое число\n");
                            i--;
                        }
                        catch (OverflowException)
                        {
                            Console.WriteLine("\nВведенное число выходит за пределы типа int. Пожалуйста, попробуйте еще раз\n");
                            i--;
                        }
                    }
                    break;
            }
            PrintMenu();
            Console.WriteLine("Массив успешно сформирован\n");
            isArrayFormed = true;
        }
        #endregion

        //печать массива
        #region PrintArray
        static void PrintArray(int[] array, bool isArrayFormed)
        {
            PrintMenu();
            if (isArrayFormed && array.Length != 0) //массив сформриован и не пустой
            {
                Console.Write("Массив: ");
                for (int i = 0; i < array.Length; i++) //цикл для печати массива
                    Console.Write($"{array[i]} ");
                Console.WriteLine("\n");
            }
            else
            {
                if (!isArrayFormed)
                    Console.WriteLine("Для выполнения данной операции сначала сформируйте массив\n");
                else
                    Console.WriteLine("Массив пустой\n");
            }
        }
        #endregion

        //удаление элементов с нечетными номерами
        #region RemoveOddPositions
        static void RemoveOddPositions(ref int[] array, bool isArrayFormed)
        {
            if (isArrayFormed && array.Length != 0)
            {
                int[] extraArray = new int[array.Length / 2]; //создание вспомогательного массива
                for (int i = 0, j = 1; i < extraArray.Length; i++, j += 2) //цикл по длине нового массива
                    extraArray[i] = array[j]; //добавление в новый массив тех элементов исходного массива, которые стоят на четных позициях
                array = extraArray;
                PrintArray(array, isArrayFormed);
                Console.WriteLine("Элементы с нечетными номерами успешно удалены\n");
            }
            else
            {
                if (!isArrayFormed) //если массив не был сформирован
                    Console.WriteLine("Для выполнения данной операции сначала сформируйте массив\n");
                else //если массив пустой
                    Console.WriteLine("Массив пустой\n");
            }
        }
        #endregion

        //добавление элемента с задаваемым номером
        #region AddElement
        static void AddElement(ref int[] array, bool isArrayFormed)
        {
            Random rnd = new Random();

            if (!isArrayFormed) //если массив не был сформирован
                Console.WriteLine("Для выполнения данной операции сначала сформируйте массив\n");

            else
            {
                int numberElement; //инициализация номера элемента
                bool isNumberCorrect = false;
                if (array.Length != 0)
                {
                    PrintArray(array, isArrayFormed);
                    do
                    {
                        //ввод номера элемента
                        numberElement = ReadNumber($"Введите номер добавляемого элемента от 1 до {array.Length + 1}: ",
                            $"Ошибка при вводе номера элемента. Пожалуйста, введите номер от 1 до {array.Length + 1}");
                        if (numberElement > 0 && numberElement <= array.Length + 1) //если номер введен корректно
                            isNumberCorrect = true;
                        else //если номер не введен корректно
                            Console.WriteLine($"\nОшибка при вводе номера элемента. Пожалуйста, введите номер от 1 до {array.Length + 1}\n");
                    } while (!isNumberCorrect);
                }

                else //если массив пустой
                {
                    Console.WriteLine("Так как массив пустой, будет добавлен элемент с номером 1");
                    numberElement = 1;
                }

                Console.WriteLine("\n1. Выбрать добавляемый элемент генератором случайных чисел");
                Console.WriteLine("2. Ввести добавляемый элемент вручную\n");

                int readAnswer;
                bool isReadAnswer = false;
                //ввод пункта меню
                do
                {
                    readAnswer = ReadNumber("Введите номер операции: ", "Ошибка при вводе пункта меню. Пожалуйста, попробуйте еще раз");
                    if (readAnswer > 0 && readAnswer < 3)
                        isReadAnswer = true;
                    else
                        Console.WriteLine("\nОшибка при вводе пункта меню. Пожалуйста, попробуйте еще раз\n");
                } while (!isReadAnswer);

                int number = 0;
                //организация меню
                switch (readAnswer)
                {
                    case 1:
                        number = rnd.Next(-200, 200);
                        break;
                    case 2:
                        Console.WriteLine();
                        number = ReadNumber("Введите добавляемый элемент: ", "Ошибка при вводе добавляемого элемента. Пожалуйста, введите целое число");
                        break;
                }

                int[] extraArray = new int[array.Length + 1]; //создание вспомогательного массива
                for (int i = 0, j = 0; i < extraArray.Length; i++, j++) //цикл по длине нового массива
                {
                    if (i + 1 == numberElement)
                    {
                        extraArray[i] = number; //добавление нового элемента в массив
                        j--;
                    }
                    else
                        extraArray[i] = array[j]; //заполнение нового массива исходными элементами
                }
                array = extraArray;
                PrintMenu();
                PrintArray(array, isArrayFormed);
                Console.WriteLine($"Был успешно добавлен элемент {number} на позицию с номером {numberElement}\n");
            }
        }
        #endregion

        //перевернуть массив
        #region ReverseArray
        static void ReverseArray(int[] array, bool isArrayFormed)
        {
            if (isArrayFormed && array.Length != 0) //массив сформирован и не пустой
            {
                int leftBorder = 0, rightBorder = array.Length - 1;
                //меняем элементы массива местами, идя по нему с обоих концов
                while (leftBorder < rightBorder)
                    //смена элементов местами
                    (array[leftBorder], array[rightBorder]) = (array[rightBorder--], array[leftBorder++]);
                PrintArray(array, isArrayFormed);
                Console.WriteLine("Массив успешно перевернут\n");
            }
            else
            {
                if (!isArrayFormed)
                    Console.WriteLine("Для выполнения данной операции сначала сформируйте массив\n");

                else
                    Console.WriteLine("Массив пустой\n");
            }
        }
        #endregion

        //поиск среднего арифметического среди элементов массива
        #region FindElement
        static void FindElement(int[] array, bool isArrayFormed)
        {
            if (isArrayFormed && array.Length != 0) //массив сформриован и не пустой
            {
                //вычисление среднего арифметического всех чисел массива
                double sumElements = 0;
                for (int i = 0; i < array.Length; i++)
                    sumElements += array[i];
                double averageElements = Math.Round(sumElements / array.Length, MidpointRounding.AwayFromZero);
                //поиск в массиве числа, равному среднему арифметическому всех его чисел; нахождение кол-ва сравнений
                int positionElement = -1, numberComparisons = 0;
                for (int i = 0; i < array.Length; i++)
                {
                    numberComparisons++;
                    if (array[i] == averageElements)
                    {
                        positionElement = i + 1;
                        break;
                    }
                }
                PrintArray(array, isArrayFormed);
                if (positionElement < 0) //если число не было найдено
                    Console.WriteLine($"В массиве не найдено среднее арифметическое всех его чисел, равное {averageElements}\n");
                else
                {
                    Console.WriteLine($"Среднее арифметическое всех чисел массива, равное {averageElements}, находится на позиции {positionElement}");
                    Console.WriteLine($"Количество потребовавшихся для поиска сравнений: {numberComparisons}\n");
                }
            }
            else
            {
                if (!isArrayFormed)
                    Console.WriteLine("Для выполнения данной операции сначала сформируйте массив\n");

                else
                    Console.WriteLine("Массив пустой\n");
            }
        }
        #endregion

        //сортировка простым выбором
        #region SortArray
        static void SortArray(int[] array, bool isArrayFormed)
        {
            if (isArrayFormed && array.Length != 0) //массив сформриован и не пустой
            {
                for (int i = 0, minElementIndex = 0; i < array.Length - 1; i++)
                {
                    //устанавливаем начальное значение минимального индекса
                    minElementIndex = i;
                    //поиск индекса элемента с минимальным значением
                    for (int j = i + 1; j < array.Length; j++)
                    {
                        if (array[j] < array[minElementIndex]) //если нашелся элемент меньше текущего
                            minElementIndex = j;
                    }
                    //смена элементов местами
                    (array[i], array[minElementIndex]) = (array[minElementIndex], array[i]);
                }
                PrintArray(array, isArrayFormed);
                Console.WriteLine("Массив успешно отсортирован\n");
            }
            else
            {
                if (!isArrayFormed)
                    Console.WriteLine("Для выполнения данной операции сначала сформируйте массив\n");

                else
                    Console.WriteLine("Массив пустой\n");
            }
        }
        #endregion

        //бинарный поиск
        #region DoBinarySearch
        static void DoBinarySearch(int[] array, bool isArrayFormed)
        {
            if (isArrayFormed && array.Length != 0 && CheckSort(array)) //массив сформриован, не пустой и отсортирован
            {
                //инициализация номера элемента, счетчика сравнений, границ массива, индекса середины массива
                int positionElement = -1, numberComparisons = 0, leftBorder = 0, rightBorder = array.Length - 1, indexMiddle = 0;

                PrintArray(array, isArrayFormed);
                int unknownNumber = ReadNumber("Введите искомый элемент: ", "Ошибка при вводе элемента. Пожалуйста, введите целое число");
                //выполняется цикл, пока левая граница не больше правой
                while (leftBorder <= rightBorder)
                {
                    indexMiddle = (rightBorder + leftBorder) / 2; //вычисление индекса середины области поиска (округляется в меньшую сторону)
                    if (array[indexMiddle] == unknownNumber) //если искомый элемент найден, выходим из цикла
                    {
                        positionElement = indexMiddle + 1;
                        numberComparisons++;
                        break;
                    }
                    //смещение левой границы вправо, так как искомое число больше полученного
                    if (array[indexMiddle] < unknownNumber)
                    {
                        leftBorder = indexMiddle + 1;
                        numberComparisons++;
                    }
                    //смещение правой границы влево, так как искомое число меньше полученного
                    else
                    {
                        rightBorder = indexMiddle - 1;
                        numberComparisons++;
                    }
                }

                if (positionElement < 0) //если число не было найдено
                    Console.WriteLine($"\nВ массиве не найден элемент, равный {unknownNumber}\n");
                else
                {
                    PrintMenu();
                    PrintArray(array, isArrayFormed);
                    Console.WriteLine($"Искомое число, равное {unknownNumber}, находится на позиции {positionElement}");
                    Console.WriteLine($"Количество потребовавшихся для поиска сравнений: {numberComparisons}\n");
                }
            }
            else
            {
                if (!isArrayFormed)
                    Console.WriteLine("Для выполнения данной операции сначала сформируйте массив\n");

                else
                {
                    if (array.Length == 0)
                        Console.WriteLine("Массив пустой\n");
                    else
                    {
                        PrintArray(array, isArrayFormed);
                        Console.WriteLine("Для выполнения данной операции сначала отсортируйте массив\n");
                    }
                }
            }

        }
        #endregion

        //шейкер-сортировка
        #region DoShakerSort
        static void DoShakerSort(int[] array, bool isArrayFormed)
        {
            if (isArrayFormed && array.Length != 0) //массив сформриован и не пустой
            {
                int leftBorder = 0, rightBorder = array.Length - 1; ; //инициализация левой и правой границ массива
                bool isSwapped; //флаг для проверки, был ли обмен элементов местами
                do
                {
                    isSwapped = false;

                    //прямой проход по массиву
                    for (int i = leftBorder; i < rightBorder; i++)
                    {
                        if (array[i] > array[i + 1])
                        {
                            //обмен элементов местами
                            (array[i], array[i + 1]) = (array[i + 1], array[i]);
                            isSwapped = true;
                        }
                    }
                    rightBorder--; //уменьшение правой границы

                    //обратный проход по массиву
                    for (int i = rightBorder; i > leftBorder; i--)
                    {
                        if (array[i] < array[i - 1])
                        {
                            //обмен элементов местами
                            (array[i], array[i - 1]) = (array[i - 1], array[i]);
                            isSwapped = true;
                        }
                    }
                    leftBorder++; //увеличение левой границы
                }
                while (isSwapped);  //цикл, пока хотя бы в одном из циклов произошел обмен элементов местами
                PrintArray(array, isArrayFormed);
                Console.WriteLine("Массив успешно отсортирован\n");
            }
            else
            {
                if (!isArrayFormed)
                    Console.WriteLine("Для выполнения данной операции сначала сформируйте массив\n");

                else
                    Console.WriteLine("Массив пустой\n");
            }
        }
        #endregion

        //проверка сортировки массива
        #region CheckSort
        static bool CheckSort(int[] array)
        {
            bool isSorted = true;
            //если массив не пустой
            if (array.Length != 1)
            {
                for (int i = 0; i < array.Length - 1; i++) //цикл по элементам массива
                {
                    if (array[i] > array[i + 1]) //нарушение признака сортировки
                    {
                        isSorted = false;
                        break;
                    }
                }
            }
            return isSorted;
        }
        #endregion

        //печать меню
        #region PrintMenu
        static void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine(" 1. Сформировать массив");
            Console.WriteLine(" 2. Распечатать массив");
            Console.WriteLine(" 3. Удалить элементы с нечетными номерами");
            Console.WriteLine(" 4. Добавить в массив элемент с заданным номером");
            Console.WriteLine(" 5. Перевернуть массив");
            Console.WriteLine(" 6. Выполнить поиск элемента, равного среднему арифметическому всех чисел массива");
            Console.WriteLine(" 7. Отсортировать массив методом простого выбора");
            Console.WriteLine(" 8. Отсортировать массив методом перемешивания");
            Console.WriteLine(" 9. Выполнить бинарный поиск заданного элемента");
            Console.WriteLine("10. Выход\n");
        }
        #endregion

        //ввод числа
        #region ReadNumber
        static int ReadNumber(string messageFirst, string messageSecond)
        {
            int num = 0;
            bool isConvert = false;
            do
            {
                try
                {
                    Console.Write(messageFirst);
                    num = int.Parse(Console.ReadLine());
                    isConvert = true;
                }
                catch (FormatException) //ошибка неправильного формата входных данных
                {
                    Console.WriteLine("\n" + messageSecond + "\n");
                }
                catch (OverflowException) //ошибка переполнения типа int
                {
                    Console.WriteLine("\nВведенное число выходит за пределы типа int. Пожалуйста, попробуйте еще раз\n");
                }
            } while (!isConvert);
            return num;
        }
        #endregion
    }
}