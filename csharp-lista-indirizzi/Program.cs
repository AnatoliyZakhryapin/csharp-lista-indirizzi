using System.IO;

namespace csharp_lista_indirizzi
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //User newUser = new User(new Person("Anatoliy", "Zakhryapin"), new Address("Via Torino 29", "Milano", "Mi", 20123));

            //user.StampaInfo();

            string path = "D:\\Studi\\CSharp\\CorsoCSharp\\csharp-lista-indirizzi\\csharp-lista-indirizzi\\addresses.csv";

            List<User> users = ReadFromFileAndCreateListOfUsers(path);

            int i = 1;
            foreach (User user in  users)
            {
                Console.WriteLine();
                Console.WriteLine($"STAMPA UTENE : {i}");
                user.StampaInfo();
                i++;
            }
        }

        public static List<User> ReadFromFileAndCreateListOfUsers(string path)
        {
            List<User> users = new List<User>();

            try
            {
                using (StreamReader fileStream = File.OpenText(path))
                {
                    int i = 0;
                    while (!fileStream.EndOfStream)
                    {
                        string line = fileStream.ReadLine();

                        i++;
                        if (i <= 1)
                            continue;

                        try
                        {
                            var lineData = line.Split(',');
                            string name = lineData[0];
                            string surname = lineData[1];
                            string street = lineData[2];
                            string city = lineData[3];
                            string province = lineData[4];
                            int zipCode = int.Parse(lineData[5]);

                            User user = new User(new Person(name, surname), new Address(street, city, province, zipCode));
                            users.Add(user);

                            //user.StampaInfo();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Errore nella riga del file: " + line);
                            Console.WriteLine(e.Message);
                            Console.WriteLine();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Si è verificato un errore durante la lettura del file: " + ex.Message);
            }

            return users;
        }
    }
}
