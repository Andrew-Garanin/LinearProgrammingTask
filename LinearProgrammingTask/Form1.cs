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

        private void devepmentMethod_SelectedIndexChanged(object sender, EventArgs e)
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
            this.tabControl.SelectedTab = this.tabPage3;// Переключение на вкладку симплекс метода
            this.artificialBaseMethodGrid.ColumnCount = 20;
            this.artificialBaseMethodGrid.RowCount = 100;
            // Ебаная хуета другого выхода я не нашел(
            foreach (DataGridViewColumn el in this.artificialBaseMethodGrid.Columns)
                el.Width = 70;
            this.artificialBaseMethodGrid.TopLeftHeaderCell.Value = "x(0)";

            // Заполение заголовков строк и столбцов
            for(int i=0;i<(int)this.variableCount.Value;i++)
                this.artificialBaseMethodGrid.Columns[i].HeaderText = String.Concat("x", (i+1).ToString());
            for(int i =0;i<(int)this.linesCount.Value;i++)
                this.artificialBaseMethodGrid.Rows[i].HeaderCell.Value = String.Concat("x", (i + 1 + (int)this.variableCount.Value).ToString());

            // Заполнение матрицы коэфициентов
            for (int i = 0; i < (int)this.linesCount.Value; i++)
                for (int j = 0; j < (int)this.variableCount.Value+1; j++)
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
                this.artificialBaseMethodGrid[i,(int)this.linesCount.Value].Value = tmp.ToString();
            }

            // Получение таблицы в которой остались переменные из начальной задачи(тут начинается цикл)

            // Нахождение всех возможных опорных элементов

            List<Point> supEl = suportElements(0);// Нашли все опорные элементы (в самой первой таблице)
            for (int i = 0; i < (int)this.linesCount.Value; i++)
                for(int j = 0; j < (int)this.variableCount.Value; j++)
                    if(supEl.Contains(new Point(j,i)))
                        this.artificialBaseMethodGrid[j,i].Style.BackColor = Color.FromArgb(255, 0, 255, 0);// Закрашивание ячейки с опорным элементом

            int startRow = (int)this.linesCount.Value + 2;// Строка, с которой будет начинаться каждая новая таблица
            this.artificialBaseMethodGrid.Rows[startRow].HeaderCell.Value="x(1)";
        }

        private List<Point> suportElements(int startRow)
        {
            List<Point> supElements = new List<Point>();
            for(int i=0;i<(int)this.variableCount.Value;i++)
            {
                if(Fraction.ToFraction(this.artificialBaseMethodGrid[i,startRow+(int)this.linesCount.Value].Value.ToString())<0)
                {
                    Fraction min = Int32.MaxValue;
                    // Поиск минимума по столбцу
                    for (int j=startRow;j<startRow+(int)this.linesCount.Value;j++)
                    {
                        if (Fraction.ToFraction(this.artificialBaseMethodGrid[i, j].Value.ToString()) > 0)
                        {
                            if (Fraction.ToFraction(this.artificialBaseMethodGrid[(int)this.variableCount.Value, j].Value.ToString()) / Fraction.ToFraction(this.artificialBaseMethodGrid[i, j].Value.ToString()) < min)
                                min = Fraction.ToFraction(this.artificialBaseMethodGrid[(int)this.variableCount.Value, j].Value.ToString()) / Fraction.ToFraction(this.artificialBaseMethodGrid[i, j].Value.ToString());
                        }
                    }

                    // Записываем все возможные элементы в столбце
                    for (int j = startRow; j < startRow + (int)this.linesCount.Value; j++)
                        if (Fraction.ToFraction(this.artificialBaseMethodGrid[i, j].Value.ToString())!=0 && Fraction.ToFraction(this.artificialBaseMethodGrid[(int)this.variableCount.Value, j].Value.ToString()) / Fraction.ToFraction(this.artificialBaseMethodGrid[i, j].Value.ToString()) == min)
                            supElements.Add(new Point(i,j));
                }
            }
            return supElements;
        }// Поиск всех возможных опорных элементов

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
    }
}
