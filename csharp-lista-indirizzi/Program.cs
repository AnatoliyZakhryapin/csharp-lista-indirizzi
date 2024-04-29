namespace csharp_lista_indirizzi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            User user = new User(new Person("Anatoliy", "Zakhryapin"), new Address("Via Torino 29", "Milano", "Mi", 20123));

            user.StampaInfo();
        }
    }
}
