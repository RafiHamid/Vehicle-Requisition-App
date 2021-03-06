﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VehicleManagementApp.Models.Models;

namespace VehicleManagementApp.ViewModels
{
    public class RequsitionViewModel
    {
        public RequsitionViewModel()
        {
            CommentViewModels = new HashSet<CommentViewModel>();
            Vehicles = new HashSet<Vehicle>();
        }

        public int Id { get; set; }

        [Required]
        public string Form { get; set; }

        [Required]
        public string To { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Journey Start")]
        [DataType(DataType.DateTime)]
        public DateTime JourneyStart { get; set; }

        [Required]
        [Display(Name = "Journey End")]
        [DataType(DataType.DateTime)]
        public DateTime JouneyEnd { get; set; }

        public string Status { get; set; }
        public string RequsitionNumber { get; set; }

        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public string DesignationName { get; set; }
        public string EmployeeNo { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        [Display(Name = "Employee")]
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public IEnumerable<Employee> Employees { get; set; }


        [Display(Name = "Vehicle Name")]
        public int VehicleId { get; set; }

        public Vehicle Vehicle { get; set; }

        public IEnumerable<Vehicle> Vehicles { get; set; }

        [Display(Name = "Vehicle Name")]
        public int ManagerId { get; set; }
        public Manager Manager { get; set; }
        public IEnumerable<Manager> Managers { get; set; }

        public int EmployeeViewModelId { get; set; }
        public EmployeeViewModel EmployeeViewModel { get; set; }

        public int CommentViewModelId { get; set; }
        public CommentViewModel CommentViewModel { get; set; }
        public virtual IEnumerable<CommentViewModel> CommentViewModels { get; set; }

        public IEnumerable<RequsitionViewModel> RequsitionViewModels { get; set; }
        public ReplayCommentViewModel ReplayCommentViewModel { get; set; }
        public virtual IEnumerable<ReplayCommentViewModel> ReplayCommentViewModels { get; set; }
        public IEnumerable<Designation> Designations { get; set; } 
    }
}