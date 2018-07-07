﻿using GrantCountyAs400.Domain.Accounting;
using GrantCountyAs400.PersistenceAdapter.Mappers;
using GrantCountyAs400.PersistenceAdapter.Models;
using System.Collections.Generic;
using System.Linq;

namespace GrantCountyAs400.PersistenceAdapter.Repositories
{
    public class PersonnelRepository : IPersonnelRepository
    {
        private readonly GrantCountyDbContext _context;

        public PersonnelRepository(GrantCountyDbContext dbContext)
        {
            _context = dbContext;
        }

        public IEnumerable<Personnel> GetAll()
        {
            return _context.AcctPersonnel
                            .Select(PersonnelMapper.Map)
                            .ToList();
        }

        public IEnumerable<Personnel> GetAllWithContracts(string firstName, string lastName, decimal Ssn, out int resultCount, int pageNumber = 1, int pageSize = 50)
        {
            List<Personnel> results = new List<Personnel>();
            IQueryable<AcctPersonnel> query = _context.AcctPersonnel;
            if (!string.IsNullOrWhiteSpace(firstName))
            {
                query = query.Where(t => t.Name.Contains(firstName));
            }
            if(!string.IsNullOrWhiteSpace(lastName))
            {
                query = query.Where(t => t.Name.Contains(lastName));
            }
            if (Ssn > 0)
            {
                query = query.Where(t => t.Ssnumber == Ssn);
            }
            List<Personnel> personnelRecords = new List<Personnel>();
            if(pageNumber > 0)
            {
                resultCount = query.Count();
                personnelRecords = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList()
                                .Select(PersonnelMapper.Map)
                                .ToList();
            }
            else
            {
                resultCount = results.Count();
                personnelRecords = query.ToList()
                              .Select(PersonnelMapper.Map)
                              .ToList();
            }

            var listOfRetrievedSSNumbers = personnelRecords.Select(personnel => personnel.SSNumber).ToList();

            var employeeRecords = _context.AcctEmployee
                                    .Where(emp => listOfRetrievedSSNumbers.Contains(emp.Ssnumber.Value))
                                    .Select(EmployeeMapper.Map)
                                    .ToList();

            foreach (var personnel in personnelRecords)
            {
                results.Add(new Personnel(
                    personnel.Id, personnel.Name, personnel.SSNumber, personnel.PersonNumber,
                    employeeRecords.Where(emp => emp.SSNumber == personnel.SSNumber)
                ));
            }

            return results.ToList();
        }
    }
}