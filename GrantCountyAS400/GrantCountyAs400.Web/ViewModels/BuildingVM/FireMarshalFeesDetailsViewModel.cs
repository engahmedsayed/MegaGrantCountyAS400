﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrantCountyAs400.Web.ViewModels.BuildingVM
{
    public class FireMarshalFeesDetailsViewModel
    {
        public BuildingPermitSystemBasicInfoViewModel BasicInfo { get; set; }

        [Display(Name = "Total")]
        public decimal? TotalValue { get; set; }

        [Display(Name = "Total Fire Marshal Fees")]
        public decimal? TotalFireMarshalFees { get; set; }

        public List<decimal?> SequenceNumber { get; set; }

        public List<string> FeeDescription { get; set; }

        public List<decimal?> NumberOfUnits { get; set; }

        public List<decimal?> UnitCharge { get; set; }

        public List<decimal?> ExtendedAmount { get; set; }

        public List<string> FeeCategory { get; set; }

        public int CoutOfLargestList
        {
            get
            {
                List<int> totalNumbers = new List<int>() { SequenceNumber.Count, FeeDescription.Count, NumberOfUnits.Count, UnitCharge.Count, FeeCategory.Count,
                                                           ExtendedAmount.Count };
                return totalNumbers.OrderByDescending(t => t).FirstOrDefault();
            }
        }


    }
}
