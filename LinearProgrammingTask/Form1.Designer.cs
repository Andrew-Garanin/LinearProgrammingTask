
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
            this.linesGrid = new System.Windows.Forms.DataGridView();
            this.variableCount = new System.Windows.Forms.NumericUpDown();
            this.linesCount = new System.Windows.Forms.NumericUpDown();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.SolutionMode = new System.Windows.Forms.ComboBox();
            this.targetFuncGrid = new System.Windows.Forms.DataGridView();
            this.btn_OK = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.baseView = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.fractionView = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.optimizeTask = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.devepmentMethod = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.graphPictureControl = new System.Windows.Forms.PictureBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.artificialBaseMethodGrid = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.Menu = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.linesGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.variableCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.linesCount)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.targetFuncGrid)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.graphPictureControl)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.artificialBaseMethodGrid)).BeginInit();
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
            this.linesGrid.Size = new System.Drawing.Size(467, 146);
            this.linesGrid.TabIndex = 0;
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
            this.variableCount.ValueChanged += new System.EventHandler(this.variablesCount_ValueChanged);
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
            this.linesCount.ValueChanged += new System.EventHandler(this.linesCount_ValueChanged);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Location = new System.Drawing.Point(12, 45);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(858, 504);
            this.tabControl.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.SolutionMode);
            this.tabPage1.Controls.Add(this.targetFuncGrid);
            this.tabPage1.Controls.Add(this.btn_OK);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.baseView);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.fractionView);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.optimizeTask);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.devepmentMethod);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.linesCount);
            this.tabPage1.Controls.Add(this.variableCount);
            this.tabPage1.Controls.Add(this.linesGrid);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(850, 475);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Условия задачи";
            this.tabPage1.UseVisualStyleBackColor = true;
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
            // SolutionMode
            // 
            this.SolutionMode.FormattingEnabled = true;
            this.SolutionMode.Items.AddRange(new object[] {
            "Автоматический",
            "Пошаговый"});
            this.SolutionMode.Location = new System.Drawing.Point(32, 397);
            this.SolutionMode.Name = "SolutionMode";
            this.SolutionMode.Size = new System.Drawing.Size(138, 24);
            this.SolutionMode.TabIndex = 17;
            this.SolutionMode.Text = "Пошаговый";
            // 
            // targetFuncGrid
            // 
            this.targetFuncGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.targetFuncGrid.Location = new System.Drawing.Point(245, 49);
            this.targetFuncGrid.Name = "targetFuncGrid";
            this.targetFuncGrid.RowHeadersWidth = 51;
            this.targetFuncGrid.RowTemplate.Height = 24;
            this.targetFuncGrid.Size = new System.Drawing.Size(467, 86);
            this.targetFuncGrid.TabIndex = 16;
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(369, 331);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(124, 32);
            this.btn_OK.TabIndex = 15;
            this.btn_OK.Text = "ОК";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
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
            this.baseView.SelectedIndexChanged += new System.EventHandler(this.baseView_SelectedIndexChanged);
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
            this.fractionView.SelectedIndexChanged += new System.EventHandler(this.fractionView_SelectedIndexChanged);
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
            this.optimizeTask.SelectedIndexChanged += new System.EventHandler(this.optimizeTask_SelectedIndexChanged);
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
            // devepmentMethod
            // 
            this.devepmentMethod.FormattingEnabled = true;
            this.devepmentMethod.Items.AddRange(new object[] {
            "Симплекс метод",
            "Графический метод"});
            this.devepmentMethod.Location = new System.Drawing.Point(32, 165);
            this.devepmentMethod.Name = "devepmentMethod";
            this.devepmentMethod.Size = new System.Drawing.Size(138, 24);
            this.devepmentMethod.TabIndex = 7;
            this.devepmentMethod.Text = "Симплекс метод";
            this.devepmentMethod.SelectedIndexChanged += new System.EventHandler(this.devepmentMethod_SelectedIndexChanged);
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
            this.label1.Location = new System.Drawing.Point(32, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Число переменных";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.graphPictureControl);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(850, 475);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Графический метод";
            this.tabPage2.UseVisualStyleBackColor = true;
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
            this.graphPictureControl.Paint += new System.Windows.Forms.PaintEventHandler(this.graphPictureControl_Paint);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.panel2);
            this.tabPage3.Controls.Add(this.panel1);
            this.tabPage3.Controls.Add(this.artificialBaseMethodGrid);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(850, 475);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Симплекс метод";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // artificialBaseMethodGrid
            // 
            this.artificialBaseMethodGrid.AllowUserToAddRows = false;
            this.artificialBaseMethodGrid.AllowUserToDeleteRows = false;
            this.artificialBaseMethodGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.artificialBaseMethodGrid.Location = new System.Drawing.Point(3, 3);
            this.artificialBaseMethodGrid.Name = "artificialBaseMethodGrid";
            this.artificialBaseMethodGrid.ReadOnly = true;
            this.artificialBaseMethodGrid.RowHeadersWidth = 70;
            this.artificialBaseMethodGrid.RowTemplate.Height = 25;
            this.artificialBaseMethodGrid.Size = new System.Drawing.Size(844, 379);
            this.artificialBaseMethodGrid.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(882, 30);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // Menu
            // 
            this.Menu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemSaveAs,
            this.MenuItemOpen});
            this.Menu.Name = "Menu";
            this.Menu.Size = new System.Drawing.Size(59, 26);
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
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Magenta;
            this.panel1.Location = new System.Drawing.Point(593, 395);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(21, 21);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Lime;
            this.panel2.Location = new System.Drawing.Point(593, 423);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(21, 21);
            this.panel2.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(637, 399);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(197, 17);
            this.label8.TabIndex = 3;
            this.label8.Text = "Выбранный опорный элеент";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(637, 427);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(205, 17);
            this.label9.TabIndex = 3;
            this.label9.Text = "Возможный опорный элемент";
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(882, 561);
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
            ((System.ComponentModel.ISupportInitialize)(this.targetFuncGrid)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.graphPictureControl)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.artificialBaseMethodGrid)).EndInit();
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
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.NumericUpDown linesCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox graphPictureControl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox devepmentMethod;
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
        private System.Windows.Forms.ComboBox SolutionMode;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView artificialBaseMethodGrid;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
    }
}

