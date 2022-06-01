[4:04 PM] Adarsh Kumar
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;namespace CodebaseTest3
{
    class Program
    {
        public static SqlConnection con;
        public static SqlCommand cmd;
        public static SqlDataReader dr; public static SqlConnection getconnection()
        {
            con = new SqlConnection("Server=RMGW58ZLPC0863\\SQLEXPRESS;Database=zensar;User Id=sa; Password= Temp1234");
            con.Open();
            return con;
        }
        public static void insertEmployee()
        {
            try
            {
                con = getconnection();
                Console.WriteLine("Enter the Employee name:- ");
                string ename = Console.ReadLine();
                Console.WriteLine("Enter the Employee Salary:- ");
                float esal = Convert.ToSingle(Console.ReadLine());
                Console.WriteLine("Enter the Employee type (C or P):- ");
                string etype = Console.ReadLine();
                cmd = new SqlCommand("Add_Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd = new SqlCommand("Execute Add_Employee @ename,@esal,@etype");
                cmd.Parameters.Add(new SqlParameter("@ename", ename));
                cmd.Parameters.Add(new SqlParameter("@esal", esal));
                cmd.Parameters.Add(new SqlParameter("@etype", etype)); cmd.Connection = con;
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    Console.WriteLine("New row added succefully :- {0} ", res);
                }
                else
                {
                    Console.WriteLine(" Something went wrong ");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }
        public static void DisplayAllEmployee()
        {
            con = getconnection();
            cmd = new SqlCommand("select * from Code_Employee", con); dr = cmd.ExecuteReader(); while (dr.Read())
            {
                Console.WriteLine("Emp_Id is : " + dr[0]);
                Console.WriteLine("Emp_name is : " + dr[1]);
                Console.WriteLine("Emp_Salary is : " + dr[2]);
                Console.WriteLine("Emp_Type is : " + dr[3]);
            }
        }
        static void Main(string[] args)
        {
            insertEmployee();
            DisplayAllEmployee();
            Console.Read();
            con.Close();
        }
    }
}
------------------------------------------SQL CODE----------------------------------------





create table Code_Employee ( empno int primary key,
empname varchar(50) not null, 
empsal numeric(10,2) check(empsal >= 25000) , 
emptype varchar(1) check(emptype in ('C', 'P')) ) 
select* from Code_Employee

INSERT INTO Code_Employee
VALUES      (1,
             'Adarsh',
             27000,
             'C'
             );



INSERT INTO Code_Employee
VALUES     (2,
             'Akshay',
             27500,
             'C'
             );



INSERT INTO Code_Employee
VALUES      (3,
             'Pramod',
            28600,'C');






SELECT*
FROM  Code_Employee

create or alter proc AddEmployee
(@ename varchar(50),
@esal numeric(10,2),
@etype varchar(1))
as
begin
declare @eid int =(select max(empno) from Code_Employee)
begin
declare @empid int = @eid + 1;
insert into Code_Employee values(@empid, @ename, @esal, @etype)
end
end
execute AddEmployee @ename='kumar', @esal = 29670, @etype = 'P'
