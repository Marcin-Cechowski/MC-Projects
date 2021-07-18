using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Neural_Network
{
    public partial class Form1 : Form
    {
        public int _epochs { get; set; }
        public double _learningRate { get; set; }

        public string _fileName { get; set; }
        public List<int> _networkModel { get; set; }
        public List<List<double>> _dataInstances { get; set; }
        public List<double> _groundTruth { get; set; }
        public List<List<double>> x_dataInstances { get; set; }
        public NeuralNetworkModel model { get; set; }

        public Form1()
        {
            InitializeComponent();
        }


        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
           
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
 
        }

        private void Epochs_TextChanged(object sender, EventArgs e)
        {
      
        }

        private void data_instance_1_TextChanged(object sender, EventArgs e)
        {
  
        }

        private void data_instance_2_TextChanged(object sender, EventArgs e)
        {
     
        }

        private void data_instance_3_TextChanged(object sender, EventArgs e)
        {
      
        }

        private void data_instance_4_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void ground_truth_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void Train_btn_Click(object sender, EventArgs e)
        {
           
            _dataInstances = new List<List<double>>();
            x_dataInstances = new List<List<double>>();
            _groundTruth = new List<double>();
            _epochs = Convert.ToInt32(Epochs.Text);
            _learningRate = Convert.ToDouble(LearningRate.Value);


            var test = textBox1.Text.Split('\n', '\r');

            foreach (var ura in test)
            {
                var newList = new List<double>();
                var temp = ura.Split(' ');
                if (ura == string.Empty)
                {
                    continue;
                }
                else
                {
                    foreach (var ora in temp)
                    {
                        newList.Add(Convert.ToDouble(ora));
                    }
                    _dataInstances.Add(newList.ToList());
                }
               
            }

           
            var tempData = ground_truth.Text.Split(';');

            
            foreach (var data in tempData)
            {
                
                var newData = data.Split(' ');
   
                _groundTruth = new List<double>();
                foreach (var ndata in newData)
                {
                    if (ndata == "")
                    {
                        continue;

                    }
                    _groundTruth.Add(Convert.ToDouble(ndata));
                }
                if (_groundTruth.Count != 0)
                    x_dataInstances.Add(_groundTruth.ToList());
            }


            DataTable table = new DataTable();
            var errorList = new List<double>();
            var epochList = new List<double>();
            var outputList = new List<List<double>>();
            var gtList = new List<List<double>>();
            var gt_out = new List<List<double>>();
            var bpOutputList = new List<List<double>>();

            table.Columns.Add("Current Epoch", typeof(int));
            table.Columns.Add("Current Error", typeof(double));
            table.Columns.Add("GT", typeof(double));
            table.Columns.Add("Out", typeof(double));
            table.Columns.Add("GT-Out", typeof(double));
            table.Columns.Add("Output after BP", typeof(double));
            
            List<int> dataIndexes = new List<int>();
            int k = 0;
            int epochs = 0;
            while (epochs != _epochs)
            {
                dataIndexes = model.Train_ops(_dataInstances, x_dataInstances, _learningRate);
                errorList = model.errorTotalValue.ToList();
                epochList = model.epoch_counter.ToList();
                outputList = model._neuron_output.ToList();
                foreach (var index in dataIndexes)
                {
                    var input = _dataInstances[index];
                    model.FeedForward(input);
                    bpOutputList.Add(model.neuron_outputs.Last());

                }
               
                
                gtList = model._groundTruth.ToList();
                gt_out = model._error_GT_output.ToList();
                
                if (epochs % 50 == 0)
                {
                    int m = 0;
                    for (int j = 0; j < x_dataInstances.Count * x_dataInstances[0].Count; j++)
                    {
                        if (j == 0)
                        {
                            table.Rows.Add(epochs, errorList[k], gtList[j][k], outputList[j][k], gt_out[j][k], bpOutputList[m][j % x_dataInstances[0].Count]);
                        }
                        else
                        {
                            if (j % x_dataInstances[0].Count == 0)
                            {
                                table.Rows.Add(null, null, null, null, null, null);
                                m++;
                            }
                            table.Rows.Add(null, null, gtList[j][k], outputList[j][k], gt_out[j][k], bpOutputList[m][j % x_dataInstances[0].Count]);
                           
                        }

                    }
                }
                errorList.Clear();
                gt_out.Clear();
                gtList.Clear();
                bpOutputList.Clear();
                epochs++;

            }


            dataGridView1.DataSource = table;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void generateModel_Click(object sender, EventArgs e)
        {
            _networkModel = new List<int>();
            _fileName = fileName.Text;

            var tempModel = networkModel.Text.Split(',');
            foreach (var model in tempModel)
            {
                _networkModel.Add(Convert.ToInt32(model));
            }
            model = new NeuralNetworkModel(_networkModel,_fileName,false);


           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            _fileName = fileName.Text;
            _networkModel = new List<int>();
            var tempModel = networkModel.Text.Split(',');
            foreach (var model in tempModel)
            {
                _networkModel.Add(Convert.ToInt32(model));
            }
            model = new NeuralNetworkModel(_networkModel, _fileName);
        }

        private void label7_Click(object sender, EventArgs e)
        {
       
        }

      

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
