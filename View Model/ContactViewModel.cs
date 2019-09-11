using ContactDetails.Enumeration;
using ContactDetails.Model;
using Microsoft.Win32;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ContactDetails.View_Model
{
    public class ContactViewModel : BindableBase
    {
        private const string filePath = "D:\\Contact.json";

        private string _FirstName;
        public string FirstName
        {
            get
            {
                return _FirstName;
            }
            set
            {
                SetProperty(ref _FirstName, value);
                RaisePropertyChanged("FullName");
            }
        }
        private string _MiddleName;
        public string MiddleName
        {
            get
            {

                return _MiddleName;
            }

            set
            {
                SetProperty(ref _MiddleName, value);
                RaisePropertyChanged("FullName");
            }
        }
        private string _LastName;
        public string LastName
        {
            get
            {

                return _LastName;
            }

            set
            {
                SetProperty(ref _LastName, value);
                RaisePropertyChanged("FullName");
            }
        }
        private string _FullName;
        public string FullName
        {
            get
            {

                return _FirstName + " " + _MiddleName + " " + _LastName;
            }

            set
            {
                SetProperty(ref _FullName, value);

            }
        }
        private Gender _Gender;
        public Gender Gender
        {
            get
            {

                return _Gender;
            }

            set
            {
                SetProperty(ref _Gender, value);
            }
        }
        private string _Address;
        public string Address
        {
            get
            {

                return _Address;
            }

            set
            {
                SetProperty(ref _Address, value);
            }
        }
        private string _Age;
        public string Age
        {
            get
            {

                return _Age;
            }

            set
            {
                SetProperty(ref _Age, value);
            }
        }
        private string _Marital;
        public string Marital
        {
            get
            {

                return _Marital;
            }

            set
            {
                SetProperty(ref _Marital, value);
            }
        }
        private string _Code;
        public string Code
        {
            get
            {

                return _Code;
            }

            set
            {
                SetProperty(ref _Code, value);
            }
        }
        private int _Number;
        public int Number
        {
            get
            {

                return _Number;
            }

            set
            {
                SetProperty(ref _Number, value);
            }
        }
        private DateTime _DOB = DateTime.Now;
        public DateTime DOB
        {
            get
            {

                return _DOB;
            }

            set
            {
                SetProperty(ref _DOB, value);
            }
        }
        private string _Picture;
        public string Picture
        {
            get
            {
                return _Picture;
            }
            set
            {
                SetProperty(ref _Picture, value);
            }
        }

        private ContactModel contactModel = new ContactModel();
        public ContactModel ContactModel

        {
            get { return contactModel; }
            set { SetProperty(ref contactModel, value); }
        }

        private ObservableCollection<ContactModel> contacts = new ObservableCollection<ContactModel>();
        public ObservableCollection<ContactModel> Contacts
        {
            get { return contacts; }
            set { contacts = value; }
        }

        public ICommand SubmitButtonClickCommand { get; set; }
        public ICommand NameValidationCommand { get; set; }
        public ICommand AgeCommand { get; set; }
        public ICommand ResetButtonClickCommnad { get; set; }
        public ICommand BrowseButtonClickCommand { get; set; }
        public ContactViewModel()
        {
            ContactModel = new ContactModel();
            Contacts = ContactListBinding();
            SubmitButtonClickCommand = new DelegateCommand(ButtonSubmitClicked);
            ResetButtonClickCommnad = new DelegateCommand(ButtonResetClicked);
            BrowseButtonClickCommand = new DelegateCommand(ButtonBrowseClicked);
            AgeCommand = new DelegateCommand(AgeCalculation);
        }

        private ObservableCollection<ContactModel> ContactListBinding()
        {
            ObservableCollection<ContactModel> lstContacts = new ObservableCollection<ContactModel>();
            if (File.Exists(filePath))
            {
                var contactDetails = File.ReadAllText(filePath);
                lstContacts = JsonConvert.DeserializeObject<ObservableCollection<ContactModel>>(contactDetails);
            }
            return lstContacts;
        }

        private void AgeCalculation()
        {
            int age = 0;
            age = DateTime.Now.Year - ContactModel.DOB.Year;
            Age = Convert.ToString(age);

        }

        //private void NameValidation(string name)
        //{
        //    Regex regex = new Regex("[^a-z|A-Z]+");
        //    if (!string.IsNullOrEmpty(ContactModel.FirstName) || 
        //        !string.IsNullOrEmpty(ContactModel.MiddleName) || 
        //        !string.IsNullOrEmpty(ContactModel.MiddleName) || 
        //        !regex.IsMatch(ContactModel.FirstName) ||
        //        !regex.IsMatch(ContactModel.MiddleName) ||
        //        !regex.IsMatch(ContactModel.LastName)

        //        )
        //    {
        //        e.Handled = regex.IsMatch(e.Text);
        //    }
        //}
        private void ButtonBrowseClicked()
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                Picture = op.FileName;
            }
        }

        //private bool ValidationContact()
        //{
        //    bool contact = false;
        //    if (ContactModel.Number == 0)
        //    {

        //        contact = false;
        //        return contact;
        //    }
        //    else if (Regex.Match(Number, (@"^(\d{1,2})?\-?\d{10}$")).Success && Number.Length == 10)
        //    {
        //        contact = true;
        //    }
        //    else
        //    {
        //        contact = false;
        //    }
        //    return contact;
        //}
        private void NameValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-z|A-Z]+");
            if (!string.IsNullOrEmpty(e.Text) || !regex.IsMatch(e.Text))
            {
                e.Handled = regex.IsMatch(e.Text);
            }
        }

        private void ButtonSubmitClicked()
        {
            ContactModel.FirstName = FirstName;
            ContactModel.MiddleName = MiddleName;
            ContactModel.LastName = LastName;
            ContactModel.Gender = Gender.ToString();
            ContactModel.Address = Address;
            ContactModel.DOB = DOB;
            ContactModel.Marital = Marital;
            ContactModel.Code = Code;
            ContactModel.Number = Number;
            ContactModel.Picture = Picture;

            if (Contacts != null)
            {
                Contacts.Add(ContactModel);
            }

            string output = Newtonsoft.Json.JsonConvert.SerializeObject(Contacts);
            File.WriteAllText(filePath, output);

            MessageBox.Show("Data is registered successfully !!");
        }

        private void ButtonResetClicked()
        {
            FirstName = MiddleName = LastName = Address = Marital = string.Empty;
            // contactModel.Number = string.Empty;
            DOB = DateTime.Now;
            // contactModel.Gender = 
        }
    }
}
