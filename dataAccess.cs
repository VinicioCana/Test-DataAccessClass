using System;
using System.Data.SqlClient;

public class Test4
{
    public class DataAccess
    {
        private string connectionString;

        public DataAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /* Devuelve un dataset del select del query que recibe,
        Se utiliza la directiva using para garantizar el cierre de la conexión. */

        public DataSet Select(string query)
        {
            DataSet result = new DataSet();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(result);
            }

            return result;
        }

        /* Devuelve el número de filas afectadas en la ejecución del query.
        Inicializamos la variable rows a O y dentro del using la modificamos
        en función del resusltado del query.
        Si el query contine un SELECT se crea una consulta anidada que permite hacer el conteo del select 
        cuya respuesta se devuelve en el parámetro de salida rowCount*/

        public int ExecuteNonQuery(string query, out int rowCount)
        {
            int rows = 0;
            rowCount = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                rows = command.ExecuteNonQuery();

                // Si la consulta contiene un SELECT, contamos el número de filas, dentro de una subconsulta 
                if (nonQuery.StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
                {
                    command.CommandText = $"SELECT COUNT(*) FROM ({query}) AS M";
                    rowCount = (int)command.ExecuteScalar();
                }
            }

            return rows;
        }
    }
}
