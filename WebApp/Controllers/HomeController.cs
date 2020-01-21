using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        readonly string connectingString = @"data source=localhost\SQLEXPRESS; initial catalog=Users; integrated security=True;";

        /// <summary>
        /// Listagem dos usuários 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            List<dynamic> result = new List<dynamic>();

            using (SqlConnection sqlCon = new SqlConnection(connectingString))
            {
                SqlCommand cmd = new SqlCommand("Select * from Users where Id = @Id", sqlCon);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result.Add( new { Username = reader["Usuario"] });
                    result.Add(new { Age = reader["Age"] });
                }
                reader.Close();
            }            

            return Json(new { AllUsers = result }, JsonRequestBehavior.AllowGet);            
        }

        /// <summary>
        /// Criacão de usuário
        /// </summary>
        /// <returs></returs>
        [HttpPost]
        public ActionResult Create(UsuarioModel usuarioModel)
        { 

            using (SqlConnection sqlCon = new SqlConnection(connectingString))
            {
                SqlCommand cmd = new SqlCommand("Insert Into Users Values(@User, @Age, @CellPhone, @Email, @DateCreate, @Endereco, @Obs)", sqlCon);

                cmd.Parameters.AddWithValue("@UserId", usuarioModel.UserId);
                cmd.Parameters.AddWithValue("@User", usuarioModel.User);
                cmd.Parameters.AddWithValue("@Age", usuarioModel.Age);
                cmd.Parameters.AddWithValue("@CellPhone", usuarioModel.CellPhone);
                cmd.Parameters.AddWithValue("@Email", usuarioModel.Email);
                cmd.Parameters.AddWithValue("@DateCreate", usuarioModel.DateCreate);
                cmd.Parameters.AddWithValue("@Endereco", usuarioModel.Endereco);
                cmd.Parameters.AddWithValue("@Obs", usuarioModel.Obs);

                cmd.ExecuteNonQuery();
            }

            return Json(new { User = usuarioModel }, JsonRequestBehavior.AllowGet);
        }
    }
}