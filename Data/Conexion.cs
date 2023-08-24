using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Conexion : DataConnection
    {
        public Conexion() : base("Conect1") { }

        public ITable<Alumno> _Alumno { get { return this.GetTable<Alumno>(); } }
 
    }
}
