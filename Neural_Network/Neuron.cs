using System;
using System.Collections.Generic;
using System.Text;


namespace Neural_Network
{
    public class Neuron
    {
        public List<Connection> Connections { get; set; }
        public Pulse OutputPulse { get; set; }

        public Neuron()
        {
            Connections = new List<Connection>();
            OutputPulse = new Pulse();
        }
       
        public void Fire()
        {
            Calculation(Connections);
        }

        public void Calculation(List<Connection> connections)
        {
            double temp = 0;
            foreach (var connection in connections)
            {
                temp += connection.InputPulse.Value * connection.SynapseWeight;
            }

            OutputPulse.Value = ActivationFunction(temp);

        }

        public double ActivationFunction(double value)
        {
            return 1.0 / (1.0 + Math.Exp(-value));
        }

    }
}