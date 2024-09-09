﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;
using Tp_WinForm_Equipo_7A;
using static System.Net.WebRequestMethods;

namespace Tp_WinForm_Equipo_7A
{
    public partial class TodosArticulos : Form
    {

        private List<Articulo> listaArticulos;

        public TodosArticulos()
        {
            InitializeComponent();
        }
        private void TodosArticulos_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void dgv_ArticulosTodos_SelectionChanged(object sender, EventArgs e)
        {
            Articulo seleccionado=(Articulo)dgv_ArticulosTodos.CurrentRow.DataBoundItem;
            cargarImagen(seleccionado.ImagenUrl);

        }

        private void cargar()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {

                listaArticulos = negocio.listarTodos();
                dgv_ArticulosTodos.DataSource = listaArticulos;
                dgv_ArticulosTodos.Columns["ImagenUrl"].Visible = false;
                dgv_ArticulosTodos.Columns["IdArticulo"].Visible = false;
                cargarImagen(listaArticulos[0].ImagenUrl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

private void cargarImagen(string imagen)
        {
            try
            {
                pxbArticulo.Load(imagen);

            }
            catch (Exception ex)
            {
                
                   pxbArticulo.Load("https://developers.elementor.com/docs/assets/img/elementor-placeholder-image.png");

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            AltaArticulo alta = new AltaArticulo();
            alta.ShowDialog();
            cargar();

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            seleccionado = (Articulo)dgv_ArticulosTodos.CurrentRow.DataBoundItem;
            AltaArticulo modificar = new AltaArticulo(seleccionado);
            modificar.ShowDialog();
            cargar();


        }
    }
}
