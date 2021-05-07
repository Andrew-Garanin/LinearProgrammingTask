using System;
using System.Drawing;
using System.Windows.Forms;
using Mehroz;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LinearProgrammingTask
{
    public partial class Form1 : Form
    {
        List<StepInformation> artificialStepInfo = new List<StepInformation>();
        List<StepInformation> simplexStepInfo = new List<StepInformation>();
        public int iter = 1;// Номер итерации в таблице
        bool isIdleStep;// Нужен ли холостой шаг
        Point mainSupElement;
        public int startRow = 0;// Строка, с которой начинается каждая следующая таблица
        public List<String> tempVars = new List<String>();// Искуственно введенные переменные
        public List<int> baseVars = new List<int>();// Базисные переменные, которые ввел пользователь
        public List<Point> supEl = new List<Point>();// Список опорных элементов на каждой итерации
        public int countTables = 1;// Номер каждой новой таблицы в симплекс методе
        FractionGausMethod Solution;// Объект, содержащий таблицу для заданного вручную базиса
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

            //Параметры для таблицы заданных базисных переменных
            basisNumbersGrid.EnableHeadersVisualStyles = false;//Разрешить красить заголовки столбцов цветом
            basisNumbersGrid.AllowUserToAddRows = false;
            basisNumbersGrid.ColumnCount = (int)variableCount.Value;
            basisNumbersGrid.RowCount = 0;
            foreach (DataGridViewColumn column in basisNumbersGrid.Columns)
            {
                column.HeaderText = String.Concat("x",
                    (column.Index + 1).ToString());
            }

            // Делаем неактивными кнопки шага назад для пошагового режима
            this.stepBackArtificial.Enabled = false;
            this.stepBackSimplex.Enabled = false;

            //this.tabPage2.Enabled = false;
            //this.tabPage3.Enabled = false;
            //this.tabPage4.Enabled = false;

            this.basisNumbersLabel.Visible = false;
            this.basisNumbersGrid.Visible = false;
        }

        private void VariablesCount_ValueChanged(object sender, EventArgs e)
        {
            // Изменения таблицы ограничений
            this.linesGrid.ColumnCount = (int)this.variableCount.Value+1;
            for(int i =0;i< this.linesGrid.ColumnCount-1;i++)
                linesGrid.Columns[i].HeaderText= String.Concat("a", (i+1).ToString());
            linesGrid.Columns[(int)variableCount.Value].HeaderText = "b";

            // Изменения таблицы целевой функции
            this.targetFuncGrid.ColumnCount = (int)this.variableCount.Value+1;
            for (int i = 0; i < this.targetFuncGrid.ColumnCount-1; i++)
                targetFuncGrid.Columns[i].HeaderText = String.Concat("c", (i+1).ToString());
            targetFuncGrid.Columns[(int)variableCount.Value].HeaderText = "c";

            // Изменения таблицы с пользовательскими базисными переменными
            this.basisNumbersGrid.ColumnCount = (int)this.variableCount.Value;
            for (int i = 0; i < this.basisNumbersGrid.ColumnCount; i++)
                basisNumbersGrid.Columns[i].HeaderText = String.Concat("x", (i + 1).ToString());
        }

        private void LinesCount_ValueChanged(object sender, EventArgs e)
        {
            // Изменения таблицы с ограничениями
            this.linesGrid.RowCount = (int)this.linesCount.Value;
            for (int i = 0; i < this.linesGrid.RowCount; i++)
                linesGrid.Rows[i].HeaderCell.Value = String.Concat("f", (i+1).ToString());
        }

        private void GraphPictureControl_Paint(object sender, PaintEventArgs e)
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

        private void DevelopmentMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void OptimizeTask_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FractionView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BaseView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.baseView.Text=="Искуственный")
            {
                this.basisNumbersLabel.Visible = false;
                this.basisNumbersGrid.Visible = false;
            }
            if(this.baseView.Text == "Заданный")
            {
                this.basisNumbersLabel.Visible = true;
                this.basisNumbersGrid.Visible = true;
            }    
        }

        private void Btn_OK_Click(object sender, EventArgs e)// Начало решения задачи ЛП
        {
            // Проверка нет ли пустых ячеек в таблицах коэффициентов
            for(int i=0;i<(int)this.variableCount.Value+1;i++)
                if(this.targetFuncGrid[i,0].Value==null)
                {
                    MessageBox.Show("В таблице коэффициентов для целевой функции присутствуют пустые ячейки");
                    return;
                }

            for (int i = 0; i < (int)this.variableCount.Value + 1; i++)
                for (int j = 0; j < (int)this.linesCount.Value; j++)
                    if (this.linesGrid[i, j].Value == null)
                    {
                        MessageBox.Show("В таблице коэффициентов для ограничений присутствуют пустые ячейки");
                        return;
                    }

            // Запись данных из таблицы ограничений в массив для ограничений
            for (int i = 0; i < (int)this.variableCount.Value + 1; i++)
                for (int j = 0; j < (int)this.linesCount.Value; j++)
                    lines[j, i] = Fraction.ToFraction(linesGrid[i, j].Value.ToString());

            // Проверка на ранг матрицы
            FractionGausMethod sheckRankTable = new FractionGausMethod((uint)this.linesCount.Value, (uint)this.variableCount.Value);
            for (int i = 0; i < (uint)this.linesCount.Value; i++)
            {
                for (int j = 0; j < (uint)this.variableCount.Value; j++)
                {
                    sheckRankTable.Matrix[i][j] = lines[i, j];
                }
            }
            if (sheckRankTable.Rank() != (int)this.linesCount.Value)
            {
                MessageBox.Show("В таблице ограничений присутствуют линейно-зависимые строки");
                return;
            }
            sheckRankTable = null;// Эта матрица больше не нужна

            if (this.baseView.Text == "Заданный")
            {
                //Проверка на число выбранных пользователем базисных переменных
                int count = 0;
                for (int i = 0; i < (int)this.variableCount.Value; i++)
                    if (this.basisNumbersGrid.Columns[i].HeaderCell.Style.BackColor == Color.FromArgb(255, 0, 255, 0))
                        count++;
                if (count != (int)this.linesCount.Value)
                {
                    MessageBox.Show("Число выбранных базисных переменных должно быть равно числу ограничений");
                    return;
                }

                // Запись выбранных базисных переменных в массив
                for (int i = 0; i < (int)this.variableCount.Value; i++)
                {
                    if (this.basisNumbersGrid.Columns[i].HeaderCell.Style.BackColor == Color.FromArgb(255, 0, 255, 0))
                        baseVars.Add(this.basisNumbersGrid.Columns[i].HeaderText.ToString()[1] - '0');
                }

                // Инициализация объекта для работы с матрицой
                Solution = new FractionGausMethod((uint)this.linesCount.Value, (uint)this.variableCount.Value);
                for (int i = 0; i < (uint)this.linesCount.Value; i++)
                {
                    for (int j = 0; j < (uint)this.variableCount.Value; j++)
                    {
                        Solution.Matrix[i][j] = lines[i, j];
                    }
                    Solution.RightPart[i] = lines[i, (uint)this.variableCount.Value];
                }

                if(Solution.SolveMatrix(baseVars)==1)// Приведение матрицы к диагональному виду методом Гаусса
                {
                    MessageBox.Show("У матрицы нет решений или их бесконечно много");
                    return;
                }
            }
            else// МЕТОД ИСКУССТВЕННОГО БАЗИСА
            {
                // Предварительная подготовка(все Bi должны быть >=0)
                for (int i = 0; i < (int)this.linesCount.Value; i++)
                {
                    if (lines[i, (int)this.variableCount.Value] < 0)
                    {
                        for (int j = 0; j < (int)this.variableCount.Value + 1; j++)
                        {
                            lines[i, j] *= -1;
                        }
                    }
                }

                this.tabControl.SelectedTab = this.tabPage2;// Переключение на вкладку метода иск. базиса
                this.artificialBaseMethodGrid.ColumnCount = 20;
                this.artificialBaseMethodGrid.RowCount = 100;
                this.artificialBaseMethodGrid.CurrentCell = null;// Чтоб в самом начале не была выбрана никакая ячейка

                foreach (DataGridViewColumn column in this.artificialBaseMethodGrid.Columns)// Отключение сортировки по столбцам
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                
                foreach (DataGridViewColumn el in this.artificialBaseMethodGrid.Columns)// Установка ширины столбцов
                    el.Width = 70;

                this.artificialBaseMethodGrid.TopLeftHeaderCell.Value = "x(0)";// Номер самой первой таблицы

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

                supEl = SuportElements(this.artificialBaseMethodGrid);// Нашли все опорные элементы
                SetColorsOnSupElements(supEl, this.artificialBaseMethodGrid);// Раскрасили все опорные элементы

                if (this.fractionView.Text == "Десятичные")
                {
                    for (int i = 0; i < (int)this.variableCount.Value + 1; i++)
                        for (int j = 0; j < (int)this.linesCount.Value + 1; j++)
                            this.artificialBaseMethodGrid[i, j].Value = Fraction.ToFraction(this.artificialBaseMethodGrid[i, j].Value.ToString()).ToDouble();
                }

                if (this.solutionMode.Text == "Пошаговый")
                {
                    mainSupElement = MainSupEl(this.artificialBaseMethodGrid);// Нахождение и закрашивание главного опорного эл-та

                    // Запоминание информации для первого шага
                    artificialStepInfo.Add(new StepInformation(startRow, new List<Point>(supEl), iter, mainSupElement, countTables, new List<String>(tempVars)));
                    return;
                }
                // Получение таблицы в которой останутся только переменные из начальной задачи
                while (!IsEndArtificialBaseMethod())
                {
                    mainSupElement = MainSupEl(this.artificialBaseMethodGrid);// Нахождение и закрашивание главного опорного эл-та

                    startRow += (int)this.linesCount.Value + 3;// Строка, с которой будет начинаться каждая новая таблица
                    this.artificialBaseMethodGrid.Rows[startRow - 1].HeaderCell.Value = String.Concat("x(", iter, ")");// Номер новой строки

                    SimplexStepArtificial(this.artificialBaseMethodGrid);// Шаг метода искуссвенного базиса
                    iter++;
                    supEl = SuportElements(this.artificialBaseMethodGrid);// Нашли все опорные элементы

                    SetColorsOnSupElements(supEl, this.artificialBaseMethodGrid);// Раскрасили все опорные элементы

                    if (this.fractionView.Text == "Десятичные")
                    {
                        for (int i = 0; i < (int)variableCount.Value + 2 - iter; i++)
                            for (int j = startRow; j < startRow + (int)this.linesCount.Value + 1; j++)
                                this.artificialBaseMethodGrid[i, j].Value = Fraction.ToFraction(this.artificialBaseMethodGrid[i, j].Value.ToString()).ToDouble();
                    }
                }
                
                // Проверка нужен ли холостой шаг
                isIdleStep = true;
                while (isIdleStep)
                {
                    isIdleStep = false;
                    for (int i = 0; i < (int)variableCount.Value + 1 - iter; i++)
                        if (Fraction.ToFraction(this.artificialBaseMethodGrid[i, startRow + (int)this.linesCount.Value].Value.ToString()) != 0)
                        {
                            isIdleStep = true;
                            break;
                        }

                    // холостой шаг(если нужен)
                    if (isIdleStep)
                        IdleStep();

                    if (this.fractionView.Text == "Десятичные")
                    {
                        for (int i = 0; i < (int)variableCount.Value + 2 - iter; i++)
                            for (int j = startRow; j < startRow + (int)this.linesCount.Value + 1; j++)
                                this.artificialBaseMethodGrid[i, j].Value = Fraction.ToFraction(this.artificialBaseMethodGrid[i, j].Value.ToString()).ToDouble();
                    }
                }
                ArtificialBasisPrinter();// Формирование искусственного базиса в строку
            }

            // СИМПЛЕКС МЕТОД
            this.tabControl.SelectedTab = this.tabPage3;// Переключение на вкладку симплекс метода
            this.simplexMethodGrid.ColumnCount = 20;
            this.simplexMethodGrid.RowCount = 100;
            this.simplexMethodGrid.CurrentCell = null;// Чтоб в самом начале не была выбрана никакая ячейка
            
            foreach (DataGridViewColumn column in this.simplexMethodGrid.Columns)// Отключение сортировки по столбцам
                column.SortMode = DataGridViewColumnSortMode.NotSortable;

            foreach (DataGridViewColumn el in this.simplexMethodGrid.Columns)// Установка ширины столбцов
                el.Width = 70;

            this.simplexMethodGrid.TopLeftHeaderCell.Value = "x(0)";// Номер самой первой таблицы

            if (this.baseView.Text == "Заданный")
            {
                // Заполнение заголовков строк и слолбцов первой таблицы
                for (int i = 0, columnCount = 0; i < (int)this.variableCount.Value; i++, columnCount++)
                {
                    if (!baseVars.Contains(i + 1))
                        this.simplexMethodGrid.Columns[columnCount].HeaderText = String.Concat("x", i + 1);
                    else
                        columnCount--;
                }
                for (int i = 0, rowCount=0; i < (int)this.variableCount.Value; i++, rowCount++)
                {
                    if (baseVars.Contains(i + 1))
                        this.simplexMethodGrid.Rows[rowCount].HeaderCell.Value = String.Concat("x", i + 1);
                    else
                        rowCount--;
                }

                // Запонение матрицы коэффициентов
                for (int i = 0; i < (int)this.linesCount.Value; i++)
                {
                    for (int j = 0, columnCount = 0; j < (int)this.variableCount.Value; j++, columnCount++)
                    {
                        if (!baseVars.Contains(j + 1))
                            this.simplexMethodGrid[columnCount, i].Value = Solution.Matrix[i][j].ToString();
                        else
                            columnCount--;
                    }
                }

                // Заполнение правой части
                for (int i = 0; i < (int)this.linesCount.Value; i++)
                    this.simplexMethodGrid[(int)this.variableCount.Value - (int)this.linesCount.Value, i].Value = Solution.RightPart[i].ToString();
            }
            else
            {
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
            }

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

            // Вернули к первоначальным значениям
            startRow = 0;
            iter = 1;

            if (SheckForInfinityPoint() == 1)// Если есть ребро в минус бесконечнсть
            {
                this.answerLabel.Text = "Целевая функция не ограничена снизу";
                this.stepForwardSimplex.Enabled = false;
                return;
            }

            supEl = SuportElements(this.simplexMethodGrid);// Нашли все опорные элементы
            SetColorsOnSupElements(supEl, this.simplexMethodGrid);// Раскрасили все опорные элементы

            if (this.solutionMode.Text == "Пошаговый")
            {
                tempVars = null;
                mainSupElement = MainSupEl(this.simplexMethodGrid);// Нахождение и закрашивание главного опорного эл-та

                // Запоминание информации для первого шага
                simplexStepInfo.Add(new StepInformation(startRow, new List<Point>(supEl), iter, mainSupElement, countTables, null));
                return;
            }

            SimplexMethod(this.simplexMethodGrid);
        }

        private void IdleStep()// Холостой шаг
        {
            //  Поиск номера строки, в которой стоит переменная от которой хотим избавиться
            int numOfExcessStr = -1;
            for (int i = startRow; i < startRow + (int)this.linesCount.Value; i++)
                if (tempVars.Contains(this.artificialBaseMethodGrid.Rows[i].HeaderCell.Value.ToString()))
                {
                    numOfExcessStr = i;
                    break;
                }

            //Выбор и закрашивание главного опорного элемента
            if (this.solutionMode.Text != "Пошаговый")
            {
                mainSupElement = supEl.Find((p) => p.Y == numOfExcessStr);
                this.artificialBaseMethodGrid[mainSupElement.X, mainSupElement.Y].Style.BackColor = Color.FromArgb(255, 255, 0, 255);
            }

            startRow += (int)this.linesCount.Value + 3;// Строка, начиная с которой, будет выводиться новая таблица
            this.artificialBaseMethodGrid.Rows[startRow-1].HeaderCell.Value = String.Concat("x(",iter,")");
            SimplexStepArtificial(this.artificialBaseMethodGrid);// Сам холостой шаг
            iter++;

            isIdleStep = false;
            for (int i = 0; i < (int)variableCount.Value + 1 - iter; i++)
                if (Fraction.ToFraction(this.artificialBaseMethodGrid[i, startRow + (int)this.linesCount.Value].Value.ToString()) != 0)
                {
                    isIdleStep = true;
                    break;
                }
            supEl = SuportElements(this.artificialBaseMethodGrid);// Нашли все опорные элементы
            SetColorsOnSupElements(supEl, this.artificialBaseMethodGrid);// Раскрасили все опорные элементы

            if (this.solutionMode.Text == "Пошаговый")
            {
                //mainSupElement = supEl.Find((p) => p.Y == numOfExcessStr);
                mainSupElement = MainSupEl(this.artificialBaseMethodGrid);
                if(mainSupElement!= new Point(-100,-100))
                    this.artificialBaseMethodGrid[mainSupElement.X, mainSupElement.Y].Style.BackColor = Color.FromArgb(255, 255, 0, 255);
            }
        }

        private List<Point> SuportElements(DataGridView dataGrid)
        {
            List<Point> supElements = new List<Point>();

            // для холостого шага
            if (isIdleStep)
            {
                for (int j = startRow; j < startRow + (int)this.linesCount.Value; j++)
                    if (tempVars.Contains(this.artificialBaseMethodGrid.Rows[j].HeaderCell.Value.ToString()))
                        for (int i = 0; i < (int)this.variableCount.Value - iter + 1; i++)// Записываем все возможные элементы в строке
                        {
                            if (Fraction.ToFraction(dataGrid[i, j].Value.ToString()) != 0)
                                supElements.Add(new Point(i, j));
                        }
            }
            else
            {

                if (dataGrid.Equals(this.simplexMethodGrid))
                    iter = (int)this.linesCount.Value + 1;
                for (int i = 0; i < (int)this.variableCount.Value - iter + 1; i++)
                {
                    if (Fraction.ToFraction(dataGrid[i, startRow + (int)this.linesCount.Value].Value.ToString()) < 0)
                    {
                        Fraction min = Int32.MaxValue;
                        // Поиск минимума по столбцу
                        for (int j = startRow; j < startRow + (int)this.linesCount.Value; j++)
                        {
                            if (Fraction.ToFraction(dataGrid[i, j].Value.ToString()) > 0)
                            {
                                if (Fraction.ToFraction(dataGrid[(int)this.variableCount.Value - iter + 1, j].Value.ToString()) / Fraction.ToFraction(dataGrid[i, j].Value.ToString()) < min)
                                    min = Fraction.ToFraction(dataGrid[(int)this.variableCount.Value - iter + 1, j].Value.ToString()) / Fraction.ToFraction(dataGrid[i, j].Value.ToString());
                            }
                        }

                        // Записываем все возможные элементы в столбце
                        for (int j = startRow; j < startRow + (int)this.linesCount.Value; j++)
                            if (Fraction.ToFraction(dataGrid[i, j].Value.ToString()) != 0 && Fraction.ToFraction(dataGrid[(int)this.variableCount.Value - iter + 1, j].Value.ToString()) / Fraction.ToFraction(dataGrid[i, j].Value.ToString()) == min)
                            {
                                if(dataGrid.Equals(this.artificialBaseMethodGrid))
                                {
                                    if (tempVars.Contains(this.artificialBaseMethodGrid.Rows[j].HeaderCell.Value.ToString()))
                                        supElements.Add(new Point(i, j));
                                }
                                else
                                //if(isIdleStep)
                                //{
                                //    if(tempVars.Contains(this.artificialBaseMethodGrid.Rows[j].HeaderCell.Value.ToString()))
                                //        supElements.Add(new Point(i, j));
                                //}
                                //else
                                supElements.Add(new Point(i, j));
                            }
                    }
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

        private Point MainSupEl(DataGridView dataGrid)
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
            return new Point(-100,-100);// Если нет опорного элемента
        }// Выбор главного опорного элемента

        private void SimplexStepArtificial( DataGridView dataGrid)
        {
            // Горизонталь
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
            // Вертикаль
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

            // Заполнение таблицы

            // Заполнение строки, которая в предыдущей таблице содержала опорный элемент
            for (int i = 0, columnVarNumber = i; i < (int)variableCount.Value + 1 - iter; i++, columnVarNumber++)
            {
                if (i == mainSupElement.X)
                    columnVarNumber++;
                dataGrid[i, mainSupElement.Y + (int)this.linesCount.Value + 3].Value = (Fraction.ToFraction(dataGrid[columnVarNumber, mainSupElement.Y].Value.ToString()) * (1 / Fraction.ToFraction(dataGrid[mainSupElement.X, mainSupElement.Y].Value.ToString()))).ToString();
            }

            // Заполнение всех остальных строк
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
        }// Шаг симплекс метода с удалением столбца с искуственной переменной

        private void SimplexStep( DataGridView dataGrid)// Обычный шаг симплекс метода
        {
            // Горизонталь
            for (int i = 0; i < (int)this.variableCount.Value - (int)this.linesCount.Value; i++)
            {
                if (i == mainSupElement.X)
                    dataGrid[i, startRow-1].Value = dataGrid.Rows[mainSupElement.Y].HeaderCell.Value;
                else
                {
                    if (startRow-1 - ((int)this.linesCount.Value + 3) < 0)
                        dataGrid[i, startRow-1].Value = dataGrid.Columns[i].HeaderText;
                    else
                        dataGrid[i, startRow-1].Value = dataGrid[i, startRow-1 - ((int)this.linesCount.Value + 3)].Value;
                }
            }

            // Вертикаль
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

            // Заполнение таблицы

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

            // Заполнение всех остальных строк
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

        private int SheckForInfinityPoint()// Проверка есть ли ребро в минус бесконечность
        {
            for (int i = 0; i < (int)this.variableCount.Value- (int)this.linesCount.Value; i++)
            {
                if (Fraction.ToFraction(this.simplexMethodGrid[i, startRow + (int)this.linesCount.Value].Value.ToString()) < 0)
                {
                    int count=0;
                    for (int j = startRow; j < startRow + (int)this.linesCount.Value; j++)
                    {
                        if (Fraction.ToFraction(this.simplexMethodGrid[i, j].Value.ToString()) > 0)
                            break;
                        else
                            count++;
                    }
                    if (count == (int)this.linesCount.Value)
                        return 1;
                }
            }
            return 0;
        }

        private void SimplexMethod(DataGridView dataGrid)
        {
            while (!IsEndSimplexMethod())
            {
                if(SheckForInfinityPoint()==1)// Если есть ребро в минус бесконечнсть
                {
                    this.answerLabel.Text = "Целевая функция не ограничена снизу";
                    return;
                }
                tempVars = null;// Как только начался сам симплекс метод, искусственных переменных в таблице не осталось

                mainSupElement = MainSupEl(dataGrid);// Нахождение и закрашивание главного порного эл-та

                startRow += (int)this.linesCount.Value + 3;// Строка, с которой будет начинаться каждая новая таблица
                dataGrid.Rows[startRow-1].HeaderCell.Value = String.Concat("x(", countTables, ")");

                SimplexStep( dataGrid);
                countTables++;
                List<Point> supEl = SuportElements(this.simplexMethodGrid);// Нашли все опорные элементы
                SetColorsOnSupElements(supEl, this.simplexMethodGrid);// Раскрасили все опорные элементы

                if (this.fractionView.Text == "Десятичные")
                {
                    for (int i = 0; i < (int)variableCount.Value - (int)linesCount.Value+1; i++)
                        for (int j = startRow; j < startRow + (int)this.linesCount.Value+1; j++)
                            this.simplexMethodGrid[i, j].Value = Fraction.ToFraction(this.simplexMethodGrid[i, j].Value.ToString()).ToDouble();
                }
            }

            // Вывод результатов
            Fraction[] answerPoint = new Fraction[(int)this.variableCount.Value];

            // Обнуление всех свободных переменных
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

            // Запоминание значений базисных переменных
            for (int i = startRow; i < startRow + (int)this.linesCount.Value; i++)
            {
                int freeVar = this.simplexMethodGrid.Rows[i].HeaderCell.Value.ToString()[1] - '0';
                answerPoint[freeVar - 1] = Fraction.ToFraction(this.simplexMethodGrid[(int)variableCount.Value - (int)this.linesCount.Value, i].Value.ToString());
            }

            // Формирование строки с ответом задачи ЛП
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

        private void ArtificialBasisPrinter()// Формирование строки искусственного базиса
        {
            Fraction[] artificialBasis = new Fraction[(int)this.variableCount.Value];
            
            // Обнуление свободных переменных
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

            // Запоминание значений базисных переменных
            for (int i = startRow; i < startRow + (int)this.linesCount.Value; i++)
            {
                int freeVar = this.artificialBaseMethodGrid.Rows[i].HeaderCell.Value.ToString()[1] - '0';
                artificialBasis[freeVar - 1] = Fraction.ToFraction(this.artificialBaseMethodGrid[(int)variableCount.Value - iter + 1, i].Value.ToString());
            }

            // Формирование строки с искуственным базисом
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
                В первой строке находятся размеры исходной задачи(количество переменных, потом количество ограничений)
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

        private void StepForwardArtificial_Click(object sender, EventArgs e)
        {
            if (!IsEndArtificialBaseMethod())
            {
                startRow += (int)this.linesCount.Value + 3;// Строка, с которой будет начинаться каждая новая таблица
                this.artificialBaseMethodGrid.Rows[startRow - 1].HeaderCell.Value = String.Concat("x(", iter, ")");// Номер новой строки

                SimplexStepArtificial(this.artificialBaseMethodGrid);// Шаг метода искуссвенного базиса
                iter++;

                // Проверка нужен ли будет холостой шаг
                if (IsEndArtificialBaseMethod())
                {
                    isIdleStep = false;
                    for (int i = 0; i < (int)variableCount.Value + 1 - iter; i++)
                        if (Fraction.ToFraction(this.artificialBaseMethodGrid[i, startRow + (int)this.linesCount.Value].Value.ToString()) != 0)
                        {
                            isIdleStep = true;
                            break;
                        }
                }
                supEl = SuportElements(this.artificialBaseMethodGrid);// Нашли все опорные элементы

                SetColorsOnSupElements(supEl, this.artificialBaseMethodGrid);// Раскрасили все опорные элементы

                if (this.fractionView.Text == "Десятичные")
                {
                    for (int i = 0; i < (int)variableCount.Value + 2 - iter; i++)
                        for (int j = startRow; j < startRow + (int)this.linesCount.Value + 1; j++)
                            this.artificialBaseMethodGrid[i, j].Value = Fraction.ToFraction(this.artificialBaseMethodGrid[i, j].Value.ToString()).ToDouble();
                }

                mainSupElement = MainSupEl(this.artificialBaseMethodGrid);// Нахождение и закрашивание главного опорного эл-та

                // Запоминание информации для очередного шага
                artificialStepInfo.Add(new StepInformation(startRow, new List<Point>(supEl), iter, mainSupElement, countTables, new List<String>(tempVars)));
                this.stepBackArtificial.Enabled = true;
            }
            else
            {
                // Проверка нужен ли холостой шаг
                isIdleStep = false;
                for (int i = 0; i < (int)variableCount.Value + 1 - iter; i++)
                    if (Fraction.ToFraction(this.artificialBaseMethodGrid[i, startRow + (int)this.linesCount.Value].Value.ToString()) != 0)
                    {
                        isIdleStep = true;
                        break;
                    }

                // холостой шаг(если нужен)
                if (isIdleStep)
                {
                    IdleStep();

                    if (this.fractionView.Text == "Десятичные")
                    {
                        for (int i = 0; i < (int)variableCount.Value + 2 - iter; i++)
                            for (int j = startRow; j < startRow + (int)this.linesCount.Value + 1; j++)
                                this.artificialBaseMethodGrid[i, j].Value = Fraction.ToFraction(this.artificialBaseMethodGrid[i, j].Value.ToString()).ToDouble();
                    }

                    // Запоминание информации для очередного шага
                    artificialStepInfo.Add(new StepInformation(startRow, new List<Point>(supEl), iter, mainSupElement, countTables, new List<String>(tempVars)));
                    this.stepBackArtificial.Enabled = true;
                    return;
                }
                ArtificialBasisPrinter();// Формирование искусственного базиса в строку

                // СИМПЛЕКС МЕТОД
                this.tabControl.SelectedTab = this.tabPage3;// Переключение на вкладку симплекс метода
                this.simplexMethodGrid.ColumnCount = 20;
                this.simplexMethodGrid.RowCount = 100;
                this.simplexMethodGrid.CurrentCell = null;// Чтоб в самом начале не была выбрана никакая ячейка

                foreach (DataGridViewColumn column in this.simplexMethodGrid.Columns)// Отключение сортировки по столбцам
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;

                foreach (DataGridViewColumn el in this.simplexMethodGrid.Columns)// Установка ширины столбцов
                    el.Width = 70;

                this.simplexMethodGrid.TopLeftHeaderCell.Value = "x(0)";// Номер самой первой таблицы

                if (this.baseView.Text == "Заданный")
                {
                    // Заполнение заголовков строк и слолбцов первой таблицы
                    for (int i = 0, columnCount = 0; i < (int)this.variableCount.Value; i++, columnCount++)
                    {
                        if (!baseVars.Contains(i + 1))
                            this.simplexMethodGrid.Columns[columnCount].HeaderText = String.Concat("x", i + 1);
                        else
                            columnCount--;
                    }
                    for (int i = 0, rowCount = 0; i < (int)this.variableCount.Value; i++, rowCount++)
                    {
                        if (baseVars.Contains(i + 1))
                            this.simplexMethodGrid.Rows[rowCount].HeaderCell.Value = String.Concat("x", i + 1);
                        else
                            rowCount--;
                    }

                    // Запонение матрицы коэффициентов
                    for (int i = 0; i < (int)this.linesCount.Value; i++)
                    {
                        for (int j = 0, columnCount = 0; j < (int)this.variableCount.Value; j++, columnCount++)
                        {
                            if (!baseVars.Contains(j + 1))
                                this.simplexMethodGrid[columnCount, i].Value = Solution.Matrix[i][j].ToString();
                            else
                                columnCount--;
                        }
                    }

                    // Заполнение правой части
                    for (int i = 0; i < (int)this.linesCount.Value; i++)
                        this.simplexMethodGrid[(int)this.variableCount.Value - (int)this.linesCount.Value, i].Value = Solution.RightPart[i].ToString();
                }
                else
                {
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
                }

                //Вычисление коэф-ов целевой функции
                for (int i = 0; i < (int)this.variableCount.Value + 1 - (int)this.linesCount.Value; i++)
                {
                    Fraction targetKoef = 0;
                    for (int j = 0; j < (int)this.linesCount.Value; j++)
                    {
                        int var = this.simplexMethodGrid.Rows[j].HeaderCell.Value.ToString()[1] - '0';
                        Fraction koef = (this.optimizeTask.Text == "Min" ? Fraction.ToFraction(this.targetFuncGrid[var - 1, 0].Value.ToString()) : -1 * Fraction.ToFraction(this.targetFuncGrid[var - 1, 0].Value.ToString()));
                        targetKoef += -1 * Fraction.ToFraction(this.simplexMethodGrid[i, j].Value.ToString()) * koef;
                    }

                    if (this.simplexMethodGrid.Columns[i].HeaderCell.Value.ToString() != "")
                    {
                        int var1 = this.simplexMethodGrid.Columns[i].HeaderCell.Value.ToString()[1] - '0';
                        this.simplexMethodGrid[i, (int)this.linesCount.Value].Value = (targetKoef +
                            (this.optimizeTask.Text == "Min" ? Fraction.ToFraction(this.targetFuncGrid[var1 - 1, 0].Value.ToString()) : -1 * Fraction.ToFraction(this.targetFuncGrid[var1 - 1, 0].Value.ToString()))).ToString();
                    }
                    else
                        this.simplexMethodGrid[i, (int)this.linesCount.Value].Value = (targetKoef +
                            (this.optimizeTask.Text == "Min" ? Fraction.ToFraction(this.targetFuncGrid[(int)this.variableCount.Value, 0].Value.ToString()) : -1 * Fraction.ToFraction(this.targetFuncGrid[(int)this.variableCount.Value, 0].Value.ToString()))).ToString();
                }

                if (this.fractionView.Text == "Десятичные")
                {
                    for (int i = 0; i < (int)variableCount.Value - (int)linesCount.Value + 1; i++)
                        for (int j = 0; j < (int)this.linesCount.Value + 1; j++)
                            this.simplexMethodGrid[i, j].Value = Fraction.ToFraction(this.simplexMethodGrid[i, j].Value.ToString()).ToDouble();
                }

                // Вернули к первоначальным значениям
                startRow = 0;
                iter = 1;

                supEl = SuportElements(this.simplexMethodGrid);// Нашли все опорные элементы
                SetColorsOnSupElements(supEl, this.simplexMethodGrid);// Раскрасили все опорные элементы
                tempVars = null;
                mainSupElement = MainSupEl(this.simplexMethodGrid);// Нахождение и закрашивание главного порного эл-та

                // Запоминание информации для первого шага симплекс метода
                simplexStepInfo.Add(new StepInformation(startRow, new List<Point>(supEl), iter, mainSupElement, countTables, null));
                this.stepBackArtificial.Enabled = false;
                this.stepForwardArtificial.Enabled = false;
                this.stepForwardSimplex.Enabled = true;
                this.stepBackSimplex.Enabled = true;
            }
        }// Пиздец тут грязь, челиксон..

        private void StepForwardSimplex_Click(object sender, EventArgs e)
        {
            if (!IsEndSimplexMethod())
            {
                if (SheckForInfinityPoint() == 1)// Если есть ребро в минус бесконечнсть
                {
                    this.answerLabel.Text = "Целевая функция не ограничена снизу";
                    this.stepForwardSimplex.Enabled = false;
                    return;
                }
                tempVars = null;
                startRow += (int)this.linesCount.Value + 3;// Строка, с которой будет начинаться каждая новая таблица
                this.simplexMethodGrid.Rows[startRow - 1].HeaderCell.Value = String.Concat("x(", countTables, ")");

                SimplexStep( this.simplexMethodGrid);
                countTables++;
                supEl = SuportElements(this.simplexMethodGrid);// Нашли все опорные элементы
                SetColorsOnSupElements(supEl, this.simplexMethodGrid);

                if (this.fractionView.Text == "Десятичные")
                {
                    for (int i = 0; i < (int)variableCount.Value - (int)linesCount.Value + 1; i++)
                        for (int j = startRow; j < startRow + (int)this.linesCount.Value + 1; j++)
                            this.simplexMethodGrid[i, j].Value = Fraction.ToFraction(this.simplexMethodGrid[i, j].Value.ToString()).ToDouble();
                }

                mainSupElement = MainSupEl(this.simplexMethodGrid);// Нахождение и закрашивание главного порного эл-та

                // Запоминание информации для очередного шага
                this.stepBackSimplex.Enabled = true;
                simplexStepInfo.Add(new StepInformation(startRow, new List<Point>(supEl), iter, mainSupElement, countTables, null));
            }
            else
            {
                this.stepForwardSimplex.Enabled = false;
                // Вывод результатов
                Fraction[] answerPoint = new Fraction[(int)this.variableCount.Value];

                // Обнуление всех свободных переменных
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

                // Запоминание значений базисных переменных
                for (int i = startRow; i < startRow + (int)this.linesCount.Value; i++)
                {
                    int freeVar = this.simplexMethodGrid.Rows[i].HeaderCell.Value.ToString()[1] - '0';
                    answerPoint[freeVar - 1] = Fraction.ToFraction(this.simplexMethodGrid[(int)variableCount.Value - (int)this.linesCount.Value, i].Value.ToString());
                }

                // Формирование строки с ответом задачи ЛП
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
        }

        private void BasisNumbersGrid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Проверка на число выбранных пользователем базисных переменных
            int count = 0;
            for (int i = 0; i < (int)this.variableCount.Value; i++)
                if (this.basisNumbersGrid.Columns[i].HeaderCell.Style.BackColor == Color.FromArgb(255, 0, 255, 0))
                    count++;
            if(count == (int)this.linesCount.Value && this.basisNumbersGrid.Columns[e.ColumnIndex].HeaderCell.Style.BackColor == Color.Empty)
            {
                MessageBox.Show("Число базисных переменных не должно превышать число ограничений");
                return;
            }

            // Если переменныя не была выбрана, то красим в зелёный, а если уже была выбрана, то убираем цвет
            if (this.basisNumbersGrid.Columns[e.ColumnIndex].HeaderCell.Style.BackColor == Color.Empty)
                this.basisNumbersGrid.Columns[e.ColumnIndex].HeaderCell.Style.BackColor = Color.FromArgb(255, 0, 255, 0);
            else
                this.basisNumbersGrid.Columns[e.ColumnIndex].HeaderCell.Style.BackColor = Color.Empty;
        }// Событие нажатия на столбец в таблице для выбора базисных переменых

        private void ArtificialBaseMethodGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex >= (int)this.variableCount.Value - iter + 1 || e.ColumnIndex < 0 ||
                e.RowIndex >= startRow + (int)this.linesCount.Value || e.RowIndex < startRow ||
                this.artificialBaseMethodGrid[e.ColumnIndex, e.RowIndex].Style.BackColor != Color.FromArgb(255, 0, 255, 0) ||
                this.artificialBaseMethodGrid[e.ColumnIndex, e.RowIndex].Style.BackColor == Color.FromArgb(255, 255, 0, 255))
                return;
            else
            {
                this.artificialBaseMethodGrid[mainSupElement.X, mainSupElement.Y].Style.BackColor = Color.FromArgb(255, 0, 255, 0);
                this.artificialBaseMethodGrid[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.FromArgb(255, 255, 0, 255);
                mainSupElement = new Point(e.ColumnIndex, e.RowIndex);

                this.artificialStepInfo[iter - 1].mainSupElement = mainSupElement;
            }
        }// Событие нажатия на ячейку в таблице метода искусс. базиса

        private void ArtificialBaseMethodGrid_SelectionChanged(object sender, EventArgs e)
        {
            this.artificialBaseMethodGrid.ClearSelection();
        }

        private void SimplexMethodGrid_SelectionChanged(object sender, EventArgs e)
        {
            this.simplexMethodGrid.ClearSelection();
        }

        private void SimplexMethodGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex >= (int)this.variableCount.Value - iter + 1 || e.ColumnIndex < 0 ||
                e.RowIndex >= startRow + (int)this.linesCount.Value || e.RowIndex < startRow ||
                this.simplexMethodGrid[e.ColumnIndex, e.RowIndex].Style.BackColor != Color.FromArgb(255, 0, 255, 0) ||
                this.simplexMethodGrid[e.ColumnIndex, e.RowIndex].Style.BackColor == Color.FromArgb(255, 255, 0, 255))
                return;
            else
            {
                this.simplexMethodGrid[mainSupElement.X, mainSupElement.Y].Style.BackColor = Color.FromArgb(255, 0, 255, 0);
                this.simplexMethodGrid[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.FromArgb(255, 255, 0, 255);
                mainSupElement = new Point(e.ColumnIndex, e.RowIndex);

                this.simplexStepInfo[countTables - 1].mainSupElement = mainSupElement;
            }
        }// Событие нажатия на ячейку в таблице симплекс метода

        private void StepBackArtificial_Click(object sender, EventArgs e)
        {
            for (int i = startRow + (int)this.linesCount.Value; i >= startRow - 1; i--)
            {
                this.artificialBaseMethodGrid.Rows.RemoveAt(i);
                this.artificialBaseMethodGrid.RowCount++;
            }

            this.artificialStepInfo.RemoveAt(this.artificialStepInfo.Count - 1);

            this.startRow = this.artificialStepInfo[this.artificialStepInfo.Count - 1].startRow;
            this.supEl = new List<Point>(this.artificialStepInfo[this.artificialStepInfo.Count - 1].supEl);
            this.iter = this.artificialStepInfo[this.artificialStepInfo.Count - 1].iter;
            this.mainSupElement = this.artificialStepInfo[this.artificialStepInfo.Count - 1].mainSupElement;
            this.countTables = this.artificialStepInfo[this.artificialStepInfo.Count - 1].countTables;
            this.tempVars = new List<String>(this.artificialStepInfo[this.artificialStepInfo.Count - 1].tempVars);

            if (this.artificialStepInfo.Count == 1)
                this.stepBackArtificial.Enabled = false;
        }

        private void StepBackSimplex_Click(object sender, EventArgs e)
        {
            this.answerLabel.Text = "";
            if (this.simplexStepInfo.Count == 1)
            {

                this.simplexStepInfo.RemoveAt(this.simplexStepInfo.Count - 1);

                this.stepBackSimplex.Enabled = false;
                this.stepForwardSimplex.Enabled = false;

                this.simplexMethodGrid.RowCount = 0;
                this.simplexMethodGrid.ColumnCount = 0;

                this.artificialBaseLabel.Text = "";

                this.stepBackArtificial.Enabled = true;
                this.stepForwardArtificial.Enabled = true;

                countTables = artificialStepInfo[artificialStepInfo.Count - 1].countTables;
                iter = artificialStepInfo[artificialStepInfo.Count - 1].iter;
                mainSupElement = artificialStepInfo[artificialStepInfo.Count - 1].mainSupElement;
                startRow = artificialStepInfo[artificialStepInfo.Count - 1].startRow;
                supEl = new List<Point>(artificialStepInfo[artificialStepInfo.Count - 1].supEl);
                tempVars = new List<String>(artificialStepInfo[artificialStepInfo.Count - 1].tempVars);
                this.tabControl.SelectedTab = this.tabPage2;
                return;
            }
            this.stepForwardSimplex.Enabled = true;
            for (int i = startRow + (int)this.linesCount.Value; i >= startRow - 1; i--)
            {
                this.simplexMethodGrid.Rows.RemoveAt(i);
                this.simplexMethodGrid.RowCount++;
            }

            this.simplexStepInfo.RemoveAt(this.simplexStepInfo.Count - 1);

            this.startRow = this.simplexStepInfo[this.simplexStepInfo.Count - 1].startRow;
            this.supEl = new List<Point>(this.simplexStepInfo[this.simplexStepInfo.Count - 1].supEl);
            this.iter = this.simplexStepInfo[this.simplexStepInfo.Count - 1].iter;
            this.mainSupElement = this.simplexStepInfo[this.simplexStepInfo.Count - 1].mainSupElement;
            this.countTables = this.simplexStepInfo[this.simplexStepInfo.Count - 1].countTables;
            this.tempVars = null;

            if (this.simplexStepInfo.Count == 1 && this.baseView.Text == "Заданный")
                this.stepBackSimplex.Enabled = false;
        }

        private void TargetFuncGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if(this.fractionView.Text =="Обыкновенные")
            {
                if (!Regex.IsMatch(e.FormattedValue.ToString(), @"(^-?\d+/\d+$)|(^$)|(^-?\d+$)"))
                {
                    MessageBox.Show("Введены некорректные данные.\nМожно вводить только целые чилса\nи обыкновенные дроби.");
                    e.Cancel = true;
                    this.targetFuncGrid.CurrentCell = this.targetFuncGrid[e.ColumnIndex, e.RowIndex];
                }
            }
            if (this.fractionView.Text == "Десятичные")
            {
                if (!Regex.IsMatch(e.FormattedValue.ToString(), @"(^-?\d+$)|(^-?\d+\.\d+$)|(^$)"))
                {
                    MessageBox.Show("Введены некорректные данные.\nМожно вводить только целые чилса\nи десятичные дроби.");
                    e.Cancel = true;
                    this.targetFuncGrid.CurrentCell = this.targetFuncGrid[e.ColumnIndex, e.RowIndex];
                }
            }
        }

        private void LinesGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (this.fractionView.Text == "Обыкновенные")
            {
                if (!Regex.IsMatch(e.FormattedValue.ToString(), @"(^-?\d+/\d+$)|(^$)|(^-?\d+$)"))
                {
                    MessageBox.Show("Введены некорректные данные.\nМожно вводить только целые чилса\nи обыкновенные дроби.");
                    e.Cancel = true;
                    this.linesGrid.CurrentCell = this.linesGrid[e.ColumnIndex, e.RowIndex];
                }
            }
            if (this.fractionView.Text == "Десятичные")
            {
                if (!Regex.IsMatch(e.FormattedValue.ToString(), @"(^-?\d+$)|(^-?\d+\.\d+$)|(^$)"))
                {
                    MessageBox.Show("Введены некорректные данные.\nМожно вводить только целые чилса\nи десятичные дроби.");
                    e.Cancel = true;
                    this.linesGrid.CurrentCell = this.linesGrid[e.ColumnIndex, e.RowIndex];
                }
            }
        }
    }
}
