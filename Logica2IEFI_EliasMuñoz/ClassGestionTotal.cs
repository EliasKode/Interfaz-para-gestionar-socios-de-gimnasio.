using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;//a aprtir de aqui esto debe agregarse
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;//para los archivos de texto
using System.Drawing;
using System.Drawing.Printing;//estos ultimos dos son para la impresion
using System.Net;
using System.Threading;


namespace Logica2IEFI_EliasMuñoz
{
    public class ClassGestionTotal
    {
        private string CadenaConexion = "Provider = Microsoft.Jet.OLEDB.4.0; Data Source = BD_Clientes (1).mdb";
        private string TablaSocios = "Socio";
        private string TablaBarrio = "Barrio";
        private string TablaActividad = "Actividad";

        private OleDbConnection ConectarABase = new OleDbConnection();
        
        



        public void CargarSocio(int dni, string nombre, string apellido, string direccion, int barrio, int actividad, Decimal saldo)
        {
            try
            {

                ConectarABase.ConnectionString = CadenaConexion;
                ConectarABase.Open();

                //DE SOCIOS
                OleDbCommand ComandoSocio = new OleDbCommand();
                ComandoSocio.Connection = ConectarABase;
                ComandoSocio.CommandType = CommandType.TableDirect;
                ComandoSocio.CommandText = TablaSocios;

                OleDbDataAdapter AdaptadorSocio = new OleDbDataAdapter();//una por tabla

                AdaptadorSocio = new OleDbDataAdapter(ComandoSocio);

                DataSet DS = new DataSet();//una vez para todas las tablas

                AdaptadorSocio.Fill(DS, TablaSocios);

                DataColumn[] DataKeySocios = new DataColumn[1];
                DataKeySocios[0] = DS.Tables[TablaSocios].Columns["IdSocio"];
                DS.Tables[TablaSocios].PrimaryKey = DataKeySocios;

                OleDbCommandBuilder ActualizaSocio = new OleDbCommandBuilder(AdaptadorSocio);


                //DE BARRIOS
                OleDbCommand ComandoBarrio = new OleDbCommand();
                ComandoBarrio.Connection = ConectarABase;
                ComandoBarrio.CommandType = CommandType.TableDirect;
                ComandoBarrio.CommandText = TablaBarrio;

                OleDbDataAdapter AdaptadorBarrio = new OleDbDataAdapter();
                AdaptadorBarrio = new OleDbDataAdapter(ComandoBarrio);

                AdaptadorBarrio.Fill(DS, TablaBarrio);

                DataColumn[] DataKeyBarrio = new DataColumn[1];
                DataKeyBarrio[0] = DS.Tables[TablaBarrio].Columns["idBarrio"];
                DS.Tables[TablaBarrio].PrimaryKey = DataKeyBarrio;


                //DE ACTIVIDAD
                OleDbCommand ComandoActividad = new OleDbCommand();
                ComandoActividad.Connection = ConectarABase;
                ComandoActividad.CommandType = CommandType.TableDirect;
                ComandoActividad.CommandText = TablaActividad;

                OleDbDataAdapter AdaptadorActividad = new OleDbDataAdapter();
                AdaptadorActividad = new OleDbDataAdapter(ComandoActividad);

                AdaptadorActividad.Fill(DS, TablaActividad);

                DataColumn[] DataKeyActividad = new DataColumn[1];
                DataKeyActividad[0] = DS.Tables[TablaActividad].Columns["idActividad"];
                DS.Tables[TablaActividad].PrimaryKey = DataKeyActividad;

                Int32 Checkdni = dni;
                DataRow DNI = DS.Tables[TablaSocios].Rows.Find(Checkdni);
                if(DNI != null)
                {
                    MessageBox.Show("El DNI ingresado pertenece a un socio en la base de datos, no puede agregarlo más de una vez", "Error",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DataRow NuevoSocio = DS.Tables[TablaSocios].NewRow();

                    NuevoSocio["IdSocio"]=dni;
                    NuevoSocio["Nombre"]=nombre + " " + apellido;
                    NuevoSocio["Direccion"]=direccion;
                    NuevoSocio["idBarrio"]=barrio;
                    NuevoSocio["idActividad"]=actividad;
                    NuevoSocio["Deuda"]= saldo;

                    DS.Tables[TablaSocios].Rows.Add(NuevoSocio);

                    AdaptadorSocio.Update(DS,TablaSocios);

                    MessageBox.Show("El socio ha sido dado de alta. Grabado en base de datos exitoso!!", "Admitido", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }


                ConectarABase.Close();






            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());
            }
        }

        public void LlenarComboBarrio(ComboBox barrio)
        {
            ConectarABase.ConnectionString = CadenaConexion;
            ConectarABase.Open();

            //DE BARRIOS
            OleDbCommand ComandoBarrio = new OleDbCommand();
            ComandoBarrio.Connection = ConectarABase;
            ComandoBarrio.CommandType = CommandType.TableDirect;
            ComandoBarrio.CommandText = TablaBarrio;

            OleDbDataAdapter AdaptadorBarrio = new OleDbDataAdapter();
            AdaptadorBarrio = new OleDbDataAdapter(ComandoBarrio);
            DataSet DS = new DataSet();
            AdaptadorBarrio.Fill(DS, TablaBarrio);

            DataColumn[] DataKeyBarrio = new DataColumn[1];
            DataKeyBarrio[0] = DS.Tables[TablaBarrio].Columns["idBarrio"];
            DS.Tables[TablaBarrio].PrimaryKey = DataKeyBarrio;

            barrio.DisplayMember = "Nombre";
            barrio.ValueMember = "idBarrio";
            barrio.DataSource = DS.Tables[TablaBarrio];
            ConectarABase.Close();
        }



        public void LlenarComboActividad(ComboBox actividad)
        {
            ConectarABase.ConnectionString = CadenaConexion;
            ConectarABase.Open();

            OleDbCommand ComandoActividad = new OleDbCommand();
            ComandoActividad.Connection = ConectarABase;
            ComandoActividad.CommandType = CommandType.TableDirect;
            ComandoActividad.CommandText = TablaActividad;

            OleDbDataAdapter AdaptadorActividad = new OleDbDataAdapter();
            AdaptadorActividad = new OleDbDataAdapter(ComandoActividad);
            DataSet DS = new DataSet();
            AdaptadorActividad.Fill(DS, TablaActividad);

            DataColumn[] DataKeyActividad = new DataColumn[1];
            DataKeyActividad[0] = DS.Tables[TablaActividad].Columns["idActividad"];
            DS.Tables[TablaActividad].PrimaryKey = DataKeyActividad;

            actividad.DisplayMember = "Nombre";
            actividad.ValueMember = "idActividad";
            actividad.DataSource = DS.Tables[TablaActividad];
            ConectarABase.Close();
        }

        public void LlenarComboSocios(ComboBox socios)
        {
            ConectarABase.ConnectionString = CadenaConexion;
            ConectarABase.Open();

            //DE SOCIOS
            OleDbCommand ComandoSocio = new OleDbCommand();
            ComandoSocio.Connection = ConectarABase;
            ComandoSocio.CommandType = CommandType.TableDirect;
            ComandoSocio.CommandText = TablaSocios;

            OleDbDataAdapter AdaptadorSocio = new OleDbDataAdapter();//una por tabla

            AdaptadorSocio = new OleDbDataAdapter(ComandoSocio);

            DataSet DS = new DataSet();//una vez para todas las tablas

            AdaptadorSocio.Fill(DS, TablaSocios);

            DataColumn[] DataKeySocios = new DataColumn[1];
            DataKeySocios[0] = DS.Tables[TablaSocios].Columns["IdSocio"];
            DS.Tables[TablaSocios].PrimaryKey = DataKeySocios;


            socios.DisplayMember = "Nombre";
            socios.ValueMember = "IdSocio";
            socios.DataSource = DS.Tables[TablaSocios];
            ConectarABase.Close();
        }



        public void MostrarParaEditar(int dniBusqueda, TextBox nombre, TextBox apellido, TextBox direccion, ComboBox barrio, ComboBox actividad, TextBox saldo)
        {
            try
            {
                ConectarABase.ConnectionString = CadenaConexion;
                ConectarABase.Open();

                //DE SOCIOS
                OleDbCommand ComandoSocio = new OleDbCommand();
                ComandoSocio.Connection = ConectarABase;
                ComandoSocio.CommandType = CommandType.TableDirect;
                ComandoSocio.CommandText = TablaSocios;

                OleDbDataAdapter AdaptadorSocio = new OleDbDataAdapter();//una por tabla

                AdaptadorSocio = new OleDbDataAdapter(ComandoSocio);

                DataSet DS = new DataSet();//una vez para todas las tablas

                AdaptadorSocio.Fill(DS, TablaSocios);

                DataColumn[] DataKeySocios = new DataColumn[1];
                DataKeySocios[0] = DS.Tables[TablaSocios].Columns["IdSocio"];
                DS.Tables[TablaSocios].PrimaryKey = DataKeySocios;

                OleDbCommandBuilder ActualizaSocio = new OleDbCommandBuilder(AdaptadorSocio);


                //DE BARRIOS
                OleDbCommand ComandoBarrio = new OleDbCommand();
                ComandoBarrio.Connection = ConectarABase;
                ComandoBarrio.CommandType = CommandType.TableDirect;
                ComandoBarrio.CommandText = TablaBarrio;

                OleDbDataAdapter AdaptadorBarrio = new OleDbDataAdapter();
                AdaptadorBarrio = new OleDbDataAdapter(ComandoBarrio);

                AdaptadorBarrio.Fill(DS, TablaBarrio);

                DataColumn[] DataKeyBarrio = new DataColumn[1];
                DataKeyBarrio[0] = DS.Tables[TablaBarrio].Columns["idBarrio"];
                DS.Tables[TablaBarrio].PrimaryKey = DataKeyBarrio;


                //DE ACTIVIDAD
                OleDbCommand ComandoActividad = new OleDbCommand();
                ComandoActividad.Connection = ConectarABase;
                ComandoActividad.CommandType = CommandType.TableDirect;
                ComandoActividad.CommandText = TablaActividad;

                OleDbDataAdapter AdaptadorActividad = new OleDbDataAdapter();
                AdaptadorActividad = new OleDbDataAdapter(ComandoActividad);

                AdaptadorActividad.Fill(DS, TablaActividad);

                DataColumn[] DataKeyActividad = new DataColumn[1];
                DataKeyActividad[0] = DS.Tables[TablaActividad].Columns["idActividad"];
                DS.Tables[TablaActividad].PrimaryKey = DataKeyActividad;
                
                string NombreGuardado = "";
                string[] NombreApellido = new string[4];
                Int32 Checkdni = dniBusqueda;
                DataRow DNI = DS.Tables[TablaSocios].Rows.Find(Checkdni);
                if (DNI != null)
                {
                    NombreGuardado = DNI["Nombre"].ToString();
                    NombreApellido = NombreGuardado.Split(' ');

                    nombre.Text = NombreApellido[0];
                    apellido.Text = NombreApellido[NombreApellido.Length-1];// ESTO EVITARA UN ERROR EN EL CASO DE QUE SE HAYAN CARGADO CON DOS NOMBRES
                    direccion.Text = DNI["Direccion"].ToString();
                    barrio.SelectedValue = int.Parse(DNI["idBarrio"].ToString());
                    actividad.SelectedValue = int.Parse(DNI["idActividad"].ToString());

                    //Otra forma de busqueda para experimentar

                    //DataRow NumBarrio = DS.Tables[TablaBarrio].Rows.Find(DNI["idBarrio"]);
                    //if (NumBarrio != null)
                    //{
                    //    barrio.Text = NumBarrio["Nombre"].ToString();

                    //}
                    //DataRow NumActividad = DS.Tables[TablaActividad].Rows.Find(DNI["idActividad"]);
                    //if(NumActividad != null)
                    //{
                    //    actividad.Text = NumActividad["Nombre"].ToString();
                    //}

                    saldo.Text = DNI["Deuda"].ToString();
                  
                }
                else
                {
                    MessageBox.Show("El socio que solicita NO existe", "No Encontrado!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                }
                

                ConectarABase.Close();

            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());
            }

        }


        public void Modificar(int dniModificar, TextBox nombre, TextBox apellido, TextBox direccion, ComboBox barrio, ComboBox actividad, TextBox saldo)
        {
            try
            {


                ConectarABase.ConnectionString = CadenaConexion;
                ConectarABase.Open();

                //DE SOCIOS
                OleDbCommand ComandoSocio = new OleDbCommand();
                ComandoSocio.Connection = ConectarABase;
                ComandoSocio.CommandType = CommandType.TableDirect;
                ComandoSocio.CommandText = TablaSocios;

                OleDbDataAdapter AdaptadorSocio = new OleDbDataAdapter();//una por tabla

                AdaptadorSocio = new OleDbDataAdapter(ComandoSocio);

                DataSet DS = new DataSet();//una vez para todas las tablas

                AdaptadorSocio.Fill(DS, TablaSocios);

                DataColumn[] DataKeySocios = new DataColumn[1];
                DataKeySocios[0] = DS.Tables[TablaSocios].Columns["IdSocio"];
                DS.Tables[TablaSocios].PrimaryKey = DataKeySocios;

                OleDbCommandBuilder ActualizaSocio = new OleDbCommandBuilder(AdaptadorSocio);


                //DE BARRIOS
                OleDbCommand ComandoBarrio = new OleDbCommand();
                ComandoBarrio.Connection = ConectarABase;
                ComandoBarrio.CommandType = CommandType.TableDirect;
                ComandoBarrio.CommandText = TablaBarrio;

                OleDbDataAdapter AdaptadorBarrio = new OleDbDataAdapter();
                AdaptadorBarrio = new OleDbDataAdapter(ComandoBarrio);

                AdaptadorBarrio.Fill(DS, TablaBarrio);

                DataColumn[] DataKeyBarrio = new DataColumn[1];
                DataKeyBarrio[0] = DS.Tables[TablaBarrio].Columns["idBarrio"];
                DS.Tables[TablaBarrio].PrimaryKey = DataKeyBarrio;


                //DE ACTIVIDAD
                OleDbCommand ComandoActividad = new OleDbCommand();
                ComandoActividad.Connection = ConectarABase;
                ComandoActividad.CommandType = CommandType.TableDirect;
                ComandoActividad.CommandText = TablaActividad;

                OleDbDataAdapter AdaptadorActividad = new OleDbDataAdapter();
                AdaptadorActividad = new OleDbDataAdapter(ComandoActividad);

                AdaptadorActividad.Fill(DS, TablaActividad);

                DataColumn[] DataKeyActividad = new DataColumn[1];
                DataKeyActividad[0] = DS.Tables[TablaActividad].Columns["idActividad"];
                DS.Tables[TablaActividad].PrimaryKey = DataKeyActividad;


                //DataTable CopiaTabla = new DataTable();
                //CopiaTabla = DS.Tables[TablaSocios];
                Int32 Checkdni = dniModificar;
                DataRow DNI = DS.Tables[TablaSocios].Rows.Find(Checkdni);
                if (DNI != null)
                {
                    DNI.BeginEdit();

                    DNI["Nombre"] = nombre.Text + " " + apellido.Text;
                    DNI["Direccion"] = direccion.Text;
                    DNI["idBarrio"] = int.Parse(barrio.SelectedValue.ToString());
                    DNI["idActividad"] = int.Parse(actividad.SelectedValue.ToString()); ;
                    DNI["Deuda"] = Convert.ToDecimal(saldo.Text);

                    DNI.EndEdit();

                    AdaptadorSocio.Update(DS, TablaSocios);
                    MessageBox.Show("El socio ha sido modificado. Grabado en base de datos exitoso!!", "Admitido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Cerciorese de que los datos sean correctos, error de admisión de datos. El socio debe existir en la base de datos para ser modificado", "Atención!!!",
                             MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                ConectarABase.Close();




            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());
            }

        }


        public void BajaSocio(int dniBaja)
        {
            try
            {

                ConectarABase.ConnectionString = CadenaConexion;
                ConectarABase.Open();

                //DE SOCIOS
                OleDbCommand ComandoSocio = new OleDbCommand();
                ComandoSocio.Connection = ConectarABase;
                ComandoSocio.CommandType = CommandType.TableDirect;
                ComandoSocio.CommandText = TablaSocios;

                OleDbDataAdapter AdaptadorSocio = new OleDbDataAdapter();//una por tabla

                AdaptadorSocio = new OleDbDataAdapter(ComandoSocio);

                DataSet DS = new DataSet();//una vez para todas las tablas

                AdaptadorSocio.Fill(DS, TablaSocios);

                DataColumn[] DataKeySocios = new DataColumn[1];
                DataKeySocios[0] = DS.Tables[TablaSocios].Columns["IdSocio"];
                DS.Tables[TablaSocios].PrimaryKey = DataKeySocios;

                OleDbCommandBuilder ActualizaSocio = new OleDbCommandBuilder(AdaptadorSocio);


                //DE BARRIOS
                OleDbCommand ComandoBarrio = new OleDbCommand();
                ComandoBarrio.Connection = ConectarABase;
                ComandoBarrio.CommandType = CommandType.TableDirect;
                ComandoBarrio.CommandText = TablaBarrio;

                OleDbDataAdapter AdaptadorBarrio = new OleDbDataAdapter();
                AdaptadorBarrio = new OleDbDataAdapter(ComandoBarrio);

                AdaptadorBarrio.Fill(DS, TablaBarrio);

                DataColumn[] DataKeyBarrio = new DataColumn[1];
                DataKeyBarrio[0] = DS.Tables[TablaBarrio].Columns["idBarrio"];
                DS.Tables[TablaBarrio].PrimaryKey = DataKeyBarrio;


                //DE ACTIVIDAD
                OleDbCommand ComandoActividad = new OleDbCommand();
                ComandoActividad.Connection = ConectarABase;
                ComandoActividad.CommandType = CommandType.TableDirect;
                ComandoActividad.CommandText = TablaActividad;

                OleDbDataAdapter AdaptadorActividad = new OleDbDataAdapter();
                AdaptadorActividad = new OleDbDataAdapter(ComandoActividad);

                AdaptadorActividad.Fill(DS, TablaActividad);

                DataColumn[] DataKeyActividad = new DataColumn[1];
                DataKeyActividad[0] = DS.Tables[TablaActividad].Columns["idActividad"];
                DS.Tables[TablaActividad].PrimaryKey = DataKeyActividad;


                //DataTable CopiaTabla = new DataTable();
                //CopiaTabla = DS.Tables[TablaSocios];
                Int32 Checkdni = dniBaja;
                DataRow DNI = DS.Tables[TablaSocios].Rows.Find(Checkdni);
                if (DNI != null)
                {
                    DNI.Delete();
                  
                    AdaptadorSocio.Update(DS, TablaSocios);
                    MessageBox.Show("El socio ha sido borrado de la base de datos exitosamente", "Admitido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Cerciorese de que los datos sean correctos, error de admisión de datos. El socio debe existir en la base de datos para ser borrado", "Atención!!!",
                             MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                ConectarABase.Close();

            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());
            }
        }


        public void CargarEtiquetas(int nombre, Label dni, Label direccion, Label barrio, Label actividad, Label deuda)
        {

            try
            {
                ConectarABase.ConnectionString = CadenaConexion;
                ConectarABase.Open();

                //DE SOCIOS
                OleDbCommand ComandoSocio = new OleDbCommand();
                ComandoSocio.Connection = ConectarABase;
                ComandoSocio.CommandType = CommandType.TableDirect;
                ComandoSocio.CommandText = TablaSocios;

                OleDbDataAdapter AdaptadorSocio = new OleDbDataAdapter();//una por tabla

                AdaptadorSocio = new OleDbDataAdapter(ComandoSocio);

                DataSet DS = new DataSet();//una vez para todas las tablas

                AdaptadorSocio.Fill(DS, TablaSocios);

                DataColumn[] DataKeySocios = new DataColumn[1];
                DataKeySocios[0] = DS.Tables[TablaSocios].Columns["IdSocio"];
                DS.Tables[TablaSocios].PrimaryKey = DataKeySocios;

                OleDbCommandBuilder ActualizaSocio = new OleDbCommandBuilder(AdaptadorSocio);


                //DE BARRIOS
                OleDbCommand ComandoBarrio = new OleDbCommand();
                ComandoBarrio.Connection = ConectarABase;
                ComandoBarrio.CommandType = CommandType.TableDirect;
                ComandoBarrio.CommandText = TablaBarrio;

                OleDbDataAdapter AdaptadorBarrio = new OleDbDataAdapter();
                AdaptadorBarrio = new OleDbDataAdapter(ComandoBarrio);

                AdaptadorBarrio.Fill(DS, TablaBarrio);

                DataColumn[] DataKeyBarrio = new DataColumn[1];
                DataKeyBarrio[0] = DS.Tables[TablaBarrio].Columns["idBarrio"];
                DS.Tables[TablaBarrio].PrimaryKey = DataKeyBarrio;


                //DE ACTIVIDAD
                OleDbCommand ComandoActividad = new OleDbCommand();
                ComandoActividad.Connection = ConectarABase;
                ComandoActividad.CommandType = CommandType.TableDirect;
                ComandoActividad.CommandText = TablaActividad;

                OleDbDataAdapter AdaptadorActividad = new OleDbDataAdapter();
                AdaptadorActividad = new OleDbDataAdapter(ComandoActividad);

                AdaptadorActividad.Fill(DS, TablaActividad);

                DataColumn[] DataKeyActividad = new DataColumn[1];
                DataKeyActividad[0] = DS.Tables[TablaActividad].Columns["idActividad"];
                DS.Tables[TablaActividad].PrimaryKey = DataKeyActividad;



               
                DataRow ComboNombre = DS.Tables[TablaSocios].Rows.Find(nombre);
                if (ComboNombre != null)
                {
      

                    dni.Text = ComboNombre["IdSocio"].ToString();
                    direccion.Text = ComboNombre["Direccion"].ToString();

                    DataRow NumBarrio = DS.Tables[TablaBarrio].Rows.Find(ComboNombre["idBarrio"]);
                    if (NumBarrio != null)
                    {
                        barrio.Text = NumBarrio["Nombre"].ToString();

                    }
                    DataRow NumActividad = DS.Tables[TablaActividad].Rows.Find(ComboNombre["idActividad"]);
                    if (NumActividad != null)
                    {
                        actividad.Text = NumActividad["Nombre"].ToString();
                    }
                   
                    deuda.Text = ComboNombre["Deuda"].ToString();

                }
                else
                {
                    MessageBox.Show("El socio que solicita NO existe", "No Encontrado!!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }


                ConectarABase.Close();

            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());
            }

        }

        public void MostrarTodosLosSocios(DataGridView ListaTodos, Label menor, Label mayor, Label promedio, Label total)
        {

            try
            {
                ConectarABase.ConnectionString = CadenaConexion;
                ConectarABase.Open();

                //DE SOCIOS
                OleDbCommand ComandoSocio = new OleDbCommand();
                ComandoSocio.Connection = ConectarABase;
                ComandoSocio.CommandType = CommandType.TableDirect;
                ComandoSocio.CommandText = TablaSocios;

                OleDbDataAdapter AdaptadorSocio = new OleDbDataAdapter();//una por tabla

                AdaptadorSocio = new OleDbDataAdapter(ComandoSocio);

                DataSet DS = new DataSet();//una vez para todas las tablas

                AdaptadorSocio.Fill(DS, TablaSocios);

                DataColumn[] DataKeySocios = new DataColumn[1];
                DataKeySocios[0] = DS.Tables[TablaSocios].Columns["IdSocio"];
                DS.Tables[TablaSocios].PrimaryKey = DataKeySocios;

                OleDbCommandBuilder ActualizaSocio = new OleDbCommandBuilder(AdaptadorSocio);


                //DE BARRIOS
                OleDbCommand ComandoBarrio = new OleDbCommand();
                ComandoBarrio.Connection = ConectarABase;
                ComandoBarrio.CommandType = CommandType.TableDirect;
                ComandoBarrio.CommandText = TablaBarrio;

                OleDbDataAdapter AdaptadorBarrio = new OleDbDataAdapter();
                AdaptadorBarrio = new OleDbDataAdapter(ComandoBarrio);

                AdaptadorBarrio.Fill(DS, TablaBarrio);

                DataColumn[] DataKeyBarrio = new DataColumn[1];
                DataKeyBarrio[0] = DS.Tables[TablaBarrio].Columns["idBarrio"];
                DS.Tables[TablaBarrio].PrimaryKey = DataKeyBarrio;


                //DE ACTIVIDAD
                OleDbCommand ComandoActividad = new OleDbCommand();
                ComandoActividad.Connection = ConectarABase;
                ComandoActividad.CommandType = CommandType.TableDirect;
                ComandoActividad.CommandText = TablaActividad;

                OleDbDataAdapter AdaptadorActividad = new OleDbDataAdapter();
                AdaptadorActividad = new OleDbDataAdapter(ComandoActividad);

                AdaptadorActividad.Fill(DS, TablaActividad);

                DataColumn[] DataKeyActividad = new DataColumn[1];
                DataKeyActividad[0] = DS.Tables[TablaActividad].Columns["idActividad"];
                DS.Tables[TablaActividad].PrimaryKey = DataKeyActividad;


                Decimal MayorDeuda = 0;
                Decimal MenorDeuda =0;
                Int32 CantidadSocios= 0;
                Decimal TotalDeuda = 0;
                Decimal Deuda = 0;

               
                if (DS.Tables[TablaSocios].Rows.Count > 0)
                {
                    foreach(DataRow Socio in DS.Tables[TablaSocios].Rows)
                    { 
                        ListaTodos.Rows.Add(Socio["IdSocio"], Socio["Nombre"], Socio["Deuda"]);
                        
                        
                        if (int.Parse(Socio["Deuda"].ToString())<MenorDeuda)
                        {
                            MenorDeuda = int.Parse(Socio["Deuda"].ToString());
                        } 
                        
                        if (int.Parse(Socio["Deuda"].ToString())>MayorDeuda)
                        {
                            MayorDeuda = int.Parse(Socio["Deuda"].ToString());
                        }
                        
                        Deuda = int.Parse(Socio["Deuda"].ToString());

                        TotalDeuda+=Deuda;

                        CantidadSocios++;

                    }
                    menor.Text=MenorDeuda.ToString();
                    mayor.Text = MayorDeuda.ToString();
                    promedio.Text = ( TotalDeuda / CantidadSocios).ToString("#.00");
                    total.Text = TotalDeuda.ToString();

                }
                else
                {
                    MessageBox.Show("El socio que solicita NO existe", "No Encontrado!!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }


                ConectarABase.Close();

            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());
            }

        }

        public void Exportar(DataGridView TodosLosListados, Label menor, Label mayor, Label promedio, Label total, string titulo)
        {
              StreamWriter ArchivoExportado = new StreamWriter("Exportacion.csv", false, Encoding.UTF8);

               ArchivoExportado.WriteLine(titulo);
               ArchivoExportado.WriteLine("");
               ArchivoExportado.WriteLine("DNI;Nombre;Deuda");

            int f = 0;

            for (f = 0; f < TodosLosListados.RowCount; f++)
            {

               ArchivoExportado.Write(TodosLosListados.Rows[f].Cells[0].Value);
               ArchivoExportado.Write(";");
               ArchivoExportado.Write(TodosLosListados.Rows[f].Cells[1].Value);
               ArchivoExportado.Write(";");
               ArchivoExportado.WriteLine(TodosLosListados.Rows[f].Cells[2].Value);
  
            }

            ArchivoExportado.WriteLine("");//como se escribieron ya todos los registros, se agregan tres renglones vacios por una cuestion formato
            ArchivoExportado.WriteLine("");
            ArchivoExportado.WriteLine("");
            ArchivoExportado.Write("Deuda Menor:;;");
            ArchivoExportado.WriteLine(menor.Text);
            ArchivoExportado.Write("Deuda Mayor:;;");
            ArchivoExportado.WriteLine(mayor.Text);
            ArchivoExportado.Write("Promedio de Deudas:;;");
            ArchivoExportado.WriteLine(promedio.Text);
            ArchivoExportado.Write("Total de Deudas:;;");//lo mismo con las deudas y promedios . Observar que no se necesitaron convertir los datos
            ArchivoExportado.WriteLine(total.Text);
  
            MessageBox.Show("La lista socios ha sido Exportada como archivo de texto exitosamente", "Admitido", MessageBoxButtons.OK, MessageBoxIcon.Information);


            ArchivoExportado.Close();
            ArchivoExportado.Dispose();
        }

        public void Imprimir(PrintPageEventArgs reporte, DataGridView TodosLosListados, Label menor, Label mayor, Label promedio, Label total, string titulo)
        {
            Font Titulo = new Font("Arial", 20);
            Font Subtitulo = new Font("Arial", 12);
            Font Texto = new Font("Arial", 10);
            Int32 fila = 180;
            reporte.Graphics.DrawString(titulo, Titulo, Brushes.Red, 150, 100);
            reporte.Graphics.DrawString("DNI", Subtitulo, Brushes.Blue, 100, 150);
            reporte.Graphics.DrawString("Nombre", Subtitulo, Brushes.Blue, 300, 150);
            reporte.Graphics.DrawString("Deuda", Subtitulo, Brushes.Blue, 500, 150);

            int f = 0;
            for (f = 0; f < TodosLosListados.RowCount; f++)
            {

                reporte.Graphics.DrawString(Convert.ToString( TodosLosListados.Rows[f].Cells[0].Value), Texto, Brushes.Black, 100, fila);
                reporte.Graphics.DrawString(Convert.ToString(TodosLosListados.Rows[f].Cells[1].Value), Texto, Brushes.Black, 300, fila);
                reporte.Graphics.DrawString(Convert.ToString(TodosLosListados.Rows[f].Cells[2].Value), Texto, Brushes.Black, 500, fila);
                fila = fila + 20;

            }
            reporte.Graphics.DrawString("Deuda Menor: " + menor.Text, Texto, Brushes.Black, 100, fila); ;
            fila = fila + 20;
            reporte.Graphics.DrawString("Deuda Mayor: " + mayor.Text, Texto, Brushes.Black, 100, fila); ;
            fila = fila + 20;
            reporte.Graphics.DrawString("Promedio de Deudas: " + promedio.Text, Texto, Brushes.Black, 100, fila); ;
            fila = fila + 20;
            reporte.Graphics.DrawString("Total de Deudas: " + total.Text, Texto, Brushes.Black, 100, fila); ;

            MessageBox.Show("Se imprimió toda la lista de socios exitosamente!!!", "Admitido", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }


        public void MostrarSociosDeudores(DataGridView ListaDeudores, Label menor, Label mayor, Label promedio, Label total)
        {

            try
            {
                ConectarABase.ConnectionString = CadenaConexion;
                ConectarABase.Open();

                //DE SOCIOS
                OleDbCommand ComandoSocio = new OleDbCommand();
                ComandoSocio.Connection = ConectarABase;
                ComandoSocio.CommandType = CommandType.TableDirect;
                ComandoSocio.CommandText = TablaSocios;

                OleDbDataAdapter AdaptadorSocio = new OleDbDataAdapter();//una por tabla

                AdaptadorSocio = new OleDbDataAdapter(ComandoSocio);

                DataSet DS = new DataSet();//una vez para todas las tablas

                AdaptadorSocio.Fill(DS, TablaSocios);

                DataColumn[] DataKeySocios = new DataColumn[1];
                DataKeySocios[0] = DS.Tables[TablaSocios].Columns["IdSocio"];
                DS.Tables[TablaSocios].PrimaryKey = DataKeySocios;

                OleDbCommandBuilder ActualizaSocio = new OleDbCommandBuilder(AdaptadorSocio);


                //DE BARRIOS
                OleDbCommand ComandoBarrio = new OleDbCommand();
                ComandoBarrio.Connection = ConectarABase;
                ComandoBarrio.CommandType = CommandType.TableDirect;
                ComandoBarrio.CommandText = TablaBarrio;

                OleDbDataAdapter AdaptadorBarrio = new OleDbDataAdapter();
                AdaptadorBarrio = new OleDbDataAdapter(ComandoBarrio);

                AdaptadorBarrio.Fill(DS, TablaBarrio);

                DataColumn[] DataKeyBarrio = new DataColumn[1];
                DataKeyBarrio[0] = DS.Tables[TablaBarrio].Columns["idBarrio"];
                DS.Tables[TablaBarrio].PrimaryKey = DataKeyBarrio;


                //DE ACTIVIDAD
                OleDbCommand ComandoActividad = new OleDbCommand();
                ComandoActividad.Connection = ConectarABase;
                ComandoActividad.CommandType = CommandType.TableDirect;
                ComandoActividad.CommandText = TablaActividad;

                OleDbDataAdapter AdaptadorActividad = new OleDbDataAdapter();
                AdaptadorActividad = new OleDbDataAdapter(ComandoActividad);

                AdaptadorActividad.Fill(DS, TablaActividad);

                DataColumn[] DataKeyActividad = new DataColumn[1];
                DataKeyActividad[0] = DS.Tables[TablaActividad].Columns["idActividad"];
                DS.Tables[TablaActividad].PrimaryKey = DataKeyActividad;


                Decimal MayorDeuda = 0;
                Decimal MenorDeuda = 99999;
                Int32 CantidadSocios = 0;
                Decimal TotalDeuda = 0;
                Decimal Deuda = 0;


                if (DS.Tables[TablaSocios].Rows.Count > 0)
                {
                    foreach (DataRow Socio in DS.Tables[TablaSocios].Rows)
                    {
                        if (Convert.ToDecimal(Socio["Deuda"]) > 0)
                        {
                            ListaDeudores.Rows.Add(Socio["IdSocio"], Socio["Nombre"], Socio["Deuda"]);


                            if (int.Parse(Socio["Deuda"].ToString()) < MenorDeuda)
                            {
                                MenorDeuda = int.Parse(Socio["Deuda"].ToString());
                            }

                            if (int.Parse(Socio["Deuda"].ToString()) > MayorDeuda)
                            {
                                MayorDeuda = int.Parse(Socio["Deuda"].ToString());
                            }

                            Deuda = int.Parse(Socio["Deuda"].ToString());

                            TotalDeuda += Deuda;

                            CantidadSocios++;
                        }
                    }
                    menor.Text = MenorDeuda.ToString();
                    mayor.Text = MayorDeuda.ToString();
                    promedio.Text = (TotalDeuda / CantidadSocios).ToString("#.00");
                    total.Text = TotalDeuda.ToString();

                }
               


                ConectarABase.Close();

            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());
            }

        }

        public void MostrarSociosDeActividad(ComboBox actividad, DataGridView ListaPorActividad, Label menor, Label mayor, Label promedio, Label total)
        {

            try
            {
                ConectarABase.ConnectionString = CadenaConexion;
                ConectarABase.Open();

                //DE SOCIOS
                OleDbCommand ComandoSocio = new OleDbCommand();
                ComandoSocio.Connection = ConectarABase;
                ComandoSocio.CommandType = CommandType.TableDirect;
                ComandoSocio.CommandText = TablaSocios;

                OleDbDataAdapter AdaptadorSocio = new OleDbDataAdapter();//una por tabla

                AdaptadorSocio = new OleDbDataAdapter(ComandoSocio);

                DataSet DS = new DataSet();//una vez para todas las tablas

                AdaptadorSocio.Fill(DS, TablaSocios);

                DataColumn[] DataKeySocios = new DataColumn[1];
                DataKeySocios[0] = DS.Tables[TablaSocios].Columns["IdSocio"];
                DS.Tables[TablaSocios].PrimaryKey = DataKeySocios;

                OleDbCommandBuilder ActualizaSocio = new OleDbCommandBuilder(AdaptadorSocio);


                //DE BARRIOS
                OleDbCommand ComandoBarrio = new OleDbCommand();
                ComandoBarrio.Connection = ConectarABase;
                ComandoBarrio.CommandType = CommandType.TableDirect;
                ComandoBarrio.CommandText = TablaBarrio;

                OleDbDataAdapter AdaptadorBarrio = new OleDbDataAdapter();
                AdaptadorBarrio = new OleDbDataAdapter(ComandoBarrio);

                AdaptadorBarrio.Fill(DS, TablaBarrio);

                DataColumn[] DataKeyBarrio = new DataColumn[1];
                DataKeyBarrio[0] = DS.Tables[TablaBarrio].Columns["idBarrio"];
                DS.Tables[TablaBarrio].PrimaryKey = DataKeyBarrio;


                //DE ACTIVIDAD
                OleDbCommand ComandoActividad = new OleDbCommand();
                ComandoActividad.Connection = ConectarABase;
                ComandoActividad.CommandType = CommandType.TableDirect;
                ComandoActividad.CommandText = TablaActividad;

                OleDbDataAdapter AdaptadorActividad = new OleDbDataAdapter();
                AdaptadorActividad = new OleDbDataAdapter(ComandoActividad);

                AdaptadorActividad.Fill(DS, TablaActividad);

                DataColumn[] DataKeyActividad = new DataColumn[1];
                DataKeyActividad[0] = DS.Tables[TablaActividad].Columns["idActividad"];
                DS.Tables[TablaActividad].PrimaryKey = DataKeyActividad;


                Decimal MayorDeuda = 0;
                Decimal MenorDeuda = 99999;
                Int32 CantidadSocios = 0;
                Decimal TotalDeuda = 0;
                Decimal Deuda = 0;

                int checkId = int.Parse(actividad.SelectedValue.ToString());


                if (DS.Tables[TablaSocios].Rows.Count > 0)
                {
                    foreach (DataRow Socio in DS.Tables[TablaSocios].Rows)
                    {
                        if (checkId == (int)Socio["idActividad"])
                        {
                            ListaPorActividad.Rows.Add(Socio["IdSocio"], Socio["Nombre"], Socio["Deuda"]);


                            if (int.Parse(Socio["Deuda"].ToString()) < MenorDeuda)
                            {
                                MenorDeuda = int.Parse(Socio["Deuda"].ToString());
                            }

                            if (int.Parse(Socio["Deuda"].ToString()) > MayorDeuda)
                            {
                                MayorDeuda = int.Parse(Socio["Deuda"].ToString());
                            }

                            Deuda = int.Parse(Socio["Deuda"].ToString());

                            TotalDeuda += Deuda;

                            CantidadSocios++;
                        }
                    }
                    menor.Text = MenorDeuda.ToString();
                    mayor.Text = MayorDeuda.ToString();
                    promedio.Text = (TotalDeuda / CantidadSocios).ToString("#.00");
                    total.Text = TotalDeuda.ToString();

                }
             
               

                ConectarABase.Close();

            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());
            }

        }

        public void ImprimirBarrio(PrintPageEventArgs reporte, DataGridView TodosLosListados, Label totalclientes, Label total, string titulo)
        {
            Font Titulo = new Font("Arial", 20);
            Font Subtitulo = new Font("Arial", 12);
            Font Texto = new Font("Arial", 10);
            Int32 fila = 180;
            reporte.Graphics.DrawString(titulo, Titulo, Brushes.Red, 150, 100);
            reporte.Graphics.DrawString("DNI", Subtitulo, Brushes.Blue, 100, 150);
            reporte.Graphics.DrawString("Nombre", Subtitulo, Brushes.Blue, 300, 150);
            reporte.Graphics.DrawString("Deuda", Subtitulo, Brushes.Blue, 500, 150);

            int f = 0;
            for (f = 0; f < TodosLosListados.RowCount; f++)
            {

                reporte.Graphics.DrawString(Convert.ToString(TodosLosListados.Rows[f].Cells[0].Value), Texto, Brushes.Black, 100, fila);
                reporte.Graphics.DrawString(Convert.ToString(TodosLosListados.Rows[f].Cells[1].Value), Texto, Brushes.Black, 300, fila);
                reporte.Graphics.DrawString(Convert.ToString(TodosLosListados.Rows[f].Cells[2].Value), Texto, Brushes.Black, 500, fila);
                fila = fila + 20;

            }
           
            reporte.Graphics.DrawString("Cantidad de Clientes: " + totalclientes.Text, Texto, Brushes.Black, 100, fila); ;
            fila = fila + 20;
            reporte.Graphics.DrawString("Total de Deudas: " + total.Text, Texto, Brushes.Black, 100, fila); ;

            MessageBox.Show("Se imprimió toda la lista de socios exitosamente!!!", "Admitido", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        public void MostrarSociosDeBarrio(ComboBox barrio, DataGridView ListaPorBarrio, Label cantidadClientes, Label total)
        {

            try
            {
                ConectarABase.ConnectionString = CadenaConexion;
                ConectarABase.Open();

                //DE SOCIOS
                OleDbCommand ComandoSocio = new OleDbCommand();
                ComandoSocio.Connection = ConectarABase;
                ComandoSocio.CommandType = CommandType.TableDirect;
                ComandoSocio.CommandText = TablaSocios;

                OleDbDataAdapter AdaptadorSocio = new OleDbDataAdapter();//una por tabla

                AdaptadorSocio = new OleDbDataAdapter(ComandoSocio);

                DataSet DS = new DataSet();//una vez para todas las tablas

                AdaptadorSocio.Fill(DS, TablaSocios);

                DataColumn[] DataKeySocios = new DataColumn[1];
                DataKeySocios[0] = DS.Tables[TablaSocios].Columns["IdSocio"];
                DS.Tables[TablaSocios].PrimaryKey = DataKeySocios;

                OleDbCommandBuilder ActualizaSocio = new OleDbCommandBuilder(AdaptadorSocio);


                //DE BARRIOS
                OleDbCommand ComandoBarrio = new OleDbCommand();
                ComandoBarrio.Connection = ConectarABase;
                ComandoBarrio.CommandType = CommandType.TableDirect;
                ComandoBarrio.CommandText = TablaBarrio;

                OleDbDataAdapter AdaptadorBarrio = new OleDbDataAdapter();
                AdaptadorBarrio = new OleDbDataAdapter(ComandoBarrio);

                AdaptadorBarrio.Fill(DS, TablaBarrio);

                DataColumn[] DataKeyBarrio = new DataColumn[1];
                DataKeyBarrio[0] = DS.Tables[TablaBarrio].Columns["idBarrio"];
                DS.Tables[TablaBarrio].PrimaryKey = DataKeyBarrio;


                //DE ACTIVIDAD
                OleDbCommand ComandoActividad = new OleDbCommand();
                ComandoActividad.Connection = ConectarABase;
                ComandoActividad.CommandType = CommandType.TableDirect;
                ComandoActividad.CommandText = TablaActividad;

                OleDbDataAdapter AdaptadorActividad = new OleDbDataAdapter();
                AdaptadorActividad = new OleDbDataAdapter(ComandoActividad);

                AdaptadorActividad.Fill(DS, TablaActividad);

                DataColumn[] DataKeyActividad = new DataColumn[1];
                DataKeyActividad[0] = DS.Tables[TablaActividad].Columns["idActividad"];
                DS.Tables[TablaActividad].PrimaryKey = DataKeyActividad;


                //Decimal MayorDeuda = 0;
                //Decimal MenorDeuda = 99999;
                Int32 CantidadSocios = 0;
                Decimal TotalDeuda = 0;
                Decimal Deuda = 0;

                int checkId = int.Parse(barrio.SelectedValue.ToString());


                if (DS.Tables[TablaSocios].Rows.Count > 0)
                {
                    foreach (DataRow Socio in DS.Tables[TablaSocios].Rows)
                    {
                        if (checkId == (int)Socio["idBarrio"])
                        {
                            ListaPorBarrio.Rows.Add(Socio["IdSocio"], Socio["Nombre"], Socio["Deuda"]);


                            //if (int.Parse(Socio["Deuda"].ToString()) < MenorDeuda)
                            //{
                            //    MenorDeuda = int.Parse(Socio["Deuda"].ToString());
                            //}

                            //if (int.Parse(Socio["Deuda"].ToString()) > MayorDeuda)
                            //{
                            //    MayorDeuda = int.Parse(Socio["Deuda"].ToString());
                            //}

                            Deuda = Decimal.Parse(Socio["Deuda"].ToString());

                            TotalDeuda += Deuda;

                            CantidadSocios++;
                        }
                    }
                    
                   
                    cantidadClientes.Text = (CantidadSocios).ToString();
                    total.Text = TotalDeuda.ToString();

                }



                ConectarABase.Close();

            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());
            }

        }

        public void ExportarBarrio(DataGridView TodosLosListados, Label totalClientes, Label total, string titulo)
        {
            StreamWriter ArchivoExportado = new StreamWriter("Exportacion.csv", false, Encoding.UTF8);

            ArchivoExportado.WriteLine(titulo);
            ArchivoExportado.WriteLine("");
            ArchivoExportado.WriteLine("DNI;Nombre;Deuda");

            int f = 0;

            for (f = 0; f < TodosLosListados.RowCount; f++)
            {

                ArchivoExportado.Write(TodosLosListados.Rows[f].Cells[0].Value);
                ArchivoExportado.Write(";");
                ArchivoExportado.Write(TodosLosListados.Rows[f].Cells[1].Value);
                ArchivoExportado.Write(";");
                ArchivoExportado.WriteLine(TodosLosListados.Rows[f].Cells[2].Value);

            }

            ArchivoExportado.WriteLine("");//como se escribieron ya todos los registros, se agregan tres renglones vacios por una cuestion formato
            ArchivoExportado.WriteLine("");
            ArchivoExportado.WriteLine("");
           
            
            ArchivoExportado.Write("Cantidad de Clientes:;;");
            ArchivoExportado.WriteLine(totalClientes.Text);
            ArchivoExportado.Write("Total de Deudas:;;");//lo mismo con las deudas y promedios . Observar que no se necesitaron convertir los datos
            ArchivoExportado.WriteLine(total.Text);

            MessageBox.Show("La lista socios ha sido Exportada como archivo de texto exitosamente", "Admitido", MessageBoxButtons.OK, MessageBoxIcon.Information);


            ArchivoExportado.Close();
            ArchivoExportado.Dispose();
        }

    }
}
