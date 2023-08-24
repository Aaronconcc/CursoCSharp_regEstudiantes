using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Alumno
    {
        [PrimaryKey, Identity]
        public string matricula {set; get; }
        public string nombre { set; get; }
        public string apellido { set; get; }
        public string correo { set; get; }
        public byte[] imagen { set; get; }
    }
}
