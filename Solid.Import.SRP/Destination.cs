﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.Refactored.SRP.Data
{
    public class Destination
    {
        public string GetDestinationConnectionString()
        {
            return @"Data Source=(local);Integrated Security=true;Initial Catalog=ImportTest;";
        }
    }
}
