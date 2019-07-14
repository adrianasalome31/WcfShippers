using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.SqlClient;
using System.Data;


namespace WcfShippers
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        SqlConnection con = new SqlConnection("Data Source = 192.168.206.128; Initial Catalog= Northwind; User id= sa; Password = P@ssword;");

        public string SearchShippers(string companyName)
        {
            string message = "";
            List<string> ListcompanyName = new List<string>();
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Shippers WHERE CompanyName = @companyName", con);
                cmd.Parameters.AddWithValue("@companyName", companyName);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    message = "Registro encontrado";
                    for (int i=0; i<dt.Rows.Count; i++)
                    {
                        string name = dt.Rows[i]["CompanyName"].ToString();
                        ListcompanyName.Add(name);

                    }
                }
                else
                {
                    message = "No hubo Coincidencias";
                }
                con.Close();
            }
            return message;


        }
        
    }
}
