using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using WebAPI.Models;
namespace WebAPI.Controllers
{
    public class DepartmentController : ApiController
    {
        [Route("api/Department")]
        [HttpGet]
        public HttpResponseMessage Get()
        {
            string queryString = "  SELECT * FROM dbo.Department ORDER BY DepartmentId";
            DataTable dataset = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Model1"].ConnectionString))
            {
                conn.Open();
                SqlTransaction transction = conn.BeginTransaction();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(queryString, conn);
                adapter.SelectCommand.Transaction = transction;
                adapter.Fill(dataset);
                transction.Commit();
                conn.Close();
                return Request.CreateResponse(HttpStatusCode.OK, dataset);
            } 
         
        }

        [Route("api/Department")]
        [HttpPost]
        public string Post(Department department)
        {
            try
            {
                string queryString = "INSERT INTO  Department  VALUES( @DepartmentName ) ";
                DataTable dataset = new DataTable();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Model1"].ConnectionString))
                {
                    conn.Open();
                    SqlTransaction transction = conn.BeginTransaction();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = new SqlCommand(queryString, conn);
                    adapter.SelectCommand.Parameters.Add("@DepartmentName", SqlDbType.NVarChar).Value = department.DepartmentName;
                    adapter.SelectCommand.Transaction = transction;
                    adapter.Fill(dataset);
                    transction.Commit();
                    conn.Close();
                    return "INSERT Successfully!!";

                }
            }
            catch (Exception ex)
            {
                return "Failed to INSERT !!";
            }
        }

        [Route("api/Department/")]
        [HttpPut]
        public string Put(Department department)
        {
            try
            {
                string queryString = "UPDATE Department ";
                queryString += " SET DepartmentName = @DepartmentName ";
                queryString += " WHERE DepartmentId = @DepartmentId ";
                DataTable dataset = new DataTable();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Model1"].ConnectionString))
                {
                    conn.Open();
                    SqlTransaction transction = conn.BeginTransaction();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = new SqlCommand(queryString, conn);
                    adapter.SelectCommand.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = department.DepartmentId;
                    adapter.SelectCommand.Parameters.Add("@DepartmentName", SqlDbType.NVarChar).Value = department.DepartmentName;
                    adapter.SelectCommand.Transaction = transction;
                    adapter.Fill(dataset);
                    transction.Commit();
                    conn.Close();
                    return "UPDATE Successfully!!";
                   
                }
            }
            catch(Exception ex )
            {
                return "Failed to UPDATE !!";
            }
          
        }
        [HttpDelete]
        public string Delete(int id)
        {
            try
            {
                string queryString = "DELETE Department ";
                queryString += " WHERE DepartmentId = @DepartmentId ";
                DataTable dataset = new DataTable();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Model1"].ConnectionString))
                {
                    conn.Open();
                    SqlTransaction transction = conn.BeginTransaction();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = new SqlCommand(queryString, conn);
                    adapter.SelectCommand.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = id;
                    adapter.SelectCommand.Transaction = transction;
                    adapter.Fill(dataset);
                    transction.Commit();
                    conn.Close();
                    return "Delete Successfully!!";

                }
            }
            catch (Exception ex)
            {
                return "Failed to Delete !!";
            }

        }
    }
}
