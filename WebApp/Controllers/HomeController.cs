using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        readonly string connectingString = @"data source=localhost\SQLEXPRESS; initial catalog=Users; integrated security=True;";

        //LIST - LISTA USUARIO
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
                }
                reader.Close();
            }            

            return Json(new { Users = result }, JsonRequestBehavior.AllowGet);            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}