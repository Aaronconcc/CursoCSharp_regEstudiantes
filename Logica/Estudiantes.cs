using Data;
using LinqToDB;
using Logica.Library;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logica
{
    public class Estudiantes : Libreria
    {
        private List<TextBox> listTextBox;
        private List<Label> listLabel;
        private PictureBox image;
        private Bitmap bmp;
        private DataGridView view;
        private NumericUpDown _numericUpDown;
        private Paginador<Alumno> _paginador;
        private int numPagina = 1;
        private int regPorPagina = 2;
        private string _accion = "Insert";

        //private Libreria libreria;

        public Estudiantes(List<TextBox> listTextBox, List<Label> listLabel, object[] objects)
        {
            this.listTextBox = listTextBox;
            this.listLabel = listLabel;
            //libreria = new Libreria();
            image = (PictureBox)objects[0];
            bmp = (Bitmap)objects[1];
            view = (DataGridView)objects[2];
            _numericUpDown = (NumericUpDown)objects[3];
            restablecer();
        }

        public void Registrar()
        {
            if (listTextBox[0].Text.Equals(""))
            {
                listLabel[0].Text = "El campo es requerido";
                listLabel[0].ForeColor = Color.Red;
                listTextBox[0].Focus();
            }
            else
            {
                if (listTextBox[1].Text.Equals(""))
                {
                    listLabel[1].Text = "El campo es requerido";
                    listLabel[1].ForeColor = Color.Red;
                    listTextBox[1].Focus();
                }
                else
                {
                    if (listTextBox[2].Text.Equals(""))
                    {
                        listLabel[2].Text = "El campo es requerido";
                        listLabel[2].ForeColor = Color.Red;
                        listTextBox[2].Focus();
                    }
                    else
                    {
                        if (listTextBox[3].Text.Equals(""))
                        {
                            listLabel[3].Text = "El campo es requerido";
                            listLabel[3].ForeColor = Color.Red;
                            listTextBox[3].Focus();
                        }
                        else
                        {
                            if (textBoxEvent.comprobarEmail(listTextBox[3].Text))
                            {
                                var user = _Alumno.Where(u => u.correo.Equals(listTextBox[3].Text)).ToList();
                                if (user.Count.Equals(0))
                                {
                                    save();
                                   
                                }
                                else
                                {
                                    if (user[0].matricula.Equals(listTextBox[0].Text))
                                    {

                                    }
                                    else
                                    {
                                        listLabel[3].Text = "Email ya registrado";
                                        listLabel[3].ForeColor = Color.Red;
                                        listTextBox[3].Focus();
                                    }

                                }
                            }
                            else
                            {
                                listLabel[3].Text = "Email no válido";
                                listLabel[3].ForeColor = Color.Red;
                                listTextBox[3].Focus();
                            }
                        }
                    }
                }

            }

        }
        private void save()
        {
            try
            {
                BeginTransactionAsync();
                var imageArray = subirImagen.ImageToByte(image.Image);
                switch (_accion)
                {
                    case "Insert":
                        _Alumno.Value(e => e.matricula, listTextBox[0].Text)
                    .Value(e => e.nombre, listTextBox[1].Text)
                    .Value(e => e.apellido, listTextBox[2].Text)
                    .Value(e => e.correo, listTextBox[3].Text)
                    .Value(e => e.imagen, imageArray)
                    .Insert(); break;
                    case "Update":
                        _Alumno.Where(e => e.matricula.Equals(_IdMatricula))
                    .Set(e => e.matricula, listTextBox[0].Text)
                    .Set(e => e.nombre, listTextBox[1].Text)
                    .Set(e => e.apellido, listTextBox[2].Text)
                    .Set(e => e.correo, listTextBox[3].Text)
                    .Set(e => e.imagen, imageArray)
                    .Update(); break;
                }

                CommitTransaction();
            }
            catch (Exception)
            {

                RollbackTransaction();
            }
        }
        public void searchStudent(String campo)
        {
            List<Alumno> query = new List<Alumno>();
            int inicio = (numPagina - 1) * regPorPagina;
            if (campo.Equals(""))
            {
                query = _Alumno.ToList();
            }
            else
            {
                query = _Alumno.Where(c => c.matricula.StartsWith(campo) || c.nombre.StartsWith(campo) || c.apellido.StartsWith(campo)).ToList();
            }
            if (0 < query.Count)
            {
                view.DataSource = query.Select(c => new
                {
                    c.matricula,
                    c.nombre,
                    c.apellido,
                    c.correo,
                    c.imagen
                }).Skip(inicio).Take(regPorPagina).ToList();
                view.Columns[4].Visible = false;
                view.Columns[1].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                view.Columns[3].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            }
            else
            {
                view.DataSource = query.Select(c => new
                {
                    c.matricula,
                    c.nombre,
                    c.apellido,
                    c.correo,
                }).ToList();
            }
        }
        private int _IdMatricula = 0;
        public void getEstudiante()
        {
            _accion = "Update";
            _IdMatricula = Convert.ToInt16(view.CurrentRow.Cells[0].Value);
            listTextBox[0].Text = Convert.ToString(view.CurrentRow.Cells[0].Value);
            listTextBox[1].Text = Convert.ToString(view.CurrentRow.Cells[1].Value);
            listTextBox[2].Text = Convert.ToString(view.CurrentRow.Cells[2].Value);
            listTextBox[3].Text = Convert.ToString(view.CurrentRow.Cells[3].Value);

            try
            {
                byte[] arrayImage = (byte[])view.CurrentRow.Cells[4].Value;
                image.Image = subirImagen.byteArrayToImage(arrayImage);
            }
            catch (Exception ex) { }
            {
                image.Image = bmp;
            }
        }
        private List<Alumno> listAlumno;
        public void paginador(string metodo)
        {
            switch (metodo)
            {
                case "Primero": numPagina = _paginador.primero(); break;
                case "Anterior": numPagina = _paginador.anterior(); break;
                case "Siguiente": numPagina = _paginador.siguiente(); break;
                case "Ultimo": numPagina = _paginador.ultimo(); break;
            }
            searchStudent("");
        }

        public void registroPaginas()
        {
            numPagina = 1;
            regPorPagina = (int)_numericUpDown.Value;
            var list = _Alumno.ToList();
            if (0 < list.Count)
            {
                _paginador = new Paginador<Alumno>(listAlumno, listLabel[4], regPorPagina);
                searchStudent("");
            }
        }

        public void eliminar()
        {
            if (_IdMatricula.Equals(0))
            {
                MessageBox.Show("Seleccione un estudiante");
            }
            else
            {
                if (MessageBox.Show("Estas seguro de eliminar el estudiante", "Eliminar estudiante", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    _Alumno.Where(c => c.matricula.Equals(_IdMatricula)).Delete();
                    restablecer();
                }
            }
        }
        public void restablecer()
        {
            _accion = "Insert";
            numPagina = 1;
            _IdMatricula = 0;
            image.Image = bmp;
            listLabel[0].Text = "Matricula";
            listLabel[1].Text = "Nombre";
            listLabel[2].Text = "Apellido";
            listLabel[3].Text = "Correo";
            listLabel[0].ForeColor = Color.LightSlateGray;
            listLabel[1].ForeColor = Color.LightSlateGray;
            listLabel[2].ForeColor = Color.LightSlateGray;
            listLabel[3].ForeColor = Color.LightSlateGray;
            listTextBox[0].Text = "";
            listTextBox[1].Text = "";
            listTextBox[2].Text = "";
            listTextBox[3].Text = "";
            listAlumno = _Alumno.ToList();
            if (0 < listAlumno.Count)
            {
                _paginador = new Paginador<Alumno>(listAlumno, listLabel[4], regPorPagina);
            }
            searchStudent("");
        }
    }
}
