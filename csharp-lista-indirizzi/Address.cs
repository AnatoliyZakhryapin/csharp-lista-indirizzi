using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_lista_indirizzi
{
    internal class Address
    {
        private string street;
        private string city;
        private string province;
        private int zipCode;

        // -----------
        // Costruttore
        // -----------

        public Address(string street, string city, string province, int zipCode)
        {
            this.street = street;
            this.city = city;
            this.province = province;
            this.zipCode = zipCode;
        }

        // ---------
        // Get e Set
        // ----------

        public string Street
        {
            get { return this.street; }
            set { this.street = value; }
        }

        public string City
        {
            get { return this.city; }
            set { this.city = value; }
        }

        public string Province
        {
            get { return this.province; }
            set { this.province = value; }
        }

        public int ZipCode
        {
            get { return this.zipCode; }
            set { this.zipCode = value; }
        }

        // ---------
        // Metodi
        // ----------

        public void StampaInfo()
        {
            Console.WriteLine();
            Console.WriteLine("Stampa l'informazione dell'indirizzo': ");
            Console.WriteLine($"Street: {street} ");
            Console.WriteLine($"City: {city}");
            Console.WriteLine($"Province: {province}");
            Console.WriteLine($"ZipCode: {zipCode}");
        }
    }
}
