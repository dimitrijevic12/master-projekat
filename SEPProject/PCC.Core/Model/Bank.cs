using System;

namespace PCC.Core.Model
{
    public class Bank
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string PAN { get; private set; }
        public Uri ServerAddress { get; private set; }

        public Bank(Guid id, string name, string pAN, Uri serverAddress)
        {
            Id = id;
            Name = name;
            PAN = pAN;
            ServerAddress = serverAddress;
        }
    }
}