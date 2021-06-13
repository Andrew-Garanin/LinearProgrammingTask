using Mehroz;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace LinearProgrammingTask
{
    class FractionGausMethod
    { 
        public uint RowCount;
        public uint ColumCount;
        public Fraction[][] Matrix { get; set; }
        public Fraction[] RightPart { get; set; }
        public Fraction[] Answer { get; set; }

        public FractionGausMethod(uint Row, uint Colum)
        {
            RowCount = Row;
            ColumCount = Colum;
            RightPart = new Fraction[Row];
            Answer = new Fraction[Row];
            Matrix = new Fraction[Row][];

            for (int i = 0; i < Row; i++)
                Matrix[i] = new Fraction[Colum];
        }

        public int SolveMatrix(List<int> baseVars)
        {
            List<int> bv = new List<int>(baseVars);
            List<Point> swaps = new List<Point>();// Каждый элемент - пара столбцов, которые нужно поменять местами
            bool doNeedSwap = false;

            for (int i = 0; i < bv.Count; i++)// Проверка, нужно ли менять слобцы местами
            {
                if (bv[i] - 1 == i)
                    continue;
                else
                {
                    doNeedSwap = true;
                    break;
                }
            }

            if (doNeedSwap)
            {
                int tmp;
                for (int j = 0; j < ColumCount; j++)
                {
                    if (bv.Contains(j + 1))
                    {
                        tmp = j;
                        for (int i = 0; i < j; i++)
                        {
                            if (!bv.Contains(i + 1))
                            {
                                bv[bv.IndexOf(j + 1)] = i + 1;
                                swaps.Add(new Point(tmp, i));// Запоминаем столбцы, которые будем менять
                                for (int k = 0; k < RowCount; k++)//Меняем столбцы местами
                                {
                                    Fraction tempValue = Matrix[k][tmp];
                                    Matrix[k][tmp] = Matrix[k][i];
                                    Matrix[k][i] = tempValue;
                                }
                                break;
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < RowCount - 1; i++)// После этого цикла имеем матрицу у которой снизу диагонали нули
            {
                SortRows(i);
                for (int j = i + 1; j < RowCount; j++)
                {
                    if (Matrix[i][i] != 0) // если главный элемент не 0, то производим вычисления
                    {
                        Fraction MultipleCoeff = Matrix[j][i] / Matrix[i][i];
                        for (int k = i; k < ColumCount; k++)
                        {
                            Matrix[j][k] -= Matrix[i][k] * MultipleCoeff;
                        }
                        RightPart[j] -= RightPart[i] * MultipleCoeff;
                    }
                    // Если главный элемент 0, то ничего не делаем
                }
            }

            if (Matrix[RowCount - 1][ColumCount - 1] == 0 && RightPart[RowCount - 1] != 0)
            {
                // Нет решений
                return 1;
            }
            if (Matrix[RowCount - 1][ColumCount - 1] == 0 && RightPart[RowCount - 1] == 0)
            {
                // Бесконечно-много решений
                return 1;
            }

            for (int i = 0; i < RowCount; i++)// Делаем на главной диагонали единицы
            {
                if (Matrix[i][i] == 0)
                    continue;
                Fraction ReversedElement = 1 / Matrix[i][i];
                for (int j = i; j < ColumCount; j++)
                {
                    Matrix[i][j] *= ReversedElement;
                }
                RightPart[i] *= ReversedElement;
            }

            for (int i = (int)RowCount - 2, h = 0; i >= 0; i--, h++)// После этого цикла имеем единичную матрицу
            {
                for (int k = i, m = 1; k >= 0; k--, m++)
                {
                    Fraction MultipleCoeff = Matrix[k][(int)(bv.Count - 1) - h];
                    for (int j = (int)(bv.Count - 1) - h; j < ColumCount; j++)
                        Matrix[k][j] -= MultipleCoeff * Matrix[k + m][j];
                    RightPart[k] -= MultipleCoeff * RightPart[k + m];
                }
            }

            for (int i = swaps.Count - 1; i >= 0; i--)// Ставим столбцы на место в обратном порядке
            {
                for (int k = 0; k < RowCount; k++)
                {
                    Fraction tempValue = Matrix[k][swaps[i].X];
                    Matrix[k][swaps[i].X] = Matrix[k][swaps[i].Y];
                    Matrix[k][swaps[i].Y] = tempValue;
                }
            }
            return 0;
        }

        private void SortRows(int SortIndex)// Метод, который поднимает строку с наибольшим числом выше.
        {
            Fraction MaxElement = Matrix[SortIndex][SortIndex];
            int MaxElementIndex = SortIndex;
            for (int i = SortIndex + 1; i < RowCount; i++)
            {
                if (Fraction.Abs(Matrix[i][SortIndex]) > MaxElement)
                {
                    MaxElement = Fraction.Abs(Matrix[i][SortIndex]);
                    MaxElementIndex = i;
                }
            }

            //теперь найден максимальный элемент ставим его на верхнее место
            if (MaxElementIndex > SortIndex)//если это не первый элемент
            {
                Fraction Temp;

                // Меняем местами элементы из правой части матрицы
                Temp = RightPart[MaxElementIndex];
                RightPart[MaxElementIndex] = RightPart[SortIndex];
                RightPart[SortIndex] = Temp;

                for (int i = 0; i < ColumCount; i++)// Меняем местами элементы строчки в которой самый большой элемент
                {                                   // и строчки, номер которой был передан как аргумент.
                    Temp = Matrix[MaxElementIndex][i];
                    Matrix[MaxElementIndex][i] = Matrix[SortIndex][i];
                    Matrix[SortIndex][i] = Temp;
                }
            }
        }

        public int Rank()
        {
            int rank = 0;
            for (int i = 0; i < RowCount - 1; i++)// После этого цикла имеем матрицу у которой снизу диагонали нули
            {
                SortRows(i);
                for (int j = i + 1; j < RowCount; j++)
                {
                    if (Matrix[i][i] != 0) //если главный элемент не 0, то производим вычисления
                    {
                        Fraction MultipleCoeff = Matrix[j][i] / Matrix[i][i];
                        for (int k = i; k < ColumCount; k++)
                        {
                            Matrix[j][k] -= Matrix[i][k] * MultipleCoeff;
                        }
                    }
                    // Если главный элемент 0, то ничего не делаем
                }
            }

            for (int i = 0; i < RowCount; i++)
            {
                int count = 0;
                for (int j = 0; j < ColumCount; j++)
                {
                    if (Matrix[i][j] == 0)
                        count++;
                }
                if (count < ColumCount)
                    rank++;
            }
            return rank;
        }

        public override String ToString()
        {
            String S = "";
            for (int i = 0; i < RowCount; i++)
            {
                S += "\r\n";
                for (int j = 0; j < ColumCount; j++)
                {
                    S += Matrix[i][j].ToString() + "\t";
                }

                S += "\t" + RightPart[i].ToString();
            }
            return S;
        }
    }
}
