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
    /// Logique d'interaction pour ListBroker.xaml
    /// </summary>
    public partial class ListBroker : Page
    {
        public List<bool> errorList = new List<bool>();

        private readonly agendaContext _db;

        public ListBroker()
        {
            _db = new agendaContext();
            InitializeComponent();
            refreshCustomerList();
        }

        private void displayBroker(object sender, RoutedEventArgs e)
        {
            Broker bk = (Broker)brokerDataGrid.SelectedItem;
            lastnameInput.Text = bk.Lastname;
            firstnameInput.Text = bk.Firstname;
            mailInput.Text = bk.Mail;
            phoneInput.Text = bk.PhoneNumber;
            id.Content = bk.IdBroker;
        }

        private void updateBroker(object sender, RoutedEventArgs e)
        {
            Broker bk = _db.Brokers.Find(Convert.ToInt32(id.Content));
            bk.Firstname = CheckString(firstnameInput.Text, "prénom");
            bk.Lastname = CheckString(lastnameInput.Text, "nom");
            bk.PhoneNumber = CheckTelephoneNumber(phoneInput.Text, "numéro de telephone");
            bk.Mail = mailInput.Text;


            if (errorList.Contains(false))
            {
                errorList = new List<bool>();
                return;
            }

            _db.Brokers.Update(bk);
            _db.SaveChanges();

            this.NavigationService.Refresh();
            refreshCustomerList();

        }

        private void deleteBroker(object sender, RoutedEventArgs e)
        {
            Broker bk = _db.Brokers.Find(Convert.ToInt32(id.Content));
            _db.Brokers.Remove(bk);
            _db.SaveChanges();
            refreshCustomerList();

        }

        private void refreshCustomerList()
        {
            List<Broker> bkList = _db.Brokers.ToList();
            brokerDataGrid.ItemsSource = bkList;
            brokerDataGrid.Items.Refresh();
        }
        public string? CheckString(string value, string name)
        {

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

        private void addTextBox(string value)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = "Le champs " + value + " n'est pas valide";
            textBlock.Foreground = new SolidColorBrush(Colors.Red);
            errorForm.Children.Add(textBlock);
        }
    }
}
