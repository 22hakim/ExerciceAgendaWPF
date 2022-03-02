using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfApp3.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Appointments = new HashSet<Appointment>();
        }

        [Key]
        [Column("idCustomer")]
        public int IdCustomer { get; set; }


        [Column("lastname")]
        public string? Lastname { get; set; } = null;


        [Column("firstname")]
        public string? Firstname { get; set; } = null;


        [Column("mail")]
        public string? Mail { get; set; } = null;


        [Column("phoneNumber")]
        public string? PhoneNumber { get; set; } = null;

        [Column("budget")]
        public int? Budget { get; set; } 

        [InverseProperty(nameof(Appointment.IdCustomerNavigation))]
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
