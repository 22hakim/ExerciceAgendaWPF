using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApp3.Data;
using WpfApp3.Models;


namespace WpfApp3
{
    /// <summary>
    /// Logique d'interaction pour addCustomer.xaml
    /// </summary>
    public partial class addCustomer : Page
    {
        public List<bool> errorList = new List<bool>();
        private readonly agendaContext _db;
        public addCustomer()
        {
            _db = new agendaContext();
            InitializeComponent();
        }

        private void Add_Customer(object sender, RoutedEventArgs e)
        {
            errorForm.Children.Clear();
            Customer customer = new Customer();

            customer.Firstname = CheckString(firstname.Text, "prénom");
            customer.Lastname = CheckString(lastname.Text, "nom");
            customer.Budget = CheckBudget(budget.Text, "budget");
            customer.PhoneNumber = CheckTelephoneNumber(phonenumber.Text, "numéro de telephone");
            customer.Mail = mail.Text;
            //CheckMail(phonenumber.Text, "numéro de telephone");

            if(errorList.Contains(false))
            {
                errorList = new List<bool>();
                return;
            }

            _db.Customers.Add(customer);
            _db.SaveChanges();

            this.NavigationService.Refresh();

        }

        public string CheckString(string value, string name)
        {
            //Regex regex = new Regex(@"^\D?(\d{3})\D?\D?(\d{3})\D?(\d{4})$");
            //Match match = regex.Match(value.ToString());
            if (value == null || value == String.Empty)
            {
                addTextBox(name);
                errorList.Add(false);
            }

            return value;

        }

        public string CheckTelephoneNumber(string value, string name)
        {
            Regex regex = new Regex(@"^\D?(\d{3})\D?\D?(\d{3})\D?(\d{4})$");
            Match match = regex.Match(value.ToString());
            if (value == String.Empty || match == Match.Empty)
            {
                addTextBox(name);
                errorList.Add(false);
            }
            return value;
        }

        public int CheckBudget(object value, string name)
        {
            int myInt;
            if (int.TryParse((string?)value, out myInt))
            {
                return myInt;
            }
            else { 
                addTextBox(name);
                errorList.Add(false);
                return 0;
            }
        }

        private void addTextBox(string value)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = "Le champs " + value + " n'est pas valide";
            textBlock.Foreground = new SolidColorBrush(Colors.Red);
            textBlock.TextWrapping = TextWrapping.Wrap;
            errorForm.Children.Add(textBlock);
        }

    }
}
