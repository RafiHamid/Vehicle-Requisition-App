﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using VehicleManagementApp.BLL.Contracts;
using VehicleManagementApp.Models;
using VehicleManagementApp.Models.Models;
using VehicleManagementApp.ViewModels;

namespace VehicleManagementApp.Controllers
{
    [Authorize(Roles = "Controller,Operator")]
    public class EmployeeController : Controller
    {
        // GET: Employee
        private IEmployeeManager _employeeManager;
        private IDepartmentManager _departmentManager;
        private IDesignationManager _designationManager;
        private IDivisionManager _divisionManager;
        private IDistrictManager _districtManager;
        private IThanaManager _thanaManager;

        public EmployeeController(IEmployeeManager employee, IDepartmentManager department,
            IDesignationManager designation,
            IDivisionManager division, IDistrictManager district, IThanaManager thana)
        {
            this._employeeManager = employee;
            this._departmentManager = department;
            this._designationManager = designation;
            this._divisionManager = division;
            this._districtManager = district;
            this._thanaManager = thana;
        }

        public ActionResult Index()
        {
            var department = _departmentManager.GetAll();
            var designation = _designationManager.GetAll();
            var employee = _employeeManager.Get(c => c.IsDriver == false && c.IsDeleted == false);
            var division = _divisionManager.GetAll();
            var district = _districtManager.GetAll();
            var thana = _thanaManager.GetAll();

            List<EmployeeViewModel> employeeViewList = new List<EmployeeViewModel>();
            foreach (var emploeedata in employee)
            {
                var employeeVM = new EmployeeViewModel();
                employeeVM.Id = emploeedata.Id;
                employeeVM.Name = emploeedata.Name;
                employeeVM.ContactNo = emploeedata.ContactNo;
                employeeVM.Email = emploeedata.Email;
                employeeVM.Address1 = emploeedata.Address1;
                employeeVM.Address2 = emploeedata.Address2;
                //employeeVM.LicenceNo = emploeedata.LicenceNo;
                employeeVM.Department = department.Where(x => x.Id == emploeedata.DepartmentId).FirstOrDefault();
                employeeVM.Designation = designation.Where(x => x.Id == emploeedata.DesignationId).FirstOrDefault();
                employeeVM.Division = division.Where(x => x.Id == emploeedata.DivisionId).FirstOrDefault();
                employeeVM.District = district.Where(x => x.Id == emploeedata.DistrictId).FirstOrDefault();
                employeeVM.Thana = thana.Where(x => x.Id == emploeedata.ThanaId).FirstOrDefault();

                employeeViewList.Add(employeeVM);
            }
            return View(employeeViewList);
        }

        // GET: Employee/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: Employee/Create
        public ActionResult Create()
        {
            var department = _departmentManager.GetAll();
            var designation = _designationManager.GetAll();
            var division = _divisionManager.GetAll();
            var district = _districtManager.GetAll();
            var thana = _thanaManager.GetAll();

            EmployeeViewModel employeeVM = new EmployeeViewModel
            {
                Departments = department,
                Designations = designation,
                Divisions = division,
                Districts = district,
                Thanas = thana
            };


            ViewBag.districtDropDown = new SelectListItem[] { new SelectListItem() { Value = "", Text = "Select..." } };
            ViewBag.DistrictId = new SelectListItem[] { new SelectListItem() { Value = "", Text = "Select..." } };
            ViewBag.ThanaId = new SelectListItem[] { new SelectListItem() { Value = "", Text = "Select..." } };

            //ViewBag.districtDropDown = new SelectLsistItem[] { new SelectListItem() { Value = "", Text = "Select..." } };

            return View(employeeVM);
        }
        
        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {
                Employee employee = new Employee
                {
                    //UserId = user.Id,
                    Name = employeeVM.Name,
                    ContactNo = employeeVM.ContactNo,
                    Email = employeeVM.Email,
                    Address1 = employeeVM.Address1,
                    Address2 = employeeVM.Address2,
                    Status = "employee",
                    //IsDriver = employeeVM.IsDriver,
                    DepartmentId = employeeVM.DepartmentId,
                    DesignationId = employeeVM.DesignationId,
                    DivisionId = employeeVM.DivisionId,
                    DistrictId = employeeVM.DivisionId,
                    ThanaId = employeeVM.ThanaId
                };

                bool isSaved = _employeeManager.Add(employee);
                if (isSaved)
                {
                    TempData["msg"] = "Employee Save Successfully!";
                    return RedirectToAction("Index");
                }

                TempData["msg"] = "Employee Not Saved!";
                return RedirectToAction("Create");
            }
            return View();
        }


        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Employee employee = _employeeManager.GetById((int)id);

            EditEmployeeViewModel employeeVM = new EditEmployeeViewModel();
            employeeVM.Id = employee.Id;
            employeeVM.Name = employee.Name;
            employeeVM.ContactNo = employee.ContactNo;
            employeeVM.Email = employee.Email;
            employeeVM.Address1 = employee.Address1;
            employeeVM.Address2 = employee.Address2;
            employeeVM.Status = "employee";
            //employeeVM.IsDriver = employee.IsDriver;
            employeeVM.DepartmentId = (int) employee.DepartmentId;
            employeeVM.DesignationId = (int) employee.DesignationId;
            employeeVM.DivisionId = (int) employee.DivisionId;
            employeeVM.DistrictId = (int) employee.DistrictId;
            employeeVM.ThanaId = (int) employee.ThanaId;

            ViewBag.DepartmentId = new SelectList(_departmentManager.GetAll(), "Id", "Name", employee.DepartmentId);
            ViewBag.DesignationId = new SelectList(_designationManager.GetAll(), "Id", "Name", employee.DesignationId);
            ViewBag.DivisionId = new SelectList(_divisionManager.GetAll(), "Id", "Name", employee.DivisionId);
            ViewBag.DistrictId = new SelectList(_districtManager.GetAll(), "Id", "Name", employee.DistrictId);
            ViewBag.ThanaId = new SelectList(_thanaManager.GetAll(), "Id", "Name", employee.ThanaId);

            return View(employeeVM);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(EditEmployeeViewModel employeeVM)
        {
            try
            {
                Employee employee = new Employee();
                employee.Id = employeeVM.Id;
                employee.Name = employeeVM.Name;
                employee.ContactNo = employeeVM.ContactNo;
                employee.Email = employeeVM.Email;
                employee.Address1 = employeeVM.Address1;
                employee.Address2 = employeeVM.Address2;
                employee.Status = "employee";
                employee.DepartmentId = employeeVM.DepartmentId;
                employee.DesignationId = employeeVM.DesignationId;
                employee.DivisionId = employeeVM.DivisionId;
                employee.DistrictId = employeeVM.DistrictId;
                employee.ThanaId = employeeVM.ThanaId;

                _employeeManager.Update(employee);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Employee employee = _employeeManager.GetById((int)id);
            _employeeManager.Remove(employee);
            return View();
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var department = _departmentManager.GetAll();
            var designation = _designationManager.GetAll();
            var division = _divisionManager.GetAll();
            var district = _districtManager.GetAll();
            var thana = _thanaManager.GetAll();
            Employee employee = _employeeManager.GetById((int)id);
            EmployeeViewModel employeeVM = new EmployeeViewModel()
            {
                Id = employee.Id,
                Name = employee.Name,
                ContactNo = employee.ContactNo,
                Email = employee.Email,
                Address1 = employee.Address1,
                Address2 = employee.Address2,
                //LicenceNo = employee.LicenceNo,
                Department = department.Where(x => x.Id == employee.DepartmentId).FirstOrDefault(),
                Designation = designation.Where(x => x.Id == employee.DesignationId).FirstOrDefault(),
                Division = division.Where(x => x.Id == employee.DivisionId).FirstOrDefault(),
                District = district.Where(x => x.Id == employee.DistrictId).FirstOrDefault(),
                Thana = thana.Where(x => x.Id == employee.ThanaId).FirstOrDefault()

            };
            return View(employeeVM);
        }
        //public ActionResult Driver()
        //{
        //    var employee = _employeeManager.Get(c => c.IsDriver == true && c.IsDeleted == false);
        //    var department = _departmentManager.GetAll();
        //    var designation = _designationManager.GetAll();
        //    var division = _divisionManager.GetAll();
        //    var district = _districtManager.GetAll();
        //    var thana = _thanaManager.GetAll();

        //    List<EmployeeViewModel> employeeViewList = new List<EmployeeViewModel>();
        //    foreach (var emploeedata in employee)
        //    {
        //        var employeeVM = new EmployeeViewModel();
        //        employeeVM.Id = emploeedata.Id;
        //        employeeVM.Name = emploeedata.Name;
        //        employeeVM.ContactNo = emploeedata.ContactNo;
        //        employeeVM.Email = emploeedata.Email;
        //        employeeVM.Address1 = emploeedata.Address1;
        //        employeeVM.Address2 = emploeedata.Address2;
        //        employeeVM.LicenceNo = emploeedata.LicenceNo;
        //        employeeVM.Department = department.Where(x => x.Id == emploeedata.DepartmentId).FirstOrDefault();
        //        employeeVM.Designation = designation.Where(x => x.Id == emploeedata.DesignationId).FirstOrDefault();
        //        employeeVM.Division = division.Where(x => x.Id == emploeedata.DivisionId).FirstOrDefault();
        //        employeeVM.District = district.Where(x => x.Id == emploeedata.DistrictId).FirstOrDefault();
        //        employeeVM.Thana = thana.Where(x => x.Id == emploeedata.ThanaId).FirstOrDefault();

        //        employeeViewList.Add(employeeVM);
        //    }
        //    return View(employeeViewList);
        //}

        public JsonResult GetEmployePhoneNo(int? employeeId)
        {
            if (employeeId == null)
            {
                return null;
            }

            var employee = _employeeManager.GetAll();
            var employeeNumber = employee.Where(c => c.Id == employeeId).ToList();
            return Json(employeeNumber, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DriverList()
        {
            var driverList = _employeeManager.Get(c => c.IsDriver == true && c.IsDeleted == false);
            var department = _departmentManager.GetAll();
            var designation = _designationManager.GetAll();

            List<DriverViewModel> AllDriverList = new List<DriverViewModel>();
            foreach (var emploeedata in driverList)
            {
                var driverVm = new DriverViewModel();
                driverVm.Id = emploeedata.Id;
                driverVm.Name = emploeedata.Name;
                driverVm.ContactNo = emploeedata.ContactNo;
                driverVm.Email = emploeedata.Email;
                driverVm.Address1 = emploeedata.Address1;
                driverVm.Address2 = emploeedata.Address2;
                driverVm.LicenceNo = emploeedata.LicenceNo;
                driverVm.IsDriver = emploeedata.IsDriver;
                driverVm.Department = department.Where(x => x.Id == emploeedata.DepartmentId).FirstOrDefault();
                driverVm.Designation = designation.Where(x => x.Id == emploeedata.DesignationId).FirstOrDefault();

                AllDriverList.Add(driverVm);
            }
            ViewBag.TotalDriver = driverList.Count;
            return View(AllDriverList);
        }

        public JsonResult IsNameExist(string ContactNo)
        {
            var contact = _employeeManager.IsContactAlreadyExist(ContactNo);
            return Json(contact, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsEmailExist(string Email)
        {
            var email = _employeeManager.IsEmailAlreadyExist(Email);
            return Json(email, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsLicenceExist(string LicenceNo)
        {
            var licence = _employeeManager.IsLicenceAlreadyExist(LicenceNo);
            return Json(licence, JsonRequestBehavior.AllowGet);
        }
    }
}
