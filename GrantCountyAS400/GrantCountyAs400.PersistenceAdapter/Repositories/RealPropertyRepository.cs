﻿using GrantCountyAs400.Domain.Assessment;
using GrantCountyAs400.Domain.Assessment.Repository;
using GrantCountyAs400.PersistenceAdapter.Mappers.Assessment;
using GrantCountyAs400.PersistenceAdapter.Models;
using System.Collections.Generic;
using System.Linq;

namespace GrantCountyAs400.PersistenceAdapter.Repositories
{
    public class RealPropertyRepository : IRealPropertyRepository
    {
        private readonly GrantCountyDbContext _context;

        public RealPropertyRepository(GrantCountyDbContext dbContext)
        {
            _context = dbContext;
        }

        public IEnumerable<RealProperty> GetAll(
            decimal minParcelNumber, decimal? maxParcelNumber, string taxpayer, string owner, string contractHolder, decimal taxcodeArea,
            out int resultCount, int pageNumber = 1, int pageSize = 50)
        {
            List<RealProperty> results = new List<RealProperty>();

            // if Max parcelNumber is null or zero, make it same as Min value as if "single" value was provided
            if (!maxParcelNumber.HasValue || maxParcelNumber.Value == 0)
            {
                maxParcelNumber = minParcelNumber;
            }

            var query = (from valueMaster in _context.ASMTValueMasterNameView
                         join codeArea in _context.AsmttaxCodeArea on valueMaster.TaxCodeArea equals codeArea.TaxCodeArea
                         where ((minParcelNumber <= 0) || valueMaster.ParcelNumber >= minParcelNumber)
                         && ((maxParcelNumber <= 0) || valueMaster.ParcelNumber <= maxParcelNumber)
                         && ((taxcodeArea <= 0) || codeArea.TaxCodeArea == taxcodeArea)
                         && (string.IsNullOrEmpty(taxpayer) || valueMaster.TaxpayerName.ToLower().Contains(taxpayer.Trim().ToLower()))
                         && (string.IsNullOrEmpty(owner) || valueMaster.TitleOwnerName.ToLower().Contains(owner.Trim().ToLower()))
                         && (string.IsNullOrEmpty(contractHolder) || valueMaster.ContractHolderName.ToLower().Contains(contractHolder.Trim().ToLower()))
                         select RealPropertyMapper.Map(valueMaster, codeArea));

            if (pageNumber > 0)
            {
                resultCount = query.Count();
                results = query.Skip((pageNumber - 1) * pageSize)
                               .Take(pageSize)
                               .OrderBy(t => t.ParcelNumber)
                               .ToList();
            }
            else
            {
                results = query.OrderBy(t => t.ParcelNumber)
                               .ToList();
                resultCount = results.Count();
            }

            return results;
        }

        public RealPropertyDetails Details(int id)
        {
            var assessmentDetails = (from namesRecord in _context.ASMTValueMasterNameView
                                     join codeArea in _context.AsmttaxCodeArea on namesRecord.TaxCodeArea equals codeArea.TaxCodeArea
                                     join valueMasterRecord in _context.AsmtrealPropertyAssessedValueMaster on namesRecord.Id equals valueMasterRecord.Id
                                     where namesRecord.Id == id
                                     select new { namesRecord, codeArea, valueMasterRecord }).SingleOrDefault();

            if (assessmentDetails != null)
            {
                var landUseCode = _context.AsmtlandUseCodes.SingleOrDefault(t => t.LandUseCode == assessmentDetails.valueMasterRecord.LandUseCode);
                var zoneCode = _context.AsmtzoneDescriptions.SingleOrDefault(t => t.ZoneCode == assessmentDetails.valueMasterRecord.ZoneCode);
                var construction = _context.AsmtnewConstruction.SingleOrDefault(t => t.ParcelNumber == assessmentDetails.valueMasterRecord.ParcelNumber &&
                                                                                    t.TaxYear == 2010);
                var citizenHistory = _context.AsmtseniorCitizenHistory.SingleOrDefault(t => t.ParcelNumber == assessmentDetails.valueMasterRecord.ParcelNumber &&
                                                                                            t.TaxYear == 2010);

                return RealPropertyMapper.Map(assessmentDetails.namesRecord,
                                            assessmentDetails.codeArea,
                                            assessmentDetails.valueMasterRecord,
                                            landUseCode,
                                            zoneCode,
                                            construction,
                                            citizenHistory);
            }
            else
            {
                return null;
            }
        }
    }
}