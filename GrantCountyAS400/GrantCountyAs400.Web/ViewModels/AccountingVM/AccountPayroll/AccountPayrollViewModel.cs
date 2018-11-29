﻿using GrantCountyAs400.Web.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GrantCountyAs400.Web.ViewModels.AccountingVM.AccountPayroll
{
    public class AccountPayrollViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        public decimal SSNumber { get; set; }
        [Display(Name = "Employee #")]
        [DisplayFormat(DataFormatString = "{0:00000}")]
        public decimal PersonNumber { get; set; }
        public IEnumerable<AccountPayrollWarrantViewModel> PayrollWarrants { get; set; }

        [Display(Name = "SSN")]
        public string SSNumberDisplay => this.SSNumber.MaskSSN();
    }

    public class AccountPayrollWarrantViewModel
    {
        [Display(Name = "Warrant #")]
        public decimal? WarrantNumber { get; set; }
        [Display(Name = "Check #")]
        public decimal? CheckNumber { get; set; }
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }
        [Display(Name = "Gross")]
        public decimal? Gross { get; set; }
        [Display(Name = "Fica")]
        public decimal? Fica { get; set; }
        [Display(Name = "Medical")]
        public decimal? Medical { get; set; }
        [Display(Name = "Retirement Benefits Employee")]
        public decimal? RetirementBenefitsEmployee { get; set; }
        [Display(Name = "Retirement Benefits Employer")]
        public decimal? RetirementBenefitsEmployer { get; set; }
        [Display(Name = "Net Pay")]
        public decimal? NetPay { get; set; }
    }
}