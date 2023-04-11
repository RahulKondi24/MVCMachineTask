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
    public class ProductandCategoryController : Controller
    {
        public ActionResult Index(string Search, int? page)
        {
            List<ProductandCategory> productandcategorylist = new List<ProductandCategory>();
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MVCMachineTaskconnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SPJOINPRODUCTANDCATEGORY", sqlConnection);
            sqlConnection.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            while (sqlDataReader.Read())
            {
                ProductandCategory product = new ProductandCategory();
                product.ProductId = Convert.ToInt32(sqlDataReader["ProductId"]);
                product.ProductName = Convert.ToString(sqlDataReader["ProductName"]);
                product.CategoryId = Convert.ToInt32(sqlDataReader["CategoryId"]);
                product.CategoryName = Convert.ToString(sqlDataReader["CategoryName"]);
                productandcategorylist.Add(product);
            }
            sqlDataReader.Close();
            sqlConnection.Close();
            var data = productandcategorylist.ToPagedList(page ?? 1, 10);
            return View(data);
        }
    }
}