using System;
using System.Drawing;
using System.Windows.Forms;
using Mehroz;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace LinearProgrammingTask
{
    public partial class Form1 : Form
    {
        public int iter = 1;
        public int startRow = 0;
        List<String> tempVars = new List<String>();// Искуственно введенные переменные
        List<Point> supEl = new List<Point>();
        int countTables = 1;
        public Fraction[,] lines = new Fraction[20, 20];// Массив для ограничений
        public Form1()
        {
            InitializeComponent();
            // Параметры для таблицы с ограничениями
            linesGrid.AllowUserToAddRows = false;
            linesGrid.ColumnCount = (int)variableCount.Value+1;
            linesGrid.RowCount = (int)linesCount.Value;
            foreach (DataGridViewColumn column in linesGrid.Columns)
            {
                column.HeaderText = String.Concat("a",
                    (column.Index+1).ToString());
            }
            linesGrid.Columns[(int)variableCount.Value].HeaderText = "b";
            foreach (DataGridViewRow row in linesGrid.Rows)
            {
                row.HeaderCell.Value = String.Concat("f",
                    (row.Index + 1).ToString());
            }

            // Параметры для таблицы с целевой функцией
            targetFuncGrid.AllowUserToAddRows = false;
            targetFuncGrid.ColumnCount = (int)variableCount.Value+1;
            targetFuncGrid.RowCount = 1;
            foreach (DataGridViewColumn column in targetFuncGrid.Columns)
            {
                column.HeaderText = String.Concat("c",
                    (column.Index + 1).ToString());
            }
            targetFuncGrid.Columns[(int)variableCount.Value].HeaderText = "c";
            targetFuncGrid.Rows[0].HeaderCell.Value = "f(x)";
        }

        private void variablesCount_ValueChanged(object sender, EventArgs e)
        {
            this.linesGrid.ColumnCount = (int)this.variableCount.Value+1;
            for(int i =0;i< this.linesGrid.ColumnCount-1;i++)
                linesGrid.Columns[i].HeaderText= String.Concat("a", (i+1).ToString());
            linesGrid.Columns[(int)variableCount.Value].HeaderText = "b";
            this.targetFuncGrid.ColumnCount = (int)this.variableCount.Value+1;
            for (int i = 0; i < this.targetFuncGrid.ColumnCount-1; i++)
                targetFuncGrid.Columns[i].HeaderText = String.Concat("c", (i+1).ToString());
            targetFuncGrid.Columns[(int)variableCount.Value].HeaderText = "c";
        }

        private void linesCount_ValueChanged(object sender, EventArgs e)
        {
            this.linesGrid.RowCount = (int)this.linesCount.Value;
            for (int i = 0; i < this.linesGrid.RowCount; i++)
                linesGrid.Rows[i].HeaderCell.Value = String.Concat("f", (i+1).ToString());
        }

        private void graphPictureControl_Paint(object sender, PaintEventArgs e)
        {   
            int centrX =graphPictureControl.Width/2;
            int centrY= graphPictureControl.Height/2;
            e.Graphics.TranslateTransform(centrX, centrY);
            e.Graphics.ScaleTransform(1, 1);
            
            Pen GreenPen = new Pen(Color.Green);
            //Прорисовка осей
            //Ось X
            Point KX1, KX2;
            KX1 = new Point(0, 600);
            KX2 = new Point(0, -600);
            e.Graphics.DrawLine(GreenPen, KX1, KX2);
            //Ось Y
            Point KY1, KY2;
            KY1 = new Point(600 , 0);
            KY2 = new Point(-600, 0);

            e.Graphics.DrawLine(GreenPen, KY1, KY2);

            var prev = new PointF(0, (float)Math.Sin(0));

            for (int x = 0; x < 50; x++)
            {
                var y = Math.Sin(x);
                var curr = new PointF(x, (float)y);
                e.Graphics.DrawLine(Pens.Black, prev, curr);
                prev = curr;
            }
        }// Для графического метода задчи ЛП

        private void developmentMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void optimizeTask_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void fractionView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void baseView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_OK_Click(object sender, EventArgs e)// Считывание таблицы в массив и собственно решение задачи ЛП
        {
            //MessageBox.Show(Fraction.ToFraction("0.113333333333333333").ToString());
            //MessageBox.Show(Fraction.ToFraction("2").ToString());
            //MessageBox.Show(Fraction.ToFraction("-0.333333").ToString());
            //MessageBox.Show(Fraction.ToFraction("3").ToString());
            //MessageBox.Show(Fraction.ToFraction("1/3").ToString());
            //MessageBox.Show(Fraction.ToFraction("-3").ToString());
            //MessageBox.Show(Fraction.ToFraction("-3").ToString());
            for (int i = 0; i < (int)this.variableCount.Value+1; i++)
            for (int j = 0; j < (int)this.linesCount.Value; j++)
                lines[j, i] = Fraction.ToFraction(linesGrid[i, j].Value.ToString());

            // тут должна быть проверка на ранг матрицы

            // Предварительная подготовка(все bi должны быть >=0)
            for(int i=0;i<(int)this.linesCount.Value;i++)
            {
                if(lines[i, (int)this.variableCount.Value]<0)
                {
                    for(int j=0;j< (int)this.variableCount.Value+1;j++)
                    {
                        lines[i, j] *= -1;
                    }
                }
            }

            // Метод искусственного базиса
            this.tabControl.SelectedTab = this.tabPage2;// Переключение на вкладку метода иск. базиса

            this.artificialBaseMethodGrid.ColumnCount = 20;
            this.artificialBaseMethodGrid.RowCount = 100;
            this.artificialBaseMethodGrid.CurrentCell = null;// Чтоб в самом начале не была выбрана никакая ячейка
            foreach (DataGridViewColumn column in this.artificialBaseMethodGrid.Columns)// Отключение сортировки по столбцам
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            // Установка ширины столбцов
            foreach (DataGridViewColumn el in this.artificialBaseMethodGrid.Columns)
                el.Width = 70;
            this.artificialBaseMethodGrid.TopLeftHeaderCell.Value = "x(0)";

            // Заполение заголовков строк и столбцов
            for (int i = 0; i < (int)this.variableCount.Value; i++)
                this.artificialBaseMethodGrid.Columns[i].HeaderText = String.Concat("x", (i + 1).ToString());
            for (int i = 0; i < (int)this.linesCount.Value; i++)
            {
                this.artificialBaseMethodGrid.Rows[i].HeaderCell.Value = String.Concat("x", (i + 1 + (int)this.variableCount.Value).ToString());
                tempVars.Add(this.artificialBaseMethodGrid.Rows[i].HeaderCell.Value.ToString());
            }
            // Заполнение матрицы коэфициентов
            for (int i = 0; i < (int)this.linesCount.Value; i++)
                for (int j = 0; j < (int)this.variableCount.Value + 1; j++)
                    this.artificialBaseMethodGrid[j, i].Value = lines[i, j].ToString();

            // Расчет коэф-ов при "искуственной" целевой функции
            for (int i = 0; i < (int)this.variableCount.Value + 1; i++)
            {
                Fraction tmp = 0;
                for (int j = 0; j < (int)this.linesCount.Value; j++)
                {

                    tmp += lines[j, i];

                }
                tmp *= -1;
                this.artificialBaseMethodGrid[i, (int)this.linesCount.Value].Value = tmp.ToString();
            }
            
            supEl = suportElements(this.artificialBaseMethodGrid);// Нашли все опорные элементы
            SetColorsOnSupElements(supEl, this.artificialBaseMethodGrid);// Раскрасили все опорные элементы

            if (this.fractionView.Text == "Десятичные")
            {
                for (int i = 0; i < (int)this.variableCount.Value+1; i++)
                    for (int j = 0; j < (int)this.linesCount.Value+1; j++)
                        this.artificialBaseMethodGrid[i, j].Value = Fraction.ToFraction(this.artificialBaseMethodGrid[i, j].Value.ToString()).ToDouble();
            }

            if (this.solutionMode.Text == "Пошаговый")
                return;
            else
            {
                // Получение таблицы в которой останутся только переменные из начальной задачи(в цикле)
                while (!IsEndArtificialBaseMethod())
                {
                    Point mainSupElement = mainSupEl(this.artificialBaseMethodGrid);// Нахождение и закрашивание главного порного эл-та

                    startRow += (int)this.linesCount.Value + 3;// Строка, с которой будет начинаться каждая новая таблица
                    this.artificialBaseMethodGrid.Rows[startRow-1].HeaderCell.Value = String.Concat("x(", iter, ")");

                    SimplexStepArtificial(mainSupElement, this.artificialBaseMethodGrid);
                    iter++;
                    supEl = suportElements( this.artificialBaseMethodGrid);// Нашли все опорные элементы

                    SetColorsOnSupElements(supEl, this.artificialBaseMethodGrid);

                    if (this.fractionView.Text == "Десятичные")
                    {
                        for (int i = 0; i < (int)variableCount.Value + 2 - iter; i++)
                            for (int j = startRow; j < startRow+(int)this.linesCount.Value+1; j++)
                                this.artificialBaseMethodGrid[i, j].Value = Fraction.ToFraction(this.artificialBaseMethodGrid[i, j].Value.ToString()).ToDouble();
                    }
                }

                bool isIdleStep = false;
                for (int i = 0; i < (int)variableCount.Value + 1 - iter; i++)
                    if (Fraction.ToFraction(this.artificialBaseMethodGrid[i, startRow + (int)this.linesCount.Value].Value.ToString()) != 0)
                    {
                        isIdleStep = true;
                        break;
                    }
                // холостой шаг(если нужен)
                if (isIdleStep)
                    IdleStep(supEl);

                if (this.fractionView.Text == "Десятичные")
                {
                    for (int i = 0; i < (int)variableCount.Value + 2 - iter; i++)
                        for (int j = startRow; j < startRow + (int)this.linesCount.Value+1; j++)
                            this.artificialBaseMethodGrid[i, j].Value = Fraction.ToFraction(this.artificialBaseMethodGrid[i, j].Value.ToString()).ToDouble();
                }
                
                ArtificialBasisPrinter();// Формирование искусственного базиса

                // СИМПЛЕКС МЕТОД
                this.tabControl.SelectedTab = this.tabPage3;// Переключение на вкладку симплекс метода
                this.simplexMethodGrid.ColumnCount = 20;
                this.simplexMethodGrid.RowCount = 100;
                this.simplexMethodGrid.CurrentCell = null;// Чтоб в самом начале не была выбрана никакая ячейка
                // Отключение сортировки по столбцам
                foreach (DataGridViewColumn column in this.simplexMethodGrid.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                // Установка ширины столбцов
                foreach (DataGridViewColumn el in this.simplexMethodGrid.Columns)
                    el.Width = 70;
                this.simplexMethodGrid.TopLeftHeaderCell.Value = "x(0)";

                // Заполнение заголовков строк и слолбцов первой таблицы
                for (int i = 0; i < (int)this.variableCount.Value - (int)this.linesCount.Value; i++)
                {
                    if(startRow!=0)
                        this.simplexMethodGrid.Columns[i].HeaderText = this.artificialBaseMethodGrid[i, startRow - 1].Value.ToString();
                    else
                        this.simplexMethodGrid.Columns[i].HeaderText = this.artificialBaseMethodGrid.Columns[i].HeaderText.ToString();
                }
                for (int i = startRow; i < startRow + (int)this.linesCount.Value; i++)
                    this.simplexMethodGrid.Rows[i - startRow ].HeaderCell.Value = this.artificialBaseMethodGrid.Rows[i].HeaderCell.Value;

                // Запонение матрицы коэффициентов
                for (int i = 0; i < (int)this.linesCount.Value; i++)
                    for (int j = 0; j < (int)this.variableCount.Value + 1 - (int)this.linesCount.Value; j++)
                        this.simplexMethodGrid[j, i].Value = this.artificialBaseMethodGrid[j, i + startRow].Value.ToString();

                //Вычисление коэф-ов целевой функции
                for (int i = 0; i < (int)this.variableCount.Value + 1 - (int)this.linesCount.Value; i++)
                {
                    Fraction targetKoef = 0;
                    for (int j = 0; j < (int)this.linesCount.Value; j++)
                    {
                        int var = this.simplexMethodGrid.Rows[j].HeaderCell.Value.ToString()[1] - '0';
                        Fraction koef = (this.optimizeTask.Text == "Min" ? Fraction.ToFraction(this.targetFuncGrid[var - 1, 0].Value.ToString()) : -1*Fraction.ToFraction(this.targetFuncGrid[var - 1, 0].Value.ToString())) ;
                        targetKoef += -1 * Fraction.ToFraction(this.simplexMethodGrid[i, j].Value.ToString()) * koef;
                    }

                    if (this.simplexMethodGrid.Columns[i].HeaderCell.Value.ToString() != "")
                    {
                        int var1 = this.simplexMethodGrid.Columns[i].HeaderCell.Value.ToString()[1] - '0';
                        this.simplexMethodGrid[i, (int)this.linesCount.Value].Value = (targetKoef + 
                            (this.optimizeTask.Text == "Min" ? Fraction.ToFraction(this.targetFuncGrid[var1 - 1, 0].Value.ToString()) : -1*Fraction.ToFraction(this.targetFuncGrid[var1 - 1, 0].Value.ToString()))).ToString();
                    }
                    else
                        this.simplexMethodGrid[i, (int)this.linesCount.Value].Value = (targetKoef + 
                            (this.optimizeTask.Text == "Min" ? Fraction.ToFraction(this.targetFuncGrid[(int)this.variableCount.Value, 0].Value.ToString()) : -1 * Fraction.ToFraction(this.targetFuncGrid[(int)this.variableCount.Value, 0].Value.ToString()))).ToString();
                }

                if (this.fractionView.Text == "Десятичные")
                {
                    for (int i = 0; i < (int)variableCount.Value - (int)linesCount.Value+1; i++)
                        for (int j =0; j <(int)this.linesCount.Value+1; j++)
                            this.simplexMethodGrid[i, j].Value = Fraction.ToFraction(this.simplexMethodGrid[i, j].Value.ToString()).ToDouble();
                }

                startRow = 0;
                iter = 1;
                
                supEl = suportElements(this.simplexMethodGrid);// Нашли все опорные элементы
                SetColorsOnSupElements(supEl, this.simplexMethodGrid);// Раскрасили все опорные элементы
                //Собственно, сам симплекс метод
                SimplexMethod(this.simplexMethodGrid);
            }
        }

        private void IdleStep(List<Point> supEl)// Холостой шаг
        {
            //  Поиск номера строки, в которой стоит переменная от которой хотим избавиться
            int numOfExcessStr = -1;
            for (int i = startRow; i < startRow + (int)this.linesCount.Value; i++)
                if (tempVars.Contains(this.artificialBaseMethodGrid.Rows[i].HeaderCell.Value.ToString()))
                {
                    numOfExcessStr = i;
                    break;
                }

            //Выбор и закрашивание главного опорного элемента(беру последний в списке опорных)
            Point mainSupElement = supEl[supEl.Count - 1];
            this.artificialBaseMethodGrid[mainSupElement.X, mainSupElement.Y].Style.BackColor = Color.FromArgb(255, 255, 0, 255);

            startRow += (int)this.linesCount.Value + 3;// Строка, начиная с которой, будет выводиться последняя таблица
            this.artificialBaseMethodGrid.Rows[startRow-1].HeaderCell.Value = String.Concat("x(",iter,")");
            SimplexStepArtificial(mainSupElement, this.artificialBaseMethodGrid);// Сам холостой шаг
            iter++;
        }

        private List<Point> suportElements(DataGridView dataGrid)
        {
            if (dataGrid.Equals(this.simplexMethodGrid))// Сделать элегантнее лол
                iter = (int)this.linesCount.Value+1;
            List<Point> supElements = new List<Point>();
            for(int i=0;i<(int)this.variableCount.Value - iter + 1; i++)
            {
                if(Fraction.ToFraction(dataGrid[i,startRow+(int)this.linesCount.Value].Value.ToString())<0)
                {
                    Fraction min = Int32.MaxValue;
                    // Поиск минимума по столбцу
                    for (int j=startRow;j<startRow+(int)this.linesCount.Value;j++)
                    {
                        if (Fraction.ToFraction(dataGrid[i, j].Value.ToString()) > 0)
                        {
                            if (Fraction.ToFraction(dataGrid[(int)this.variableCount.Value-iter+1, j].Value.ToString()) / Fraction.ToFraction(dataGrid[i, j].Value.ToString()) < min)
                                min = Fraction.ToFraction(dataGrid[(int)this.variableCount.Value-iter+1, j].Value.ToString()) / Fraction.ToFraction(dataGrid[i, j].Value.ToString());
                        }
                    }

                    // Записываем все возможные элементы в столбце
                    for (int j = startRow; j < startRow + (int)this.linesCount.Value; j++)
                        if (Fraction.ToFraction(dataGrid[i, j].Value.ToString())!=0 && Fraction.ToFraction(dataGrid[(int)this.variableCount.Value - iter + 1, j].Value.ToString()) / Fraction.ToFraction(dataGrid[i, j].Value.ToString()) == min)
                            supElements.Add(new Point(i,j));
                }
            }
            return supElements;
        }// Поиск всех возможных опорных элементов

        private void SetColorsOnSupElements(List<Point> supEl, DataGridView dateGrid)// Закрашивание всех возможных элементов зеленым цветом
        {
            for (int i = startRow; i < startRow + (int)this.linesCount.Value; i++)
                for (int j = 0; j < (int)this.variableCount.Value - iter + 1; j++)
                    if (supEl.Contains(new Point(j, i)))
                        dateGrid[j, i].Style.BackColor = Color.FromArgb(255, 0, 255, 0);
        }

        private Point mainSupEl(DataGridView dataGrid)
        {
            for (int i = startRow; i < startRow+(int)this.linesCount.Value; i++)
            {
                for (int j = 0; j < (int)this.variableCount.Value; j++)
                {
                    // Беру первый попавшийся опорный элемент
                    if (tempVars != null)
                    {
                        if (dataGrid[j, i].Style.BackColor == Color.FromArgb(255, 0, 255, 0) &&
                            tempVars.Contains(dataGrid.Rows[i].HeaderCell.Value.ToString()))
                        {
                            dataGrid[j, i].Style.BackColor = Color.FromArgb(255, 255, 0, 255);
                            return new Point(j, i);
                        }
                    }
                    else
                    {
                        if (dataGrid[j, i].Style.BackColor == Color.FromArgb(255, 0, 255, 0))
                        {
                            dataGrid[j, i].Style.BackColor = Color.FromArgb(255, 255, 0, 255);
                            return new Point(j, i);
                        }
                    }
                }
            }
            // Пока хз
            return new Point(0,0);
        }// Выбор главного опорного элемента(Допустим, утверждено)

        private void SimplexStepArtificial(Point mainSupElement, DataGridView dataGrid)
        {
            //горизонталь (Допустим, утверждено)
            for (int i = 0, columnVarNumber = i; i < (int)this.variableCount.Value - 1 * iter; i++, columnVarNumber++)
            {
                if (i == mainSupElement.X)
                {
                    if (startRow-1 - ((int)this.linesCount.Value + 3) < 0)
                        dataGrid[i, startRow-1].Value = dataGrid.Columns[columnVarNumber + 1].HeaderText;
                    else
                        dataGrid[i, startRow-1].Value = dataGrid[columnVarNumber + 1, startRow- 1 - ((int)this.linesCount.Value + 3)].Value;
                    columnVarNumber++;
                }
                else
                {
                    if (startRow - 1 - ((int)this.linesCount.Value + 3) < 0)
                        dataGrid[i, startRow-1].Value = dataGrid.Columns[columnVarNumber].HeaderText;
                    else
                        dataGrid[i, startRow-1].Value = dataGrid[columnVarNumber, startRow - 1 - ((int)this.linesCount.Value + 3)].Value;
                }
            }
            //вертикаль (Допустим, утверждено)
            for (int i = startRow; i < startRow + (int)this.linesCount.Value; i++)
            {
                if ((i - ((int)this.linesCount.Value + 3)) == mainSupElement.Y)
                {
                    if (startRow-1 - ((int)this.linesCount.Value + 3) < 0)
                        dataGrid.Rows[i].HeaderCell.Value = dataGrid.Columns[mainSupElement.X].HeaderText;
                    else
                        dataGrid.Rows[i].HeaderCell.Value = dataGrid[mainSupElement.X, startRow - 1  - ((int)this.linesCount.Value + 3)].Value;
                }
                else
                    dataGrid.Rows[i].HeaderCell.Value = dataGrid.Rows[i - ((int)this.linesCount.Value + 3)].HeaderCell.Value;
            }

            // заполнение таблицы
            // Заполнение строки, которая в предыдущей таблице содержала опорный элемент (Допустим, утверждено)
            for (int i = 0, columnVarNumber = i; i < (int)variableCount.Value + 1 - iter; i++, columnVarNumber++)
            {
                if (i == mainSupElement.X)
                    columnVarNumber++;
                dataGrid[i, mainSupElement.Y + (int)this.linesCount.Value + 3].Value = (Fraction.ToFraction(dataGrid[columnVarNumber, mainSupElement.Y].Value.ToString()) * (1 / Fraction.ToFraction(dataGrid[mainSupElement.X, mainSupElement.Y].Value.ToString()))).ToString();
            }

            // заполнение всех остальных строк (Допустим, утверждено)
            for (int i = startRow; i < startRow + (int)linesCount.Value + 1; i++)
            {
                if (i - ((int)this.linesCount.Value + 3) == mainSupElement.Y)
                    continue;
                for (int j = 0, columnVarNumber = j; j < (int)variableCount.Value + 1 - iter; j++, columnVarNumber++)
                {
                    if (j == mainSupElement.X)
                        columnVarNumber++;
                    dataGrid[j, i].Value = (Fraction.ToFraction(dataGrid[columnVarNumber, i - ((int)this.linesCount.Value + 3)].Value.ToString()) - Fraction.ToFraction(dataGrid[mainSupElement.X, i - ((int)this.linesCount.Value + 3)].Value.ToString()) * Fraction.ToFraction(dataGrid[j, mainSupElement.Y + ((int)this.linesCount.Value + 3)].Value.ToString())).ToString();
                }
            }
        }// Шаг симплекс метода с удаление столбца с искуственной переменной(Допустим, утверждено)

        private void SimplexStep(Point mainSupElement, DataGridView dataGrid)// Обычный шаг симплекс метода
        {
            //Горизонталь
            for (int i = 0; i < (int)this.variableCount.Value - (int)this.linesCount.Value; i++)
            {
                if (i == mainSupElement.X)
                {
                   // if (startRow - ((int)this.linesCount.Value + 3) < 0)
                        dataGrid[i, startRow-1].Value = dataGrid.Rows[mainSupElement.Y].HeaderCell.Value;
                    //else
                      //  dataGrid[i, startRow].Value = dataGrid[i, startRow - ((int)this.linesCount.Value + 3)].Value;
                }
                else
                {
                    if (startRow-1 - ((int)this.linesCount.Value + 3) < 0)
                        dataGrid[i, startRow-1].Value = dataGrid.Columns[i].HeaderText;
                    else
                        dataGrid[i, startRow-1].Value = dataGrid[i, startRow-1 - ((int)this.linesCount.Value + 3)].Value;
                }
            }
            //вертикаль
            for (int i = startRow; i < startRow + (int)this.linesCount.Value; i++)
            {
                if ((i - ((int)this.linesCount.Value + 3)) == mainSupElement.Y)
                {
                    if (startRow- 1 - ((int)this.linesCount.Value + 3) < 0)
                        dataGrid.Rows[i].HeaderCell.Value = dataGrid.Columns[mainSupElement.X].HeaderText;
                    else
                        dataGrid.Rows[i].HeaderCell.Value = dataGrid[mainSupElement.X, startRow-1 - ((int)this.linesCount.Value + 3)].Value;
                }
                else
                    dataGrid.Rows[i].HeaderCell.Value = dataGrid.Rows[i - ((int)this.linesCount.Value + 3)].HeaderCell.Value;
            }

            // заполнение таблицы
            // Заполнение строки, которая в предыдущей таблице содержала опорный элемент 
            for (int i = 0; i < (int)variableCount.Value + 1 - (int)this.linesCount.Value; i++)
            {
                if (i == mainSupElement.X)
                {
                    dataGrid[i, mainSupElement.Y + (int)this.linesCount.Value + 3].Value = (1 / Fraction.ToFraction(dataGrid[mainSupElement.X, mainSupElement.Y].Value.ToString())).ToString();
                }
                else
                    dataGrid[i, mainSupElement.Y + (int)this.linesCount.Value + 3].Value = (Fraction.ToFraction(dataGrid[i, mainSupElement.Y].Value.ToString()) * (1 / Fraction.ToFraction(dataGrid[mainSupElement.X, mainSupElement.Y].Value.ToString()))).ToString();
            }
            // Заполнение столбца, который в предыдущей таблице содержал опорный элемент
            for (int i = startRow ; i < startRow + (int)this.linesCount.Value+1; i++)
            {
                if (i - ((int)this.linesCount.Value + 3) == mainSupElement.Y)
                    continue;
                else
                    dataGrid[mainSupElement.X, i].Value = (-1* 1 / Fraction.ToFraction(dataGrid[mainSupElement.X, mainSupElement.Y].Value.ToString())*Fraction.ToFraction(dataGrid[mainSupElement.X, i - ((int)this.linesCount.Value + 3)].Value.ToString())).ToString();
            }
            // заполнение всех остальных строк 
            for (int i = startRow; i < startRow + (int)linesCount.Value + 1; i++)
            {
                if (i - ((int)this.linesCount.Value + 3) == mainSupElement.Y)
                    continue;
                for (int j = 0; j < (int)variableCount.Value + 1 -(int)this.linesCount.Value; j++)
                {
                    if (j == mainSupElement.X)
                        continue;
                    dataGrid[j, i].Value = (Fraction.ToFraction(dataGrid[j, i - ((int)this.linesCount.Value + 3)].Value.ToString()) - Fraction.ToFraction(dataGrid[mainSupElement.X, i - ((int)this.linesCount.Value + 3)].Value.ToString()) * Fraction.ToFraction(dataGrid[j, mainSupElement.Y + ((int)this.linesCount.Value + 3)].Value.ToString())).ToString();
                }
            }
        }

        private void SimplexMethod(DataGridView dataGrid)
        {
            while (!IsEndSimplexMethod())
            {
                tempVars = null;
                Point mainSupElement = mainSupEl(dataGrid);// Нахождение и закрашивание главного порного эл-та

                startRow += (int)this.linesCount.Value + 3;// Строка, с которой будет начинаться каждая новая таблица
                dataGrid.Rows[startRow-1].HeaderCell.Value = String.Concat("x(", countTables, ")");

                SimplexStep(mainSupElement, dataGrid);
                countTables++;
                List<Point> supEl = suportElements(this.simplexMethodGrid);// Нашли все опорные элементы
                SetColorsOnSupElements(supEl, this.simplexMethodGrid);


                if (this.fractionView.Text == "Десятичные")
                {
                    for (int i = 0; i < (int)variableCount.Value - (int)linesCount.Value+1; i++)
                        for (int j = startRow; j < startRow + (int)this.linesCount.Value+1; j++)
                            this.simplexMethodGrid[i, j].Value = Fraction.ToFraction(this.simplexMethodGrid[i, j].Value.ToString()).ToDouble();
                }
            }

            // Вывод результатов
            Fraction[] answerPoint = new Fraction[(int)this.variableCount.Value];
            for (int i = 0; i < (int)this.variableCount.Value - (int)this.linesCount.Value; i++)
            {
                if (startRow != 0)
                {
                    int freeVar = this.simplexMethodGrid[i, startRow-1].Value.ToString()[1] - '0';
                    answerPoint[freeVar - 1] = 0;
                }
                else
                {
                    int freeVar = this.simplexMethodGrid.Columns[i].HeaderText.ToString()[1] - '0';
                    answerPoint[freeVar - 1] = 0;
                }

            }

            for (int i = startRow; i < startRow + (int)this.linesCount.Value; i++)
            {
                int freeVar = this.simplexMethodGrid.Rows[i].HeaderCell.Value.ToString()[1] - '0';
                answerPoint[freeVar - 1] = Fraction.ToFraction(this.simplexMethodGrid[(int)variableCount.Value - (int)this.linesCount.Value, i].Value.ToString());
            }

            this.answerLabel.Text = "f*(";
            for (int i = 0; i < (int)this.variableCount.Value; i++)
            {
                if (this.fractionView.Text == "Десятичные")
                    this.answerLabel.Text = String.Concat(this.answerLabel.Text, answerPoint[i].ToDouble().ToString());
                else
                    this.answerLabel.Text = String.Concat(this.answerLabel.Text, answerPoint[i].ToString());
                this.answerLabel.Text = String.Concat(this.answerLabel.Text, ';');
            }
            this.answerLabel.Text = this.answerLabel.Text.Remove(this.answerLabel.Text.Length - 1);
            if (this.optimizeTask.Text == "Min")
            {
                if (this.fractionView.Text == "Десятичные")
                    this.answerLabel.Text = String.Concat(this.answerLabel.Text, ")=", (Fraction.ToFraction(this.simplexMethodGrid[(int)variableCount.Value - (int)this.linesCount.Value, startRow + (int)this.linesCount.Value].Value.ToString()) * -1).ToDouble().ToString());
                else
                    this.answerLabel.Text = String.Concat(this.answerLabel.Text, ")=", (Fraction.ToFraction(this.simplexMethodGrid[(int)variableCount.Value - (int)this.linesCount.Value, startRow + (int)this.linesCount.Value].Value.ToString()) * -1).ToString());
            }
            else
            {
                if (this.fractionView.Text == "Десятичные")
                    this.answerLabel.Text = String.Concat(this.answerLabel.Text, ")=", Fraction.ToFraction(this.simplexMethodGrid[(int)variableCount.Value - (int)this.linesCount.Value, startRow + (int)this.linesCount.Value].Value.ToString()).ToDouble().ToString());
                else
                    this.answerLabel.Text = String.Concat(this.answerLabel.Text, ")=", (Fraction.ToFraction(this.simplexMethodGrid[(int)variableCount.Value - (int)this.linesCount.Value, startRow + (int)this.linesCount.Value].Value.ToString())).ToString());
            }

        }

        private void ArtificialBasisPrinter()// Формирование искусственного базиса
        {
            Fraction[] artificialBasis = new Fraction[(int)this.variableCount.Value];
            for (int i = 0; i < (int)this.variableCount.Value + 1 - iter; i++)
            {
                if (startRow != 0)
                {
                    int freeVar = this.artificialBaseMethodGrid[i, startRow - 1].Value.ToString()[1] - '0';
                    artificialBasis[freeVar - 1] = 0;
                }
                else
                {
                    int freeVar = this.artificialBaseMethodGrid.Columns[i].HeaderText.ToString()[1] - '0';
                    artificialBasis[freeVar - 1] = 0;
                }

            }

            for (int i = startRow; i < startRow + (int)this.linesCount.Value; i++)
            {
                int freeVar = this.artificialBaseMethodGrid.Rows[i].HeaderCell.Value.ToString()[1] - '0';
                artificialBasis[freeVar - 1] = Fraction.ToFraction(this.artificialBaseMethodGrid[(int)variableCount.Value - iter + 1, i].Value.ToString());
            }
            this.artificialBaseLabel.Text = "Искуственный базис:(";
            for (int i = 0; i < (int)this.variableCount.Value; i++)
            {
                if (this.fractionView.Text == "Десятичные")
                    this.artificialBaseLabel.Text = String.Concat(this.artificialBaseLabel.Text, artificialBasis[i].ToDouble().ToString());
                else
                    this.artificialBaseLabel.Text = String.Concat(this.artificialBaseLabel.Text, artificialBasis[i].ToString());

                this.artificialBaseLabel.Text = String.Concat(this.artificialBaseLabel.Text, ';');
            }
            this.artificialBaseLabel.Text = this.artificialBaseLabel.Text.Remove(this.artificialBaseLabel.Text.Length - 1);
            this.artificialBaseLabel.Text = String.Concat(this.artificialBaseLabel.Text, ')');
        }

        private bool IsEndArtificialBaseMethod()
        {
            if (Fraction.ToFraction(this.artificialBaseMethodGrid[(int)this.variableCount.Value -iter+1, startRow + (int)this.linesCount.Value].Value.ToString()) == 0)
                return true;
            return false;
        }

        private bool IsEndSimplexMethod()
        {
            for (int i = 0; i < (int)this.variableCount.Value-(int)this.linesCount.Value; i++)
            {
                if (Fraction.ToFraction(this.simplexMethodGrid[i, startRow + (int)this.linesCount.Value].Value.ToString()) < 0)
                {
                    return false;
                }
            }
            return true;
        }

        private void MenuItemSaveAs_Click(object sender, EventArgs e)
        {
            /*
             Формат файла:
                В первой строке находятся размеры исходной задачи
                Во второй строке - коэффициенты при целевой функции
                Во всех остальных - таблица коэффициентов для ограничений
             */

            // Создание строки, которая будет записана в файл.

            // Строка с размерностью задачи
            StringBuilder sb = new StringBuilder(lines.Length);
            sb.Append(variableCount.Value.ToString());
            sb.Append(" ");
            sb.Append(linesCount.Value.ToString());
            sb.Append("\n");

            // Строка с коэффициентами при целевой функции
            for (int i = 0; i < (int)this.variableCount.Value+1; i++)
            {
                sb.Append(targetFuncGrid[i,0].Value.ToString());
                sb.Append(" ");
            }
            sb.Append("\n");

            // Строки с коэфициентами для ограничений
            for (int i = 0; i < (int)this.linesCount.Value; i++)
            {
                for (int j = 0; j < (int)this.variableCount.Value+1; j++)
                {
                    sb.Append(linesGrid[j, i].Value.ToString());
                    sb.Append(" ");
                }
                sb.Append("\n");
            }

            string value = sb.ToString();

            // Собственно, сама запись всей строки в файл
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Текстовый документ (*.txt)|*.txt|Все файлы (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
                File.WriteAllText(sfd.FileName, value);

        }

        private void MenuItemOpen_Click(object sender, EventArgs e)
        {
            // Открытие и запись файла в строку
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Текстовый документ (*.txt)|*.txt|Все файлы (*.*)|*.*";
            string value;
            if (ofd.ShowDialog() == DialogResult.OK)
                value = File.ReadAllText(ofd.FileName);
            else return;// Проверка жопы

            char[] separators = new char[] { ' ', '\n' };
            string[] subs = value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            
            // Заполнение таблиц на форме считанными из файла данными
            this.variableCount.Value = Convert.ToInt32(subs[0]);
            this.linesCount.Value = Convert.ToInt32(subs[1]);
            int i;
            for(i = 0 ; i<(int)this.variableCount.Value+1 ; i++)
            {
                this.targetFuncGrid[i, 0].Value = subs[i+2];
            }
            i = 2 + (int)this.variableCount.Value+1;
            for(int k=0;k< (int)this.linesCount.Value; k++)
                for(int j=0;j<(int)this.variableCount.Value+1;j++)
                {
                    this.linesGrid[j, k].Value = subs[i];
                    i++;
                }
        }

        private void stepForwardArtificial_Click(object sender, EventArgs e)
        {
            if (!IsEndArtificialBaseMethod())
            {
                Point mainSupElement = mainSupEl(this.artificialBaseMethodGrid);// Нахождение и закрашивание главного порного эл-та

                startRow += (int)this.linesCount.Value + 3;// Строка, с которой будет начинаться каждая новая таблица
                this.artificialBaseMethodGrid.Rows[startRow - 1].HeaderCell.Value = String.Concat("x(", iter, ")");

                SimplexStepArtificial(mainSupElement, this.artificialBaseMethodGrid);
                iter++;
                supEl = suportElements(this.artificialBaseMethodGrid);// Нашли все опорные элементы

                SetColorsOnSupElements(supEl, this.artificialBaseMethodGrid);
            }
            else
            {
                bool isIdleStep = false;
                for (int i = 0; i < (int)variableCount.Value + 1 - iter; i++)
                    if (Fraction.ToFraction(this.artificialBaseMethodGrid[i, startRow + (int)this.linesCount.Value].Value.ToString()) != 0)
                    {
                        isIdleStep = true;
                        break;
                    }
                // холостой шаг(если нужен)
                if (isIdleStep)
                    IdleStep(supEl);

                // Формирование искусственного базиса
                ArtificialBasisPrinter();

                // СИМПЛЕКС МЕТОД
                this.stepForwardArtificial.Enabled = false;
                this.tabControl.SelectedTab = this.tabPage3;// Переключение на вкладку симплекс метода
                this.simplexMethodGrid.ColumnCount = 20;
                this.simplexMethodGrid.RowCount = 100;
                this.simplexMethodGrid.CurrentCell = null;// Чтоб в самом начале не была выбрана никакая ячейка
                // Отключение сортировки по столбцам
                foreach (DataGridViewColumn column in this.simplexMethodGrid.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                // Установка ширины столбцов
                foreach (DataGridViewColumn el in this.simplexMethodGrid.Columns)
                    el.Width = 70;
                this.simplexMethodGrid.TopLeftHeaderCell.Value = "x(0)";

                // Заполнение заголовков строк и слолбцов первой таблицы
                for (int i = 0; i < (int)this.variableCount.Value - (int)this.linesCount.Value; i++)
                {
                    if (startRow != 0)
                        this.simplexMethodGrid.Columns[i].HeaderText = this.artificialBaseMethodGrid[i, startRow - 1].Value.ToString();
                    else
                        this.simplexMethodGrid.Columns[i].HeaderText = this.artificialBaseMethodGrid.Columns[i].HeaderText.ToString();
                }
                for (int i = startRow; i < startRow + (int)this.linesCount.Value; i++)
                    this.simplexMethodGrid.Rows[i - startRow].HeaderCell.Value = this.artificialBaseMethodGrid.Rows[i].HeaderCell.Value;

                // Запонение матрицы коэффициентов
                for (int i = 0; i < (int)this.linesCount.Value; i++)
                    for (int j = 0; j < (int)this.variableCount.Value + 1 - (int)this.linesCount.Value; j++)
                        this.simplexMethodGrid[j, i].Value = this.artificialBaseMethodGrid[j, i + startRow].Value.ToString();

                //Вычисление коэф-ов целевой функции
                for (int i = 0; i < (int)this.variableCount.Value + 1 - (int)this.linesCount.Value; i++)
                {
                    Fraction targetKoef = 0;
                    for (int j = 0; j < (int)this.linesCount.Value; j++)
                    {
                        int var = this.simplexMethodGrid.Rows[j].HeaderCell.Value.ToString()[1] - '0';
                        Fraction koef = Fraction.ToFraction(this.targetFuncGrid[var - 1, 0].Value.ToString());
                        targetKoef += -1 * Fraction.ToFraction(this.simplexMethodGrid[i, j].Value.ToString()) * koef;
                    }

                    if (this.simplexMethodGrid.Columns[i].HeaderCell.Value.ToString() != "")
                    {
                        int var1 = this.simplexMethodGrid.Columns[i].HeaderCell.Value.ToString()[1] - '0';
                        this.simplexMethodGrid[i, (int)this.linesCount.Value].Value = (targetKoef + Fraction.ToFraction(this.targetFuncGrid[var1 - 1, 0].Value.ToString())).ToString();
                    }
                    else
                        this.simplexMethodGrid[i, (int)this.linesCount.Value].Value = (targetKoef + Fraction.ToFraction(this.targetFuncGrid[(int)this.variableCount.Value, 0].Value.ToString())).ToString();
                }
                startRow = 0;
                iter = 1;

                supEl = suportElements(this.simplexMethodGrid);// Нашли все опорные элементы
                SetColorsOnSupElements(supEl, this.simplexMethodGrid);// Раскрасили все опорные элементы
            }
        }

        private void stepForwardSimplex_Click(object sender, EventArgs e)
        {
            if (!IsEndSimplexMethod())
            {
                tempVars = null;
                Point mainSupElement = mainSupEl(this.simplexMethodGrid);// Нахождение и закрашивание главного порного эл-та

                startRow += (int)this.linesCount.Value + 3;// Строка, с которой будет начинаться каждая новая таблица
                this.simplexMethodGrid.Rows[startRow - 1].HeaderCell.Value = String.Concat("x(", countTables, ")");

                SimplexStep(mainSupElement, this.simplexMethodGrid);
                countTables++;
                List<Point> supEl = suportElements(this.simplexMethodGrid);// Нашли все опорные элементы
                SetColorsOnSupElements(supEl, this.simplexMethodGrid);
            }
            else
            {
                this.stepForwardSimplex.Enabled = false;
                // Вывод результатов
                Fraction[] answerPoint = new Fraction[(int)this.variableCount.Value];
                for (int i = 0; i < (int)this.variableCount.Value - (int)this.linesCount.Value; i++)
                {
                    if (startRow != 0)
                    {
                        int freeVar = this.simplexMethodGrid[i, startRow - 1].Value.ToString()[1] - '0';
                        answerPoint[freeVar - 1] = 0;
                    }
                    else
                    {
                        int freeVar = this.simplexMethodGrid.Columns[i].HeaderText.ToString()[1] - '0';
                        answerPoint[freeVar - 1] = 0;
                    }

                }

                for (int i = startRow; i < startRow + (int)this.linesCount.Value; i++)
                {
                    int freeVar = this.simplexMethodGrid.Rows[i].HeaderCell.Value.ToString()[1] - '0';
                    answerPoint[freeVar - 1] = Fraction.ToFraction(this.simplexMethodGrid[(int)variableCount.Value - (int)this.linesCount.Value, i].Value.ToString());
                }

                this.answerLabel.Text = "f*(";
                for (int i = 0; i < (int)this.variableCount.Value; i++)
                {
                    this.answerLabel.Text = String.Concat(this.answerLabel.Text, answerPoint[i].ToString());
                    this.answerLabel.Text = String.Concat(this.answerLabel.Text, ';');
                }
                this.answerLabel.Text = this.answerLabel.Text.Remove(this.answerLabel.Text.Length - 1);
                this.answerLabel.Text = String.Concat(this.answerLabel.Text, ")=", (Fraction.ToFraction(this.simplexMethodGrid[(int)variableCount.Value - (int)this.linesCount.Value, startRow + (int)this.linesCount.Value].Value.ToString()) * -1).ToString());
            }
        }

        /*public static string ConvertToFraction(decimal value)
        {
            for (int i = 1; i < 100; i++)
            {
                decimal multipliedValue = value * i;
                if (Math.Abs(Math.Round(multipliedValue) - multipliedValue) <= 0.00000001M)
                    return Math.Round(multipliedValue).ToString() + "/" + i.ToString();
            }
            return value.ToString();
        }*/

        /*public Fraction RealToFraction(string strValue)
        {
            decimal value = Convert.ToDecimal(strValue);
            decimal accuracy = 0.000000000000001M;

            int sign = Math.Sign(value);

            if (sign == -1)
            {
                value = Math.Abs(value);
            }

            // Accuracy is the maximum relative error; convert to absolute maxError
            decimal maxError = (sign == 0 ? accuracy : value * accuracy);

            int n = (int)Math.Floor(value);
            value -= n;

            if (value < maxError)
            {
                return new Fraction(sign * n, 1);
            }

            if (1 - maxError < value)
            {
                return new Fraction(sign * (n + 1), 1);
            }

            // The lower fraction is 0/1
            int lower_n = 0;
            int lower_d = 1;

            // The upper fraction is 1/1
            int upper_n = 1;
            int upper_d = 1;

            while (true)
            {
                // The middle fraction is (lower_n + upper_n) / (lower_d + upper_d)
                int middle_n = lower_n + upper_n;
                int middle_d = lower_d + upper_d;

                if (middle_d * (value + maxError) < middle_n)
                {
                    // real + error < middle : middle is our new upper
                    upper_n = middle_n;
                    upper_d = middle_d;
                }
                else if (middle_n < (value - maxError) * middle_d)
                {
                    // middle < real - error : middle is our new lower
                    lower_n = middle_n;
                    lower_d = middle_d;
                }
                else
                {
                    // Middle is our best fraction
                    return new Fraction((n * middle_d + middle_n) * sign, middle_d);
                }
            }
        }*/

    }

}
