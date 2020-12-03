using System;
using System.Collections.Generic;
using System.Text;

namespace ContactsAgendaXamarin.Models
{
    class Contact
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public Contact(string name, string phoneNumber)
        {
            Name = name;
            PhoneNumber = phoneNumber;
        }

        public Contact()
        {
        }
    }
}
