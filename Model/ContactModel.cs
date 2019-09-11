using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ContactDetails.Model
{
    public class ContactModel : BindableBase
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Marital { get; set; }
        public string Code { get; set; }
        public int Number { get; set; }
        public DateTime DOB { get; set; }
        public string Picture { get; set; }
    }  
}
