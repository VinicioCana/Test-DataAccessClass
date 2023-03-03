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
        en función del resusltado del query.*/
        
        public int ExecuteNonQuery(string query)
        {
            int rows = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                rows = command.ExecuteNonQuery();
            }

            return rowsAffected;
        }

        // Devuelve el número de filas que devolveria el select del query que recibe como parámetro de entrada.

        public int GetRowCountSelect(string query)
        {
            int rowCount = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                rowCount = (int)command.ExecuteScalar();
            }

            return rowCount;
        }
    }
}
