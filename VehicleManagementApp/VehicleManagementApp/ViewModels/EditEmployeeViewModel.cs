﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VehicleManagementApp.Models.Models;

namespace VehicleManagementApp.ViewModels
{
    public class EditEmployeeViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ContactNo { get; set; }
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Address1 { get; set; }
        public string Address2 { get; set; }

        
        public string Status { get; set; }
        //public bool IsDriver { get; set; }
        
        [Display(Name = "Department")]
        [Required]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public IEnumerable<Department> Departments { get; set; }

        [Display(Name = "Designation")]
        [Required]
        public int DesignationId { get; set; }
        public Designation Designation { get; set; }
        public IEnumerable<Designation> Designations { get; set; }

        [Display(Name = "Division")]
        public int DivisionId { get; set; }
        public Division Division { get; set; }
        public IEnumerable<Division> Divisions { get; set; }

        [Display(Name = "District")]
        public int DistrictId { get; set; }
        public District District { get; set; }
        public IEnumerable<District> Districts { get; set; }

        [Display(Name = "Thana/Upzilla")]
        public int ThanaId { get; set; }
        public Thana Thana { get; set; }
        public IEnumerable<Thana> Thanas { get; set; }

        
    }
}