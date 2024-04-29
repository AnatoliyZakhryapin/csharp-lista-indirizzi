using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_lista_indirizzi
{
    internal class User
    {
        private Person person;
        private Address address;

        // -----------
        // Costruttore
        // -----------
        public User(Person person, Address address)
        {
            this.person = person;
            this.address = address;
        }

        // ---------
        // Metodi
        // ----------

        public void StampaInfo()
        {
            this.person.StampaInfo();
            this.address.StampaInfo();
        }
    }
}
