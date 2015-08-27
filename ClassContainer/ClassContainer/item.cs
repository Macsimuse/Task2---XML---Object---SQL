using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ClassContainer
{
    [XmlRoot("Item`")]
    [Serializable]
    public class item
    {
        public int Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Job> listJob = new List<Job>();
        public List<Position> listPosition = new List<Position>();
    }
}
