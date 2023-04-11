using MVCMachineTask.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCMachineTask.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult Index()
        {
            List<Category> categorylist = new List<Category>();
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MVCMachineTaskconnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SPGETCATEGORY", sqlConnection);
            sqlConnection.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            while (sqlDataReader.Read())
            {
                Category c = new Category();
                c.CategoryId = Convert.ToInt32(sqlDataReader["CategoryId"]);
                c.CategoryName = Convert.ToString(sqlDataReader["CategoryName"]);
                categorylist.Add(c);
            }
            sqlDataReader.Close();
            sqlConnection.Close();
            return View(categorylist);
        }
        [HttpGet]
        [Route("Details/{ID}")]
        public ActionResult CategoryById(int ID)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MVCMachineTaskconnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SPCATEGORYBYID", sqlConnection);
            cmd.Parameters.AddWithValue("@CategoryId", ID);
            sqlConnection.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            Category c = new Category();
            while (sqlDataReader.Read())
            {
                c.CategoryId = Convert.ToInt32(sqlDataReader["CategoryId"]);
                c.CategoryName = Convert.ToString(sqlDataReader["CategoryName"]);
            }
            sqlDataReader.Close();
            sqlConnection.Close();
            return View(c);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category category)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MVCMachineTaskconnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SPADDCATEGORY", sqlConnection);
            cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName);
            cmd.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int ID)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MVCMachineTaskconnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SPCATEGORYBYID", sqlConnection);
            cmd.Parameters.AddWithValue("@CategoryId", ID);
            sqlConnection.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            Category c = new Category();
            while (sqlDataReader.Read())
            {
                c.CategoryId = Convert.ToInt32(sqlDataReader["CategoryId"]);
                c.CategoryName = Convert.ToString(sqlDataReader["CategoryName"]);
            }
            sqlDataReader.Close();
            sqlConnection.Close();
            return View(c);
        }
        [HttpPost]
        public ActionResult Edit(Category category)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MVCMachineTaskconnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SPEDITCATEGORY", sqlConnection);
            cmd.Parameters.AddWithValue("@CategoryId", category.CategoryId);
            cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName);
            cmd.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int ID)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MVCMachineTaskconnection"].ConnectionString;
                    SqlConnection sqlConnection = new SqlConnection(connectionString);
                    SqlCommand cmd = new SqlCommand("SPCATEGORYBYID", sqlConnection);
                    cmd.Parameters.AddWithValue("@CategoryId", ID);
                    sqlConnection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    Category c = new Category();
                    while (sqlDataReader.Read())
                    {
                        c.CategoryId = Convert.ToInt32(sqlDataReader["CategoryId"]);
                        c.CategoryName = Convert.ToString(sqlDataReader["CategoryName"]);
                    }
                    sqlDataReader.Close();
                    sqlConnection.Close();
                    return View(c);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult Delete(int? id)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MVCMachineTaskconnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SPDELETECATEGORY", sqlConnection);
            cmd.Parameters.AddWithValue("@CategoryId", id);
            sqlConnection.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
            return RedirectToAction("Index");
        }
    }
}
