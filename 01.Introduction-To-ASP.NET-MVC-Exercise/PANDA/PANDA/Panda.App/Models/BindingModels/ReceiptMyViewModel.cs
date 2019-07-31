using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Panda.App.Models.BindingModels
{
    public class ReceiptMyViewModel
    {
        public string Id { get; set; }

        public string IssuedOn { get; set; }

        public decimal Fee { get; set; }

        public string Recipient { get; set; }
    }
}
