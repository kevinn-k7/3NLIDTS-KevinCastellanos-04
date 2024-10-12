using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3NLIDTS_KevinCastellanos_04
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            txtEdad.TextChanged += validarEdad;
            txtTelefono.Leave += validarTelefono;
            txtNombre.TextChanged += validarNombre;
            txtApellido.TextChanged += validarApellidos;
            txtEstatura.TextChanged += validarEstatura;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            string nombres = txtNombre.Text;
            string apellidos = txtApellido.Text;
            string edad = txtEdad.Text;
            string estatura = txtEstatura.Text;
            string telefono = txtTelefono.Text;

            string genero = "";
            if (RbHombre.Checked)
            {
                genero = "Hombre";
            }
            else if (RbMujer.Checked)
            {
                genero = "Mujer";
            }

            
            if (EsEnteroValido(edad) && EsDecimalValido(estatura) && EsEnteroValido100Digitos(telefono) && EsTextoValido(nombres) && EsTextoValido(apellidos))
            {
                // Si las validaciones son correctas, se generan los datos y se guardan
                string datos = $"Nombres: {nombres}\r\nApellidos: {apellidos}\r\nEdad: {edad} años\r\nEstatura: {estatura} cm\r\nTelefono: {telefono}\r\nGenero: {genero}";

                // Especifica la ruta completa 
                string rutaArchivo = "C:\\Users\\cack0\\OneDrive\\Documentos\\Unach\\Semestre 3\\Programacion Avanzada\\FormularioDatos.txt";

                // Abre el archivo para escritura, en modo adjunto (append)
                using (StreamWriter writer = new StreamWriter(rutaArchivo, true))
                {
                    writer.WriteLine(datos);
                    writer.WriteLine(); 
                }

                MessageBox.Show("Datos guardados exitosamente.");
            }
            else
            {
                
                MessageBox.Show("Verifique los datos ingresados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool EsEnteroValido(string valor)
        {
            int resultado;
            return int.TryParse(valor, out resultado);
        }

        private bool EsDecimalValido(string valor)
        {
            decimal resultado;
            return decimal.TryParse(valor, out resultado);
        }

        private bool EsEnteroValido100Digitos(string valor)
        {
            return valor.Length == 10 && valor.All(char.IsDigit) && long.TryParse(valor, out _);
        }

        private bool EsTextoValido(string valor)
        {
            return Regex.IsMatch(valor, @"^[a-zA-Z\s]+$");
        }

        private void validarEdad(object sender, EventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (!EsEnteroValido(textbox.Text))
            {
                MessageBox.Show("Ingrese una edad valida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textbox.Clear();
            }
        }

        private void validarEstatura(object sender, EventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (!EsDecimalValido(textbox.Text))
            {
                MessageBox.Show("Ingrese una estatura valida", "Error estatura", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textbox.Clear();
            }
        }

        private void validarTelefono(object sender, EventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (textbox.Text.Length == 10 && EsEnteroValido100Digitos(textbox.Text))
            {
                textbox.BackColor = Color.Green;
            }
            else
            {
                textbox.BackColor = Color.Red;
                MessageBox.Show("Ingrese un teléfono valido", "Error teléfono", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textbox.Clear();
            }
        }

        private void validarApellidos(object sender, EventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (!EsTextoValido(textbox.Text))
            {
                MessageBox.Show("Ingrese un apellido valido", "Error apellido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textbox.Clear();
            }
        }

        private void validarNombre(object sender, EventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (!EsTextoValido(textbox.Text))
            {
                MessageBox.Show("Ingrese un nombre valido", "Error nombre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textbox.Clear();
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtEdad.Clear();
            txtTelefono.Clear();
            txtEstatura.Clear();
            RbHombre.Checked = false;
            RbMujer.Checked = false;
        }
    }
}


