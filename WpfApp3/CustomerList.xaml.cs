using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WpfApp3.Data;
using WpfApp3.Models;

namespace WpfApp3
{
    /// <summary>
    /// Logique d'interaction pour CustomerList.xaml
    /// </summary>
    public partial class CustomerList : Page
    {
        public List<bool> errorList = new List<bool>();

        private readonly agendaContext _db;
        public CustomerList()
        {

            _db = new agendaContext();
            InitializeComponent();
            this.DataContext = new Customer();
            List<Customer> customerList = _db.Customers.ToList();
            customerDataGrid.ItemsSource = customerList;
        }

        private void displayCustomer(object sender, RoutedEventArgs e)
        {
            Customer customer_row = (Customer)customerDataGrid.SelectedItem;
            lastnameInput.Text = customer_row.Lastname;
            firstnameInput.Text = customer_row.Firstname;
            mailInput.Text = customer_row.Mail;
            phoneInput.Text = customer_row.PhoneNumber;
            budgetInput.Text = customer_row.Budget.ToString();
            id.Content = customer_row.IdCustomer;
        }

        private void updateCustomer(object sender, System.Windows.RoutedEventArgs e)
        {
            Customer newCustomer = _db.Customers.Find(Convert.ToInt32(id.Content));
            newCustomer.Firstname = CheckString(firstnameInput.Text, "prénom");
            newCustomer.Lastname = CheckString(lastnameInput.Text, "nom");
            newCustomer.Budget = CheckBudget(budgetInput.Text, "budget");
            newCustomer.PhoneNumber = CheckTelephoneNumber(phoneInput.Text, "numéro de telephone");
            newCustomer.Mail = mailInput.Text;


            if (errorList.Contains(false))
            {
                errorList = new List<bool>();
                return;
            }

            _db.Customers.Update(newCustomer);
            _db.SaveChanges();

            this.NavigationService.Refresh();

        }

        private void deleteCustomer(object sender, System.Windows.RoutedEventArgs e)
        {

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
            else
            {
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
            errorForm.Children.Add(textBlock);
        }

    }
}