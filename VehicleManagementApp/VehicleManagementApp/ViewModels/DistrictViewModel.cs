﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VehicleManagementApp.Models.Models;

namespace VehicleManagementApp.ViewModels
{
    public class DistrictViewModel
    {
        public int Id { get; set; }

        [Display(Name = "District Name")]
        [Required]
        [Remote("IsNameExist", "District", HttpMethod = "POST", ErrorMessage = "District Already Exist, Try Another")]
        public string Name { get; set; }

        [Display(Name = "Division")]
        public int DivisionId { get; set; }
        public Division Division { get; set; }
        public IEnumerable<Division> Divisions { get; set; }
    }
}