﻿using System.ComponentModel.DataAnnotations;

namespace AttendanceFe.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public int? NumberSlot { get; set; }
    }
}
