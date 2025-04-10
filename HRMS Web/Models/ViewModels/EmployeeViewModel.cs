﻿namespace HRMS_Web.Models.ViewModels
{
    public class EmployeeViewModel
    {
        public string Id { get; set; }//for delete and update cases
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public char Gender { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DOE { get; set; }
        public DateTime? DOR { get; set; }
        public string Address { get; set; }
        public decimal BasicSalary { get; set; }
        public string Phone { get; set; }
        public string DepartmentID { get; set; }//for create , update and delete in employee
        public string PositionID { get; set; }//for create , update and delete in employee
        public string DepartmentInfo { get; set; }//for list showing
        public string PositionInfo { get; set; }//for list showing



        //to use as data of dropdownlist in Employee Entry form
        public IList<PositionViewModel> PositionViewModels { get; set; }
        public IList<DepartmentViewModel> DepartmentViewModels { get; set; }
    }
}
