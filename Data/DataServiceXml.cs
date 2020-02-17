using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using Demo_Wpf_TheSimpleGame.Models;

namespace Demo_Wpf_TheSimpleGame.Data
{
    public class DataServiceXml : IDataService
    {
        private string _dataFilePath;

        /// <summary>
        /// read the xml file and load a list of character objects
        /// </summary>
        public List<Player> ReadAll()
        {
            List<Player> players = new List<Player>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Player>), new XmlRootAttribute("Players"));

            try
            {
                StreamReader reader = new StreamReader(_dataFilePath);
                using (reader)
                {
                    players = (List<Player>)serializer.Deserialize(reader);
                }

            }
            catch (Exception)
            {
                throw; // all exceptions are handled in the ListForm class
            }

            return players;
        }

        /// <summary>
        /// write the current list of characters to the xml data file
        /// </summary>
        public void WriteAll(List<Player> players)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Player>), new XmlRootAttribute("Players"));

            try
            {
                StreamWriter writer = new StreamWriter(_dataFilePath);
                using (writer)
                {
                    serializer.Serialize(writer, players);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataServiceXml()
        {
            _dataFilePath = @"Data\Data.xml";
        }
    }
}
