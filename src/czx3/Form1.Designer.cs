namespace czx3
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            menuStrip1 = new MenuStrip();
            文件ToolStripMenuItem = new ToolStripMenuItem();
            menuOpenWKSFile = new ToolStripMenuItem();
            mnusave = new ToolStripMenuItem();
            mnusaveworkspace = new ToolStripMenuItem();
            mnuOutputMapAsFie = new ToolStripMenuItem();
            数据源ToolStripMenuItem = new ToolStripMenuItem();
            menuOpenSQLDS = new ToolStripMenuItem();
            menuCreateSQLDS = new ToolStripMenuItem();
            数据集ToolStripMenuItem = new ToolStripMenuItem();
            menuCopyDataset = new ToolStripMenuItem();
            空间数据表达ToolStripMenuItem = new ToolStripMenuItem();
            menuLayerStyle = new ToolStripMenuItem();
            mnuUniqueTheme = new ToolStripMenuItem();
            mnuLabelTheme = new ToolStripMenuItem();
            数据表达ToolStripMenuItem = new ToolStripMenuItem();
            mnuThemeRange = new ToolStripMenuItem();
            mnuThemeDotDensity = new ToolStripMenuItem();
            mnuThemeGraph = new ToolStripMenuItem();
            splitContainer1 = new SplitContainer();
            splitContainer2 = new SplitContainer();
            splitContainer3 = new SplitContainer();
            dataGridView1 = new DataGridView();
            toolStrip1 = new ToolStrip();
            toolZoomIn = new ToolStripButton();
            toolZoomOut = new ToolStripButton();
            toolZoomFree = new ToolStripButton();
            toolPan = new ToolStripButton();
            toolViewEntire = new ToolStripButton();
            toolStripLabel1 = new ToolStripLabel();
            cboLayersinMap = new ToolStripComboBox();
            toolStripLabel2 = new ToolStripLabel();
            cboDistrictNames = new ToolStripComboBox();
            toolStripLabel3 = new ToolStripLabel();
            toolStripTextBox1 = new ToolStripTextBox();
            toolStripButton1 = new ToolStripButton();
            btn_createline = new ToolStripButton();
            btnCreateRegion = new ToolStripButton();
            btnCreateCircleByCode = new ToolStripButton();
            btnCreatePointByCode = new ToolStripButton();
            btnCreatelineByCode = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            btnTrackDrawPolygon = new ToolStripButton();
            btnPlayTrack = new ToolStripButton();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            toolStripStatusLabel3 = new ToolStripStatusLabel();
            openFileDialog1 = new OpenFileDialog();
            Flashtimer = new System.Windows.Forms.Timer(components);
            timer1 = new System.Windows.Forms.Timer(components);
            saveFileDialog1 = new SaveFileDialog();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripButton2 = new ToolStripButton();
            toolStripButton3 = new ToolStripButton();
            toolStripButton4 = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer3).BeginInit();
            splitContainer3.Panel2.SuspendLayout();
            splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            toolStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { 文件ToolStripMenuItem, 数据源ToolStripMenuItem, 数据集ToolStripMenuItem, 空间数据表达ToolStripMenuItem, 数据表达ToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1995, 32);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            文件ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { menuOpenWKSFile, mnusave, mnusaveworkspace, mnuOutputMapAsFie });
            文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            文件ToolStripMenuItem.Size = new Size(108, 28);
            文件ToolStripMenuItem.Text = "文件（F&）";
            // 
            // menuOpenWKSFile
            // 
            menuOpenWKSFile.Name = "menuOpenWKSFile";
            menuOpenWKSFile.Size = new Size(254, 34);
            menuOpenWKSFile.Text = "打开工作空间文件";
            menuOpenWKSFile.Click += menuOpenWKSFile_Click;
            // 
            // mnusave
            // 
            mnusave.Name = "mnusave";
            mnusave.Size = new Size(254, 34);
            mnusave.Text = "另存地图";
            mnusave.Click += mnusave_Click;
            // 
            // mnusaveworkspace
            // 
            mnusaveworkspace.Name = "mnusaveworkspace";
            mnusaveworkspace.Size = new Size(254, 34);
            mnusaveworkspace.Text = "保存工作空间";
            mnusaveworkspace.Click += mnusaveworkspace_Click;
            // 
            // mnuOutputMapAsFie
            // 
            mnuOutputMapAsFie.Name = "mnuOutputMapAsFie";
            mnuOutputMapAsFie.Size = new Size(254, 34);
            mnuOutputMapAsFie.Text = "输出地图为文件";
            mnuOutputMapAsFie.Click += mnuOutputMapAsFie_Click;
            // 
            // 数据源ToolStripMenuItem
            // 
            数据源ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { menuOpenSQLDS, menuCreateSQLDS });
            数据源ToolStripMenuItem.Name = "数据源ToolStripMenuItem";
            数据源ToolStripMenuItem.Size = new Size(80, 28);
            数据源ToolStripMenuItem.Text = "数据源";
            // 
            // menuOpenSQLDS
            // 
            menuOpenSQLDS.Name = "menuOpenSQLDS";
            menuOpenSQLDS.Size = new Size(234, 34);
            menuOpenSQLDS.Text = "打开SQL数据源";
            menuOpenSQLDS.Click += menuOpenSQLDS_Click;
            // 
            // menuCreateSQLDS
            // 
            menuCreateSQLDS.Name = "menuCreateSQLDS";
            menuCreateSQLDS.Size = new Size(234, 34);
            menuCreateSQLDS.Text = "新建SQL数据源";
            menuCreateSQLDS.Click += menuCreateSQLDS_Click;
            // 
            // 数据集ToolStripMenuItem
            // 
            数据集ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { menuCopyDataset });
            数据集ToolStripMenuItem.Name = "数据集ToolStripMenuItem";
            数据集ToolStripMenuItem.Size = new Size(80, 28);
            数据集ToolStripMenuItem.Text = "数据集";
            // 
            // menuCopyDataset
            // 
            menuCopyDataset.Name = "menuCopyDataset";
            menuCopyDataset.Size = new Size(200, 34);
            menuCopyDataset.Text = "复制数据集";
            menuCopyDataset.Click += menuCopyDataset_Click;
            // 
            // 空间数据表达ToolStripMenuItem
            // 
            空间数据表达ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { menuLayerStyle, mnuUniqueTheme, mnuLabelTheme });
            空间数据表达ToolStripMenuItem.Name = "空间数据表达ToolStripMenuItem";
            空间数据表达ToolStripMenuItem.Size = new Size(134, 28);
            空间数据表达ToolStripMenuItem.Text = "空间数据表达";
            // 
            // menuLayerStyle
            // 
            menuLayerStyle.Name = "menuLayerStyle";
            menuLayerStyle.Size = new Size(270, 34);
            menuLayerStyle.Text = "图层风格";
            menuLayerStyle.Click += menuLayerStyle_Click;
            // 
            // mnuUniqueTheme
            // 
            mnuUniqueTheme.Name = "mnuUniqueTheme";
            mnuUniqueTheme.Size = new Size(270, 34);
            mnuUniqueTheme.Text = "单值专题图";
            mnuUniqueTheme.Click += mnuUniqueTheme_Click;
            // 
            // mnuLabelTheme
            // 
            mnuLabelTheme.Name = "mnuLabelTheme";
            mnuLabelTheme.Size = new Size(270, 34);
            mnuLabelTheme.Text = "标签专题图";
            mnuLabelTheme.Click += mnuLabelTheme_Click;
            // 
            // 数据表达ToolStripMenuItem
            // 
            数据表达ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { mnuThemeRange, mnuThemeDotDensity, mnuThemeGraph });
            数据表达ToolStripMenuItem.Name = "数据表达ToolStripMenuItem";
            数据表达ToolStripMenuItem.Size = new Size(98, 28);
            数据表达ToolStripMenuItem.Text = "数据表达";
            // 
            // mnuThemeRange
            // 
            mnuThemeRange.Name = "mnuThemeRange";
            mnuThemeRange.Size = new Size(270, 34);
            mnuThemeRange.Text = "范围分段专题图";
            mnuThemeRange.Click += mnuThemeRange_Click;
            // 
            // mnuThemeDotDensity
            // 
            mnuThemeDotDensity.Name = "mnuThemeDotDensity";
            mnuThemeDotDensity.Size = new Size(270, 34);
            mnuThemeDotDensity.Text = "人口密度图";
            mnuThemeDotDensity.Click += mnuThemeDotDensity_Click;
            // 
            // mnuThemeGraph
            // 
            mnuThemeGraph.Name = "mnuThemeGraph";
            mnuThemeGraph.Size = new Size(270, 34);
            mnuThemeGraph.Text = "统计图";
            mnuThemeGraph.Click += mnuThemeGraph_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 65);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(splitContainer3);
            splitContainer1.Panel2.Paint += splitContainer1_Panel2_Paint;
            splitContainer1.Size = new Size(1995, 859);
            splitContainer1.SplitterDistance = 664;
            splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = Orientation.Horizontal;
            splitContainer2.Size = new Size(664, 859);
            splitContainer2.SplitterDistance = 462;
            splitContainer2.TabIndex = 0;
            // 
            // splitContainer3
            // 
            splitContainer3.Dock = DockStyle.Fill;
            splitContainer3.Location = new Point(0, 0);
            splitContainer3.Name = "splitContainer3";
            splitContainer3.Orientation = Orientation.Horizontal;
            // 
            // splitContainer3.Panel2
            // 
            splitContainer3.Panel2.Controls.Add(dataGridView1);
            splitContainer3.Size = new Size(1327, 859);
            splitContainer3.SplitterDistance = 664;
            splitContainer3.TabIndex = 0;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(1327, 191);
            dataGridView1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(24, 24);
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolZoomIn, toolZoomOut, toolZoomFree, toolPan, toolViewEntire, toolStripButton2, toolStripButton3, toolStripLabel3, toolStripTextBox1, toolStripButton4, toolStripLabel1, cboLayersinMap, toolStripLabel2, cboDistrictNames, toolStripButton1, toolStripSeparator3, btn_createline, btnCreateRegion, btnCreateCircleByCode, btnCreatePointByCode, btnCreatelineByCode, toolStripSeparator1, btnTrackDrawPolygon, toolStripSeparator2, btnPlayTrack });
            toolStrip1.Location = new Point(0, 32);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1995, 33);
            toolStrip1.TabIndex = 2;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolZoomIn
            // 
            toolZoomIn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolZoomIn.Image = (Image)resources.GetObject("toolZoomIn.Image");
            toolZoomIn.ImageTransparentColor = Color.Magenta;
            toolZoomIn.Name = "toolZoomIn";
            toolZoomIn.Size = new Size(34, 28);
            toolZoomIn.Text = "放大";
            toolZoomIn.Click += toolZoomIn_Click;
            // 
            // toolZoomOut
            // 
            toolZoomOut.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolZoomOut.Image = (Image)resources.GetObject("toolZoomOut.Image");
            toolZoomOut.ImageTransparentColor = Color.Magenta;
            toolZoomOut.Name = "toolZoomOut";
            toolZoomOut.Size = new Size(34, 28);
            toolZoomOut.Text = "缩小";
            toolZoomOut.Click += toolZoomOut_Click;
            // 
            // toolZoomFree
            // 
            toolZoomFree.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolZoomFree.Image = (Image)resources.GetObject("toolZoomFree.Image");
            toolZoomFree.ImageTransparentColor = Color.Magenta;
            toolZoomFree.Name = "toolZoomFree";
            toolZoomFree.Size = new Size(34, 28);
            toolZoomFree.Text = "自由缩放";
            toolZoomFree.Click += toolZoomFree_Click;
            // 
            // toolPan
            // 
            toolPan.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolPan.Image = (Image)resources.GetObject("toolPan.Image");
            toolPan.ImageTransparentColor = Color.Magenta;
            toolPan.Name = "toolPan";
            toolPan.Size = new Size(34, 28);
            toolPan.Text = "漫游";
            toolPan.Click += toolPan_Click;
            // 
            // toolViewEntire
            // 
            toolViewEntire.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolViewEntire.Image = (Image)resources.GetObject("toolViewEntire.Image");
            toolViewEntire.ImageTransparentColor = Color.Magenta;
            toolViewEntire.Name = "toolViewEntire";
            toolViewEntire.Size = new Size(34, 28);
            toolViewEntire.Text = "全幅";
            toolViewEntire.Click += toolViewEntire_Click;
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(64, 28);
            toolStripLabel1.Text = "图层：";
            // 
            // cboLayersinMap
            // 
            cboLayersinMap.Name = "cboLayersinMap";
            cboLayersinMap.Size = new Size(200, 33);
            // 
            // toolStripLabel2
            // 
            toolStripLabel2.Name = "toolStripLabel2";
            toolStripLabel2.Size = new Size(82, 28);
            toolStripLabel2.Text = "行政区：";
            // 
            // cboDistrictNames
            // 
            cboDistrictNames.Items.AddRange(new object[] { "浦东新区", "杨浦区", "普陀区", "徐汇区", "长宁区", "闵行区", "虹口区", "嘉定区", "南市区", "静安区", "宝山区", "黄浦区", "宝山区", "闵行区", "浦东新区" });
            cboDistrictNames.Name = "cboDistrictNames";
            cboDistrictNames.Size = new Size(181, 33);
            cboDistrictNames.SelectedIndexChanged += cboDistrictNames_SelectedIndexChanged;
            // 
            // toolStripLabel3
            // 
            toolStripLabel3.Name = "toolStripLabel3";
            toolStripLabel3.Size = new Size(136, 28);
            toolStripLabel3.Text = "输入查询条件：";
            // 
            // toolStripTextBox1
            // 
            toolStripTextBox1.Name = "toolStripTextBox1";
            toolStripTextBox1.Size = new Size(148, 33);
            // 
            // toolStripButton1
            // 
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton1.Image = (Image)resources.GetObject("toolStripButton1.Image");
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(50, 28);
            toolStripButton1.Text = "查询";
            toolStripButton1.Click += toolStripButton1_Click;
            // 
            // btn_createline
            // 
            btn_createline.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btn_createline.Image = (Image)resources.GetObject("btn_createline.Image");
            btn_createline.ImageTransparentColor = Color.Magenta;
            btn_createline.Name = "btn_createline";
            btn_createline.Size = new Size(68, 28);
            btn_createline.Text = "绘制线";
            btn_createline.Click += btn_createline_Click;
            // 
            // btnCreateRegion
            // 
            btnCreateRegion.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnCreateRegion.Image = (Image)resources.GetObject("btnCreateRegion.Image");
            btnCreateRegion.ImageTransparentColor = Color.Magenta;
            btnCreateRegion.Name = "btnCreateRegion";
            btnCreateRegion.Size = new Size(68, 28);
            btnCreateRegion.Text = "绘制面";
            btnCreateRegion.Click += btnCreateRegion_Click;
            // 
            // btnCreateCircleByCode
            // 
            btnCreateCircleByCode.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnCreateCircleByCode.Image = (Image)resources.GetObject("btnCreateCircleByCode.Image");
            btnCreateCircleByCode.ImageTransparentColor = Color.Magenta;
            btnCreateCircleByCode.Name = "btnCreateCircleByCode";
            btnCreateCircleByCode.Size = new Size(86, 28);
            btnCreateCircleByCode.Text = "绘制圆形";
            btnCreateCircleByCode.Click += btnCreateCircleByCode_Click;
            // 
            // btnCreatePointByCode
            // 
            btnCreatePointByCode.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnCreatePointByCode.Image = (Image)resources.GetObject("btnCreatePointByCode.Image");
            btnCreatePointByCode.ImageTransparentColor = Color.Magenta;
            btnCreatePointByCode.Name = "btnCreatePointByCode";
            btnCreatePointByCode.Size = new Size(104, 28);
            btnCreatePointByCode.Text = "代码绘制点";
            btnCreatePointByCode.Click += btnCreatePointByCode_Click;
            // 
            // btnCreatelineByCode
            // 
            btnCreatelineByCode.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnCreatelineByCode.Image = (Image)resources.GetObject("btnCreatelineByCode.Image");
            btnCreatelineByCode.ImageTransparentColor = Color.Magenta;
            btnCreatelineByCode.Name = "btnCreatelineByCode";
            btnCreatelineByCode.Size = new Size(104, 28);
            btnCreatelineByCode.Text = "代码绘制线";
            btnCreatelineByCode.Click += btnCreatelineByCode_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 33);
            // 
            // btnTrackDrawPolygon
            // 
            btnTrackDrawPolygon.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnTrackDrawPolygon.Image = (Image)resources.GetObject("btnTrackDrawPolygon.Image");
            btnTrackDrawPolygon.ImageTransparentColor = Color.Magenta;
            btnTrackDrawPolygon.Name = "btnTrackDrawPolygon";
            btnTrackDrawPolygon.Size = new Size(122, 28);
            btnTrackDrawPolygon.Text = "跟踪层绘制面";
            btnTrackDrawPolygon.Click += btnTrackDrawPolygon_Click;
            // 
            // btnPlayTrack
            // 
            btnPlayTrack.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnPlayTrack.Image = (Image)resources.GetObject("btnPlayTrack.Image");
            btnPlayTrack.ImageTransparentColor = Color.Magenta;
            btnPlayTrack.Name = "btnPlayTrack";
            btnPlayTrack.Size = new Size(120, 28);
            btnPlayTrack.Text = "播放GPS轨迹";
            btnPlayTrack.Click += btnPlayTrack_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(24, 24);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripStatusLabel2, toolStripStatusLabel3 });
            statusStrip1.Location = new Point(0, 893);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1995, 31);
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(234, 24);
            toolStripStatusLabel1.Text = "2025广大地理-GIS开发课程";
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(195, 24);
            toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            // 
            // toolStripStatusLabel3
            // 
            toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            toolStripStatusLabel3.Size = new Size(185, 24);
            toolStripStatusLabel3.Text = "陈芷欣32301400033";
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // Flashtimer
            // 
            Flashtimer.Tick += Flashtimer_Tick;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 33);
            // 
            // toolStripButton2
            // 
            toolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton2.Image = (Image)resources.GetObject("toolStripButton2.Image");
            toolStripButton2.ImageTransparentColor = Color.Magenta;
            toolStripButton2.Name = "toolStripButton2";
            toolStripButton2.Size = new Size(34, 28);
            toolStripButton2.Text = "toolStripButton2";
            // 
            // toolStripButton3
            // 
            toolStripButton3.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton3.Image = (Image)resources.GetObject("toolStripButton3.Image");
            toolStripButton3.ImageTransparentColor = Color.Magenta;
            toolStripButton3.Name = "toolStripButton3";
            toolStripButton3.Size = new Size(86, 28);
            toolStripButton3.Text = "图查属性";
            // 
            // toolStripButton4
            // 
            toolStripButton4.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton4.Image = (Image)resources.GetObject("toolStripButton4.Image");
            toolStripButton4.ImageTransparentColor = Color.Magenta;
            toolStripButton4.Name = "toolStripButton4";
            toolStripButton4.Size = new Size(86, 28);
            toolStripButton4.Text = "属性查图";
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 33);
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1995, 924);
            Controls.Add(statusStrip1);
            Controls.Add(splitContainer1);
            Controls.Add(toolStrip1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer3).EndInit();
            splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem 文件ToolStripMenuItem;
        private SplitContainer splitContainer1;
        private ToolStripMenuItem menuOpenWKSFile;
        private ToolStripMenuItem 数据源ToolStripMenuItem;
        private ToolStripMenuItem menuOpenSQLDS;
        private ToolStripMenuItem menuCreateSQLDS;
        private ToolStripMenuItem 数据集ToolStripMenuItem;
        private ToolStripMenuItem menuCopyDataset;
        private ToolStrip toolStrip1;
        private ToolStripButton toolZoomIn;
        private ToolStripButton toolZoomOut;
        private ToolStripButton toolZoomFree;
        private ToolStripButton toolPan;
        private ToolStripButton toolViewEntire;
        private ToolStripLabel toolStripLabel1;
        private ToolStripComboBox cboLayersinMap;
        private ToolStripLabel toolStripLabel2;
        private ToolStripComboBox cboDistrictNames;
        private ToolStripLabel toolStripLabel3;
        private ToolStripTextBox toolStripTextBox1;
        private ToolStripButton toolStripButton1;
        private ToolStripButton btn_createline;
        private ToolStripButton btnCreateRegion;
        private SplitContainer splitContainer2;
        private SplitContainer splitContainer3;
        private ToolStripButton btnCreateCircleByCode;
        private ToolStripButton btnCreatePointByCode;
        private ToolStripButton btnCreatelineByCode;
        private ToolStripButton btnTrackDrawPolygon;
        private StatusStrip statusStrip1;
        private DataGridView dataGridView1;
        private OpenFileDialog openFileDialog1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Timer Flashtimer;
        private ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Timer timer1;
        private ToolStripButton btnPlayTrack;
        private ToolStripMenuItem 空间数据表达ToolStripMenuItem;
        private ToolStripMenuItem menuLayerStyle;
        private ToolStripMenuItem mnuUniqueTheme;
        private ToolStripMenuItem mnuLabelTheme;
        private ToolStripMenuItem 数据表达ToolStripMenuItem;
        private ToolStripMenuItem mnuThemeRange;
        private ToolStripMenuItem mnuThemeDotDensity;
        private ToolStripMenuItem mnuThemeGraph;
        private ToolStripMenuItem mnusave;
        private ToolStripStatusLabel toolStripStatusLabel3;
        private ToolStripMenuItem mnusaveworkspace;
        private ToolStripMenuItem mnuOutputMapAsFie;
        private SaveFileDialog saveFileDialog1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton toolStripButton2;
        private ToolStripButton toolStripButton3;
        private ToolStripButton toolStripButton4;
        private ToolStripSeparator toolStripSeparator3;
    }
}
