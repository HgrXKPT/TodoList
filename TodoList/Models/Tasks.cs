﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Models {
    internal class Tasks {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string EstimatedTime { get; set; }
    }
}
