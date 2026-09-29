using SuperMap.Data;
using SuperMap.Mapping;
using SuperMap.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ExcelDataReader;


namespace czx3
{
    public partial class Form1 : Form
    {
        private Workspace workspace1;
        private MapControl mapControl1;
        private WorkspaceTree workspaceTree1;
        private LayersControl layersControl1;

        private Geometry currentDistrictGeometry = null;
        private List<Point2D> gpsPoints = new List<Point2D>();  // GPS����㼯��
        private int currentIndex = 0;  // ��ǰ���ŵ��ڼ�����
        private GeoPoint movingPoint = null;  // ��̬��ʾ�ĵ�
        private bool isPlaying = false;  // ����״̬
        private int lastGpsPointTrackingId = -1;  // ��һ�����Tracking ID

        public bool isFlash = false;//ȫ�ֱ���������ָʾtimer�ؼ��Ƿ�ִ����˸����
        public Recordset flashres;//ȫ�ֱ��������������ѯ�����¼��
        public Form1()
        {
            InitializeComponent();
            // ����SuperMap Online�ʺţ���¼�Զ�������������(username, password��Ҫ����ע��)
            SuperMap.Data.CloudLicense.Login("15362663132", "czx15362663132!");
            // �����������е�����ģ�飬��֤�Ƿ������ɻ�ȡ�Ƿ�ɹ����ɹ�����0
            SuperMap.Data.License license = new SuperMap.Data.License();
            int code = license.Connect(65400);  //��������ģ��ID 65400
            Console.WriteLine("code = {0}", code);
            workspace1 = new Workspace();
            mapControl1 = new MapControl(workspace1);
            mapControl1.Dock = DockStyle.Fill;
            splitContainer3.Panel1.Controls.Add(mapControl1);
            //���ӹ����ռ����ؼ�
            workspaceTree1 = new WorkspaceTree();
            workspaceTree1.Dock = DockStyle.Fill;
            splitContainer2.Panel1.Controls.Add(workspaceTree1);
            //����ͼ��������ؼ�
            layersControl1 = new LayersControl();
            layersControl1.Dock = DockStyle.Fill;
            splitContainer2.Panel2.Controls.Add(layersControl1);
            //
            //workspaceTree1.NodeMouseClick += new TreeNodeMouseClickEventHandler(workspaceTree1_NodeMouseClick);

            // �� WorkspaceTree ��˫���¼�
            workspaceTree1.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(WorkspaceTree1_NodeMouseDoubleClick);

            mapControl1.Tracked += new TrackedEventHandler(mapControl1_Tracked);

            mapControl1.Tracking += new TrackingEventHandler(mapControl1_Tracking);
        }

        private void mapControl1_Tracking(object sender, TrackingEventArgs e)
        {
            toolStripStatusLabel2.Text = "�����Ϊ��" + e.TotalArea.ToString();
        }

        // �����ڵ�˫���¼�
        private void WorkspaceTree1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // ��ȡ��ǰ�ڵ㡢���ڵ㼰�游�ڵ�
            TreeNode currentNode = e.Node;
            TreeNode parentNode = currentNode.Parent;
            TreeNode grandParentNode = parentNode.Parent;


            // ���ڵ�㼶��Ч��
            if (parentNode == null || grandParentNode == null)
                return;

            try
            {
                // ˫������"���ݼ�"���ͽڵ㣨�游�ڵ��ı�������Դ��
                if (grandParentNode.Text == "����Դ")
                {
                    // ��ȡ����Դ���������ݼ�����
                    string datasourceAlias = parentNode.Text;
                    string datasetName = currentNode.Text;

                    // �ӹ����ռ��ȡ����Դ
                    Datasource datasource = workspace1.Datasources[datasourceAlias];
                    if (datasource == null)
                    {
                        MessageBox.Show($"����Դ '{datasourceAlias}' �����ڣ�");
                        return;
                    }


                    // ��ȡ���ݼ�
                    Dataset dataset = datasource.Datasets[datasetName];
                    if (dataset == null)
                    {
                        MessageBox.Show($"���ݼ� '{datasetName}' �����ڣ�");
                        return;
                    }

                    // �����ݼ����ӵ���ǰ��ͼ����
                    Layer layer = mapControl1.Map.Layers.Add(dataset, true);
                    layer.IsVisible = true;
                    mapControl1.Map.Refresh();
                    mapControl1.Action = SuperMap.UI.Action.Pan; // ���õ�ͼ����״̬
                    // ����ͼ������ͼ���������ʹ��������еĵ�ͼͼ��
                    layersControl1.Map = mapControl1.Map;
                    layersControl1.Refresh();
                    cboLayersinMap.BeginUpdate();
                    cboLayersinMap.Items.Add(layer.Name);
                    cboLayersinMap.EndUpdate();

                }
                // ˫������"��ͼ"���ͽڵ�
                else if (parentNode.Text == "��ͼ")
                {
                    string mapName = currentNode.Text;
                    // �򿪵�ͼ����ͼ����
                    mapControl1.Map.Open(mapName);
                    //ˢ�µ�ͼ����
                    mapControl1.Map.Refresh();
                    // ����ͼ������ͼ���������ʹ��������еĵ�ͼͼ��
                    layersControl1.Map = mapControl1.Map;
                    layersControl1.Refresh();
                    //��ʾ����ͼ����Ϣ��cboLayersInMap�ؼ���
                    cboLayersinMap.BeginUpdate();
                    foreach (Layer lyr in mapControl1.Map.Layers)
                    {
                        string lyrName = lyr.Name;
                        cboLayersinMap.Items.Add(lyrName);
                    }
                    cboLayersinMap.EndUpdate();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"����ʧ��: {ex.Message}");
            }
        }

        //�����ռ����Ϳؼ��Ľڵ㵥���¼���������
        private void workspaceTree1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (workspaceTree1.MapsNode.FirstNode.IsSelected)
            {
                mapControl1.Map.Open(e.Node.Text);
                //ˢ�µ�ͼ����
                mapControl1.Map.Refresh();
                // ����ͼ������ͼ���������ʹ��������еĵ�ͼͼ��
                layersControl1.Map = mapControl1.Map;
                layersControl1.Refresh();
            }

        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void menuOpenWKSFile_Click(object sender, EventArgs e)
        {
            //���ù��ô򿪶Ի���
            openFileDialog1.Filter = "SuperMap �����ռ��ļ�(*.smwu)|*.smwu";
            //�жϴ򿪵Ľ��������򿪾�ִ�����в���
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //���������򿪹����ռ䵼�³����쳣     
                mapControl1.Map.Close();
                workspace1.Close();
                mapControl1.Map.Refresh();
                //����򿪹����ռ��ļ���
                String fileName = openFileDialog1.FileName;
                //�򿪹����ռ��ļ�
                WorkspaceConnectionInfo connectionInfo = new WorkspaceConnectionInfo(fileName);
                //�򿪹����ռ�
                workspace1.Open(connectionInfo);
                workspaceTree1.Workspace = workspace1;
                workspaceTree1.Refresh();
                //����MapControl��Workspace������
                mapControl1.Map.Workspace = workspace1;
                //�жϹ����ռ����Ƿ��е�ͼ
                if (workspace1.Maps.Count == 0)
                {
                    MessageBox.Show("��ǰ�����ռ��в����ڵ�ͼ!");
                    return;
                }
                //ͨ�����ƴ򿪹����ռ��еĵ�ͼ(��ͼ������Ҫȷ�����빤���ռ��б���һ��)
                //mapControl1.Map.Open(workspace1.Maps[0]);
                ////ˢ�µ�ͼ����
                //mapControl1.Map.Refresh();
                //// ����ͼ������ͼ���������ʹ��������еĵ�ͼͼ��
                //layersControl1.Map = mapControl1.Map;
                //layersControl1.Refresh();
            }
        }

        private void toolZoomIn_Click(object sender, EventArgs e)
        {
            mapControl1.Action = SuperMap.UI.Action.ZoomIn;
        }

        private void toolZoomOut_Click(object sender, EventArgs e)
        {
            mapControl1.Action = SuperMap.UI.Action.ZoomOut;
        }

        private void toolZoomFree_Click(object sender, EventArgs e)
        {
            mapControl1.Action = SuperMap.UI.Action.ZoomFree;
        }

        private void toolPan_Click(object sender, EventArgs e)
        {
            mapControl1.Action = SuperMap.UI.Action.Pan;
        }

        private void toolViewEntire_Click(object sender, EventArgs e)
        {
            mapControl1.Map.ViewEntire();
        }

        private void menuOpenSQLDS_Click(object sender, EventArgs e)
        {
            // ���� workspace ��һ���Ѿ��򿪵Ĺ����ռ����
            Workspace workspace = workspace1;

            // 1. ��������������Դ������Ϣ
            DatasourceConnectionInfo connectionInfo = new DatasourceConnectionInfo();

            // �������ã������������ݿ⡢��������
            // ���� SQL Server��Server ��ʽͨ��Ϊ "������IP\ʵ����" �� "������IP,�˿�"
            // ���� PostgreSQL��Server ��ʽͨ��Ϊ "������IP:�˿�" (�˿ں��� EngineType ���������ã�������˵��)
            connectionInfo.Server = "YOUR_SERVER";
            connectionInfo.Database = "newDS"; // Ҫ���ӵ����ݿ���
            connectionInfo.User = "YOUR_USERNAME";
            connectionInfo.Password = "YOUR_PASSWORD";
            connectionInfo.Alias = "sql_ds"; // �ڹ����ռ�����ʾ�ı���
            connectionInfo.Driver = "SQL Server";

            // 2. ָ�����ݿ��������� (���ǹؼ�����)
            // ����������ݿ����ͣ�ѡ���Ӧ�� EngineType��
            // - EngineType.OraclePlus
            // - EngineType.SQLPlus
            // - EngineType.PostgreSQL
            // - EngineType.DB2
            // - EngineType.MySQL
            connectionInfo.EngineType = EngineType.SQLPlus; // ���滻Ϊ������ݿ�����

            // �ر�˵�������� PostgreSQL��ͨ������Ҫ�� Server ������ָ���˿ڣ����磺"127.0.0.1:5432"��
            // ���� EngineType Ϊ EngineType.PostgreSQL��

            // 3. ������Դ
            Datasource openedDatasource = workspace.Datasources.Open(connectionInfo);

            // 4. ����Ƿ񴴽��ɹ�
            if (openedDatasource != null)
            {
                System.Windows.Forms.MessageBox.Show("SQL ����Դ�򿪳ɹ���");
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("SQL ����Դ��ʧ�ܣ��������Ӳ�����ȷ������Դ�Ƿ��Ѵ��ڡ�");
            }
        }

        private void menuCreateSQLDS_Click(object sender, EventArgs e)
        {
            //�½�SQL server����Դ

            // ���� workspace ��һ���Ѿ��򿪵Ĺ����ռ����
            Workspace workspace = workspace1;

            // 1. ��������������Դ������Ϣ
            DatasourceConnectionInfo connectionInfo = new DatasourceConnectionInfo();

            // �������ã������������ݿ⡢��������
            // ���� SQL Server��Server ��ʽͨ��Ϊ "������IP\ʵ����" �� "������IP,�˿�"
            // ���� PostgreSQL��Server ��ʽͨ��Ϊ "������IP:�˿�" (�˿ں��� EngineType ���������ã�������˵��)
            connectionInfo.Server = "YOUR_SERVER";
            connectionInfo.Database = "newDS"; // Ҫ���ӵ����ݿ���
            connectionInfo.User = "YOUR_USERNAME";
            connectionInfo.Password = "YOUR_PASSWORD";
            connectionInfo.Alias = "sql_ds"; // �ڹ����ռ�����ʾ�ı���
            connectionInfo.Driver = "SQL Server";

            // 2. ָ�����ݿ��������� (���ǹؼ�����)
            // ����������ݿ����ͣ�ѡ���Ӧ�� EngineType��
            // - EngineType.OraclePlus
            // - EngineType.SQLPlus
            // - EngineType.PostgreSQL
            // - EngineType.DB2
            // - EngineType.MySQL
            connectionInfo.EngineType = EngineType.SQLPlus; // ���滻Ϊ������ݿ�����

            // �ر�˵�������� PostgreSQL��ͨ������Ҫ�� Server ������ָ���˿ڣ����磺"127.0.0.1:5432"��
            // ���� EngineType Ϊ EngineType.PostgreSQL��

            // 3. ��������Դ
            Datasource newDatasource = workspace.Datasources.Create(connectionInfo);

            // 4. ����Ƿ񴴽��ɹ�
            if (newDatasource != null)
            {
                System.Windows.Forms.MessageBox.Show("SQL ����Դ�����ɹ���");
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("SQL ����Դ����ʧ�ܣ��������Ӳ�����ȷ������Դ�Ƿ��Ѵ��ڡ�");
            }
        }

        private void menuCopyDataset_Click(object sender, EventArgs e)
        {
            ////��������һ�����ݼ�
            ////��ȡĿ�����ݼ���Ŀ������Դ
            //Datasource datasource = workspace1.Datasources["world"];
            //Datasource datasourceDes = workspace1.Datasources["sql_ds"];

            //DatasetVector datasetVector = datasource.Datasets["Capital"] as DatasetVector;

            ////���һ�����õ����ݼ�����
            //String datasetName = datasourceDes.Datasets.GetAvailableDatasetName("Capital_Copy");

            ////��Ŀ�����ݼ����Ƶ�ָ��������Դ�С�
            //Dataset newDataset = datasourceDes.CopyDataset(datasetVector, datasetName, EncodeType.Int32);
            //�뽫�ļ�������Դ��World.udb/udd���е����ݼ�����һ�ݣ�������½������ݿ�������Դ��SQLServer���С�
            Workspace workspace = workspace1;
            // 1. ��Դ����Դ��UDB�ļ�������Դ��
            Datasource srcDatasource = workspace.Datasources["world"];
            // 2. ��Ŀ������Դ��SQL Server���ݿ�������Դ��
            Datasource desDatasource = workspace.Datasources["sql_ds"];

            // 3. �������ݼ�
            // ����Դ����Դ�е��������ݼ�������ѡ���ض����ݼ����и���
            foreach (Dataset srcDataset in srcDatasource.Datasets)
            {
                // ��ȡһ����Ŀ������Դ�п��õ����ݼ�����
                string desDatasetName = desDatasource.Datasets.GetAvailableDatasetName(srcDataset.Name + "_copy");
                // �������ݼ���������뷽ʽʹ��Ĭ�ϣ�����EncodeType.None�������Ը�����Ҫ�޸�
                // ע�⣺����CAD���ݼ���EncodeTypeֻ��ΪNone���������ݼ�����ѡ����ʵı��뷽ʽ
                Dataset newDataset = desDatasource.CopyDataset(srcDataset, desDatasetName, EncodeType.None);
                if (newDataset != null)
                {
                    MessageBox.Show($"���ݼ� {srcDataset.Name} ���Ƴɹ���Ŀ�����ݼ���Ϊ {desDatasetName}");
                }
                else
                {
                    MessageBox.Show($"���ݼ� {srcDataset.Name} ����ʧ��");
                }
            }

            workspaceTree1.Refresh();
            MessageBox.Show("������ɣ�");
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //�ж�toolStripTextBox1�����������Ƿ�Ϊ��
            if (toolStripTextBox1.Text.Length == 0)
            {
                MessageBox.Show("��ѯ��Ϣ����Ϊ��");
                return;
            }

            //����ͼ�����
            Int32 layerCount = mapControl1.Map.Layers.Count;
            //�жϵ�ǰ��ͼ�������Ƿ��д򿪵�ͼ��
            if (layerCount == 0)
            {
                MessageBox.Show("���ȴ�һ��ʸ�����ݼ���");
                return;
            }

            //�����ѯ������Ϣ;
            QueryParameter queryParameter = new QueryParameter();
            queryParameter.AttributeFilter = toolStripTextBox1.Text;
            queryParameter.CursorType = CursorType.Static;

            //��ȡ���������ļ��ζ���ѡ�е�ĳ����������
            Selection[] sels = mapControl1.Map.FindSelection(true);//�õ�ͼѡ�񼯺�������
            Geometry geoSearch = sels[0].ToRecordset().GetGeometry();//��ȡ��ǰѡ�еļ��ζ���
            queryParameter.SpatialQueryObject = geoSearch;
            //���ÿռ�λ�ù�ϵ
            queryParameter.SpatialQueryMode = SpatialQueryMode.Contain;

            Boolean hasGeometry = false;
            //��ȡ��ǰѡ���ͼ�����
            Layer layer = mapControl1.Map.Layers[cboLayersinMap.SelectedItem.ToString()];

            //�õ�ʸ�����ݼ���ǿ��ת��Ϊʸ�����ݼ�����
            DatasetVector dataset = layer.Dataset as DatasetVector;

            if (dataset == null)
            {
                return;
            }
            //ͨ����ѯ������ʸ�����ݼ����в�ѯ,�����ݼ��в�ѯ���������ݣ�
            Recordset recordset = dataset.Query(queryParameter);
            //�ж��Ƿ��в�ѯ���
            if (recordset.RecordCount > 0)
            {
                hasGeometry = true;
            }
            //�����ѯ������ݼ���ȫ�ֱ���
            flashres = recordset;

            ////�Ѳ�ѯ�õ������ݼ��뵽ѡ����(ʹ�������ʾ)
            //Selection selection = layer.Selection;
            //selection.FromRecordset(recordset);
            //recordset.Dispose();
            // //û�в�ѯ�����������ʾ
            //if (!hasGeometry)
            // {
            //     MessageBox.Show("û�з��ϲ�ѯ�����Ľ�����ѯ��������������ȷ�Ϻ��ѯ��");
            // }

            // //���ɴ�������ʹ����Ϻ�ʹ��Dispose�������ͷ���ռ�õ��ڲ���Դ��
            // queryParameter.Dispose();
            // //ˢ�µ�ͼ������ʾ
            // mapControl1.Refresh();
            // hasGeometry = false;

            ////����ѯ����ڸ��ٲ�����ʾ
            //recordset.MoveFirst();//�ƶ�����һ����¼
            //// ������з������
            //GeoStyle geoStyle_P = new GeoStyle();
            //geoStyle_P.MarkerSize = new Size2D(10, 10);//���Ŵ�С
            //geoStyle_P.MarkerSymbolID = 10;//
            //geoStyle_P.LineColor = Color.Red;//������ɫ
            //geoStyle_P.FillForeColor = Color.Red;
            ////��ȡ���ٲ����
            //TrackingLayer traLyer = mapControl1.Map.TrackingLayer;
            //for (int i = 0; i < recordset.RecordCount; i++)
            //{
            //    Geometry geo = recordset.GetGeometry();//��ȡ��ǰ��¼��Ӧ�ļ��ζ���
            //    GeoPoint geoPoint = (GeoPoint)geo; //ʵ����һ���㼸�ζ���
            //    geoPoint.Style = geoStyle_P;//���õ����ķ��
            //    traLyer.Add(geoPoint, "��ѯ�����_"+i.ToString());
            //    recordset.MoveNext();//�ƶ�����һ����¼
            //}

            // ˢ�¸���ͼ��
            //mapControl1.Map.RefreshTrackingLayer();

            //������ʱ��
            Flashtimer.Interval = 100;
            Flashtimer.Start();
            isFlash = true;//������˸����Ϊtrue��������˸Ч��
        }

        private void cboDistrictNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            Layer layer = mapControl1.Map.Layers["District_R@Shanghai"];
            Boolean hasGeometry = false;
            //�õ�ʸ�����ݼ���ǿ��ת��Ϊʸ�����ݼ�����
            DatasetVector dataset = layer.Dataset as DatasetVector;
            //�����ѯ������Ϣ;
            QueryParameter queryParameter = new QueryParameter();
            queryParameter.AttributeFilter = "name = " + "'" + cboDistrictNames.SelectedItem.ToString() + "'";
            queryParameter.CursorType = CursorType.Static;
            //ͨ����ѯ������ʸ�����ݼ����в�ѯ,�����ݼ��в�ѯ���������ݣ�
            Recordset recordset = dataset.Query(queryParameter);
            //�ж��Ƿ��в�ѯ���
            if (recordset.RecordCount > 0)
            {
                hasGeometry = true;
            }
            //�Ѳ�ѯ�õ������ݼ��뵽ѡ����(ʹ�������ʾ)
            Selection selection = layer.Selection;
            selection.FromRecordset(recordset);
            //recordset.Dispose();

            if (hasGeometry)
            {
                DataTable dt = new DataTable();

                // �����У������ֶ���Ϣ��
                for (int i = 0; i < recordset.FieldCount; i++)
                {
                    string fieldName = recordset.GetFieldInfos()[i].Name;
                    dt.Columns.Add(fieldName);
                }

                // ���Ӽ�¼��
                recordset.MoveFirst();
                while (!recordset.IsEOF)
                {
                    DataRow row = dt.NewRow();
                    for (int i = 0; i < recordset.FieldCount; i++)
                    {
                        row[i] = recordset.GetFieldValue(i);
                    }
                    dt.Rows.Add(row);
                    recordset.MoveNext();
                }

                // ���浱ǰ�������ļ��ζ��󣨹�������ѯʹ�ã�
                recordset.MoveFirst();
                currentDistrictGeometry = recordset.GetGeometry();

                // �󶨵� DataGridView
                dataGridView1.DataSource = dt;
            }

            //�ͷŲ�ѯ�����
            recordset.Dispose();

            queryParameter.Dispose();

            //û�в�ѯ�����������ʾ
            if (!hasGeometry)
            {
                MessageBox.Show("û�з��ϲ�ѯ�����Ľ�����ѯ��������������ȷ�Ϻ��ѯ��");
            }

            //���ɴ�������ʹ����Ϻ�ʹ��Dispose�������ͷ���ռ�õ��ڲ���Դ��
            queryParameter.Dispose();
            //ˢ�µ�ͼ������ʾ
            mapControl1.Refresh();
            hasGeometry = false;
        }

        private void btn_createline_Click(object sender, EventArgs e)
        {

            Layers layers = mapControl1.Map.Layers; //��ȡ��ͼ��ͼ�㼯��
            Layer layer = layers[0];//��ȡ��һ��ͼ�����

            //����ͼ��Ϊ�ɱ༭״̬
            layer.IsEditable = true;

            //����������Ϊ��������
            mapControl1.Action = SuperMap.UI.Action.CreatePolyline;

            //ˢ�µ�ͼ����
            mapControl1.Refresh();
        }

        private void btnCreateRegion_Click(object sender, EventArgs e)
        {
            Layers layers = mapControl1.Map.Layers; //��ȡ��ͼ��ͼ�㼯��
            Layer layer = layers[0];//��ȡ��һ��ͼ�����

            //����ͼ��Ϊ�ɱ༭״̬
            layer.IsEditable = true;

            //����������Ϊ���ƶ���ζ���
            mapControl1.Action = SuperMap.UI.Action.CreatePolygon;

            //ˢ�µ�ͼ����
            mapControl1.Refresh();
        }

        private void btnCreateCircleByCode_Click(object sender, EventArgs e)
        {
            try
            {
                // ��ȡ��ͼͼ�㼯��
                Layers layers = mapControl1.Map.Layers;
                if (layers.Count == 0)
                {
                    MessageBox.Show("���ȼ���һ��ʸ��ͼ�㣡");
                    return;
                }

                // ��ȡ��һ��ͼ�㣨�����������ѡ��
                Layer layer = layers[0];
                DatasetVector datasetVector = layer.Dataset as DatasetVector;
                if (datasetVector == null)
                {
                    MessageBox.Show("��ǰͼ�㲻��ʸ�����ݼ����޷��༭��");
                    return;
                }

                // ����Ϊ�ɱ༭״̬
                layer.IsEditable = true;

                // ��������ģʽ��Ϊ����Բ�Σ�GeoCircle��
                mapControl1.Action = SuperMap.UI.Action.CreateCircle;

                // ��ѡ����ʾ�û�
                MessageBox.Show("���ڵ�ͼ�ϵ�����϶�������Բ�μ��ζ���", "��ʾ");

                // ˢ����ʾ
                mapControl1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("���û���Բ��ģʽ������" + ex.Message);
            }
        }

        private void btnCreatePointByCode_Click(object sender, EventArgs e)
        {
            //������ݼ�
            DatasetVector datasetVector;
            datasetVector = workspace1.Datasources[0].Datasets["Point"] as DatasetVector;

            //��õ�ͼ��������λ�õ�����ֵ
            Point2D centerPoint = mapControl1.Map.Center;

            //����㼸�ζ��󣬲������ĵ�����ֵ��ֵ���㼸�ζ���
            GeoPoint geoPoint = new GeoPoint(centerPoint);

            //�����ݼ��л�ö�Ӧ�ļ�¼��
            Recordset recordset = datasetVector.GetRecordset(true, CursorType.Dynamic);

            //���Ӽ�¼���Լ���Ӧ�ļ��ζ���
            recordset.AddNew(geoPoint);
            recordset.Update();
            //��ʾ���ݼ�
            mapControl1.Map.Layers.Add(datasetVector, true);
            mapControl1.Map.Refresh();
            recordset.Dispose();//�ͷ��ڴ���Դ

        }

        private void btnCreatelineByCode_Click(object sender, EventArgs e)
        {
            //������ݼ�
            DatasetVector datasetVector;
            datasetVector = workspace1.Datasources[0].Datasets["Line"] as DatasetVector;

            //����2��������
            Point2D point2D1 = new Point2D(81.9387412673, 8814.6961854469);
            Point2D point2D2 = new Point2D(12531.9733110417, 1.2024623821);

            //�����ά�㼯�϶��󣬲������������ӵ��㼯����
            Point2Ds point2Ds = new Point2Ds();
            point2Ds.Add(point2D1);
            point2Ds.Add(point2D2);

            //���������㹹��һ��ֱ�߶���
            GeoLine geoLine = new GeoLine(point2Ds);
            //�����ݼ��л�ö�Ӧ�ļ�¼��
            Recordset recordset = datasetVector.GetRecordset(true, CursorType.Dynamic);

            //���Ӽ�¼���Լ���Ӧ�ļ��ζ���
            recordset.AddNew(geoLine);
            recordset.Update();

            //��ʾ���ݼ�
            mapControl1.Map.Layers.Add(datasetVector, true);
            mapControl1.Map.ViewEntire();
            mapControl1.Map.Refresh();
            recordset.Dispose();

        }

        private void btnTrackDrawPolygon_Click(object sender, EventArgs e)
        {
            // �������ڴ��л��ƶ����ڻ�ȡ��������ӵ�����ͼ����
            mapControl1.TrackMode = TrackMode.Track;
            // ���û������action
            mapControl1.Action = SuperMap.UI.Action.CreatePolygon;

        }
        private void mapControl1_Tracked(object sender, TrackedEventArgs e)
        {
            try
            {
                // ���ø��ٲ��ϻ��Ƽ��ζ������ʾ���
                GeoStyle geoStyle_R = new GeoStyle();
                geoStyle_R.LineColor = Color.Green;
                geoStyle_R.FillForeColor = Color.Red;
                e.Geometry.Style = geoStyle_R;
                // �ڸ���ͼ������ʾ���Ƶ������
                mapControl1.Map.TrackingLayer.Add(e.Geometry, "polygon");
                // ˢ�¸���ͼ��
                mapControl1.Map.RefreshTrackingLayer();
            }
            catch (Exception e2)
            {
                MessageBox.Show(e2.Message);
            }
        }
        private void AddGeoToTrackingLayer(Recordset recordset)
        {
            //����ѯ����ڸ��ٲ�����ʾ
            recordset.MoveFirst();//�ƶ�����һ����¼
                                  // ������з������
            GeoStyle geoStyle_P = new GeoStyle();
            geoStyle_P.MarkerSize = new Size2D(10, 10);//���Ŵ�С
            geoStyle_P.MarkerSymbolID = 10;//
            geoStyle_P.LineColor = Color.Red;//������ɫ
            geoStyle_P.FillForeColor = Color.Red;
            //��ȡ���ٲ����
            TrackingLayer traLyer = mapControl1.Map.TrackingLayer;
            for (int i = 0; i < recordset.RecordCount; i++)
            {
                Geometry geo = recordset.GetGeometry();//��ȡ��ǰ��¼��Ӧ�ļ��ζ���
                GeoPoint geoPoint = (GeoPoint)geo; //ʵ����һ���㼸�ζ���
                geoPoint.Style = geoStyle_P;//���õ����ķ��
                traLyer.Add(geoPoint, "��ѯ�����_" + i.ToString());
                recordset.MoveNext();//�ƶ�����һ����¼
            }
            // ˢ�¸���ͼ��
            mapControl1.Map.RefreshTrackingLayer();
        }

        private void Flashtimer_Tick(object sender, EventArgs e)
        {
            //ͨ���ظ������ӡ�ɾ�����ζ��󣬴Ӷ�ʵ�ֶ�����˸��ģ��Ч��
            if (isFlash == true)
            {
                AddGeoToTrackingLayer(flashres);
                isFlash = false;
            }
            else
            {
                mapControl1.Map.TrackingLayer.Clear();
                // ˢ�¸���ͼ��
                mapControl1.Map.RefreshTrackingLayer();
                isFlash = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (currentIndex >= gpsPoints.Count)
            {
                if (timer1 != null) timer1.Stop();
                isPlaying = false;
                btnPlayTrack.Text = "����GPS�켣";
                MessageBox.Show("������ϣ�");
                return;
            }

            // ��ȡ��ǰ��
            Point2D point = gpsPoints[currentIndex];

            // ���Ƴ���һ�����ӵĶ�̬�㣨������ڣ�
            try
            {
                if (lastGpsPointTrackingId != -1)
                {
                    // Remove ��Ҫ int id
                    mapControl1.Map.TrackingLayer.Remove(lastGpsPointTrackingId);
                    lastGpsPointTrackingId = -1;
                }
            }
            catch
            {
                // �����Ƴ��쳣�����������µ�
                lastGpsPointTrackingId = -1;
            }

            // ���Ƶ�ǰ�ƶ��㣨С��λ�ã�
            GeoPoint geoPoint = new GeoPoint(point);
            GeoStyle style = new GeoStyle();
            style.MarkerSize = new Size2D(10, 10);
            style.MarkerSymbolID = 1;  // �ɻ�����ID
            style.LineColor = Color.Red;
            geoPoint.Style = style;

            // Add ���� int id����¼���������´��Ƴ���
            try
            {
                lastGpsPointTrackingId = mapControl1.Map.TrackingLayer.Add(geoPoint, "GPS��");
            }
            catch
            {
                // ��� Add û�з��� int�����ټ���������Ϊ -1���������������
                lastGpsPointTrackingId = -1;
                mapControl1.Map.TrackingLayer.Add(geoPoint, "GPS��");
            }

            // ������ʻ·�������ߣ�
            if (currentIndex > 0)
            {
                // ��ȡǰһ����͵�ǰ��
                Point2D prevPoint = gpsPoints[currentIndex - 1];
                Point2Ds pts = new Point2Ds();
                pts.Add(prevPoint);
                pts.Add(point);

                GeoLine line = new GeoLine(pts);
                GeoStyle lineStyle = new GeoStyle();
                lineStyle.LineColor = Color.Red;
                lineStyle.LineWidth = 1.5;
                line.Style = lineStyle;

                // Ϊ�˱��ֹ켣�ۻ���ʾ�����Ƴ����ߣ�ֱ������
                // Ϊ���� Id ��ͻ�����ﲻ���ߴ� id�������Ҫ����켣����ʵ��������д��ض� tag �Ķ���
                mapControl1.Map.TrackingLayer.Add(line, "GPS�켣��_" + currentIndex.ToString());
            }

            // ��ͼ��ͼ�����ƶ����������ƶ�����ǰ�㣩
            mapControl1.Map.Center = point;

            // ˢ�¸��ٲ�͵�ͼ��ʾ
            mapControl1.Map.RefreshTrackingLayer();
            mapControl1.Map.Refresh();

            currentIndex++;
        }

        private void btnPlayTrack_Click(object sender, EventArgs e)
        {
            if (!isPlaying)
            {
                // �����û���ţ��ȶ�ȡExcel��������
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Excel �ļ� (*.xlsx)|*.xlsx|�����ļ� (*.*)|*.*";
                if (ofd.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    gpsPoints.Clear();
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                    // ��ȡExcel����
                    using (var stream = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelDataReader.ExcelReaderFactory.CreateReader(stream))
                        {
                            while (reader.Read())
                            {
                                // ���� Excel �еĵ�һ���� X�����ȣ����ڶ����� Y��γ�ȣ�
                                if (reader.GetValue(0) == null || reader.GetValue(1) == null)
                                    continue;

                                double x, y;
                                if (double.TryParse(reader.GetValue(0).ToString(), out x) &&
                                    double.TryParse(reader.GetValue(1).ToString(), out y))
                                {
                                    gpsPoints.Add(new Point2D(x, y));
                                }
                            }
                        }
                    }

                    if (gpsPoints.Count == 0)
                    {
                        MessageBox.Show("δ��ȡ����Ч���������ݣ�");
                        return;
                    }

                    // ��ʼ������
                    currentIndex = 0;
                    isPlaying = true;
                    btnPlayTrack.Text = "��ͣ����";

                    // ===== ʹ�����е� timer1 =====
                    timer1.Interval = 500; // ÿ0.5���ƶ�һ�Σ����Ե����ٶ�
                    timer1.Tick -= timer1_Tick; // �Ƚ��һ�Σ���ֹ�ظ���
                    timer1.Tick += timer1_Tick;
                    timer1.Start();

                    // ������ͼ����
                    mapControl1.Map.TrackingLayer.Clear();
                    mapControl1.Map.RefreshTrackingLayer();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("��ȡGPS����ʧ�ܣ�" + ex.Message);
                }
            }
            else
            {
                // ��ͣ����
                timer1.Stop();
                isPlaying = false;
                btnPlayTrack.Text = "����GPS�켣";
            }
        }

        private void menuLayerStyle_Click(object sender, EventArgs e)
        {
            ////���ȴ򿪡�������ͼ��
            //this.mapControl1.Map.Close();
            //this.mapControl1.Map.Open("������ͼ");
            //this.mapControl1.Map.Refresh();
            //// ����ͼ������ͼ���������ʹ��������еĵ�ͼͼ��
            //layersControl1.Map = mapControl1.Map;
            //layersControl1.Refresh();
            ////����ͼ��
            //Datasource datasource = workspace1.Datasources[0];

            //Dataset objDt = datasource.Datasets["��Ҫ��Ʒ������"];//�������ƻ��ר�����ݼ�
            //Dataset objDtText = datasource.Datasets["��Ʒ������ע��"];//�������ƻ��ר�����ݼ�
            //if (objDt == null)
            //{
            //    MessageBox.Show("����Դ��û�С���Ҫ��Ʒ�����ء������ݼ�");
            //    return;
            //}

            //Layers objLayers = this.mapControl1.Map.Layers;
            //Layer objLayer = objLayers["��Ҫ��Ʒ������@China"];//����ͼ�����ƻ��ר��ͼ��
            //Layer objLayerText = objLayers["��Ʒ������ע��@China"];//����ͼ�����ƻ��ר��ͼ��
            //Layer objLayerMainCities = objLayers["��Ҫ����@China"];
            //objLayerMainCities.IsVisible = false; //���ø�ͼ�㲻�ɼ�
            //if (objLayer == null)
            //{
            //    objLayer = objLayers.Add(objDt, true);//�����ͼ�㲻�ڵ�ǰ��ͼ�����У��򿪸����ݼ�
            //}
            //if (objLayerText == null)
            //{
            //    objLayerText = objLayers.Add(objDtText, true);//�����ݼ�
            //}
            ////������ͼ��ķ��
            //GeoStyle objStyle = new GeoStyle();
            ////objStyle.FillForeColor = Color.FromArgb(255, 140, 85);
            ////objStyle.LineColor = Color.FromArgb(255, 0, 128);
            //objStyle.FillForeColor = Color.Yellow;
            //objStyle.LineColor = Color.Green;
            //objStyle.LineWidth = 1;
            //LayerSettingVector set1 = new LayerSettingVector();
            //set1.Style = objStyle;
            //objLayer.AdditionalSetting = set1;  //������ͼ����

            ////�����ı��Ķ�����(�ں����Ǳ༭�ı�����)
            //objLayerText.IsEditable = true;
            ////�ı���ʽ
            //TextStyle objTextStyle = new TextStyle();
            //objTextStyle.FontName = "΢���ź�"; //��������
            //objTextStyle.ForeColor = Color.FromArgb(0, 0, 0);
            //objTextStyle.IsSizeFixed = false;

            //objTextStyle.FontWidth = 50000; //�������
            //objTextStyle.FontHeight = 100000;
            //objTextStyle.Bold = true; //����Ӵ�
            ////objTextStyle.Outline = true;
            ////ͨ����¼����ȡ�ı�����
            //DatasetVector objDtVText = objDtText as DatasetVector;
            //Recordset objRd = objDtVText.GetRecordset(false, CursorType.Dynamic);
            //objRd.MoveFirst();//�ƶ�����һ����¼
            //for (int i = 1; i <= objRd.RecordCount; i++)
            //{
            //    GeoText objGeoText = (GeoText)objRd.GetGeometry();//����ı����ζ���
            //    objGeoText.TextStyle = objTextStyle;//�����ı�����ķ��

            //    objRd.Edit(); //������¼
            //    objRd.SetGeometry((Geometry)objGeoText);//�����ı����
            //    objRd.Update(); //��������
            //    objRd.MoveNext();
            //}
            //objRd.Close();
            //this.mapControl1.Map.ViewEntire();//ȫ����ʾ��ͼ
            //this.mapControl1.Map.Refresh();

        }

        private void mnuUniqueTheme_Click(object sender, EventArgs e)
        {
            ////���ȴ򿪡�������ͼ��,���ӡ��¶ȴ��Ļ��֡������ݼ�
            //mapControl1.Map.Close();
            //this.mapControl1.Map.Open("������ͼ");
            //this.mapControl1.Map.Refresh();

            ////�жϲ����ӡ��¶ȴ��Ļ��֡������ݼ���������ͼ��  
            //DatasetVector objDt = workspace1.Datasources[0].Datasets["�ҹ��¶ȴ��Ļ���"] as DatasetVector;
            //if (objDt == null)
            //{
            //    MessageBox.Show("����Դ��û�С��¶ȴ��Ļ��֡������ݼ�");
            //    return;
            //}

            ////����һ����õ�ֵר��ͼ����
            //Layer objLayer = null;
            //Layers objLayers = this.mapControl1.Map.Layers;
            //objLayer = objLayers["�ҹ��¶ȴ��Ļ���@China"];//���ר��ͼ��
            ////����Ψһֵר��ͼ����
            //ThemeUnique objThemeUnique = ThemeUnique.MakeDefault(objDt, "code");

            ////�������Ϊÿ����ͬ�ĵȼ����÷����ɫ
            //Colors objColors = new Colors();
            //objColors.Add(Color.FromArgb(182, 189, 243));
            //objColors.Add(Color.FromArgb(218, 192, 224));
            //objColors.Add(Color.FromArgb(153, 210, 115));
            //objColors.Add(Color.FromArgb(253, 235, 151));
            //objColors.Add(Color.FromArgb(254, 194, 105));
            //objColors.Add(Color.FromArgb(236, 157, 70));
            ////����ѭ�������θ�ÿ����������÷��
            //for (int i = 0; i < objThemeUnique.Count; i++)
            //{
            //    GeoStyle objStyle = new GeoStyle();
            //    objStyle.FillForeColor = objColors[i];  //�����ɫ
            //    objStyle.LineWidth = 1;   //������ı��߷��
            //    objThemeUnique[i].Style = objStyle;//����ÿ����ֵ�ķ��

            //}
            //if (objLayer == null)
            //{
            //    objLayer = objLayers.Add(objDt, objThemeUnique, false);//����ר��ͼ��
            //}

            //this.mapControl1.Map.ViewEntire();
            //this.mapControl1.Map.Refresh(); //ˢ�µ�ͼ
            //// ����ͼ������ͼ���������ʹ��������еĵ�ͼͼ��
            //layersControl1.Map = mapControl1.Map;
            //layersControl1.Refresh();
        }

        private void mnuLabelTheme_Click(object sender, EventArgs e)
        {
            //mapControl1.Map.Close();
            //this.mapControl1.Map.Open("������ͼ");
            //this.mapControl1.Map.Refresh();

            //DatasetVector objDt = workspace1.Datasources[0].Datasets["�ҹ��¶ȴ��Ļ���"] as DatasetVector;
            //if (objDt == null)
            //{
            //    MessageBox.Show("����Դ��û�С��¶ȴ��Ļ��֡������ݼ�");
            //    return;
            //}
            ////������ǩר��ͼ����
            //ThemeLabel objThemeLabel = new ThemeLabel();
            //objThemeLabel.LabelExpression = "Name";//����������Ϊ��ǩ���ֶ�


            //TextStyle objTextStyle = new TextStyle();   //������ʾ�����������
            //objTextStyle.FontName = "΢���ź�";                 //������ʾ��������
            //objTextStyle.IsSizeFixed = true;
            //objTextStyle.FontWidth = 3;
            //objTextStyle.FontHeight = 5;
            //objTextStyle.Outline = true;
            //objTextStyle.ForeColor = Color.Green;

            //objThemeLabel.UniformStyle = objTextStyle;
            //mapControl1.Map.Layers.Add(objDt, true);
            //mapControl1.Map.Layers.Add(objDt, objThemeLabel, true);//����ר��ͼ��
            //this.mapControl1.Map.ViewEntire();
            //this.mapControl1.Map.Refresh(); //ˢ�µ�ͼ
            //// ����ͼ������ͼ���������ʹ��������еĵ�ͼͼ��
            //layersControl1.Map = mapControl1.Map;
            //layersControl1.Refresh();
        }

        private void mnuThemeRange_Click(object sender, EventArgs e)
        {
            //Ȼ�������꽵ˮ�������ݼ��������߷��ͻ�����ֲַ��߽�
            //�����꽵ˮ���ı����ݼ�����ʾ�꽵ˮ��ֵ

            mapControl1.Map.Close();
            this.mapControl1.Map.Open("������ͼ");
            this.mapControl1.Map.Refresh();
            //����ͼ��
            Datasource datasource = workspace1.Datasources[0];

            DatasetVector objDt = datasource.Datasets["�ҹ��꽵ˮ��"] as DatasetVector;//�������ƻ��ר�����ݼ�
            if (objDt == null)
            {
                MessageBox.Show("����Դ��û�С��ҹ��꽵ˮ���������ݼ�");
                return;
            }

            Layers objLayers = this.mapControl1.Map.Layers;
            if (objLayers == null) return;
            Layer objLayer = objLayers["�ҹ��꽵ˮ��@China"];//ע�����ִ�Сд
            ThemeRange objThemeRange = ThemeRange.MakeDefault(objDt, "rainfall", RangeMode.EqualInterval, 6);

            Colors objColors = new Colors();
            objColors.Add(Color.FromArgb(240, 243, 255));
            objColors.Add(Color.FromArgb(189, 215, 231));
            objColors.Add(Color.FromArgb(106, 174, 214));
            objColors.Add(Color.FromArgb(48, 130, 189));
            objColors.Add(Color.FromArgb(7, 89, 173));
            objColors.Add(Color.FromArgb(5, 67, 158));

            for (int i = 0; i < objThemeRange.Count; i++)
            {
                GeoStyle objStyle = new GeoStyle();
                objStyle.FillForeColor = objColors[i];
                objStyle.LineColor = Color.FromArgb(0, 255, 255, 255);
                objThemeRange[i].Style = objStyle;
            }
            if (objLayer == null)
            {
                objLayer = objLayers.Add(objDt, objThemeRange, false);//�����ݼ�
            }
            this.mapControl1.Map.ViewEntire();
            this.mapControl1.Map.Refresh();  //ˢ�µ�ͼ����
        }

        private void mnuThemeDotDensity_Click(object sender, EventArgs e)
        {
            //�����˿��ܶ�ͼ����ʾ�й����˿ڷֲ�������������˿ڷֽ���
            mapControl1.Map.Close();
            this.mapControl1.Map.Open("������ͼ");
            this.mapControl1.Map.Refresh();
            //����ͼ��
            Datasource datasource = workspace1.Datasources[0];

            DatasetVector objDt = datasource.Datasets["���������"] as DatasetVector;//�������ƻ��ר�����ݼ�
            if (objDt == null)
            {
                MessageBox.Show("����Դ��û�С�����������������ݼ�");
                return;
            }
            ThemeDotDensity objThemeDotDensity = new ThemeDotDensity(); //���ܶ�ר��ͼ����
            objThemeDotDensity.DotExpression = "Pop_1990";
            objThemeDotDensity.Value = 100;

            //�����ҹ����˿ڷֽ��ߵ���ͼ������
            Dataset objDt1 = datasource.Datasets["�ڳ����ں�"];
            Layer objLayerLine = mapControl1.Map.Layers.Add(objDt1, true); //�˿ڷֽ��߶�Ӧ��ͼ��
            GeoStyle objStyle = new GeoStyle();
            objStyle.LineColor = Color.FromArgb(173, 1, 1);
            objStyle.MarkerSize = new Size2D(4, 4);
            objThemeDotDensity.Style = objStyle;
            mapControl1.Map.Layers.Add(objDt, objThemeDotDensity, false);

            this.mapControl1.Map.ViewEntire(); //��ͼȫ����ʾ
        }

        private void mnuThemeGraph_Click(object sender, EventArgs e)
        {
            //����ͳ��ר��ͼ����ʾ97 / 98 / 98���й���ʡ�ݻ���������GDPֵ�Ա�
            mapControl1.Map.Close();
            this.mapControl1.Map.Open("������ͼ");
            this.mapControl1.Map.Refresh();
            //����ͼ��
            Datasource datasource = workspace1.Datasources[0];

            DatasetVector objDt = datasource.Datasets["���������"] as DatasetVector;//�������ƻ��ר�����ݼ�
            if (objDt == null)
            {
                MessageBox.Show("����Դ��û�С�����������������ݼ�");
                return;
            }
            ThemeGraph objThemeGraph = new ThemeGraph(); //���ͳ��ר��ͼ����

            ThemeGraphItem item1 = new ThemeGraphItem();
            item1.GraphExpression = "GDP_1997";
            GeoStyle style1 = new GeoStyle();
            style1.FillForeColor = Color.FromArgb(200, 147, 67);
            item1.UniformStyle = style1;

            ThemeGraphItem item2 = new ThemeGraphItem();
            item2.GraphExpression = "GDP_1998";
            GeoStyle style2 = new GeoStyle();
            style2.FillForeColor = Color.FromArgb(191, 79, 71);
            item2.UniformStyle = style2;

            ThemeGraphItem item3 = new ThemeGraphItem();
            item3.GraphExpression = "GDP_1999";
            GeoStyle style3 = new GeoStyle();
            style3.FillForeColor = Color.FromArgb(60, 170, 135);
            item3.UniformStyle = style3;

            objThemeGraph.Add(item1);
            objThemeGraph.Add(item2);
            objThemeGraph.Add(item3);
            objThemeGraph.IsAxesDisplayed = false;
            objThemeGraph.GraphType = ThemeGraphType.Bar;

            mapControl1.Map.Layers.Add(objDt, objThemeGraph, false);
            this.mapControl1.Map.ViewEntire();//��ͼȫ����ʾ
        }

        private void mnusave_Click(object sender, EventArgs e)
        {
            //�����ͼ
            string strMapName = "newMap";
            string strCurrentMinute = System.DateTime.Now.Minute.ToString();
            string strCurrentSecond = System.DateTime.Now.Second.ToString();
            workspace1.Maps.Add(strMapName + strCurrentMinute + strCurrentSecond, mapControl1.Map.ToXML());
            MessageBox.Show("��ͼ����ɹ�!", "��ʾ");
        }

        private void mnusaveworkspace_Click(object sender, EventArgs e)
        {
            workspace1.Save();//���湤���ռ�
        }

        private void mnuOutputMapAsFie_Click(object sender, EventArgs e)
        {
            String strPicName = string.Empty;
            saveFileDialog1.Filter = "PNG Files(.png)|*.png||";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                strPicName = saveFileDialog1.FileName;  //����Ҫ�����Ӱ���ļ���
                if (this.mapControl1.Map.OutputMapToPNG(strPicName, false))
                {
                    MessageBox.Show("�����ͼΪӰ���ļ��ɹ���");
                }
                else
                {
                    MessageBox.Show("�����ͼΪӰ���ļ�ʧ�ܣ�");
                }
            }
            else
            {
                return;
            }
            this.mapControl1.Map.Refresh();
        }
    }
}
