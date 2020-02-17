using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_Wpf_TheSimpleGame.Models;

namespace Demo_Wpf_TheSimpleGame.Data
{
    public interface IDataService
    {
        List<Player> ReadAll();
        void WriteAll(List<Player> players);
    }
}
