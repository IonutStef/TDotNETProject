using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LectorASP.Models
{
    public class DataBaseShowCustomContainer
    {
        public Guid ID { get; set; }

        public string FirsStringField { get; set; }

        public string SecondStringField { get; set; }

        public string ThirdStringField { get; set; }
    }


    public struct IDNamePair
    {
        public IDNamePair(Guid id, String name, int val = 0)
        {
            ID = id.ToString();
            Name = name;
            Value = val;
        }

        public override string ToString()
        {
            return (string.Format("({0}, {1} - {2})", ID, Name, Value));
        }

        public string ID { set; get; }
        public string Name { set; get; }
        public int Value { set; get; }
    }
}