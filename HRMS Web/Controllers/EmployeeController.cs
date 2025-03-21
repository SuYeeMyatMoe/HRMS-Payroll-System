﻿using HRMS_Web.DAO;
using HRMS_Web.Models.DataModels;
using HRMS_Web.Models.ViewModels;
using HRMS_Web.Utilities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Numerics;
using System.Reflection;

namespace HRMS_Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly HRMSWebDBContext _dBContext;

        public EmployeeController(HRMSWebDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        [HttpGet]
        public IActionResult Entry()
        {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel()//create object of EmployeeViewModel
            {
                DepartmentViewModels = GetAllDepartments(),

                PositionViewModels = GetAllPositions()
            };
            return View(employeeViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Entry(EmployeeViewModel employeeViewModel)
        {
            try
            {
                EmployeeEntity employeeEntity = new EmployeeEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    Code = employeeViewModel.Code,
                    Name = employeeViewModel.Name,
                    Email = employeeViewModel.Email,
                    DOB = employeeViewModel.DOB,
                    DOE = employeeViewModel.DOE,
                    DOR = employeeViewModel.DOR ?? null, // Explicit handling of nullable DateTime
                    //to use that syntax, it must be nullable in viewmodel (not just in entity)

                    Address = employeeViewModel.Address,
                    BasicSalary = employeeViewModel.BasicSalary,
                    Phone = employeeViewModel.Phone,
                    DepartmentID = employeeViewModel.DepartmentID,
                    PositionID = employeeViewModel.PositionID,
                    Gender = employeeViewModel.Gender,
                    IsActive = true,
                    CreatedBy = "Admin",
                    CreatedAt = DateTime.Now,
                    Ip = await NetworkHelper.GetIPAddress()
                };//DTO process which is changing from viewmodel to entity(datamodel)
                _dBContext.Employees.Add(employeeEntity);//add entity to dbcontext
                await _dBContext.SaveChangesAsync();//save the changes to database
                //Message to show success 
                TempData["Msg"] = "New Employee is added successfully!";
                //check if error is occured
                TempData["IsErrorOccur"] = false;
            }
            catch (Exception e)
            {
                TempData["Msg"] = "There are some errors in storing the record!";
                //check if error is occured
                TempData["IsErrorOccur"] = true;

            }
            return RedirectToAction("List");
        }
        public IActionResult List()
        {
            IList<EmployeeViewModel> employeeViews = (from e in _dBContext.Employees
                                                      join p in _dBContext.Positions
                                                      on e.PositionID equals p.Id
                                                      join d in _dBContext.Departments
                                                      on e.DepartmentID equals d.Id
                                                      where e.IsActive && d.IsActive && p.IsActive
                                                      orderby e.CreatedAt ascending
                                                      select new EmployeeViewModel{
                                                          Id=e.Id,
                                                          Code=e.Code,
                                                          Name=e.Name,
                                                          Email=e.Email,
                                                          Gender=e.Gender,
                                                          DOB=e.DOB,
                                                          DOE=e.DOE,
                                                          DOR=e.DOR.Value,//value is used to get the value of nullable datetime
                                                          Address=e.Address,
                                                          BasicSalary=e.BasicSalary,
                                                          Phone=e.Phone,
                                                          DepartmentInfo=d.Description,
                                                          PositionInfo=p.Description
                                                      }).ToList();

            return View(employeeViews);// this will return the list of employee (without adding employeeViews will return null in Model of view page)
        }

        public IActionResult Edit(string id)
        {
            EmployeeViewModel employeeView=_dBContext.Employees.Where(e=>e.IsActive && e.Id==id).Select(e=>new EmployeeViewModel
            {
                Id = e.Id,
                Code = e.Code,
                Name = e.Name,
                Email = e.Email,
                Gender = e.Gender,
                DOB = e.DOB,
                DOE = e.DOE,
                DOR = e.DOR.Value,//value is used to get the value of nullable datetime
                Address = e.Address,
                BasicSalary = e.BasicSalary,
                Phone = e.Phone,
                DepartmentID = e.DepartmentID,
                PositionID = e.PositionID
            }).FirstOrDefault();
            employeeView.DepartmentViewModels = GetAllDepartments();
            employeeView.PositionViewModels = GetAllPositions();
           return View(employeeView);
        }
        [HttpPost]
        public async Task<ActionResult> Update(EmployeeViewModel employeeViewModel)
        {
            try
            {
                EmployeeEntity currentEmployee = _dBContext.Employees
            .Where(e => e.IsActive && e.Id == employeeViewModel.Id)
            .FirstOrDefault();

                if (currentEmployee == null)
                {
                    TempData["Msg"] = "Employee not found!";
                    TempData["IsErrorOccur"] = true;
                    return RedirectToAction("List");
                }

                currentEmployee.Code = employeeViewModel.Code;
                currentEmployee.Name = employeeViewModel.Name;
                currentEmployee.Email = employeeViewModel.Email;
                currentEmployee.DOB = employeeViewModel.DOB;
                currentEmployee.DOE = employeeViewModel.DOE;
                currentEmployee.DOR = employeeViewModel.DOR; 

                currentEmployee.Address = employeeViewModel.Address;
                currentEmployee.BasicSalary = employeeViewModel.BasicSalary;
                currentEmployee.Phone = employeeViewModel.Phone;
                currentEmployee.DepartmentID = employeeViewModel.DepartmentID;
                currentEmployee.PositionID = employeeViewModel.PositionID;
                currentEmployee.Gender = employeeViewModel.Gender;

                currentEmployee.UpdatedBy = "Admin";
                currentEmployee.UpdatedAt = DateTime.Now;
                currentEmployee.Ip = await NetworkHelper.GetIPAddress();

                _dBContext.Employees.Update(currentEmployee);
                await _dBContext.SaveChangesAsync(); // Use Async

                TempData["Msg"] = "New Data is updated successfully!";
                TempData["IsErrorOccur"] = false;
            }
            catch (Exception e)
            {
                TempData["Msg"] = "There are some errors in updating the record!";
                //check if error is occured
                TempData["IsErrorOccur"] = true;
            }
            return RedirectToAction("List");
        }
        public IActionResult Delete(string id)
        {
            try
            {
                EmployeeEntity employeeEntity = _dBContext.Employees.Where(e => e.IsActive && e.Id == id).FirstOrDefault();
                if (employeeEntity is not null)//if it is exist
                {
                    employeeEntity.IsActive = false;
                    _dBContext.Employees.Update(employeeEntity);//update the record in the context
                    _dBContext.SaveChanges();
                    TempData["Msg"] = "Data has been deleted successfully";
                    TempData["IsErrorOccur"] = false;
                }
            }
            catch (Exception e)
            {
                TempData["Msg"] = "There is some issue in deleting record";
                TempData["IsErrorOccur"] = true;

            }
            return RedirectToAction("List");
        }

        private IList<DepartmentViewModel> GetAllDepartments()
        {
            return _dBContext.Departments.Where(w => w.IsActive).Select(s => new DepartmentViewModel
            {
                Id = s.Id,
                Code = s.Code,
                Description = s.Description
            }).ToList();
        }

        private IList<PositionViewModel> GetAllPositions()
        {
            return _dBContext.Positions.Where(w => w.IsActive).Select(s => new PositionViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description
            }).ToList();
        }
    }
    }

