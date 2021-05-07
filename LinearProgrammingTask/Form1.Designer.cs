
namespace LinearProgrammingTask
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.linesGrid = new System.Windows.Forms.DataGridView();
            this.variableCount = new System.Windows.Forms.NumericUpDown();
            this.linesCount = new System.Windows.Forms.NumericUpDown();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.basisNumbersLabel = new System.Windows.Forms.Label();
            this.basisNumbersGrid = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.solutionMode = new System.Windows.Forms.ComboBox();
            this.targetFuncGrid = new System.Windows.Forms.DataGridView();
            this.btn_OK = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.baseView = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.fractionView = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.optimizeTask = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.developmentMethod = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.stepBackArtificial = new System.Windows.Forms.Button();
            this.stepForwardArtificial = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.artificialBaseLabel = new System.Windows.Forms.Label();
            this.artificialBaseMethodGrid = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.stepBackSimplex = new System.Windows.Forms.Button();
            this.stepForwardSimplex = new System.Windows.Forms.Button();
            this.answerLabel = new System.Windows.Forms.Label();
            this.simplexMethodGrid = new System.Windows.Forms.DataGridView();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.graphPictureControl = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.Menu = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.linesGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.variableCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.linesCount)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.basisNumbersGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.targetFuncGrid)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.artificialBaseMethodGrid)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.simplexMethodGrid)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.graphPictureControl)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // linesGrid
            // 
            this.linesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.linesGrid.Location = new System.Drawing.Point(245, 141);
            this.linesGrid.Name = "linesGrid";
            this.linesGrid.RowHeadersWidth = 51;
            this.linesGrid.RowTemplate.Height = 24;
            this.linesGrid.Size = new System.Drawing.Size(715, 146);
            this.linesGrid.TabIndex = 0;
            this.linesGrid.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.LinesGrid_CellValidating);
            // 
            // variableCount
            // 
            this.variableCount.Location = new System.Drawing.Point(32, 49);
            this.variableCount.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.variableCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.variableCount.Name = "variableCount";
            this.variableCount.Size = new System.Drawing.Size(138, 22);
            this.variableCount.TabIndex = 3;
            this.variableCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.variableCount.ValueChanged += new System.EventHandler(this.VariablesCount_ValueChanged);
            // 
            // linesCount
            // 
            this.linesCount.Location = new System.Drawing.Point(32, 103);
            this.linesCount.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.linesCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.linesCount.Name = "linesCount";
            this.linesCount.Size = new System.Drawing.Size(138, 22);
            this.linesCount.TabIndex = 4;
            this.linesCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.linesCount.ValueChanged += new System.EventHandler(this.LinesCount_ValueChanged);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Controls.Add(this.tabPage4);
            this.tabControl.Location = new System.Drawing.Point(12, 31);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(988, 529);
            this.tabControl.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.basisNumbersLabel);
            this.tabPage1.Controls.Add(this.basisNumbersGrid);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.solutionMode);
            this.tabPage1.Controls.Add(this.targetFuncGrid);
            this.tabPage1.Controls.Add(this.btn_OK);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.baseView);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.fractionView);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.optimizeTask);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.developmentMethod);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.linesCount);
            this.tabPage1.Controls.Add(this.variableCount);
            this.tabPage1.Controls.Add(this.linesGrid);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(980, 500);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Условия задачи";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // basisNumbersLabel
            // 
            this.basisNumbersLabel.AutoSize = true;
            this.basisNumbersLabel.Location = new System.Drawing.Point(242, 299);
            this.basisNumbersLabel.Name = "basisNumbersLabel";
            this.basisNumbersLabel.Size = new System.Drawing.Size(189, 17);
            this.basisNumbersLabel.TabIndex = 20;
            this.basisNumbersLabel.Text = "Номера базисных векторов";
            // 
            // basisNumbersGrid
            // 
            this.basisNumbersGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.basisNumbersGrid.Location = new System.Drawing.Point(245, 319);
            this.basisNumbersGrid.Name = "basisNumbersGrid";
            this.basisNumbersGrid.RowHeadersWidth = 51;
            this.basisNumbersGrid.RowTemplate.Height = 24;
            this.basisNumbersGrid.Size = new System.Drawing.Size(715, 44);
            this.basisNumbersGrid.TabIndex = 19;
            this.basisNumbersGrid.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.BasisNumbersGrid_ColumnHeaderMouseClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(32, 377);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(114, 17);
            this.label7.TabIndex = 18;
            this.label7.Text = "Режим решения";
            // 
            // solutionMode
            // 
            this.solutionMode.FormattingEnabled = true;
            this.solutionMode.Items.AddRange(new object[] {
            "Автоматический",
            "Пошаговый"});
            this.solutionMode.Location = new System.Drawing.Point(32, 397);
            this.solutionMode.Name = "solutionMode";
            this.solutionMode.Size = new System.Drawing.Size(138, 24);
            this.solutionMode.TabIndex = 17;
            this.solutionMode.Text = "Пошаговый";
            // 
            // targetFuncGrid
            // 
            this.targetFuncGrid.AllowUserToAddRows = false;
            this.targetFuncGrid.AllowUserToDeleteRows = false;
            this.targetFuncGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.targetFuncGrid.Location = new System.Drawing.Point(245, 49);
            this.targetFuncGrid.MultiSelect = false;
            this.targetFuncGrid.Name = "targetFuncGrid";
            this.targetFuncGrid.RowHeadersWidth = 51;
            this.targetFuncGrid.RowTemplate.Height = 24;
            this.targetFuncGrid.Size = new System.Drawing.Size(715, 86);
            this.targetFuncGrid.TabIndex = 16;
            this.targetFuncGrid.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.TargetFuncGrid_CellValidating);
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(426, 452);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(124, 32);
            this.btn_OK.TabIndex = 15;
            this.btn_OK.Text = "ОК";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.Btn_OK_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(32, 319);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 17);
            this.label6.TabIndex = 14;
            this.label6.Text = "Базис";
            // 
            // baseView
            // 
            this.baseView.FormattingEnabled = true;
            this.baseView.Items.AddRange(new object[] {
            "Искуственный",
            "Заданный"});
            this.baseView.Location = new System.Drawing.Point(32, 339);
            this.baseView.Name = "baseView";
            this.baseView.Size = new System.Drawing.Size(138, 24);
            this.baseView.TabIndex = 13;
            this.baseView.Text = "Искуственный";
            this.baseView.SelectedIndexChanged += new System.EventHandler(this.BaseView_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 257);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 17);
            this.label5.TabIndex = 12;
            this.label5.Text = "Вид дробей";
            // 
            // fractionView
            // 
            this.fractionView.FormattingEnabled = true;
            this.fractionView.Items.AddRange(new object[] {
            "Обыкновенные",
            "Десятичные"});
            this.fractionView.Location = new System.Drawing.Point(32, 277);
            this.fractionView.Name = "fractionView";
            this.fractionView.Size = new System.Drawing.Size(138, 24);
            this.fractionView.TabIndex = 11;
            this.fractionView.Text = "Обыкновенные";
            this.fractionView.SelectedIndexChanged += new System.EventHandler(this.FractionView_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 199);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(148, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Задача оптимизации";
            // 
            // optimizeTask
            // 
            this.optimizeTask.FormattingEnabled = true;
            this.optimizeTask.Items.AddRange(new object[] {
            "Min",
            "Max"});
            this.optimizeTask.Location = new System.Drawing.Point(32, 219);
            this.optimizeTask.Name = "optimizeTask";
            this.optimizeTask.Size = new System.Drawing.Size(138, 24);
            this.optimizeTask.TabIndex = 9;
            this.optimizeTask.Text = "Min";
            this.optimizeTask.SelectedIndexChanged += new System.EventHandler(this.OptimizeTask_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Метод решения";
            // 
            // developmentMethod
            // 
            this.developmentMethod.FormattingEnabled = true;
            this.developmentMethod.Items.AddRange(new object[] {
            "Симплекс метод",
            "Графический метод"});
            this.developmentMethod.Location = new System.Drawing.Point(32, 165);
            this.developmentMethod.Name = "developmentMethod";
            this.developmentMethod.Size = new System.Drawing.Size(138, 24);
            this.developmentMethod.TabIndex = 7;
            this.developmentMethod.Text = "Симплекс метод";
            this.developmentMethod.SelectedIndexChanged += new System.EventHandler(this.DevelopmentMethod_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Число ограничений";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.label1.Location = new System.Drawing.Point(32, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Число переменных";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.stepBackArtificial);
            this.tabPage2.Controls.Add(this.stepForwardArtificial);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.panel3);
            this.tabPage2.Controls.Add(this.panel4);
            this.tabPage2.Controls.Add(this.artificialBaseLabel);
            this.tabPage2.Controls.Add(this.artificialBaseMethodGrid);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(980, 500);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "Метод искусственного базиса";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // stepBackArtificial
            // 
            this.stepBackArtificial.Location = new System.Drawing.Point(675, 243);
            this.stepBackArtificial.Name = "stepBackArtificial";
            this.stepBackArtificial.Size = new System.Drawing.Size(146, 70);
            this.stepBackArtificial.TabIndex = 13;
            this.stepBackArtificial.Text = "Шаг назад";
            this.stepBackArtificial.UseVisualStyleBackColor = true;
            this.stepBackArtificial.Click += new System.EventHandler(this.StepBackArtificial_Click);
            // 
            // stepForwardArtificial
            // 
            this.stepForwardArtificial.Location = new System.Drawing.Point(675, 138);
            this.stepForwardArtificial.Name = "stepForwardArtificial";
            this.stepForwardArtificial.Size = new System.Drawing.Size(146, 72);
            this.stepForwardArtificial.TabIndex = 12;
            this.stepForwardArtificial.Text = "Шаг вперёд";
            this.stepForwardArtificial.UseVisualStyleBackColor = true;
            this.stepForwardArtificial.Click += new System.EventHandler(this.StepForwardArtificial_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(71, 470);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(205, 17);
            this.label12.TabIndex = 10;
            this.label12.Text = "Возможный опорный элемент";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(71, 442);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(197, 17);
            this.label13.TabIndex = 11;
            this.label13.Text = "Выбранный опорный элеент";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Lime;
            this.panel3.Location = new System.Drawing.Point(27, 466);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(21, 21);
            this.panel3.TabIndex = 9;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Magenta;
            this.panel4.Location = new System.Drawing.Point(27, 438);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(21, 21);
            this.panel4.TabIndex = 8;
            // 
            // artificialBaseLabel
            // 
            this.artificialBaseLabel.AutoSize = true;
            this.artificialBaseLabel.Location = new System.Drawing.Point(614, 453);
            this.artificialBaseLabel.Name = "artificialBaseLabel";
            this.artificialBaseLabel.Size = new System.Drawing.Size(0, 17);
            this.artificialBaseLabel.TabIndex = 7;
            // 
            // artificialBaseMethodGrid
            // 
            this.artificialBaseMethodGrid.AllowUserToAddRows = false;
            this.artificialBaseMethodGrid.AllowUserToDeleteRows = false;
            this.artificialBaseMethodGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.artificialBaseMethodGrid.DefaultCellStyle = dataGridViewCellStyle1;
            this.artificialBaseMethodGrid.Location = new System.Drawing.Point(27, 46);
            this.artificialBaseMethodGrid.MultiSelect = false;
            this.artificialBaseMethodGrid.Name = "artificialBaseMethodGrid";
            this.artificialBaseMethodGrid.ReadOnly = true;
            this.artificialBaseMethodGrid.RowHeadersWidth = 70;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.artificialBaseMethodGrid.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.artificialBaseMethodGrid.RowTemplate.Height = 25;
            this.artificialBaseMethodGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.artificialBaseMethodGrid.Size = new System.Drawing.Size(515, 379);
            this.artificialBaseMethodGrid.TabIndex = 0;
            this.artificialBaseMethodGrid.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.ArtificialBaseMethodGrid_CellMouseClick);
            this.artificialBaseMethodGrid.SelectionChanged += new System.EventHandler(this.ArtificialBaseMethodGrid_SelectionChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.stepBackSimplex);
            this.tabPage3.Controls.Add(this.stepForwardSimplex);
            this.tabPage3.Controls.Add(this.answerLabel);
            this.tabPage3.Controls.Add(this.simplexMethodGrid);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.panel2);
            this.tabPage3.Controls.Add(this.panel1);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(980, 500);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Симплекс метод";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // stepBackSimplex
            // 
            this.stepBackSimplex.Location = new System.Drawing.Point(540, 234);
            this.stepBackSimplex.Name = "stepBackSimplex";
            this.stepBackSimplex.Size = new System.Drawing.Size(146, 70);
            this.stepBackSimplex.TabIndex = 15;
            this.stepBackSimplex.Text = "Шаг назад";
            this.stepBackSimplex.UseVisualStyleBackColor = true;
            this.stepBackSimplex.Click += new System.EventHandler(this.StepBackSimplex_Click);
            // 
            // stepForwardSimplex
            // 
            this.stepForwardSimplex.Location = new System.Drawing.Point(540, 129);
            this.stepForwardSimplex.Name = "stepForwardSimplex";
            this.stepForwardSimplex.Size = new System.Drawing.Size(146, 72);
            this.stepForwardSimplex.TabIndex = 14;
            this.stepForwardSimplex.Text = "Шаг вперёд";
            this.stepForwardSimplex.UseVisualStyleBackColor = true;
            this.stepForwardSimplex.Click += new System.EventHandler(this.StepForwardSimplex_Click);
            // 
            // answerLabel
            // 
            this.answerLabel.AutoSize = true;
            this.answerLabel.Location = new System.Drawing.Point(686, 440);
            this.answerLabel.Name = "answerLabel";
            this.answerLabel.Size = new System.Drawing.Size(0, 17);
            this.answerLabel.TabIndex = 8;
            // 
            // simplexMethodGrid
            // 
            this.simplexMethodGrid.AllowUserToAddRows = false;
            this.simplexMethodGrid.AllowUserToDeleteRows = false;
            this.simplexMethodGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.simplexMethodGrid.Location = new System.Drawing.Point(15, 39);
            this.simplexMethodGrid.Name = "simplexMethodGrid";
            this.simplexMethodGrid.ReadOnly = true;
            this.simplexMethodGrid.RowHeadersWidth = 70;
            this.simplexMethodGrid.RowTemplate.Height = 25;
            this.simplexMethodGrid.Size = new System.Drawing.Size(420, 379);
            this.simplexMethodGrid.TabIndex = 5;
            this.simplexMethodGrid.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.SimplexMethodGrid_CellMouseClick);
            this.simplexMethodGrid.SelectionChanged += new System.EventHandler(this.SimplexMethodGrid_SelectionChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(59, 465);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(205, 17);
            this.label9.TabIndex = 3;
            this.label9.Text = "Возможный опорный элемент";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(59, 437);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(197, 17);
            this.label8.TabIndex = 3;
            this.label8.Text = "Выбранный опорный элеент";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Lime;
            this.panel2.Location = new System.Drawing.Point(15, 461);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(21, 21);
            this.panel2.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Magenta;
            this.panel1.Location = new System.Drawing.Point(15, 433);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(21, 21);
            this.panel1.TabIndex = 1;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.graphPictureControl);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(980, 500);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Графический метод";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // graphPictureControl
            // 
            this.graphPictureControl.BackColor = System.Drawing.Color.PeachPuff;
            this.graphPictureControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.graphPictureControl.Location = new System.Drawing.Point(313, 22);
            this.graphPictureControl.Name = "graphPictureControl";
            this.graphPictureControl.Size = new System.Drawing.Size(400, 400);
            this.graphPictureControl.TabIndex = 7;
            this.graphPictureControl.TabStop = false;
            this.graphPictureControl.Paint += new System.Windows.Forms.PaintEventHandler(this.GraphPictureControl_Paint);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1007, 28);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // Menu
            // 
            this.Menu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemSaveAs,
            this.MenuItemOpen});
            this.Menu.Name = "Menu";
            this.Menu.Size = new System.Drawing.Size(59, 24);
            this.Menu.Text = "Файл";
            // 
            // MenuItemSaveAs
            // 
            this.MenuItemSaveAs.Name = "MenuItemSaveAs";
            this.MenuItemSaveAs.Size = new System.Drawing.Size(201, 26);
            this.MenuItemSaveAs.Text = "Сохранить как...";
            this.MenuItemSaveAs.Click += new System.EventHandler(this.MenuItemSaveAs_Click);
            // 
            // MenuItemOpen
            // 
            this.MenuItemOpen.Name = "MenuItemOpen";
            this.MenuItemOpen.Size = new System.Drawing.Size(201, 26);
            this.MenuItemOpen.Text = "Открыть...";
            this.MenuItemOpen.Click += new System.EventHandler(this.MenuItemOpen_Click);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1007, 567);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Text = "Задача линейного программирования";
            ((System.ComponentModel.ISupportInitialize)(this.linesGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.variableCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.linesCount)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.basisNumbersGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.targetFuncGrid)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.artificialBaseMethodGrid)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.simplexMethodGrid)).EndInit();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.graphPictureControl)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView linesGrid;
        private System.Windows.Forms.NumericUpDown variableCount;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.NumericUpDown linesCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox graphPictureControl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox developmentMethod;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox optimizeTask;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox fractionView;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox baseView;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem Menu;
        private System.Windows.Forms.ToolStripMenuItem MenuItemSaveAs;
        private System.Windows.Forms.DataGridView targetFuncGrid;
        private System.Windows.Forms.ToolStripMenuItem MenuItemOpen;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox solutionMode;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView artificialBaseMethodGrid;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView simplexMethodGrid;
        private System.Windows.Forms.Label artificialBaseLabel;
        private System.Windows.Forms.Label answerLabel;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button stepBackArtificial;
        private System.Windows.Forms.Button stepForwardArtificial;
        private System.Windows.Forms.Button stepBackSimplex;
        private System.Windows.Forms.Button stepForwardSimplex;
        private System.Windows.Forms.DataGridView basisNumbersGrid;
        private System.Windows.Forms.Label basisNumbersLabel;
    }
}

