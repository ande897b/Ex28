using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Ex28
{
    class Program
    {
        private static string connectionString = "Server=EALSQL1.eal.local; Database= C_DB01_2018; User Id= C_STUDENT01; Password= C_OPENDB01";
  

        static void Main(string[] args)
        {
            Program prog = new Ex28.Program();
            Console.WriteLine("tryk 1 for InsertPet");
            Console.WriteLine("tryk 2 for vis alle dyr");
            string menuValg = Console.ReadLine();
            if (menuValg == "1")
            {
                prog.Run();
            }
            else if (menuValg =="2")
            {
                prog.ShowPets();
            }
            else
            {
                Console.WriteLine("fejl i input, Prøv igen");
            }
           
        }
       
        
        private void Run()
        {
            Console.WriteLine("Input name");
            string petName = Console.ReadLine();
            Console.WriteLine("Input Type");
            string petType = Console.ReadLine();
            Console.WriteLine("Input Breed");
            string petBreed = Console.ReadLine();
            Console.WriteLine("input DOB dd/mm/yy");
            string petDOB = Console.ReadLine();
            Console.WriteLine("input Weight");
            string petWeight = Console.ReadLine();
            Console.WriteLine("Input ownerID 1-4");
            string ownerID = Console.ReadLine();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("InsertPet", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
           

                    cmd1.Parameters.Add(new SqlParameter("@PetName", petName));
                    cmd1.Parameters.Add(new SqlParameter("@PetType", petType));
                    cmd1.Parameters.Add(new SqlParameter("@PetBreed", petBreed));
                    cmd1.Parameters.Add(new SqlParameter("@PetDOB", petDOB));
                    cmd1.Parameters.Add(new SqlParameter("@PetWeight", petWeight));
                    cmd1.Parameters.Add(new SqlParameter("@OwnerID", ownerID));

                    cmd1.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("ups" + e.Message);
                }
            }
        }
        private void ShowPets()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("GetPets", con);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    

                    SqlDataReader reader = cmd2.ExecuteReader();

                    if (reader.HasRows)
                    {


                        while (reader.Read())
                        {
                            string petName = reader["petName"].ToString();
                            string petType = reader["petType"].ToString();
                            string petBreed = reader["petBreed"].ToString();
                            string petDOB = reader["petDOB"].ToString();
                            string petWeight = reader["petWeight"].ToString();
                            string ownerID = reader["ownerID"].ToString();

                            Console.WriteLine(petName + " " + petType + " " + petBreed + " " + petDOB + " " + petWeight + " " + ownerID);
                     
                        }
                    }               
                }
                catch (SqlException e)
                {
                    Console.WriteLine("ups" + e.Message);
                }
            }


        }
    }
}
