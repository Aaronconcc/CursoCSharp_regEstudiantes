using Logica;
using Logica.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practice
{
    public partial class Form1 : Form
    {
        //private Libreria libreria; 
        Estudiantes estudiante;
        public Form1()
        {
            InitializeComponent();
            //libreria = new Libreria();
            var listTextBox = new List<TextBox>();
            listTextBox.Add(textBoxMatricula);
            listTextBox.Add(textBoxName);
            listTextBox.Add(textBoxLastName);
            listTextBox.Add(textBoxEmail);
           

            var listLabel = new List<Label>();
            listLabel.Add(labelMatricula);
            listLabel.Add(labelName);
            listLabel.Add(labelLastName);
            listLabel.Add(labelEmail);
            listLabel.Add(labelPagina);
            Object[] objects = { pictureBox1,
            Properties.Resources._107,
            dataGridView1,
            numericUpDown1};
            estudiante = new Estudiantes(listTextBox,listLabel,objects);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            estudiante.subirImagen.cargarImagen(pictureBox1);
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            if (textBoxName.Equals(""))
            {
                labelName.ForeColor = Color.LightSlateGray;

            }
            else
            {
                labelName.ForeColor = Color.Green;
                labelName.Text = "Name";
            }

        }

        private void textBoxName_KeyPress(object sender, KeyPressEventArgs e)
        {
            estudiante.textBoxEvent.TextKeyPress(e);
        }

        private void textBoxLastName_TextChanged(object sender, EventArgs e)
        {
            if (textBoxLastName.Equals(""))
            {
                labelLastName.ForeColor = Color.LightSlateGray;

            }
            else
            {
                labelLastName.ForeColor = Color.Green;
                labelLastName.Text = "LastName";
            }

        }

        private void textBoxLastName_KeyPress(object sender, KeyPressEventArgs e)
        {
            estudiante.textBoxEvent.TextKeyPress(e);
        }

        private void textBoxMatricula_TextChanged(object sender, EventArgs e)
        {
            if (textBoxMatricula.Equals(""))
            {
                labelMatricula.ForeColor = Color.LightSlateGray;

            }
            else
            {
                labelMatricula.ForeColor = Color.Green;
                labelMatricula.Text = "Matricula";
            }

        }

        private void textBoxMatricula_KeyPress(object sender, KeyPressEventArgs e)
        {
            estudiante.textBoxEvent.NumKeyPress(e);
        }

        private void textBoxEmail_TextChanged(object sender, EventArgs e)
        {
            if (textBoxEmail.Equals(""))
            {
                labelEmail.ForeColor = Color.LightSlateGray;

            }
            else
            {
                labelEmail.ForeColor = Color.Green;
                labelEmail.Text = "Email";
            }
        }

        private void textBoxEmail_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            estudiante.Registrar();
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            estudiante.searchStudent(textBoxSearch.Text);
        }

        private void buttonPrimero_Click(object sender, EventArgs e)
        {
            estudiante.paginador("Primero");
        }

        private void buttonUltimo_Click(object sender, EventArgs e)
        {
            estudiante.paginador("Ultimo");
        }

        private void buttonAnterior_Click(object sender, EventArgs e)
        {
            estudiante.paginador("Anterior");
        }

        private void buttonSiguiente_Click(object sender, EventArgs e)
        {
            estudiante.paginador("Siguiente");
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            estudiante.registroPaginas();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                estudiante.getEstudiante();
            }
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                estudiante.getEstudiante();
            }
        }

       

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            estudiante.restablecer();
        }

        private void buttonBorrar_Click(object sender, EventArgs e)
        {
            estudiante.eliminar();
        }
    }
}
