using ContactsAgendaXamarin.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ContactsAgendaXamarin.ViewModels
{
    class AddContactViewModel : BaseViewModel
    {
        private Contact oldContact;
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public ICommand SaveCommand { get; }
        public ObservableCollection<Contact> Contacts { get; set; }

        public AddContactViewModel(ObservableCollection<Contact> contacts, Contact contact = null)
        {
            Contacts = contacts;
            oldContact = contact;
            Name = contact?.Name;
            PhoneNumber = contact?.PhoneNumber;
            SaveCommand = new Command(async () => await OnSaveContact());
        }

        public async Task OnSaveContact()
        {
            if(string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(PhoneNumber))
            {
                await App.Current.MainPage.DisplayAlert("Alerta", "No puede dejar campos vacios", "Ok");
                return;
            }

            if(oldContact != null)
            {
                Contacts.Remove(oldContact);
            }

            Contacts.Add(new Contact(Name, PhoneNumber));
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
