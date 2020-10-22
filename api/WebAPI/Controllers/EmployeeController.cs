using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class EmployeeController : ApiController
    {
        public HttpResponseMessage Get()
        {
            
            string queryString = "SELECT EmployeeId,EmployeeName,Department, convert(varchar(10), DateOfJoining, 120) as DateOfJoining, PhotoFileName FROM Employee ORDER BY EmployeeId ";
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
        public string Post(Employee employee)
        {
            try
            {
                string queryString = "INSERT INTO  Employee  VALUES( @EmployeeName ,@Department ,@DateOfJoining ,@PhotoFileName ) ";
                DataTable dataset = new DataTable();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Model1"].ConnectionString))
                {
                    conn.Open();
                    SqlTransaction transction = conn.BeginTransaction();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = new SqlCommand(queryString, conn);
                    adapter.SelectCommand.Parameters.Add("@EmployeeName", SqlDbType.NVarChar).Value = employee.EmployeeName;
                    adapter.SelectCommand.Parameters.Add("@Department", SqlDbType.NVarChar).Value = employee.Department;
                    adapter.SelectCommand.Parameters.Add("@DateOfJoining", SqlDbType.NVarChar).Value = employee.DateOfJoining;
                    adapter.SelectCommand.Parameters.Add("@PhotoFileName", SqlDbType.NVarChar).Value = employee.PhotoFileName;
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

        public string Put(Employee employee)
        {
            try
            {
                string queryString = "UPDATE Employee ";
                queryString += " SET EmployeeName = @EmployeeName ";
                queryString += " , Department = @Department ";
                queryString += " , DateOfJoining = @DateOfJoining ";
                queryString += " , PhotoFileName = @PhotoFileName ";
                queryString += " WHERE EmployeeId = @EmployeeId ";
                DataTable dataset = new DataTable();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Model1"].ConnectionString))
                {
                    conn.Open();
                    SqlTransaction transction = conn.BeginTransaction();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = new SqlCommand(queryString, conn);
                    adapter.SelectCommand.Parameters.Add("@EmployeeId", SqlDbType.NVarChar).Value = employee.EmployeeId;
                    adapter.SelectCommand.Parameters.Add("@EmployeeName", SqlDbType.NVarChar).Value = employee.EmployeeName;
                    adapter.SelectCommand.Parameters.Add("@Department", SqlDbType.NVarChar).Value = employee.Department;
                    adapter.SelectCommand.Parameters.Add("@DateOfJoining", SqlDbType.NVarChar).Value = employee.DateOfJoining;
                    adapter.SelectCommand.Parameters.Add("@PhotoFileName", SqlDbType.NVarChar).Value = employee.PhotoFileName;
                    adapter.SelectCommand.Transaction = transction;
                    adapter.Fill(dataset);
                    transction.Commit();
                    conn.Close();
                    return "UPDATE Successfully!!";

                }
            }
            catch (Exception ex)
            {
                return "Failed to UPDATE !!";
            }

        }
        public string Delete(int id)
        {
            try
            {
                string queryString = "DELETE Employee ";
                queryString += " WHERE EmployeeId = @EmployeeId ";
                DataTable dataset = new DataTable();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Model1"].ConnectionString))
                {
                    conn.Open();
                    SqlTransaction transction = conn.BeginTransaction();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = new SqlCommand(queryString, conn);
                    adapter.SelectCommand.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = id;
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
        [Route("api/Employee/GetAllDepartmentNames")]
        [HttpGet]
        public HttpResponseMessage GetAllDepartmentNames()
        {
            try
            {
                string queryString = "SELECT * FROM Department ";
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
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NoContent, "Error");

            }

        }
        [Route("api/Employee/SaveFile")]
        public string SaveFile()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var postFile = httpRequest.Files[0];
                string filename = postFile.FileName;
                var physicalPath = HttpContext.Current.Server.MapPath("~/Photos/") + filename;

                postFile.SaveAs(physicalPath);
                return filename;
            }
            catch (Exception ex)
            {
                return "anonymous.png";
            }

        }

    }
}
