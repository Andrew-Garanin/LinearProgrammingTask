using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearProgrammingTask
{
    class StepInformation
    {
        public int startRow;// Строка, с которой начинается таблица
        public List<Point> supEl = new List<Point>();// Список опорных элементов
        public int iter;// Номер итерации
        public Point mainSupElement;
        public int countTables;// Номер таблицы в симплекс методе(не для метода искусств. базиса)
        public List<String> tempVars = new List<String>();// Искуственно введенные переменные(для метода искусств. базиса)
        public StepInformation(int startRow, List<Point> supEl, int iter, Point mainSupElement, int countTables, List<String> tempVars)
        {
            this.startRow = startRow;
            this.supEl = supEl;
            this.iter = iter;
            this.mainSupElement = mainSupElement;
            this.countTables = countTables;
            this.tempVars = tempVars;
        }
    }
}
