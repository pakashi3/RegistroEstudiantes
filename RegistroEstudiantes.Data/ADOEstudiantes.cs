using System.Data.SqlClient;
using RegistroEstudiantes.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace RegistroEstudiantes.Data
{
    public class ADOEstudiantes : IEstudianteService
    {
        private readonly string connectinString;

        private int rowsAffected;
        List<Estudiante> EstudiantesParaActualizar;
        List<Estudiante> EstudiantesParaCrear;
        public ADOEstudiantes(string connectinString)
        {
            this.connectinString = connectinString;
            EstudiantesParaActualizar = new List<Estudiante>();
            EstudiantesParaCrear = new List<Estudiante>();
        }
        public Estudiante ActualizarEstudiante(Estudiante estudianteActualizado)
        {
            EstudiantesParaActualizar.Add(estudianteActualizado);
            return estudianteActualizado;
        }

        public Estudiante CrearEstudiante(Estudiante estudiante)
        {
            EstudiantesParaCrear.Add(estudiante);
            return estudiante;
        }

        public Estudiante GetEstudiantesPorId(int Id)
        {
            using (SqlConnection conn = new SqlConnection(connectinString))
            {
                var query = @"select Id, Matricula, Nombre, Apellido, FechaNac, Edad, Meta, Carrera, Sexo
                     From Estudiantes as Id
                        where Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", Id);

                Estudiante estudiante = null;
                conn.Open();
                var dataReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (dataReader.Read())
                {
                    estudiante = new Estudiante
                    {
                        Id = Convert.ToInt32(dataReader["Id"]),
                        Matricula = dataReader["Matricula"].ToString(),
                        Nombre = dataReader["Nombre"].ToString(),
                        Apellido = dataReader["Apellido"].ToString(),
                        FechaNac = (DateTime)dataReader["FechaNac"],
                        Edad = Convert.ToInt32(dataReader["Edad"]),
                        Meta = dataReader["Meta"].ToString(),
                        Carrera = (Carrera)dataReader["Carrera"],
                        Sexo = (Sexo)dataReader["Sexo"],
                    };
                }
                return estudiante;

            }
        }


        public IList<Estudiante> GetEstudiantesPorNombre(string texto)
        {
            using (SqlConnection conn = new SqlConnection(this.connectinString))
            {
                var query = @"Select * 
                        from Estudiantes
                        where Nombre Like'%' + @text + '%' or isnull(@text, '') = ''";

                SqlCommand cmd = new SqlCommand(query, conn);

                SqlParameter text = new SqlParameter("@text", texto);

                if (texto == null)
                {
                    text.Value = DBNull.Value;
                }

                text.SqlDbType = System.Data.SqlDbType.VarChar;

                cmd.Parameters.Add(text);

                conn.Open();

                var dataReader = cmd.ExecuteReader();

                List<Estudiante> estudiantes = new List<Estudiante>();

                while (dataReader.Read())
                {
                    estudiantes.Add(new Estudiante
                    {

                        Id = Convert.ToInt32(dataReader["Id"]),
                        Matricula = dataReader["Matricula"].ToString(),
                        Nombre = dataReader["Nombre"].ToString(),
                        Apellido = dataReader["Apellido"].ToString(),
                        FechaNac = (DateTime)dataReader["FechaNac"],
                        Edad = Convert.ToInt32(dataReader["Edad"]),
                        Meta = dataReader["Meta"].ToString(),
                        Carrera = (Carrera)dataReader["Carrera"],
                        Sexo = (Sexo)dataReader["Sexo"],

                    });

                }
                conn.Close();
                conn.Dispose();

                return estudiantes;
            }
        }

        public int GuardarCambios()
        { 

        using (SqlConnection conn = new SqlConnection(connectinString))
            {
                using (TransactionScope scope = new TransactionScope())
                { 
                    foreach (var estudiante in EstudiantesParaActualizar)
                    {

                        ProcesarActualizacion(estudiante, conn);
}

                foreach (var estudiante in EstudiantesParaCrear)
                {
                    ProcesarCreacion(estudiante, conn);

                }

                    scope.Complete();
                }

            }

            return rowsAffected;
        }

        private void ProcesarCreacion(Estudiante estudiante, SqlConnection conn)
        {
            var query = @"dbo.InsertarEstudiante";

            //Insert into Estudiantes(Matricula, Nombre, Apellido, FechaNac, Edad, Meta, Carrera, Sexo ) values(@matricula, @nombre, @apellido, @fechnac, @edad, @Meta, @Carrera, @sexo);

            //select SCOPE_IDENTITY() as Id

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@matricula", estudiante.Nombre);
            cmd.Parameters.AddWithValue("@nombre", estudiante.Nombre);
            cmd.Parameters.AddWithValue("@apellido", estudiante.Apellido);
            cmd.Parameters.AddWithValue("@fechnac", estudiante.FechaNac);
            cmd.Parameters.AddWithValue("@edad", estudiante.Edad);
            cmd.Parameters.AddWithValue("@meta", estudiante.Meta);
            cmd.Parameters.AddWithValue("@carrera", estudiante.Carrera);
            cmd.Parameters.AddWithValue("@sexo", estudiante.Sexo);

            conn.Open();
            int id = Convert.ToInt32(cmd.ExecuteScalar()); //cmd.ExecuteScalar
            conn.Close();

            estudiante.Id = id;

            rowsAffected++;
        }

        private void ProcesarActualizacion(Estudiante estudianteActualizado, SqlConnection conn)
        {

       var query = @"update Estudiantes
                        set Matricula = @matricula,
                        Nombre = @nombre,
                        Apellido = @apellido,
                        FechaNac = @fechnac,
                        Edad = @edad,
                        Meta = @Meta,
                        Carrera = @Carrera,
                        Sexo = @sexo
                   where Id = @id;";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", estudianteActualizado.Id);
            cmd.Parameters.AddWithValue("@matricula", estudianteActualizado.Matricula);
            cmd.Parameters.AddWithValue("@nombre", estudianteActualizado.Nombre);
            cmd.Parameters.AddWithValue("@apellido", estudianteActualizado.Apellido);
            cmd.Parameters.AddWithValue("@fechnac", estudianteActualizado.FechaNac);
            cmd.Parameters.AddWithValue("@edad", estudianteActualizado.Edad);
            cmd.Parameters.AddWithValue("@meta", estudianteActualizado.Meta);
            cmd.Parameters.AddWithValue("@carrera", estudianteActualizado.Carrera);
            cmd.Parameters.AddWithValue("@sexo", estudianteActualizado.Sexo);

            conn.Open();
            rowsAffected++;
            conn.Close();

        }

        public Estudiante Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public object GetTotalEstudaintesRegistrados()
        {
            throw new NotImplementedException();
        }
    }
}
