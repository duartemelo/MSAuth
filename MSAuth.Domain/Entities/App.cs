﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSAuth.Domain.Entities
{
    public class App
    {
        public int Id { get; set; }
        public string AppKey { get; set; } 

        public App()
        {
            AppKey = Guid.NewGuid().ToString();
        }
    }
}