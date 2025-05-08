﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopBooking.Application.Features.ServiceTypeFeature.DTOs
{
    public class ServiceTypeInputDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Duration { get; set; } // "HH:mm:ss"
    }
}
