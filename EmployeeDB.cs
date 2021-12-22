using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Data;
using System.Web;

namespace WebApplication1.Models
{
    public class EmployeeDB
    {
        string str = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

        public List<Employee> ListAll()
        {
            List<Employee> list = new List<Employee>();
            using(SqlConnection con=new SqlConnection(str))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Employee",con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", 4);
                SqlDataReader dr = cmd.ExecuteReader();
                while(dr.Read())
                {
                    list.Add(new Employee
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        EmpName=dr["EmpName"].ToString(),
                        Age=Convert.ToInt32(dr["Age"]),
                        State=dr["State"].ToString(),
                        Country=dr["Country"].ToString()
                    });
                }
                return list;
            }
        }

        public int Insert(Employee emp)
        {
            int i;
            using(SqlConnection con=new SqlConnection(str))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", 1);
                cmd.Parameters.AddWithValue("@EmpName", emp.EmpName);
                cmd.Parameters.AddWithValue("@Age", emp.Age);
                cmd.Parameters.AddWithValue("@State", emp.State);
                cmd.Parameters.AddWithValue("@Country", emp.Country);
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }

        public int Update(Employee emp) 
        {
            int i;
            using (SqlConnection con = new SqlConnection(str))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", 2);
                cmd.Parameters.AddWithValue("@Id", emp.Id);
                cmd.Parameters.AddWithValue("@EmpName", emp.EmpName);
                cmd.Parameters.AddWithValue("@Age", emp.Age);
                cmd.Parameters.AddWithValue("@State", emp.State);
                cmd.Parameters.AddWithValue("@Country", emp.Country);
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }

        public int Delete(int ID)
        {
            int i;
            using (SqlConnection con = new SqlConnection(str))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", 3);
                cmd.Parameters.AddWithValue("@Id", ID);
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }

        public List<Employee> ListEmployeeName()
        {
            List<Employee> list = new List<Employee>();
            using (SqlConnection con = new SqlConnection(str))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", 5);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(new Employee
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        EmpName = dr["EmpName"].ToString()                        
                    });
                }
                return list;
            }
        }

    }
}