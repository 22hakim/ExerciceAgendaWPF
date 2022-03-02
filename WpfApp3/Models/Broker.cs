using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WpfApp3.Data;

namespace WpfApp3.Models
{
    public partial class Broker
    {
        public Broker()
        {
            Appointments = new HashSet<Appointment>();
        }

        [Key]
        [Column("idBroker")]
        public int IdBroker { get; set; }


        [Column("lastname")]
        public string Lastname { get; set; } = null!;


        [Column("firstname")]
        public string Firstname { get; set; } = null!;


        [Column("mail")]
        public string Mail { get; set; } = null!;

        [Column("phoneNumber")]
        public string PhoneNumber { get; set; } = null!;

        [InverseProperty(nameof(Appointment.IdBrokerNavigation))]
        public virtual ICollection<Appointment> Appointments { get; set; }

    }
}
