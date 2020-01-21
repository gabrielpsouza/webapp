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
        /// List users 
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
                    result.Add(new { UserId = reader["UserId"] });
                    result.Add( new { Username = reader["Username"] });
                    result.Add(new { Age = reader["Age"] });
                    result.Add(new { CellPhone = reader["CellPhone"] });
                    result.Add(new { Email = reader["Email"] });
                    result.Add(new { DateCreate = reader["DateCreate"] });
                    result.Add(new { Address = reader["Address"] });
                    result.Add(new { Obs = reader["Obs"] });
                }

                reader.Close();
            }            

            return Json(new { AllUsers = result }, JsonRequestBehavior.AllowGet);            
        }

        /// <summary>
        /// Create users
        /// </summary>
        /// <returs></returs>
        [HttpPost]
        public ActionResult Create(UserModel userModel)
        { 

            using (SqlConnection sqlCon = new SqlConnection(connectingString))
            {
                SqlCommand cmd = new SqlCommand("Insert Into Users Values(@Username, @Age, @CellPhone, @Email, @DateCreate, @Address, @Obs)", sqlCon);

                cmd.Parameters.AddWithValue("@UserId", userModel.UserId);
                cmd.Parameters.AddWithValue("@Username", userModel.Username);
                cmd.Parameters.AddWithValue("@Age", userModel.Age);
                cmd.Parameters.AddWithValue("@CellPhone", userModel.CellPhone);
                cmd.Parameters.AddWithValue("@Email", userModel.Email);
                cmd.Parameters.AddWithValue("@DateCreate", userModel.DateCreate);
                cmd.Parameters.AddWithValue("@Address", userModel.Address);
                cmd.Parameters.AddWithValue("@Obs", userModel.Obs);

                cmd.ExecuteNonQuery();
            }

            return Json(new { User = userModel }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Delete users
        /// </summary>
        /// <returs></returs>
        public ActionResult Delete(int id)
        {
            
            using (SqlConnection sqlCon = new SqlConnection(connectingString))
            {
                SqlCommand cmd = new SqlCommand("Delete From Users Where UserId = @UserId", sqlCon);
                cmd.Parameters.AddWithValue("@UserId", id);

                cmd.ExecuteNonQuery();
            }

            return Json(new { Message = true }, JsonRequestBehavior.AllowGet);
        }
    }
}