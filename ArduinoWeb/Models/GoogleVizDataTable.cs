using System.Collections.Generic;
using System.Linq;

namespace ArduinoWeb.Models
{
    /// <summary>
    /// Classe usada para facilitar a serialização do JSON num formato conhecido pelo Google
    /// para criar a Data Table que permite construir um gráfico. 
    /// </summary>
    public class GoogleVizDataTable
    {
        public IList<Col> cols { get; set; } = new List<Col>();

        public IList<Row> rows { get; set; } = new List<Row>();

        public class Col
        {
            public string label { get; set; }
            public string type { get; set; }
        }

        public class Row
        {
            public IEnumerable<RowValue> c { get; set; }

            public class RowValue
            {
                public object v;
            }
        }
    }
}