using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bau302TodoList.Models
{
    public class Customer:BaseEntity
    {
        [StringLength(200), Required(ErrorMessage = "Bu alan doldurulmalıdır!"), DisplayName("Ad")]
        public string Name { get; set; }

        [StringLength(200), Required(ErrorMessage = "Bu alan doldurulmalıdır!"), DisplayName("E-Posta")]
        public string Email { get; set; }

        [StringLength(20),DisplayName("Telefon")]
        public string Phone { get; set; }

        [StringLength(20), DisplayName("Fax")]
        public string Fax { get; set; }

        [StringLength(200), Required(ErrorMessage = "Bu alan doldurulmalıdır!"), DisplayName("Web Sitesi")]
        public string WebSite { get; set; }

        [StringLength(4000), Required(ErrorMessage = "Bu alan doldurulmalıdır!"), DisplayName("Adres")]
        public string Addres { get; set; }
        public virtual ICollection<TodoItem> TodoItem { get; set; }

    }
}