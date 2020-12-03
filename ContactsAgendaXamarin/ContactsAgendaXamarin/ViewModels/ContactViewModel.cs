using ContactsAgendaXamarin.Models;
using ContactsAgendaXamarin.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace ContactsAgendaXamarin.ViewModels
{
    class ContactViewModel: BaseViewModel
    {
        private Contact selectedContact;
        public ICommand AddCommand { get; }
        public ICommand MoreCommand { get; }
        public ICommand DeleteCommand { get; }

        public ObservableCollection<Contact> Contacts { get; set; }
        public Contact SelectedContact
        {
            get { return selectedContact; }
            set
            {
                if (value != null)
                {
                    selectedContact = value;
                }
            }
        }

        public ContactViewModel()
        {
            AddCommand = new Command(OnAddContact);
            MoreCommand = new Command<Contact>(OnMore);
            DeleteCommand = new Command<Contact>(OnDelete);

            Contacts = new ObservableCollection<Contact>();
        }

        private async void OnAddContact()
        {
           await App.Current.MainPage.Navigation.PushAsync(new AddContactPage() { BindingContext = new AddContactViewModel(Contacts, null) });
        }

        private async void OnMore(Contact contact)
        {
            string option = await App.Current.MainPage.DisplayActionSheet(null, "Cancel", null, $"Call +{contact.PhoneNumber}", "Edit" );

            if (string.IsNullOrEmpty(option) || option == "Cancel")
                return;

            if (option == "Edit")
            {
                OnEdit(contact);
            }
            else
            {
                OnCall(contact.PhoneNumber);
            }
        }

        private void OnDelete(Contact contact)
        {
            Contacts.Remove(contact);
        }

        private void OnCall (string phoneNumber)
        {
            try
            {
                PhoneDialer.Open(phoneNumber);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async void OnEdit(Contact contact)
        {
           await App.Current.MainPage.Navigation.PushAsync(new AddContactPage() { BindingContext = new AddContactViewModel(Contacts, contact) });
        }
    }

}
