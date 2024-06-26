﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoStop.Models
{
    public class User
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public byte[]? Img { get; set; }
        public float? Rating { get; set; }
    }
}
