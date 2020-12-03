using ContactsAgendaXamarin.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ContactsAgendaXamarin.ViewModels
{
    class AddContactViewModel : BaseViewModel
    {
        private Contact contact;
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public ICommand SaveCommand { get; }
        public ObservableCollection<Contact> Contacts { get; set; }
        public AddContactViewModel(ObservableCollection<Contact> contacts, Contact contact)
        {
            Contacts = contacts;
            this.contact = contact;
            Name = contact?.Name;
            PhoneNumber = contact?.PhoneNumber;

            SaveCommand = new Command(OnSaveContact);
        }

        public async void OnSaveContact()
        {
            if(this.contact != null)
            {
                Contacts.Remove(contact);
            }

            Contacts.Add(new Contact(Name, PhoneNumber));
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
