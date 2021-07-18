using System;
using System.Collections.Generic;
using System.Text;

namespace Neural_Network
{
    public class NeuronLayer
    {
        public List<Neuron> neurons;
        public NeuronLayer(int n_neurons)
        {
            neurons = new List<Neuron>();
            for (int i = 0; i < n_neurons; i++)
            {
                neurons.Add((new Neuron()));
            }
        }
    }
}

