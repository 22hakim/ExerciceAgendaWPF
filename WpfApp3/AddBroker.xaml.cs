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
    public partial class addBroker : Page
    {
        public List<bool> errorList = new List<bool>();
        private readonly agendaContext _db;
        Broker broker = new();
        public addBroker()
        {
            _db = new();
            InitializeComponent();
            this.DataContext = broker;
        }

        private void Add_Broker(object sender, RoutedEventArgs e)
        {
            errorForm.Children.Clear();

            CheckString(broker.Firstname, "prénom");
            CheckString(broker.Lastname, "nom");
            CheckTelephoneNumber(broker.PhoneNumber, "numéro de telephone");


            if (errorList.Contains(false))
            {
                errorList = new List<bool>();
                return;
            }

            _db.Brokers.Add(broker);
            _db.SaveChanges();

            this.NavigationService.Refresh();

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


        public int CheckInt(int? value, string name)
        {
            if (value == null)
            {
                addTextBox(name);
                errorList.Add(false);
            }

            return value ?? 0;
        }

        public string CheckTelephoneNumber(string value, string name)
        {
            Regex regex = new Regex(@"^\D?(\d{3})\D?\D?(\d{3})\D?(\d{4})$");
            if (value == null || value == String.Empty)
            {
                addTextBox(name);
                errorList.Add(false);
                return value;
            }


            Match match = regex.Match(value.ToString());
            if (match == Match.Empty)
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
            textBlock.TextWrapping = TextWrapping.Wrap;
            errorForm.Children.Add(textBlock);
        }

    }
}
