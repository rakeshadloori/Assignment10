using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using EmployeeBO;


namespace EmployeeRepo
{
    public class EmpRepo
    {
        // SQL Connection
        static string connection = ConfigurationManager.ConnectionStrings["EmpContext"].ToString();

        #region Multiple Table DataAdapter
        public static EmpViewModel getEmployees()
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            DataSet ds = new DataSet();
            EmpViewModel empVModel = new EmpViewModel();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "spMultitableData";

                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(ds);

                List<EmpBO> table1 = ds.Tables[0].AsEnumerable().Select(row =>
                    new EmpBO
                    {
                        Id = row.Field<Int32>("Id"),
                        Name = row.Field<string>("Name"),
                        Age = row.Field<Int32>("Age"),
                        JoiningDate = row.Field<DateTime>("JoiningDate")
                    }).ToList();

                List<EmpDetailsBO> table2 = ds.Tables[1].AsEnumerable().Select(row =>
                    new EmpDetailsBO
                    {
                        Id = row.Field<Int32>("Id"),
                        EmpId = row.Field<Int32>("EmpID"),
                        Phone = row.Field<Int32>("Phone"),
                        Email = row.Field<string>("Email")
                    }).ToList();


                empVModel.EmpBO = table1;
                empVModel.EmpDetailsBO = table2;

                #region Joinging Lists
                //   List<EmpViewModel> empViewModel =  table1.Join(
                //        table2, t1 => t1.Id, t2 => t2.EmpId,
                //        (t1, t2) => new EmpViewModel
                //        {
                //            EmpBO = new EmpBO
                //            {
                //                Id = t1.Id,
                //                Name = t1.Name,
                //                Age = t1.Age,
                //                JoiningDate = t1.JoiningDate
                //            },

                //            EmpDetailsBO = new EmpDetailsBO
                //            {
                //                Id = t2.Id,
                //                EmpId = t2.EmpId,
                //                Phone = t2.Phone,
                //                Email = t2.Email
                //            }
                //        }
                //     ).ToList();
                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return empVModel;
        }

        #endregion

        #region Data Adapter
        public static DataTable getEmployee()
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            EmpViewModel empVModel = new EmpViewModel();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "spGetEmp";

                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(ds);
                dt = ds.Tables[0];

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return dt;
        }
        #endregion

        

        #region Multiple Table DataReader 
        public static EmpViewModel getEmployeesList()
        {
            SqlConnection conn = new SqlConnection(connection);
            List<EmpBO> list = new List<EmpBO>();
            List<EmpDetailsBO> list1 = new List<EmpDetailsBO>();
            EmpViewModel eVM = new EmpViewModel();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "spMultitableData";

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    EmpBO eb = new EmpBO();

                    eb.Id = Convert.ToInt32(dr["Id"].ToString());
                    eb.Name = dr["Name"].ToString();
                    eb.Age = Convert.ToInt32(dr["Age"]);
                    eb.JoiningDate = Convert.ToDateTime(dr["JoiningDate"]);
                    list.Add(eb);
                }
                dr.NextResult();
                while (dr.Read())
                {
                    EmpDetailsBO eb = new EmpDetailsBO();

                    eb.Id = Convert.ToInt32(dr["Id"].ToString());
                    eb.EmpId = Convert.ToInt32(dr["EmpID"].ToString());
                    eb.Phone = Convert.ToInt32(dr["Phone"]);
                    eb.Email = dr["Email"].ToString();
                    list1.Add(eb);
                }


                eVM.EmpBO = list;
                eVM.EmpDetailsBO = list1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return eVM;
        }
        #endregion

        #region Scalar value
        public static int getScalarValue(EmpBO emp)
        {
            SqlConnection conn = new SqlConnection(connection);
            int id = 0;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "spInsertEmp";
                cmd.Parameters.AddWithValue("@Name", emp.Name);
                cmd.Parameters.AddWithValue("@Age", emp.Age);
                cmd.Parameters.AddWithValue("@JoiningDate", emp.JoiningDate);
                cmd.Parameters.AddWithValue("@Salary", emp.Salary);
                object id1 = cmd.ExecuteScalar();
                id = Convert.ToInt32(id1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return id;
        }
        #endregion

        #region Output param
        //public static int getOutputParam(string name)
        //{
        //    SqlConnection conn = new SqlConnection(connection);
        //    int id = 0;
        //    try
        //    {
        //        conn.Open();
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = conn;
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.CommandText = "spInsertEmp";
        //        cmd.Parameters.AddWithValue("@Name", emp.Name);
        //        id = Convert.ToInt32(id1);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (conn.State == ConnectionState.Open)
        //            conn.Close();
        //    }
        //    return id;
        //}
        #endregion

        #region Pk and FK 
        public static int insertIntoTwoTables(EmpViewModel1 emp)
        {
            SqlConnection conn = new SqlConnection(connection);
            int id = 0;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "spInsertIntoTwoTables";
                cmd.Parameters.AddWithValue("@Name", emp.EmpBO.Name);
                cmd.Parameters.AddWithValue("@Age", emp.EmpBO.Age);
                cmd.Parameters.AddWithValue("@JoiningDate", emp.EmpBO.JoiningDate);
                cmd.Parameters.AddWithValue("@Salary", emp.EmpBO.Salary);
                cmd.Parameters.AddWithValue("@Phone", emp.EmpDetailsBO.Phone);
                cmd.Parameters.AddWithValue("@Email", emp.EmpDetailsBO.Email);
                cmd.Parameters.AddWithValue("@Skills", emp.EmpDetailsBO.Skills);
                object id1 = cmd.ExecuteScalar();
                id = Convert.ToInt32(id1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return id;
        }
        #endregion

        #region Bool
        public static string findEmployee(string empName)
        {
            SqlConnection conn = new SqlConnection(connection);
            string id = "";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "spBool";
                cmd.Parameters.AddWithValue("@Name", empName);
                
                object id1 = cmd.ExecuteScalar();
                id = id1.ToString(); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return id;
        }
        #endregion

        #region Mulitple Insert
        public static void MultipleInsert(List<EmpBO> emp)
        {
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
        
                foreach(var e in emp)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "spInsertEmp";
                    cmd.Parameters.AddWithValue("@Name", e.Name);
                    cmd.Parameters.AddWithValue("@Age", e.Age);
                    cmd.Parameters.AddWithValue("@JoiningDate", e.JoiningDate);
                    cmd.Parameters.AddWithValue("@Salary", e.Salary);
                    cmd.ExecuteNonQuery();
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
        #endregion

        #region delete user if conditions
        public static string checkUser(int id)
        {
            SqlConnection conn = new SqlConnection(connection);
            string output = "";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "spDeleteEmp";
                cmd.Parameters.AddWithValue("@id", id);

                object id1 = cmd.ExecuteScalar();
                output = id1.ToString(); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return output;
        }
        #endregion

        #region Update Salary
        public static string updateSalary()
        {
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "spUpdateSalary";
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return "Updated";
        }
        #endregion

        #region Get User Info
        public static Object UserInfo(string Name)
        {
            SqlConnection conn = new SqlConnection(connection);
            Object r1 = new Object();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "spCheckUserDetails";
                cmd.Parameters.AddWithValue("@Name", Name);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    if(rd.FieldCount > 1)
                    {
                        EmpBO emp = new EmpBO();
                        emp.Name = rd["Name"].ToString();
                        emp.Age = Convert.ToInt32(rd["Age"]);
                        emp.JoiningDate = Convert.ToDateTime(rd["JoiningDate"]);
                        emp.Salary = Convert.ToInt32(rd["Salary"]);
                        r1 = emp;
                    }
                    else
                    {
                        r1 = rd["Message"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return r1;
        }
        #endregion

        #region User Defined Table
        public static string UDT(EmpBO emp)
        {
            SqlConnection conn = new SqlConnection(connection);
            DataTable dt = new DataTable();
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Age", typeof(int));
            dt.Columns.Add("JoiningDate", typeof(DateTime));
            dt.Columns.Add("Salary", typeof(int));
            try
            {
                conn.Open();
                DataRow dr = dt.NewRow();
                
                dr["Name"] = emp.Name;
                dr["Age"] = emp.Age;
                dr["JoiningDate"] = emp.JoiningDate;
                dr["Salary"] = emp.Salary;
                dt.Rows.Add(dr);
                
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserDefinedTable", dt);
                cmd.CommandText = "spCustomTable1";
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return "Created";
        }
        #endregion

        #region DataReader
        public static List<EmpBO> getEmployeeList()
        {
            SqlConnection conn = new SqlConnection(connection);
            List<EmpBO> list = new List<EmpBO>();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "spGetEmp";

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    EmpBO eb = new EmpBO();

                    eb.Id = Convert.ToInt32(dr["Id"].ToString());
                    eb.Name = dr["Name"].ToString();
                    eb.Age = Convert.ToInt32(dr["Age"]);
                    eb.JoiningDate = Convert.ToDateTime(dr["JoiningDate"]);
                    list.Add(eb);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return list;
        }
        #endregion

        #region Get User Info By Id
        public static EmpBO GetUserInfoById(int id)
        {
            SqlConnection conn = new SqlConnection(connection);
            EmpBO r1 = new EmpBO();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "spGetUserById";
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {

                        EmpBO emp = new EmpBO();
                        emp.Name = rd["Name"].ToString();
                        emp.Age = Convert.ToInt32(rd["Age"]);
                        emp.JoiningDate = Convert.ToDateTime(rd["JoiningDate"]);
                        emp.Salary = Convert.ToInt32(rd["Salary"]);
                        r1 = emp;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return r1;
        }
        #endregion

        #region  Insert
        public static void InsertEmp(EmpBO e)
        {
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "spInsertEmp";
                cmd.Parameters.AddWithValue("@Name", e.Name);
                cmd.Parameters.AddWithValue("@Age", e.Age);
                cmd.Parameters.AddWithValue("@JoiningDate", e.JoiningDate);
                cmd.Parameters.AddWithValue("@Salary", e.Salary);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
        #endregion

        #region  Update
        public static void UpdateEmp(EmpBO e)
        {
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "spUpdateEmp";
                cmd.Parameters.AddWithValue("@Id", e.Id);
                cmd.Parameters.AddWithValue("@Name", e.Name);
                cmd.Parameters.AddWithValue("@Age", e.Age);
                cmd.Parameters.AddWithValue("@JoiningDate", e.JoiningDate);
                cmd.Parameters.AddWithValue("@Salary", e.Salary);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
        #endregion

        #region Delete User By Id
        public static string DeleteEmpById(int id)
        {
            SqlConnection conn = new SqlConnection(connection);
            string output = "";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "spDeleteEmpById";
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return output;
        }
        #endregion
    }
}

