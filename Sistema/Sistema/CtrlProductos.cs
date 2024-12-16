using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Sistema
{
    class CtrlProductos : Conexion
    {
        public List<Object> consulta(string dato)
            {
            MySqlDataReader reader;
            List<Object> lista = new List<object>();
            string sql;

            if (dato == null)
            {
                sql = "SELECT id, codigo, nombre, descripcion, precio_publico, existencias FROM productos ORDER BY nombre ASC";
            }
            else
            {
                sql = "SELECT id, codigo, nombre, descripcion, precio_publico, existencias FROM productos WHERE codigo LIKE '%" + dato + "%' OR nombre LIKE '%" + dato + "%' OR descripcion LIKE '%" + dato + "%' OR precio_publico LIKE '%" + dato + "%' OR existencias LIKE '%" + dato + "%' ORDER BY nombre ASC";
            }

            try
            {
                MySqlConnection conexionBD = base.conexio();
                conexionBD.Open();
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Productos _producto = new Productos();
                    _producto.Id = int.Parse(reader.GetString(0));
                    _producto.Codigo = reader[1].ToString();
                    _producto.Nombre = reader.GetString("descripcion");
                    _producto.Descripcion = reader[3].ToString();
                    _producto.Precio_publico = double.Parse(reader[4].ToString());
                    _producto.Existencias = int.Parse(reader.GetString(5));
                    lista.Add(_producto);
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            return lista;
        }
    }
}
