using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saobracaj.Dokumenta.TrainList
{
    public class TrainListModel
    {
        public int Id { get; set; }
        public DateTime VremeDolaska { get; set; }
        public string KomOznaka { get; set; }
        public string Napomena { get; set; }
    }
}

