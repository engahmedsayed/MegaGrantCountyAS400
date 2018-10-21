﻿using System;
using System.Collections.Generic;

namespace GrantCountyAs400.PersistenceAdapter.Models
{
    public partial class BldgassessorApproval
    {
        public int Id { get; set; }
        public string RecordStatus { get; set; }
        public string JurisdictionCode { get; set; }
        public string DepartmentCode { get; set; }
        public decimal? ApplicationYear { get; set; }
        public decimal? ApplicationNumber { get; set; }
        public decimal? AddendumNumber { get; set; }
        public string Comments { get; set; }
        public string ApprovedByAssr { get; set; }
        public DateTime? ChangeDateAssr { get; set; }
        public string UserIdassr { get; set; }
        public decimal? PercentComplete { get; set; }
        public decimal? CheckedYearMonth { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? ChangeDate { get; set; }
        public string UserId { get; set; }
    }
}
