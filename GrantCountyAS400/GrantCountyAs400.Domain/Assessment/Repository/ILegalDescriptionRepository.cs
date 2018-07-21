﻿using System.Collections.Generic;

namespace GrantCountyAs400.Domain.Assessment.Repository
{
    public interface ILegalDescriptionRepository
    {
        IEnumerable<LegalDescription> GetAll(decimal parcelNumber, out int resultCount, int pageNumber = 1, int pageSize = 50);

        LegalDescriptionDetails Details(int id);
    }
}