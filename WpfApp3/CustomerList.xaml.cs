using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp3.Data;
using WpfApp3.Models;

namespace WpfApp3
{
    /// <summary>
    /// Logique d'interaction pour CustomerList.xaml
    /// </summary>
    public partial class CustomerList : Page
    {
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

        }

        private void deleteCustomer(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
