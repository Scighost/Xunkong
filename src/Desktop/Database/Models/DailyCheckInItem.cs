﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xunkong.Desktop.Models
{
    public class DailyCheckInItem
    {

        public int Id { get; set; }

        public int Uid { get; set; }

        public string Date { get; set; }

        public DateTimeOffset Time { get; set; }


    }
}