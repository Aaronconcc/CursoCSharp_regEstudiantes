using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Label = System.Windows.Forms.Label;

namespace Logica.Library
{
    public class Paginador<T>
    {
        private List<T> _dataList;
        private Label _label;
        private static int maxReg, regPorPagina, pageCount, numPag = 1;

        public Paginador(List<T> dataList, Label label, int regPorPag)
        {
            _dataList = dataList;
            _label = label;
            regPorPagina = regPorPag;
            cargarDatos();
        }

        private void cargarDatos()
        {
            numPag = 1;
            maxReg = _dataList.Count;
            pageCount = (maxReg/regPorPagina);
            if ((maxReg % regPorPagina) > 0)
            {
                pageCount = +1;
            }
            _label.Text = $"Páginas 1/{pageCount}";
        }

        public int primero()
        {
            numPag = 1;
            _label.Text = $"Página {numPag}/{pageCount}";
            return numPag;
        }

        public int anterior()
        {
            if (numPag > 1)
            {
                numPag -= 1;
                _label.Text = $"Página {numPag}/{pageCount}";
            }
            return numPag;
        }

        public int siguiente()
        {
            if (numPag == pageCount)
            {
                numPag -= 1;
            }
            if (numPag < pageCount)
            {
                numPag += 1;
                _label.Text = $"Página {numPag}/{pageCount}";
            }
            return numPag;
        }

        public int ultimo()
        {
            numPag = pageCount;
            _label.Text = $"Página {numPag}/{pageCount}";
            return numPag;
        }
    }
}
