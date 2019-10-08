using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.OleDb;
using System.Data; 

namespace Pedidos_de_Amazon_Ulala
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OleDbConnection con;
        DataTable dt;
        public MainWindow()
        {
            InitializeComponent();
            con = new OleDbConnection();
            con.ConnectionString = "Provider=Microsoft.Jet.Oledb.4.0; Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "\\Pedidos-Amazon.mdb";
            MostrarDatos();
        }
        private void MostrarDatos()
        {
            OleDbCommand cmd = new OleDbCommand();
            if (con.State != ConnectionState.Open)
                con.Open();
            cmd.Connection = con;
            cmd.CommandText = "select * from Pedidos";
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            gvDatos.ItemsSource = dt.AsDataView();

            if (dt.Rows.Count > 0)
            {
                lbContenido.Visibility = System.Windows.Visibility.Hidden;
                gvDatos.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                lbContenido.Visibility = System.Windows.Visibility.Visible;
                gvDatos.Visibility = System.Windows.Visibility.Hidden;
            }

        }
        private void LimpiaTodo()
        {
            txtPedido.Text = "";
            txtProducto.Text = "";
            txtPrecio.Text = "";
            cbForma.SelectedIndex = 0;
            cbColor.SelectedIndex = 0;
            cbTalla.SelectedIndex = 0;
            txtDestinado.Text = "";
            txtTelefono.Text = "";
            txtDireccion.Text = "";
            btnNuevo.Content = "Nuevo";
            txtPedido.IsEnabled = true;
        }
        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (gvDatos.SelectedItems.Count > 0)
            {
                DataRowView row = (DataRowView)gvDatos.SelectedItems[0];
                txtPedido.Text = row["Id"].ToString();
                txtProducto.Text = row["Nombre"].ToString();
                txtProducto.Text = row["Nombre"].ToString();
                cbForma.Text = row["Genero"].ToString();
                cbColor.Text = row["Genero"].ToString();
                cbTalla.Text = row["Genero"].ToString();
                txtDestinado.Text = row["Telefono"].ToString();
                txtTelefono.Text = row["Telefono"].ToString();
                txtDireccion.Text = row["Direccion"].ToString();
                txtPedido.IsEnabled = false;
                btnNuevo.Content = "Actualizar";
            }
            else
            {
                MessageBox.Show("Favor de seleccionar la id del producto que desea editar");
            }
        }
        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (gvDatos.SelectedItems.Count > 0)
            {
                DataRowView row = (DataRowView)gvDatos.SelectedItems[0];

                OleDbCommand cmd = new OleDbCommand();
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd.Connection = con;
                cmd.CommandText = "delete from Pedidos where Id=" + row["Id"].ToString();
                cmd.ExecuteNonQuery();
                MostrarDatos();
                MessageBox.Show("Pedido Eliminado (cancelado)");
                LimpiaTodo();
            }
            else
            {
                MessageBox.Show("Favor de elegir el pedido que desea eliminar");
            }
        }
        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand();
            if (con.State != ConnectionState.Open)
                con.Open();
            cmd.Connection = con;

            if (txtPedido.Text != "")
            {
                if (txtPedido.IsEnabled == true)
                {
                    if (cbForma.Text != "Forma de Pago")
                    {
                        if (cbColor.Text != "Selecciona Color")
                        {
                            if (cbTalla.Text != "Selecciona Talla")
                            {
                                cmd.CommandText = "insert into Pedidos(Producto,Precio,Forma,Color,Talla,Comprador,Telefono,Direccion) " +
                                    "Values(" + txtProducto.Text + ",'" + txtPrecio.Text + "','" + cbForma.Text + "','" + cbColor.Text + ",'" + cbTalla + "','"
                                    + txtDestinado + "','" + txtTelefono.Text + ",'" + txtDireccion.Text + "')";
                                cmd.ExecuteNonQuery();
                                MostrarDatos();
                                MessageBox.Show("Nuevo pedido agregado correctamente...");
                                LimpiaTodo();

                            }
                            else
                            {
                                MessageBox.Show("Selecciona la Talla");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Selecciona el Color");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Selecciona forma de Pago");
                    }   
                }
                else
                {
                    cmd.CommandText = "update Pedidos set Nombre='" + txtProducto.Text + "',Precio='" + txtPrecio.Text +
                         ",Forma='" + cbForma.Text + ",Color='" + cbColor.Text + ",Talla='" + cbTalla.Text + ",Destinado='" + txtDestinado.Text + 
                        "',Telefono=" + txtTelefono.Text + ",Direccion='" + txtDireccion.Text + "' where Id=" + txtPedido.Text;
                    cmd.ExecuteNonQuery();
                    MostrarDatos();
                    MessageBox.Show("Datos del alumno Actualizados...");
                    LimpiaTodo();
                }
            }
            else
            {
                MessageBox.Show("Favor de poner la Id del pedido");
            }
        }
        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiaTodo();
        }

    }

}
