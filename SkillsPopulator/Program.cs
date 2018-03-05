using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace SkillsPopulator
{
    internal class Program
    {
        private const string connectionString = @"Server=.\SQLEXPRESS;Database=Hooxit;Trusted_Connection=True;Integrated Security=True";

        internal static void Main(string[] args)
        {
            var sqlCreateTable = @"CREATE TABLE Skills (
                                    ID int IDENTITY(1, 1) PRIMARY KEY,
                                    SkillName varchar(255)
                                 );";

            var sqlInsert = "insert into Skills (SkillName) values(@SkillName)";
            var skills = GetAllSkills();
            var sqlInstertStatements = new Dictionary<IList<string>, string>();

            ExecuteQuery(sqlCreateTable);

            var numberOfIterations = (skills.Count % 1000) > 0 ? (skills.Count / 1000) + 1 : skills.Count / 1000;
            var skip = 0;

            while (numberOfIterations > 0)
            {
                var takenSkills = skills.Skip(skip).Take(1000);

                ExecuteInsert(sqlInsert, takenSkills);

                numberOfIterations -= 1;
                skip += 1000;
            }
        }

        internal static IList<string> GetAllSkills()
        {
            var skills = string.Empty;
            var fileStream = new FileStream("allSkills.txt", FileMode.Open);

            using (var reader = new StreamReader(fileStream))
            {
                skills = reader.ReadToEnd();
            }

            return string.IsNullOrEmpty(skills) ? null : skills.Split('\n').ToList();
        }

        internal static void ExecuteInsert(string insertQuery, IEnumerable<string> skills)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.Add("@SkillName", SqlDbType.NVarChar);

                        connection.Open();
                        foreach (var item in skills)
                        {
                            command.Parameters["@SkillName"].Value = item;

                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting records: {ex.Message}");
            }
        }

        internal static void ExecuteQuery(string queryString)
        {
            var adapter = new SqlDataAdapter();
            var cnn = new SqlConnection(connectionString);

            try
            {
                cnn.Open();

                adapter.InsertCommand = new SqlCommand(queryString, cnn);
                adapter.InsertCommand.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating table: {ex.Message}");
            }
        }
    }
}
