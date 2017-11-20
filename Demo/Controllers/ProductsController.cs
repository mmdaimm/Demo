using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Demo.Models;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.IO;

namespace Demo.Controllers
{
    public class ProductsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult ViewProducts()
        {
            IEnumerable<Products> _data = new List<Products>
            {
                new Products()
                {
                    P_id = 01,
                    P_name = "Book"
                },
                new Products()
                {
                    P_id = 02,
                    P_name = "Paper"
                }
            };
            return View(_data);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Adddata(string email, string password, HttpPostedFileBase file)
        {
            string filename = Path.GetFileName(file.FileName.ToString());
            MySqlConnection con = new MySqlConnection("host=localhost;user=test;password=1234;database=testdata");
            if (email != "" && password != "")
            {

                string StrSql = "INSERT INTO testtable (id,password,picture) VALUES ('" + email + "','" + password + "','" + filename + "')";
                con.Open();
                MySqlCommand cmd = new MySqlCommand(StrSql, con);
                cmd.ExecuteNonQuery();
            }
            if (file != null)
            {
                string pic = Path.GetFileName(file.FileName.ToString());
                string path = Path.Combine(Server.MapPath("~/Picture"), pic);
                // file is uploaded
                file.SaveAs(path);

                // save the image path path to the database or you can send image 
                // directly to database
                // in-case if you want to store byte[] ie. for DB
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }

            }
            else
            {
                Response.Write("Your ID or PASSWORD is empty");
            }
            return View("Index");

            // "VALUES" + "('" + "Null" + "','" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "','" + 
        }

        public ActionResult Show()
        {

            return View("Index");
        }

        public ActionResult data()
        {
            MySqlConnection con = new MySqlConnection("host=localhost;user=test;password=1234;database=testdata");
            string StrSql = "SELECT * FROM testtable";
            MySqlCommand cmd = new MySqlCommand(StrSql, con);
            con.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            try
            {
                while (dr.Read())
                {
                    ViewBag.MessageID = dr.GetString(0);
                    ViewBag.MessagePW = dr.GetString(1);
                }
                //   var obj = cmd.ExecuteScalar();
                //   if(obj != DBNull.Value)
                //   {
                //      ViewBag.MessageID = obj.ToString();
                //   }
            }
            finally
            {
                con.Close();
            }

            return View("Index");
        }

        public ActionResult AddToBasket()
        {
            MySqlConnection con = new MySqlConnection("host=localhost;user=test;password=1234;database=testdata");
            string StrSql = "SELECT * FROM testtable";
            MySqlCommand cmd = new MySqlCommand(StrSql, con);

            var model = new List<Products>();
            con.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            try
            {
                while (dr.Read())
                {
                    var product = new Products();
                    model.Add(product);
                }
            }
            finally
            {
                con.Close();
            }

            return View();
        }

    }
}