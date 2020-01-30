using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        readonly string connectingString = @"data source=localhost\SQLEXPRESS; initial catalog=Users; integrated security=True;";

        /// <summary>
        /// Index - Start the system 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {            
            return View();
        }

        /// <summary>
        /// List users
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            List<dynamic> result = new List<dynamic>();

            using (SqlConnection sqlCon = new SqlConnection(connectingString))
            {
                SqlCommand cmd = new SqlCommand("Select * from Users", sqlCon);
                sqlCon.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(new { UserId = reader["UserId"] });
                    result.Add(new { Username = reader["Username"] });
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
        /// <param name="userModel"></param>
        /// <returs></returs>
        [HttpPost]
        public ActionResult Create(UserModel userModel)
        {

            var message = string.Empty;

            try
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
            }
            catch (Exception ex)
            {
                message = $"Não foi possível realizar esta função, {ex.Message}";
            }
            return Json(new { User = userModel, Message = message }, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// Delete users
        /// </summary>
        /// <param name="id"></param>
        /// <returs></returs>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var sucesso = true;
            var message = string.Empty;

            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectingString))
                {
                    sqlCon.Open();
                    SqlCommand cmd = new SqlCommand("Delete From Users Where UserId = @UserId", sqlCon);
                    cmd.Parameters.AddWithValue("@UserId", id);

                    int execucao = cmd.ExecuteNonQuery();

                    if (execucao < 1)
                    {
                        sucesso = false;
                        message = "Nenhuma linha foi afetada. O delete não foi realizado";
                    }
                }
            }
            catch (Exception ex)
            {
                //log erro
                sucesso = false;
                message = $"Erro ao realizar o comando: {ex.Message}";
            }

            return Json(new { Sucesso = sucesso, Message = message }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Edit users
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public ActionResult Edit(int userid)
        {
            UserModel user = new UserModel();
            DataTable dataTable = new DataTable();
            var sucesso = true;
            var message = string.Empty;

            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectingString))
                {
                    sqlCon.Open();
                    SqlCommand cmd = new SqlCommand("Select * from Users where UserId = @UserId", sqlCon);
                    SqlDataAdapter sqlData = new SqlDataAdapter(cmd.ToString(), sqlCon);
                    sqlData.SelectCommand.Parameters.AddWithValue("@UserId", userid);
                    sqlData.Fill(dataTable);

                    if (dataTable.Rows.Count == 1)
                    {
                        user.UserId = Convert.ToInt32(dataTable.Rows[0][0].ToString());
                        user.Username = dataTable.Rows[0][1].ToString();
                        user.Age = Convert.ToInt32(dataTable.Rows[0][2].ToString());
                        user.CellPhone = dataTable.Rows[0][3].ToString();
                        user.Email = dataTable.Rows[0][4].ToString();
                        user.DateCreate = Convert.ToDateTime(dataTable.Rows[0][5].ToString());
                        user.Address = dataTable.Rows[0][6].ToString();
                        user.Obs = dataTable.Rows[0][7].ToString();

                        sucesso = true;
                        message = $"Dados disponíveis para edição";
                    }
                }
            }
            catch (Exception ex)
            {
                sucesso = false;
                message = $"Erro ao realizar o comando: {ex.Message}";
                
                return Json(new { Sucesso = sucesso, Message = message }, JsonRequestBehavior.AllowGet);

            }

            return Json(new { Sucesso = sucesso, Message = message, Data = user }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult Edit(UserModel user)
        {
            var sucesso = true;
            var message = string.Empty;

            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectingString))
                {
                    sqlCon.Open();
                    SqlCommand cmd = new SqlCommand("Update Users Set Username = @Username, @Age = Age, CellPhone = @CellPhone, Email = @Email, DateCreate = @DateCreate, Address = @Address, Obs = @Obs", sqlCon);
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Age", user.Age);
                    cmd.Parameters.AddWithValue("@CellPhone", user.CellPhone);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@DateCreate", user.DateCreate);
                    cmd.Parameters.AddWithValue("@Address", user.Address);
                    cmd.Parameters.AddWithValue("@Obs", user.Obs);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                //log erro
                sucesso = false;
                message = $"Erro ao realizar o comando: {ex.Message}";
            }

            return Json(new { Sucesso = sucesso, Message = message }, JsonRequestBehavior.AllowGet);

        }
    }
}