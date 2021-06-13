using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Graph_Method
{
    //структура, содержащая информацию о линии
    public struct Line
    {
        public double A;            // коэффициенты
        public double B;            // в уравнении прямой
        public double C;            // Ax + By + C = 0
        public int InequalitySign;  // знак в неравенстве Ax + By ? -C: если -1, то <=; если 1, то >=; для целевой функции: -1 - ищем минимум, 1 - ищем максимум
        public Boolean HelpLine;    // признак вспомогательной прямой
        public Boolean IsPaint;     // признак необходимости рисования линии
    }
    //структура, содержащая информацию о точке пересечения прямых
    public struct CrossPoint
    {
        public double X;            // координата X
        public double Y;            // координата Y
        public List<int> ListLines; // список линий (индексов в списке), пересекающих эту точку
    }

    class GraphicalMethod
    {
        private int Q_Points=0;                //количество найденных точек пересечения прямых
        private PaintEventArgs e;              //объект, предоставляющий функцию рисования
        private int ScaleIntervalX;            //цена деления по оси X
        private int ScaleIntervalY;            //цена деления по оси Y
        private const double EPS = 1e-9;       //погрешность сравнения двух чисел double для признания их равными
        private int Q_Lines;                   //количество переданных линий в объект
        private List<Line> LinesList;          //список переданных линий
        private List<CrossPoint> PointsList;   //список точек пересечения прямых
        private double WidthUnit;              //ширина области в единицах
        private double HeightUnit;             //высота области в единицах
        private double X0_Unit;                //начало координат по оси X в единицах относительно левого нижнего угла
        private double Y0_Unit;                //начало координат по оси Y в единицах относительно левого нижнего угла
        private Line ObjFunction;              //целевая функция

        public Line ObjectFunction { get { return ObjFunction; } }          //доступ к целевой функции
        public List<CrossPoint> Point_List { get { return PointsList; } }   //доступ к списку точек
        public double Xmax { get; set; }       //максимальная координада по оси X для точки, принадлежащей искомой области
        public double Ymax { get; set; }       //максимальная координада по оси Y для точки, принадлежащей искомой области

        public GraphicalMethod()
        {
            Q_Lines = 0;
            LinesList = new List<Line>();
            PointsList = new List<CrossPoint>();
            ObjFunction = new Line();
        }

        //инициализация объектов, необходимых для рисования
        public void InitPaintFunction(PaintEventArgs E, int scale_interval_x, int scale_interval_y, double width_unit, double height_unit, double x0_unit, double y0_unit)
        {
            e = E;
            ScaleIntervalX = scale_interval_x;
            ScaleIntervalY = scale_interval_y;
            WidthUnit = width_unit;
            HeightUnit = height_unit;
            X0_Unit = x0_unit;
            Y0_Unit = y0_unit;
        }

        //добавление линии в список
        /// <summary>
        /// добавление линии вида AX + BY + C = 0 в список неравенств
        /// </summary>
        /// <param name="a">коэффициент A уравнения</param>
        /// <param name="b">коэффициент B уравнения</param>
        /// <param name="c">коэффициент C уравнения</param>
        /// <param name="help_line">признак вспомогательной линии (необходимой для поиска решений в крайних ситуациях)</param>
        /// <param name="is_paint">признак необходимости рисовать линию</param>
        /// <param name="inequality_sign">знак в неравенстве Ax + By ? -C: если -1, то <=; если 1, то >=</param>
        public void AddLine(double a, double b, double c, Boolean help_line, Boolean is_paint, int inequality_sign)
        {
            LinesList.Add(new Line() { A=a, B=b, C=c, HelpLine=help_line, IsPaint=is_paint, InequalitySign=inequality_sign });
            Q_Lines++;
        }

        //определение целевой функции
        /// <summary>
        /// определение уравнения AX + BY + C = 0 целевой функции
        /// </summary>
        /// <param name="a">коэффициент A уравнения</param>
        /// <param name="b">коэффициент B уравнения</param>
        /// <param name="c">коэффициент C уравнения</param>
        /// <param name="min_max">вид поиска на области определения: -1 - ищем минимум, 1 - ищем максимум</param>
        public void SetObjectFunction(double a, double b, double c, int min_max)
        {
            ObjFunction.A = a;
            ObjFunction.B = b;
            ObjFunction.C = c;
            ObjFunction.InequalitySign = min_max;
        }

        //поиск всех точек пересечения прямых
        public void FindAllCrossPoint()
        {
            int I1, I2, IPoint;
            double x, y, D, D1, D2;
            PointsList.Clear();
            Q_Points = 0;
            Xmax = 0;
            Ymax = 0;
            for (I1 = 0; I1 < Q_Lines; I1++)           // проход по всем линиям списка
                for (I2 = I1+1; I2 < Q_Lines; I2++)
                {
                    D = LinesList[I1].A * LinesList[I2].B - LinesList[I2].A * LinesList[I1].B;
                    D1 = LinesList[I1].C * LinesList[I2].B - LinesList[I2].C * LinesList[I1].B;
                    D2 = LinesList[I1].A * LinesList[I2].C - LinesList[I2].A * LinesList[I1].C;
                    if (Math.Abs(D)>EPS)    //если определитель D не равен 0
                    {
                        x = -D1 / D;
                        y = -D2 / D;
                        if (InArea(x,y))                           //точка принадлежит области
                        {
                            IPoint = FindPointIndex(x, y);
                            if (IPoint ==- 1)                      //точка в списке не найдена
                            {
                                PointsList.Add(new CrossPoint() { X = x, Y = y, ListLines = new List<int>()});
                                PointsList[Q_Points].ListLines.Add(I1);
                                PointsList[Q_Points].ListLines.Add(I2);
                                Ymax = Math.Max(Ymax, y);
                                Xmax = Math.Max(Xmax, x);
                                Q_Points++;
                            }
                            else
                            {
                                PointsList[IPoint].ListLines.Add(I2);
                            }
                        }
                    }
                }
        }

        //проверка на принадлежность точки области, определенной прямыми (неравенствами)
        private Boolean InArea(double x, double y)
        {
            int I;
            for (I = 0; I < Q_Lines; I++)       // проход по всем линиям списка
            {
                if (LinesList[I].InequalitySign==-1 && LinesList[I].A * x + LinesList[I].B * y > -LinesList[I].C ||
                    LinesList[I].InequalitySign == 1 && LinesList[I].A * x + LinesList[I].B * y < -LinesList[I].C)  //если точка не принадлежит области
                    return false;
            }
            return true;
        }

        //поиск искомой точки (min/max) для целевой функции
        public int FindObjectPoint()
        {
            int point, point_result, line, q_lines;
            double fun_result, cur_result;
            if (Q_Points == 0) return -1;       //если точек для искомой области не существует
            point_result = 0;
            fun_result = ObjFunction.A * PointsList[0].X + ObjFunction.B * PointsList[0].Y + ObjFunction.C;  //значение функции в первой точке
            for (point = 1; point < Q_Points; point++)
            {
                cur_result = ObjFunction.A * PointsList[point].X + ObjFunction.B * PointsList[point].Y + ObjFunction.C;
                if (ObjFunction.InequalitySign == -1 && cur_result < fun_result || ObjFunction.InequalitySign == 1 && cur_result > fun_result)
                {
                    point_result = point;
                    fun_result = cur_result;
                }
            }
            for (line = 0, q_lines = 0; line < PointsList[point_result].ListLines.Count; line++)
                if (!LinesList[PointsList[point_result].ListLines[line]].HelpLine) q_lines++;
            if (q_lines < 2)        //если в точке пересечения меньше 2 основных линий
                point_result = -1;
            return point_result;
        }

        //рисование линии по уравнению
        public void PaintLine(Line line, Pen drawPen)
        {
            double X1, Y1, X2, Y2;
            double X_min, X_max, Y_min, Y_max;
            X_min = -X0_Unit - 1;
            X_max = WidthUnit - X0_Unit + 1;
            Y_min = -Y0_Unit - 1;
            Y_max = HeightUnit - Y0_Unit + 1;
            if (line.B != 0.0D)
            {
                X1 = X_min;
                X2 = X_max;
                Y1 = (-line.C - line.A * X1) / line.B;
                Y2 = (-line.C - line.A * X2) / line.B;
            }
            else
            {
                Y1 = Y_min;
                Y2 = Y_max;
                X1 = (-line.C - line.B * Y1) / line.A;
                X2 = (-line.C - line.B * Y2) / line.A;
            }
            e.Graphics.DrawLine(drawPen, (int)Math.Round(X1 * ScaleIntervalX, 0), (int)Math.Round(-Y1 * ScaleIntervalY, 0), (int)Math.Round(X2 * ScaleIntervalX, 0), (int)Math.Round(-Y2 * ScaleIntervalY, 0));
        }

        //рисование всех линий
        public void PaintAllLines(Pen drawPen)
        {
            int I;
            double X1, Y1, X2, Y2;
            double X_min, X_max, Y_min, Y_max;
            X_min = -X0_Unit - 1;
            X_max = WidthUnit - X0_Unit + 1;
            Y_min = -Y0_Unit - 1;
            Y_max = HeightUnit - Y0_Unit + 1;
            for (I=0; I<Q_Lines; I++)       // проход по всем линиям списка
            {
                if (LinesList[I].IsPaint)   // если линию рисовать нужно
                {
                    if (LinesList[I].B != 0.0D)
                    {
                        X1 = X_min;
                        X2 = X_max;
                        Y1 = (-LinesList[I].C - LinesList[I].A * X1) / LinesList[I].B;
                        Y2 = (-LinesList[I].C - LinesList[I].A * X2) / LinesList[I].B;
                    }
                    else
                    {
                        Y1 = Y_min;
                        Y2 = Y_max;
                        X1 = (-LinesList[I].C - LinesList[I].B * Y1) / LinesList[I].A;
                        X2 = (-LinesList[I].C - LinesList[I].B * Y2) / LinesList[I].A;
                    }
                    e.Graphics.DrawLine(drawPen, (int)Math.Round(X1 * ScaleIntervalX, 0), (int)Math.Round(-Y1 * ScaleIntervalY, 0), (int)Math.Round(X2 * ScaleIntervalX, 0), (int)Math.Round(-Y2 * ScaleIntervalY, 0));
                }
            }
        }

        //рисование всех точек списка (принадлежащих искомой области)
        public void PaintAllPoints(SolidBrush ColorBrush)
        {
            int I;
            double X, Y;
            for (I = 0; I < Q_Points; I++)       // проход по всем точкам списка
            {
                X = PointsList[I].X;
                Y = PointsList[I].Y;
                e.Graphics.FillEllipse(ColorBrush, (int)Math.Round(X * ScaleIntervalX, 0)-3, (int)Math.Round(-Y * ScaleIntervalY, 0)-3, 6, 6);
            }
        }

        //закрашивание нужной области
        public void FillArea(SolidBrush ColorBrush)
        {
            int Cnt, IPoint, IPoint1, IPoint2, IPoint_Prev;
            int Line1, Line2;

            Point[] curvePoints = new Point[PointsList.Count];
            curvePoints[0] = new Point((int)Math.Round(PointsList[0].X * ScaleIntervalX), (int)Math.Round(-PointsList[0].Y * ScaleIntervalY, 0));
            IPoint1 = 0;                                 //первая точка закрашиваемой области
            IPoint_Prev = -1;
            for (Cnt=1; Cnt<PointsList.Count; Cnt++)     //щетчик точек массива для закрашивания области
            {
                IPoint = -1;                             //следующая точка не подобрана
                for (IPoint2 = 0; IPoint2 < PointsList.Count && IPoint == -1; IPoint2++)    //для очередной точки подыскиваем следующую
                    if (IPoint1 != IPoint2)
                    {
                        for (Line1=0; Line1<PointsList[IPoint1].ListLines.Count && IPoint == -1; Line1++)          //просматриваем линии, проходящие через первую точку очередной пары
                            for (Line2 = 0; Line2 < PointsList[IPoint2].ListLines.Count && IPoint == -1; Line2++)  //просматриваем линии, проходящие через вторую точку очередной пары
                                if (PointsList[IPoint1].ListLines[Line1] == PointsList[IPoint2].ListLines[Line2] && IPoint2 != IPoint_Prev)
                                {
                                    IPoint = IPoint2;
                                }
                    }
                curvePoints[Cnt] = new Point((int)Math.Round(PointsList[IPoint].X * ScaleIntervalX), (int)Math.Round(-PointsList[IPoint].Y * ScaleIntervalY, 0));
                IPoint_Prev = IPoint1;
                IPoint1 = IPoint;
            }
            e.Graphics.FillPolygon(ColorBrush, curvePoints);
        }

        //поиск номера точки в списке по координатам
        public int FindPointIndex(double x, double y)
        {
            return PointsList.FindIndex(p => Math.Abs(p.X-x)<EPS && Math.Abs(p.Y - y) < EPS);
        }
        
        //поиск номера линии в списке по коэффициентам
        public int FindLineIndex(double a, double b, double c)
        {
            return LinesList.FindIndex(l => Math.Abs(l.A - a) < EPS && Math.Abs(l.B - b) < EPS && Math.Abs(l.C - c) < EPS);
        }
    }
}
