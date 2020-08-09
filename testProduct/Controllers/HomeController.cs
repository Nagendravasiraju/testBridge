using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;

namespace testProduct.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DataSet  response = getAllProducts();
            ViewBag.prodList = response.Tables[0]; 
            return View();
        }

        public DataSet getAllProducts()
        {
            string _connString;
            _connString = ConfigurationManager.AppSettings["connstring"];
            SqlConnection scon = new SqlConnection(_connString);

            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("getProducts");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = scon;
            cmd.Connection.Open();
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(ds);
            cmd.Connection.Close();
            return ds;
        }

        [HttpPost]
        public ActionResult saveItem(string name, string desc, string price, HttpPostedFileBase imgfile)
        {
            try
            { 
            string _connString;
            _connString = ConfigurationManager.AppSettings["connstring"];

                using (SqlConnection scon = new SqlConnection(_connString))
                {
                    //Extract Image File Name.
                    string fileName = System.IO.Path.GetFileName(imgfile.FileName);
                    //Set the Image File Path.
                    if ((!Directory.Exists(Server.MapPath("~/uploadimages/"))))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/uploadimages/"));
                    }
                    string filePath = "~/uploadimages/" + fileName;
                    string imgPath = Server.MapPath(filePath);
                    //Save the Image File in Folder.
                    imgfile.SaveAs(imgPath);


                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand("insertProduct");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@productName", SqlDbType.VarChar).Value = name;
                    cmd.Parameters.Add("@ProductDesc", SqlDbType.VarChar).Value = desc;
                    cmd.Parameters.Add("@productPrice", SqlDbType.Decimal).Value = price;
                    cmd.Parameters.Add("@productImage", SqlDbType.VarChar).Value = "uploadimages/" + fileName;
                    cmd.Connection = scon;
                    cmd.Connection.Open();
                    SqlDataAdapter DA = new SqlDataAdapter(cmd);
                    DA.Fill(ds);
                    cmd.Connection.Close();
                    //return ds;
                    //Refresh the List 
                    DataSet response = getAllProducts();
                    ViewBag.prodList = response.Tables[0];
                    return PartialView("prodList");
                }
        }
            catch
            {
                return PartialView("");
            }
        }
    
        public ActionResult removeItem(string id)
        {
            try
            {
                string _connString;
                _connString = ConfigurationManager.AppSettings["connstring"];
                SqlConnection scon = new SqlConnection(_connString);

                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("removeProduct");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@productId", SqlDbType.VarChar).Value = id;

                cmd.Connection = scon;
                cmd.Connection.Open();
                SqlDataAdapter DA = new SqlDataAdapter(cmd);
                DA.Fill(ds);
                cmd.Connection.Close();
                //return ds;
                //Refresh the List 
                DataSet response = getAllProducts();
                ViewBag.prodList = response.Tables[0];
                return PartialView("prodList");
            }
            catch
            {
               return  PartialView("");
            }

        }

        public ActionResult display(string id)
        {
            string _connString;
            _connString = ConfigurationManager.AppSettings["connstring"];
            SqlConnection scon = new SqlConnection(_connString);

            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("getProducts");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@productId", SqlDbType.VarChar).Value = id;

            cmd.Connection = scon;
            cmd.Connection.Open();
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(ds);
            cmd.Connection.Close();
            if (ds.Tables[0].Rows.Count >= 1)
            {
                ViewBag.prodDisplay = ds.Tables[0];
            } 
            return View();
        }
    }
}

