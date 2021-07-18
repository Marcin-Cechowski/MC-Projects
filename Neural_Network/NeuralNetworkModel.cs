using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.IO;


namespace Neural_Network
{

    public class NeuralNetworkModel
    {
        public List<NeuronLayer> layers { get; set; }

        public List<List<double>> weights { get; set; }
        public List<List<double>> neuron_outputs { get; set; }
        public List<double> errorTotalValue { get; set; }
        public List<double> epoch_counter { get; set; }
        public List<List<double>> _neuron_output { get; set; }

        public List<List<double>> _groundTruth { get; set; }

        public List<List<double>> _error_GT_output { get; set; }

        public List<double> output_after_bp { get; set; }

        Random rnd = new Random();

        static string Filename { get; set; }

        public NeuralNetworkModel(List<int> InputNumbers, string fileName, bool WeightsGenereated = false)
        {

            layers = new List<NeuronLayer>();
            weights = new List<List<double>>();
            neuron_outputs = new List<List<double>>();
            epoch_counter = new List<double>();
            errorTotalValue = new List<double>();
            _groundTruth = new List<List<double>>();
            _error_GT_output = new List<List<double>>();
            output_after_bp = new List<double>();
            Filename = fileName;
            

            int InputCounter = InputNumbers.Count();


            foreach (var number in InputNumbers)
            {
                if (InputCounter == 1)
                {
                    AddLayer(new NeuronLayer(number), true);
                }
                else
                {
                    AddLayer(new NeuronLayer(number + 1), false);
                    InputCounter--;
                }
            }
            if (File.Exists(Filename) &&  new FileInfo(Filename).Length == 0)
            {
                WriteWeightsToFile(Filename);
            }
            else
            {
                File.Create(Filename).Close();


                WriteWeightsToFile(Filename);
            }

        }
        public NeuralNetworkModel(List<int> InputNumbers, string fileName)
        {
            layers = new List<NeuronLayer>();
            weights = new List<List<double>>();
            neuron_outputs = new List<List<double>>();
            epoch_counter = new List<double>();
            errorTotalValue = new List<double>();
            _groundTruth = new List<List<double>>();
            _error_GT_output = new List<List<double>>();

            int InputCounter = InputNumbers.Count();


            foreach (var number in InputNumbers)
            {
                if (InputCounter == 1)
                {
                    AddLayer(new NeuronLayer(number), true);
                }
                else
                {
                    AddLayer(new NeuronLayer(number + 1), false);
                    InputCounter--;
                }
            }

            InitialiseLayers(fileName);
            Filename = fileName;

        }
        public void WriteWeightsToFile(string fileName)
        {

            using (StreamWriter sp = new StreamWriter(fileName))
            {
                int j = 0;
                List<double> weights = new List<double>();
                foreach (var layer in layers)
                {
                    foreach (var neuron in layer.neurons)
                    {
                        foreach (var connection in neuron.Connections)
                        {
                            weights.Add(connection.SynapseWeight);
                        }
                    }
                }
                string weights_line = String.Join(' ', weights);
                sp.WriteLine(weights_line);
            }
        }

        //public void InitialiseLayers(string fileName)
        //{
        //    using (StreamReader sp = new StreamReader(fileName))
        //    {
        //        int j = 0;
        //        weights = new List<List<double>>();
        //        var weight_line = sp.ReadLine().Split(' ');
        //        foreach (var weight in weight_line)
        //        {

        //            weights.Add(Convert.ToDouble(weight));
        //        }
        //        foreach (var layer in layers)
        //        {
        //            if (layers.IndexOf(layer) == 0)
        //            {
        //                continue;
        //            }
        //            foreach (var neuron in layer.neurons)
        //            {
        //                foreach (var connection in neuron.Connections)
        //                {
        //                    connection.SynapseWeight = weights[j];
        //                    j++;
        //                }
        //            }
        //        }

        //    }

        //}

        public void InitialiseLayers(string fileName)
        {
            using (StreamReader sp = new StreamReader(fileName))
            {
                int j = 0;
                weights = new List<List<double>>();
                var weight_line = sp.ReadLine().Split(' ');
                int i = 0;
                int numL = 1;
                List<double> tempW = new List<double>();
                // Kiedy jest jeden output neuron
                if (layers.Last().neurons.Count == 1)
                {

                    foreach (var weight in weight_line)
                    {
                        if (i == 0)
                        {
                            tempW = new List<double>();
                            tempW.Add(Convert.ToDouble(weight));
                            i++;
                            continue;
                        }
                        else
                        {
                            tempW.Add(Convert.ToDouble(weight));
                            i++;
                        }

                        if (i == ((layers[numL].neurons.Count - 1) * layers[numL].neurons[0].Connections.Count))
                        {
                            weights.Add(tempW.ToList());
                            i = 0;
                            numL++;
                        }
                        else if (i == layers[numL].neurons.Count * layers[numL].neurons[0].Connections.Count && numL == layers.Count - 1)
                        {
                            weights.Add(tempW.ToList());
                        }

                    }

                    foreach (var layer in layers)
                    {
                        if (layers.IndexOf(layer) == 0)
                        {
                            continue;
                        }

                        j = 0;
                        foreach (var neuron in layer.neurons)
                        {
                            foreach (var connection in neuron.Connections)
                            {
                                connection.SynapseWeight = weights[layers.IndexOf(layer) - 1][j];
                                j++;
                            }
                        }
                    }
                }
                else
                {
                    foreach (var weight in weight_line)
                    {
                        if (i == 0)
                        {
                            tempW = new List<double>();
                            tempW.Add(Convert.ToDouble(weight));
                            i++;
                            continue;
                        }
                        else
                        {
                            tempW.Add(Convert.ToDouble(weight));
                            i++;
                        }

                        if (i == ((layers[numL].neurons.Count - 1) * layers[numL].neurons[0].Connections.Count) && numL != layers.Count - 1)
                        {
                            weights.Add(tempW.ToList());
                            i = 0;
                            numL++;
                        }
                        else if (i == layers[numL].neurons.Count * layers[numL].neurons[0].Connections.Count && numL == layers.Count - 1)
                        {
                            weights.Add(tempW.ToList());
                        }
                    }

                    foreach (var layer in layers)
                    {
                        if (layers.IndexOf(layer) == 0)
                        {
                            continue;
                        }

                        j = 0;
                        foreach (var neuron in layer.neurons)
                        {
                            foreach (var connection in neuron.Connections)
                            {
                                connection.SynapseWeight = weights[layers.IndexOf(layer) - 1][j];
                                j++;
                            }
                        }
                    }
                }

            }

        }


        public void AddLayer(NeuronLayer layer, bool _last)
        {
            int dendriteCounter = 1;
            if (layers.Count > 0)
            {
                dendriteCounter = layers.Last().neurons.Count;
            }

            Neuron last = layer.neurons.Last();

            double bias = rnd.NextDouble();
            layers.Add(layer);

            if (layers.Count > 1)
            {
                foreach (var neuron in layer.neurons)
                {
                    if (neuron == last && layer.neurons.Count() != 1 && !_last)
                    {
                        continue;
                    }

                    if (dendriteCounter == 1)
                    {
                        continue;
                    }
                    else
                    {
                        for (int i = 0; i < dendriteCounter; i++)
                        {

                            if (i == dendriteCounter - 1)
                            {
                                neuron.Connections.Add(new Connection(bias));
                                continue;
                            }

                            double init_weight = rnd.NextDouble();
                            var new_weight = Convert.ToDouble((String.Format("{0:0.00}", init_weight).Replace('.', ',')));

                            var conn = new Connection(new_weight);
                            neuron.Connections.Add(conn);

                        }
                    }
                }


            }
        }


        public void FeedForward(List<double> inputValues)
        {
            List<double> outputs = new List<double>();
            List<double> inputs = new List<double>();
            neuron_outputs = new List<List<double>>();

            int i = 0;
            foreach (var layer in layers)
            {


                if (layers.IndexOf(layer) == 0)
                {

                    foreach (var neuron in layer.neurons)
                    {
                        if (i == layer.neurons.Count - 1)
                        {
                            var next_layer = layers[layers.IndexOf(layer) + 1];
                            var bias = next_layer.neurons.Take(1).ToList();
                            outputs.Add(bias[0].Connections.Last().SynapseWeight);
                            continue;
                        }
                        neuron.OutputPulse.Value = inputValues[i];
                        i++;
                        outputs.Add(neuron.OutputPulse.Value);
                    }

                    neuron_outputs.Add(outputs.ToList());
                    outputs = new List<double>();
                }
                else
                {
                    foreach (var neuron in layer.neurons)
                    {

                        if (neuron.Connections.Count == 0 && neuron == layer.neurons.Last() && layer != layers.Last())
                        {
                            var next_layer = layers[layers.IndexOf(layer) + 1];
                            var bias = next_layer.neurons.Take(1).ToList();
                            outputs.Add(bias[0].Connections.Last().SynapseWeight);

                            continue;
                        }
                        else
                        {
                            foreach (var conn in neuron.Connections)
                            {
                                if (conn == neuron.Connections.Last())
                                {
                                    continue;
                                }

                                conn.InputPulse.Value =
                                    neuron_outputs[layers.IndexOf(layer) - 1][neuron.Connections.IndexOf(conn)];
                            }
                            neuron.Fire();

                            outputs.Add(neuron.OutputPulse.Value);
                        }
                    }
                    neuron_outputs.Add(outputs.ToList());
                    outputs = new List<double>();
                }

            }

        }
        public double BackPropagation(List<List<double>> data_instance, List<List<double>> groundTruth,
                                     List<List<List<double>>> ff_dataInstance, double learningRate)
        {



            double error_total = 0;

            foreach (var input in data_instance)
            {

                var error_instance = 0.0;


                double delta_neuron = 0;
                List<List<double>> deltas = new List<List<double>>();
                List<double> tempDelta = new List<double>();
                int j = 0;
                for (int i = layers.Count - 1; i > 0; i--)
                {
                    int k = 0;
                    var temp = layers[i];

                    foreach (var neuron in temp.neurons)
                    {

                        var n_output = ff_dataInstance[data_instance.IndexOf(input)][i][temp.neurons.IndexOf(neuron)];


                        if (temp == layers.Last())
                        {
                            error_instance += Math.Pow(groundTruth[data_instance.IndexOf(input)][temp.neurons.IndexOf(neuron)] - n_output, 2) / 2;

                            delta_neuron = (-groundTruth[data_instance.IndexOf(input)][temp.neurons.IndexOf(neuron)] + n_output) * n_output * (1 - n_output);

                            tempDelta.Add(delta_neuron);
                        }

                        else if (neuron != temp.neurons.Last())
                        {
                            delta_neuron = 0;
                            foreach (var n in layers[i + 1].neurons)
                            {
                                if (layers[i + 1] == layers.Last())
                                {
                                    if (k >= weights[i].Count)
                                    {
                                        k = layers[i].neurons.IndexOf(neuron);
                                    }

                                    delta_neuron += deltas[j - 1][layers[i + 1].neurons.IndexOf(n)] *
                                                    weights[i][k];
                                    k += layers[i].neurons.Count;
                                    continue;
                                }
                                if (n == layers[i + 1].neurons.Last() && layers[i + 1] != layers.Last())
                                {
                                    continue;
                                }
                                if (k >= weights[i].Count)
                                {
                                    k = layers[i].neurons.IndexOf(neuron);
                                }

                                delta_neuron += deltas[j - 1][layers[i + 1].neurons.IndexOf(n)] *
                                                weights[i][k];
                                k += layers[i].neurons.Count;
                            }
                            tempDelta.Add(delta_neuron);
                        }
                    }
                    deltas.Add(tempDelta.ToList());
                    tempDelta.Clear();

                    foreach (var neuron in temp.neurons)
                    {
                        if (neuron == temp.neurons.Last() && temp.neurons.Count > 1 && temp != layers.Last())
                        {
                            continue;
                        }

                        foreach (var conn in neuron.Connections)
                        {

                            var poch_output_neuron =
                                ff_dataInstance[data_instance.IndexOf(input)][i][temp.neurons.IndexOf(neuron)] *
                                (1 - ff_dataInstance[data_instance.IndexOf(input)][i][
                                    temp.neurons.IndexOf(neuron)]);

                            var poch_net_waga = ff_dataInstance[data_instance.IndexOf(input)][i - 1][neuron.Connections.IndexOf(conn)];
                            if (i == layers.Count() - 1)
                            {
                                if (conn == neuron.Connections.Last())
                                {
                                    conn.SynapseWeight = conn.SynapseWeight - learningRate *
                                        deltas[j][temp.neurons.IndexOf(neuron)];
                                }
                                else
                                {
                                    conn.SynapseWeight = conn.SynapseWeight - learningRate *
                                        deltas[j][temp.neurons.IndexOf(neuron)] *
                                        poch_net_waga;
                                }

                            }
                            else
                            {
                                if (conn == neuron.Connections.Last())
                                {
                                    conn.SynapseWeight = conn.SynapseWeight - learningRate *
                                        deltas[j][temp.neurons.IndexOf(neuron)] * poch_output_neuron;
                                }
                                else
                                {
                                    conn.SynapseWeight = conn.SynapseWeight - learningRate *
                                        deltas[j][temp.neurons.IndexOf(neuron)] * poch_output_neuron *
                                        poch_net_waga;
                                }
                            }
                        }
                    }

                    j++;
                }
                WriteWeightsToFile(Filename);
                error_total += error_instance;
            }

            weights.Clear();
            foreach (var layer in layers)
            {
                List<double> tempW = new List<double>();
                foreach (var n in layer.neurons)
                {
                    foreach (var conn in n.Connections)
                    {
                        tempW.Add(conn.SynapseWeight);
                    }
                }
                weights.Add(tempW.ToList());
            }

            return error_total;

        }
        public void Train(int epochs, double learningRate)
        {

            List<List<List<double>>> ff_dataInstance = new List<List<List<double>>>();
            List<List<double>> data_instance = new List<List<double>>();


            data_instance.Add(new List<double>(new[] { 0.0, 0.0 }));
            data_instance.Add(new List<double>(new[] { 0.0, 1.0 }));
            data_instance.Add(new List<double>(new[] { 1.0, 0.0 }));
            data_instance.Add(new List<double>(new[] { 1.0, 1.0 }));

            List<List<double>> groundTruth = new List<List<double>>();
            groundTruth.Add(new List<double>(new[] { 0.0 }));
            groundTruth.Add(new List<double>(new[] { 1.0 }));
            groundTruth.Add(new List<double>(new[] { 1.0 }));
            groundTruth.Add(new List<double>(new[] { 0.0 }));


            var _epochs = 0;
            int i = 0;

            double error_total = 1000;
            double error_p = 0;
            double epsilon = 0.000001;
            while (_epochs != epochs)
            {
                ff_dataInstance.Clear();
                foreach (var input in data_instance)
                {
                    FeedForward(input);
                    int counter = 0;
                    foreach (var n in neuron_outputs.Last())
                    {
                        Console.WriteLine($"Output and GT: {n - groundTruth[data_instance.IndexOf(input)][counter]}");
                        counter++;
                    }

                    ff_dataInstance.Add(neuron_outputs.ToList());
                }

                error_total = BackPropagation(data_instance, groundTruth, ff_dataInstance, learningRate);
                if (i == 100)
                {
                    var dramat = error_total;
                }
                if (i++ >= epochs)
                {

                }

                if (Math.Abs(error_p - error_total) < epsilon)
                {
                    break;
                }

                error_p = error_total;
                _epochs++;
            }

        }

        public void Train_op(List<List<double>> data_instances, List<List<double>> ground_truth, double learningRate, int epochs)
        {
            List<List<List<double>>> ff_dataInstance = new List<List<List<double>>>();
            List<List<double>> newDataInstances = new List<List<double>>();
            List<List<double>> newGroundTruth = new List<List<double>>();


            Random rand = new Random();

            double error_p = 0;
            double epsilon = 0.000001;
            int _epochs = 0;


            InitialiseLayers(Filename);

            errorTotalValue = new List<double>();
            _neuron_output = new List<List<double>>();
            _error_GT_output = new List<List<double>>();
            _groundTruth = new List<List<double>>();




            int index = 0;

            double error_total = 1000;
            while (epochs != _epochs)
            {
                var random = Enumerable.Range(0, data_instances.Count).OrderBy(x => rand.Next()).ToList();
                for (int i = 0; i < data_instances.Count; i++)
                {
                    newDataInstances.Add(data_instances[random[i]].ToList());
                    newGroundTruth.Add(ground_truth[random[i]].ToList());
                }

                ff_dataInstance.Clear();
                foreach (var input in newDataInstances)
                {
                    FeedForward(input);
                    int counter = 0;

                    if (_epochs % 50 == 0)
                    {
                        foreach (var n in neuron_outputs.Last())
                        {


                            _neuron_output.Add(new List<double>().ToList());
                            _error_GT_output.Add(new List<double>().ToList());
                            _groundTruth.Add(new List<double>().ToList());

                            var result = Math.Abs(n - newGroundTruth[newDataInstances.IndexOf(input)][counter]);
                            _groundTruth[index].Add(newGroundTruth[newDataInstances.IndexOf(input)][counter]);
                            _neuron_output[index].Add(n);
                            _error_GT_output[index].Add(result);
                            counter++;
                            index++;
                        }
                    }

                    ff_dataInstance.Add(neuron_outputs.ToList());
                }
                error_total = BackPropagation(newDataInstances, newGroundTruth, ff_dataInstance, learningRate);
                if (_epochs % 50 == 0)
                {
                    errorTotalValue.Add(error_total);
                    epoch_counter.Add(_epochs);
                }

                if (Math.Abs(error_p - error_total) < epsilon)
                {
                    break;
                }

                error_p = error_total;

                _epochs++;

                newGroundTruth.Clear();
                newDataInstances.Clear();
            }
        }
        public List<int> Train_ops(List<List<double>> data_instances, List<List<double>> ground_truth, double learningRate)
        {
            List<List<List<double>>> ff_dataInstance = new List<List<List<double>>>();
            List<List<double>> newDataInstances = new List<List<double>>();
            List<List<double>> newGroundTruth = new List<List<double>>();

            Random rand = new Random();

            InitialiseLayers(Filename);

            errorTotalValue = new List<double>();
            _neuron_output = new List<List<double>>();
            _error_GT_output = new List<List<double>>();
            _groundTruth = new List<List<double>>();

            int index = 0;
            double error_total = 1000;

            var random = Enumerable.Range(0, data_instances.Count).OrderBy(x => rand.Next()).ToList();
            for (int i = 0; i < data_instances.Count; i++)
            {
                newDataInstances.Add(data_instances[random[i]].ToList());
                newGroundTruth.Add(ground_truth[random[i]].ToList());
            }

            ff_dataInstance.Clear();
            foreach (var input in newDataInstances)
            {
                FeedForward(input);
                int counter = 0;

                foreach (var n in neuron_outputs.Last())
                {

                    _neuron_output.Add(new List<double>().ToList());
                    _error_GT_output.Add(new List<double>().ToList());
                    _groundTruth.Add(new List<double>().ToList());

                    var result = Math.Abs(n - newGroundTruth[newDataInstances.IndexOf(input)][counter]);
                    _groundTruth[index].Add(newGroundTruth[newDataInstances.IndexOf(input)][counter]);
                    _neuron_output[index].Add(n);
                    _error_GT_output[index].Add(result);
                    counter++;
                    index++;
                }
                ff_dataInstance.Add(neuron_outputs.ToList());
            }
            error_total = BackPropagation(newDataInstances, newGroundTruth, ff_dataInstance, learningRate);

            errorTotalValue.Add(error_total);

            newGroundTruth.Clear();
            newDataInstances.Clear();
            return random;

        }
    }
}








