using System;
using System.Collections.Generic;
using System.Text;

namespace Neural_Network
{
    public class Connection
    {
        public Pulse InputPulse { get; set; }
        public double SynapseWeight { get; set; }

        public Connection(double init_weight)
        {
            SynapseWeight = init_weight;
            InputPulse = new Pulse();
            InputPulse.Value = 1;
        }
    }
}