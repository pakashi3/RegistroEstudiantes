
using RegistroEstudiantes.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Transactions;

namespace RegistroEstudiantes.Data
{
    public class ADOMaterias : IMateriaService
    {
        private readonly string connectinString;

        private int rowsAffected;

        List<Materia> MateriasParaActualizar;
        List<Materia> MateriasParaCrear;
        public ADOMaterias(string connectinString)
        {
            this.connectinString = connectinString;
            MateriasParaActualizar = new List<Materia>();
            MateriasParaCrear = new List<Materia>();
        }
        public Materia ActualizarMateria(Materia materiaActualizada)
        {
            MateriasParaActualizar.Add(materiaActualizada);
            return materiaActualizada;

            //using (SqlConnection conn = new SqlConnection(connectinString))
            //{
            //var query = @"Update Materias
            //set Nombre = @nombre
            // Codigo = @codigo
            // Area = @area
            //Disponible = @disponible
            //Objetivos = @objetivos
            //where Id = @id";

            //SqlCommand cmd = new SqlCommand(query, conn);
            // cmd.Parameters.AddWithValue("@id", materiaActualizada.Id);
            //    cmd.Parameters.AddWithValue("@nombre", materiaActualizada.Nombre);
            //    cmd.Parameters.AddWithValue("@codigo", materiaActualizada.Codigo);
            //    cmd.Parameters.AddWithValue("@area", materiaActualizada.Area);
            //  cmd.Parameters.AddWithValue("@disponible", materiaActualizada.Disponible);
            //   cmd.Parameters.AddWithValue("@objetivos", materiaActualizada.Objetivos);

            //   conn.Open();
            //    cmd.ExecuteNonQuery();
            //    conn.Close();
            //    return materiaActualizada;
            //   }
        }

        public Materia CrearMateria(Materia materia)
        {
            MateriasParaCrear.Add(materia);
             
            return materia;

            
        }

        public Materia GetMateriaPorId(int Id)
        {
            using (SqlConnection conn = new SqlConnection(connectinString))
            {
                var query = @"select Id, Nombre, Codigo, Area, Disponible, Objetivos
                    from Materias 
                    Where Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", Id);

                Materia materia = null;
                conn.Open();
                var dataReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    materia = new Materia
                    {
                        Id = Convert.ToInt32(dataReader["Id"]),
                        Nombre = dataReader["Nombre"].ToString(),
                        Codigo = dataReader["Codigo"].ToString(),
                        Area = (Area)dataReader["Area"],
                        Objetivos = dataReader["Objetivos"].ToString()
                    };
                }
                return materia;

            }
        }

        public IList<Materia> GetMateriasPorNombre(string texto)
        {
            using (SqlConnection conn = new SqlConnection(this.connectinString))
            {
                var query = @"Select * 
                        from Materias
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

                List<Materia> materias = new List<Materia>();

                while (dataReader.Read())
                {
                    materias.Add(new Materia
                    {
                        Id = Convert.ToInt32(dataReader["Id"]),
                        Nombre = dataReader["Nombre"].ToString(),
                        Codigo = dataReader["Codigo"].ToString(),
                        Area = (Area)dataReader["Area"],
                        Objetivos = dataReader["Objetivos"].ToString()
                    });
                    //dataReader["Id"];
                    //dataReader["Nombre"];
                }
                conn.Close();
                conn.Dispose();

                return materias;
            }

        }



        public int GuardarCambios()
        {
            using (SqlConnection conn = new SqlConnection(connectinString))
            {
                using (TransactionScope scope = new TransactionScope())
                { 
                    foreach (var materia in MateriasParaActualizar)
                    {

                        ProcesarActualizacion(materia, conn);
                    }

                foreach (var materia in MateriasParaCrear)
                {
                    ProcesarCreacion(materia, conn);

                }

                    scope.Complete();
                }

            }

            return rowsAffected;
        }

        private void ProcesarCreacion(Materia materia, SqlConnection conn)
        {
                    var query = @"dbo.InsertarMateria";

            //Insert into Materias(Nombre, Codigo, Area, Disponible, Objetivos) values(@nombre, @codigo, @area, @disponible, @objetivos); select SCOPE_IDENTITY()

                      SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", materia.Nombre);
                  cmd.Parameters.AddWithValue("@codigo", materia.Codigo);
                 cmd.Parameters.AddWithValue("@area", materia.Area);
                   cmd.Parameters.AddWithValue("@disponible", materia.Disponible);
                 cmd.Parameters.AddWithValue("@objetivos", materia.Objetivos);

               conn.Open();
               int id = Convert.ToInt32(cmd.ExecuteScalar()); //cmd.ExecuteScalar
            conn.Close();

              materia.Id = id;

            rowsAffected++;
            }
        

        private void ProcesarActualizacion(Materia materiaActualizada, SqlConnection conn)
        {
            
            var query = @"update Materias
            set Nombre = @nombre,
                Codigo = @codigo,
                Area = @area,
                Disponible = @disponible,
                Objetivos = @objetivos,
                where Id = @id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", materiaActualizada.Id);
                cmd.Parameters.AddWithValue("@nombre", materiaActualizada.Nombre);
                cmd.Parameters.AddWithValue("@codigo", materiaActualizada.Codigo);
                cmd.Parameters.AddWithValue("@area", materiaActualizada.Area);
              cmd.Parameters.AddWithValue("@disponible", materiaActualizada.Disponible);
               cmd.Parameters.AddWithValue("@objetivos", materiaActualizada.Objetivos);

               conn.Open();
                rowsAffected++;
                conn.Close();
              
               }

        public Materia Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public object GetTotalMateriasRegistradas()
        {
            throw new NotImplementedException();
        }
    }
    }






