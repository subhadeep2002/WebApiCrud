using Microsoft.Data.SqlClient;
using System.Data;

namespace WebApiCrud.Models
{
    public class DAL
    {
        public Response GetAllEmployees(SqlConnection connection)
        {
            Response response = new Response();
            SqlDataAdapter da = new SqlDataAdapter("select*from tblsample",
           connection);
            DataTable dt = new DataTable();
            List<Employee> lstEmployees = new List<Employee>();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Employee employee = new Employee();
                    employee.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                    employee.Name = Convert.ToString(dt.Rows[i]["Name"]);
                    employee.Email = Convert.ToString(dt.Rows[i]["Email"]);
                    employee.IsActive = Convert.ToInt32(dt.Rows[i]["IsActive"]);
                    lstEmployees.Add(employee);
                }
            }
            if (lstEmployees.Count > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Data found";
                response.listEmployee = lstEmployees;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No Data found";
                response.listEmployee = null;
            }
            return response;
        }
        public Response GetEmployeeById(SqlConnection connection, int id)
        {
            Response response = new Response();
            SqlDataAdapter da = new SqlDataAdapter("select*from tblsample where id = '" + id + "' and IsActive = 1", connection);
           
            DataTable dt = new DataTable();
            Employee Employees = new Employee();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Employee employee = new Employee();
                employee.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                employee.Name = Convert.ToString(dt.Rows[0]["Name"]);
                employee.Email = Convert.ToString(dt.Rows[0]["Email"]);
                employee.IsActive = Convert.ToInt32(dt.Rows[0]["IsActive"]);
                response.StatusCode = 200;
                response.StatusMessage = "Data found";
                response.Employee = employee;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No Data found";
                response.listEmployee = null;
            }
            return response;
        }
        public Response AddEmployee(SqlConnection connection, Employee employee)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("insert into tblsample values('" +employee.Name + "','" + employee.Email + "','" + employee.IsActive +"',GETDATE())", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();//ExecuteNonQuery open action method -- first open then close con
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Employee Record inserted";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No Record inserted";
            }
            return response;
        }
        public Response UpdateEmployee(SqlConnection connection, Employee employee)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("update tblsample set Name = '"+employee.Name+ "', Email = '" + employee.Email + "' where id = '"+employee.Id+"'",connection);
           
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Employee Record Updated";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No Record Updated";
            }
            return response;
        }
        public Response DeleteEmployee(SqlConnection connection, int id)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("delete from tblsample where id = '"+id+"'",connection);
           
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Employee Record Deleted";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No Record Deleted";
            }
            return response;
        }

    }
}
