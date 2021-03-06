﻿using GrantCountyAs400.Domain.Treasurer;
using GrantCountyAs400.Domain.Treasurer.Repository;
using GrantCountyAs400.Web.Extensions;
using GrantCountyAs400.Web.ViewModels;
using GrantCountyAs400.Web.ViewModels.TreasurerVM.TaxReceivable;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GrantCountyAs400.Web.Controllers.Treasurer
{
    [Route("tax-receivable")]
    public class TaxReceivableController : Controller
    {
        private readonly ITaxReceivableRepository _taxReceivableRepository;

        public TaxReceivableController(ITaxReceivableRepository taxReceivableRepository)
        {
            _taxReceivableRepository = taxReceivableRepository;
        }

        [HttpGet]
        public IActionResult Index(TaxReceivableFilterViewModel filter, int pageNumber = 1)
        {
            var pagingInfo = new PagingInfo() { PageNumber = pageNumber };
            var results = _taxReceivableRepository.GetAll(filter.MinParcelNumber, filter.MaxParcelNumber, out int resultCount, pageNumber, AppSettings.PageSize).ToList();

            pagingInfo.Total = resultCount;
            ViewBag.FilterViewModel = filter;
            return View(results.ToMappedPagedList<TaxReceivable, TaxReceivableViewModel>(pagingInfo));
        }

        [HttpGet]
        [Route("{parcelNumber}/{parcelExtension}")]
        public IActionResult Details(decimal parcelNumber, decimal parcelExtension)
        {
            var entity = _taxReceivableRepository.Details(parcelNumber, parcelExtension);
            if (entity == null)
                return NotFound();

            var viewmodel = AutoMapper.Mapper.Map<TaxReceivableDetailsViewModel>(entity);
            return View(viewmodel);
        }
    }
}