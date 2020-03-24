﻿using System;

namespace AlvTimeWebApi.Dto
{
    public class EconomyDto
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public int ProjectId { get; set; }
        public decimal Value { get; set; }
        public int UserId { get; set; }
        public string Date { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ProjectName { get; set; }
        public string CustomerName { get; set; }
        public int CustomerId { get; set; }
        public decimal HourRate { get; set; }
    }
}
