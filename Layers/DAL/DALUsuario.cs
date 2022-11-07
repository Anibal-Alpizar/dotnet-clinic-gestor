using Clinic_gestor.Interfaces;
using Clinic_gestor.Layers.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Clinic_gestor.Layers.DAL
{
    internal class DALUsuario : IDALUsuario
    {
        private static readonly ILog _MyLogControlEventos = log4net.LogManager.GetLogger("MyControlEventos");

        public Usuario SaveUsuario(Usuario pUsuario)
        {
            StringBuilder conexion = new StringBuilder();
            SqlCommand command = new SqlCommand();
            Usuario oUsuario = null;
            string sql = @"Insert into Usuario(Login,IdRol,Password,Nombre ) values (@Login,@IdRol,@Password,@Nombre )";
            double row = 0;
            try
            {
                command.Parameters.AddWithValue("@Login", pUsuario.Login);
                command.Parameters.AddWithValue("@IdRol", pUsuario.IdRol);
                command.Parameters.AddWithValue("@Password", pUsuario.Password);
                command.Parameters.AddWithValue("@Nombre", pUsuario.Nombre);
                command.CommandText = sql;
                command.CommandType = CommandType.Text;


                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    row = db.ExecuteNonQuery(command, IsolationLevel.ReadCommitted);
                }

                if (row > 0)
                    oUsuario = GetUsuarioById(pUsuario.Login);

                return oUsuario;
            }
            catch (Exception er)
            {
                StringBuilder msg = new StringBuilder();
                if (er is SqlException)
                {
                    msg.AppendFormat("{0}\n", UtilError.CreateSQLExceptionsErrorDetails(MethodBase.GetCurrentMethod(), command, er as SqlException));
                    _MyLogControlEventos.ErrorFormat("Error {0}", msg.ToString());
                    throw new CustomException(UtilError.GetCustomErrorByNumber(er as SqlException));
                }
                else
                {
                    msg.AppendFormat(UtilError.CreateGenericErrorExceptionDetail(MethodBase.GetCurrentMethod(), er));
                    _MyLogControlEventos.ErrorFormat("Error {0}", msg.ToString());
                    throw;
                }
            }
        }


        public Usuario UpdateUsuario(Usuario pUsuario)
        {
            StringBuilder conexion = new StringBuilder();
            SqlCommand command = new SqlCommand();
            Usuario oUsuario = null;
            string sql = @"Update Usuario SET IdRol=@IdRol, Password=@Password, Nombre= @Nombre Where (Login = @Login)";
            double row = 0;
            try
            {
                command.Parameters.AddWithValue("@Login", pUsuario.Login);
                command.Parameters.AddWithValue("@IdRol", pUsuario.IdRol);
                command.Parameters.AddWithValue("@Password", pUsuario.Password);
                command.Parameters.AddWithValue("@Nombre", pUsuario.Nombre);
                command.CommandText = sql;
                command.CommandType = CommandType.Text;


                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    row = db.ExecuteNonQuery(command, IsolationLevel.ReadCommitted);
                }

                if (row > 0)
                    oUsuario = GetUsuarioById(pUsuario.Login);

                return oUsuario;
            }
            catch (Exception er)
            {
                StringBuilder msg = new StringBuilder();
                if (er is SqlException)
                {
                    msg.AppendFormat("{0}\n", UtilError.CreateSQLExceptionsErrorDetails(MethodBase.GetCurrentMethod(), command, er as SqlException));
                    _MyLogControlEventos.ErrorFormat("Error {0}", msg.ToString());
                    throw new CustomException(UtilError.GetCustomErrorByNumber(er as SqlException));
                }
                else
                {
                    msg.AppendFormat(UtilError.CreateGenericErrorExceptionDetail(MethodBase.GetCurrentMethod(), er));
                    _MyLogControlEventos.ErrorFormat("Error {0}", msg.ToString());
                    throw;
                }
            }
        }

        public Usuario Login(string pLogin, string pPassword)
        {
            StringBuilder conexion = new StringBuilder();
            SqlCommand command = new SqlCommand();
            IDataReader reader = null;
            Usuario oUsuario = null;
            try
            {
                command.CommandText = @"select * from usuario with (rowlock)   where Login = @pLogin and Password = @pPassword";
                command.Parameters.AddWithValue("@pLogin", pLogin);
                command.Parameters.AddWithValue("@pPassword", pPassword);
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    reader = db.ExecuteReader(command);

                    while (reader.Read())
                    {
                        oUsuario = new Usuario();
                        oUsuario.Login = reader["Login"].ToString();
                        oUsuario.IdRol = int.Parse(reader["IdRol"].ToString());
                        oUsuario.Password = reader["Password"].ToString();
                        oUsuario.Nombre = reader["Nombre"].ToString();
                        oUsuario.Estado = bool.Parse(reader["Estado"].ToString());
                    }
                }

                return oUsuario;
            }
            catch (Exception er)
            {
                StringBuilder msg = new StringBuilder();
                if (er is SqlException)
                {
                    msg.AppendFormat("{0}\n", UtilError.CreateSQLExceptionsErrorDetails(MethodBase.GetCurrentMethod(), command, er as SqlException));
                    _MyLogControlEventos.ErrorFormat("Error {0}", msg.ToString());
                    throw new CustomException(UtilError.GetCustomErrorByNumber(er as SqlException));
                }
                else
                {
                    msg.AppendFormat(UtilError.CreateGenericErrorExceptionDetail(MethodBase.GetCurrentMethod(), er));
                    _MyLogControlEventos.ErrorFormat("Error {0}", msg.ToString());
                    throw;
                }
            }
        }

        public Usuario GetUsuarioById(string pLogin)
        {
            StringBuilder conexion = new StringBuilder();
            SqlCommand command = new SqlCommand();
            IDataReader reader = null;
            Usuario oUsuario = null;
            try
            {
                command.CommandText = @"select * from usuario with (rowlock)   where Login = @login";
                command.Parameters.AddWithValue("@login", pLogin);

                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    reader = db.ExecuteReader(command);

                    while (reader.Read())
                    {
                        oUsuario = new Usuario();
                        oUsuario.Login = reader["Login"].ToString();
                        oUsuario.IdRol = int.Parse(reader["IdRol"].ToString());
                        oUsuario.Password = reader["Password"].ToString();
                        oUsuario.Nombre = reader["Nombre"].ToString();
                        oUsuario.Estado = bool.Parse(reader["Estado"].ToString());
                    }
                }

                return oUsuario;
            }
            catch (Exception er)
            {
                StringBuilder msg = new StringBuilder();
                if (er is SqlException)
                {
                    msg.AppendFormat("{0}\n", UtilError.CreateSQLExceptionsErrorDetails(MethodBase.GetCurrentMethod(), command, er as SqlException));
                    _MyLogControlEventos.ErrorFormat("Error {0}", msg.ToString());
                    throw new CustomException(UtilError.GetCustomErrorByNumber(er as SqlException));
                }
                else
                {
                    msg.AppendFormat(UtilError.CreateGenericErrorExceptionDetail(MethodBase.GetCurrentMethod(), er));
                    _MyLogControlEventos.ErrorFormat("Error {0}", msg.ToString());
                    throw;
                }
            }
        }

        public IEnumerable<Usuario> GetAllLogin()
        {
            StringBuilder conexion = new StringBuilder();
            SqlCommand command = new SqlCommand();
            IDataReader reader = null;
            Usuario oUsuario = null;
            IList<Usuario> lista = new List<Usuario>();
            try
            {
                command.CommandText = @"select * from usuario with (rowlock)  ";
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    reader = db.ExecuteReader(command);

                    while (reader.Read())
                    {
                        oUsuario = new Usuario();
                        oUsuario.Login = reader["Login"].ToString();
                        oUsuario.IdRol = int.Parse(reader["IdRol"].ToString());
                        oUsuario.Password = reader["Password"].ToString();
                        oUsuario.Nombre = reader["Nombre"].ToString();
                        oUsuario.Estado = bool.Parse(reader["Estado"].ToString());
                        lista.Add(oUsuario);
                    }
                }

                return lista;
            }
            catch (Exception er)
            {
                StringBuilder msg = new StringBuilder();
                if (er is SqlException)
                {
                    msg.AppendFormat("{0}\n", UtilError.CreateSQLExceptionsErrorDetails(MethodBase.GetCurrentMethod(), command, er as SqlException));
                    _MyLogControlEventos.ErrorFormat("Error {0}", msg.ToString());
                    throw new CustomException(UtilError.GetCustomErrorByNumber(er as SqlException));
                }
                else
                {
                    msg.AppendFormat(UtilError.CreateGenericErrorExceptionDetail(MethodBase.GetCurrentMethod(), er));
                    _MyLogControlEventos.ErrorFormat("Error {0}", msg.ToString());
                    throw;
                }
            }
        }

        public bool DeleteUsuario(string pLogin)
        {
            StringBuilder conexion = new StringBuilder();
            SqlCommand command = new SqlCommand();

            string sql = @"Delete  from  Usuario  where login = @Login";
            double row = 0;
            try
            {
                command.Parameters.AddWithValue("@Login", pLogin);
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    row = db.ExecuteNonQuery(command, IsolationLevel.ReadCommitted);
                }


                return row > 0 ? true : false;
            }
            catch (Exception er)
            {
                StringBuilder msg = new StringBuilder();
                if (er is SqlException)
                {
                    msg.AppendFormat("{0}\n", UtilError.CreateSQLExceptionsErrorDetails(MethodBase.GetCurrentMethod(), command, er as SqlException));
                    throw new CustomException(UtilError.GetCustomErrorByNumber(er as SqlException));
                }
                else
                {
                    msg.AppendFormat(UtilError.CreateGenericErrorExceptionDetail(MethodBase.GetCurrentMethod(), er));
                    throw;
                }
            }
        }

    }
}
