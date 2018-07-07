﻿using GrantCountyAs400.Domain.Accounting;
using GrantCountyAs400.Web.Extensions;
using GrantCountyAs400.Web.ViewModels;
using GrantCountyAs400.Web.ViewModels.AccountingVM;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GrantCountyAs400.Web.Controllers.Accounting
{
    [Route("personnel")]
    public class PersonnelController : Controller
    {
        private readonly IPersonnelRepository _personnelRepository;

        public PersonnelController(IPersonnelRepository personnelRepository)
        {
            _personnelRepository = personnelRepository;
        }

        public IActionResult Index(int pageNumber = 1, AccountingFilterVm filter = default(AccountingFilterVm))
        {
            int resultCount;
            var pagingInfo = new PagingInfo() { PageNumber = pageNumber };
            if (!ModelState.IsValid)
            {
                return View();
            }
            var results = _personnelRepository
                                .GetAllWithContracts(filter.FirstName, filter.LastName, filter.SSN, out resultCount, pageNumber, AppSettings.PageSize)
                                .ToList();

            pagingInfo.Total = resultCount;
            ViewBag.FilterViewModel = filter;
            return View(results.ToManualPagedList(pagingInfo));
        }
    }
}