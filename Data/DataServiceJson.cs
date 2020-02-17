using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using Newtonsoft;
using Newtonsoft.Json;
using Demo_Wpf_TheSimpleGame.Models;
using Demo_Wpf_TheSimpleGame.Data;

namespace Demo_Wpf_TheSimpleGame.Data
{
    public class DataServiceJson : IDataService
    {
        private string _dataFilePath;


        /// <summary>
        /// read the json file and load a list of character objects
        /// </summary>
        public List<Player> ReadAll()
        {
            List<Player> players;

            try
            {
                using (StreamReader sr = new StreamReader(_dataFilePath))
                {
                    string jsonString = sr.ReadToEnd();

                    players = JsonConvert.DeserializeObject<List<Player>>(jsonString);
                }

            }
            catch (Exception)
            {
                throw;
            }

            return players;
        }

        /// <summary>
        /// write the current list of players to the json data file
        /// </summary>
        public void WriteAll(List<Player> characters)
        {
            string jsonString = JsonConvert.SerializeObject(characters, Formatting.Indented);

            try
            {
                StreamWriter writer = new StreamWriter(_dataFilePath);
                using (writer)
                {
                    writer.WriteLine(jsonString);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataServiceJson()
        {
            _dataFilePath = @"Data\Data.json";
        }
    }
}
