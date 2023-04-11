using MVCMachineTask.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;


namespace MVCMachineTask.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(string Search,int? no)
        {
            List<Category> categorylist = new List<Category>();
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MVCMachineTaskconnection"].ConnectionString);
            SqlCommand command = new SqlCommand("SPGETCATEGORY", con);
            con.Open();
            command.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                Category c = new Category();
                c.CategoryId = Convert.ToInt32(dr["CategoryId"]);
                c.CategoryName = Convert.ToString(dr["CategoryName"]);
                categorylist.Add(c);
            }
            dr.Close();
            con.Close();
            ViewBag.Categories = categorylist;



            List<Product> productlist = new List<Product>();
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MVCMachineTaskconnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SPGETPRODUCT", sqlConnection);
            sqlConnection.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            while (sqlDataReader.Read())
            {
                Product p = new Product();
                p.ProductId = Convert.ToInt32(sqlDataReader["ProductId"]);
                p.ProductName = Convert.ToString(sqlDataReader["ProductName"]);
                p.CategoryId = Convert.ToInt32(sqlDataReader["CategoryId"]);
                productlist.Add(p);
            }
            sqlDataReader.Close();
            sqlConnection.Close();
            return View(productlist);
        }
        public ActionResult ProductById(int ID)
        {
            List<Category> categorylist = new List<Category>();
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MVCMachineTaskconnection"].ConnectionString);
            SqlCommand command = new SqlCommand("SPGETCATEGORY", con);
            con.Open();
            command.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                Category c = new Category();
                c.CategoryId = Convert.ToInt32(dr["CategoryId"]);
                c.CategoryName = Convert.ToString(dr["CategoryName"]);
                categorylist.Add(c);
            }
            dr.Close();
            con.Close();
            ViewBag.Categories = categorylist;
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MVCMachineTaskconnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SPPRODUCTBYID", sqlConnection);
            cmd.Parameters.AddWithValue("@ProductId", ID);
            sqlConnection.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            Product p = new Product();
            while (sqlDataReader.Read())
            {
                p.ProductId = Convert.ToInt32(sqlDataReader["ProductId"]);
                p.ProductName = Convert.ToString(sqlDataReader["ProductName"]);
                p.CategoryId = Convert.ToInt32(sqlDataReader["CategoryId"]);
            }
            sqlDataReader.Close();
            sqlConnection.Close();
            return View(p);
        }
        public ActionResult Create()
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
            ViewBag.Categories = new SelectList(categorylist, "CategoryId", "CategoryName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MVCMachineTaskconnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SPADDPRODUCT", sqlConnection);
            cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
            cmd.Parameters.AddWithValue("@CategoryId", product.CategoryId);
            cmd.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int ID)
        {
            List<Category> categorylist = new List<Category>();
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MVCMachineTaskconnection"].ConnectionString);
            SqlCommand command = new SqlCommand("SPGETCATEGORY", con);
            con.Open();
            command.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                Category c = new Category();
                c.CategoryId = Convert.ToInt32(dr["CategoryId"]);
                c.CategoryName = Convert.ToString(dr["CategoryName"]);
                categorylist.Add(c);
            }
            dr.Close();
            con.Close();
            ViewBag.Categories = new SelectList(categorylist, "CategoryId", "CategoryName");

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MVCMachineTaskconnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SPPRODUCTBYID", sqlConnection);
            cmd.Parameters.AddWithValue("@ProductId", ID);
            sqlConnection.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            Product p = new Product();
            while (sqlDataReader.Read())
            {
                p.ProductId = Convert.ToInt32(sqlDataReader["ProductId"]);
                p.ProductName = Convert.ToString(sqlDataReader["ProductName"]);
                p.CategoryId = Convert.ToInt32(sqlDataReader["CategoryId"]);
            }
            sqlDataReader.Close();
            sqlConnection.Close();
            return View(p);
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MVCMachineTaskconnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SPEDITPRODUCT", sqlConnection);
            cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
            cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
            cmd.Parameters.AddWithValue("@CategoryId", product.CategoryId);
            sqlConnection.Open();
            cmd.CommandType = CommandType.StoredProcedure;
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
                    SqlCommand cmd = new SqlCommand("SPPRODUCTBYID", sqlConnection);
                    cmd.Parameters.AddWithValue("@ProductId", ID);
                    sqlConnection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    Product p = new Product();
                    while (sqlDataReader.Read())
                    {
                        p.ProductId = Convert.ToInt32(sqlDataReader["ProductId"]);
                        p.ProductName = Convert.ToString(sqlDataReader["ProductName"]);
                        p.CategoryId = Convert.ToInt32(sqlDataReader["CategoryId"]);
                    }
                    sqlDataReader.Close();
                    sqlConnection.Close();
                    Product product = new Product();
                    product.ProductId = p.ProductId;
                    return View(p);
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
            SqlCommand cmd = new SqlCommand("SPDELETEPRODUCT", sqlConnection);
            cmd.Parameters.AddWithValue("@ProductId", id);
            sqlConnection.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
            return RedirectToAction("Index");
        }
    }
}