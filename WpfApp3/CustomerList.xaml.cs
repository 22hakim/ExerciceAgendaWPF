using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
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
    }
}
