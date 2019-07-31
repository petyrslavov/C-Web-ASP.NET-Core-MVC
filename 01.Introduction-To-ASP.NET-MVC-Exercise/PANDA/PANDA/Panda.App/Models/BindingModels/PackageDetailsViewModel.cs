﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Panda.App.Models.BindingModels
{
    public class PackageDetailsViewModel
    {
        public string ShippingAddress { get; set; }

        public string Status { get; set; }

        public string EstimatedDeliveryDate { get; set; }

        public double Weight { get; set; }

        public string Recipient { get; set; }

        public string Description { get; set; }
    }

}
