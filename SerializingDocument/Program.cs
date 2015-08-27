using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using ClassContainer;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SerializingDocument
{
   public class DataManipulation
    {
        // 1. File txt to File xml
        public void Serialized_TxtInXml(string _textFile, string _xmlFile)
        {
            List<string> list = new List<string>();

            if (_textFile.Contains(".txt") && _xmlFile.Contains(".xml"))
            {
                try
                {
                    using (StreamReader fileReader = new StreamReader(_textFile))
                    {

                        while (fileReader.ReadLine() != null)
                        {
                            list.Add(fileReader.ReadToEnd());
                        }
                    }
                    using (StreamWriter file = new StreamWriter(_xmlFile))
                    {
                        XmlSerializer xmlSerialized = new XmlSerializer(typeof(List<string>));
                        xmlSerialized.Serialize(file, list);
                    }
                }
                catch (FileNotFoundException ex) { Console.WriteLine(ex.Message); }
            }
            else Console.WriteLine("Wrong type of file");
        }

        // 2. Object to File xml
        public void Serialized_Object(string _xmlFile, object _objectForSerializing)
        {
            if (_xmlFile.Contains(".xml"))
            {
                try
                {
                    using (StreamWriter file = new StreamWriter(_xmlFile))
                    {
                        XmlSerializer xmlSerialized = new XmlSerializer(_objectForSerializing.GetType());
                        xmlSerialized.Serialize(file, _objectForSerializing);
                    }
                }
                catch (FileNotFoundException ex) { Console.WriteLine(ex.Message); }
            }
            else Console.WriteLine("Wrong type of file");
        }
       
       // 3. From Xml into Class
        public item[] GetDeserialized_XMLToObject(string _xmlFile)
        {
            item [] temp = null;
            if (_xmlFile.Contains(".xml"))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(_xmlFile))
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(item[]));
                       temp = (item[])xmlSerializer.Deserialize(reader);
                    }
                }
                catch (FileNotFoundException) { }
            }
            else Console.WriteLine("Wrong type of file");
            return temp;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            string _textFilePath = "../../TextContainer.txt";
            string _xmlFilePath = "../../XMLContainer.xml";
            DataManipulation dataManipulation = new DataManipulation();
            item[] items = null;
  
      //1.   Invoke File to File
           // dataManipulation.Serialized_TxtInXml(_textFilePath,_xmlFilePath);
            
      // 2.  Invoke Object to File
           // dataManipulation.Serialized_Object(_xmlFilePath, GetListOfStaff());
       
      // 3. XML to Class
           items = dataManipulation.GetDeserialized_XMLToObject(_xmlFilePath);
      
      // 4. Connect and Add data from XML To DataSQL  
            ADO_Connection.AddToDataBase(items);
           // ADO_Connection.CleanData();
  
            Console.ReadKey();
        }
    }
}
