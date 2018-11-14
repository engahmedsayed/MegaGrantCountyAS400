﻿using System;
using System.Collections.Generic;

namespace GrantCountyAs400.Domain.Treasurer
{
    public class TaxReceivableDetails
    {
        // ASMTRealPropertyAssessedValueMaster => RPAVM
        public int Id { get; }
        public decimal? ParcelNumber { get; }
        public decimal? TaxCodeArea { get; }
        public string TaxpayerCode { get; }
        public string TaxpayerName { get; }

        public IEnumerable<PropertyTaxReceivable> PropertyTaxReceivables { get; }

        public TaxReceivableDetails(int id, decimal? parcelNumber, decimal? taxCodeArea, string taxpayerCode, string taxpayerName)
        {
            Id = id;
            ParcelNumber = parcelNumber;
            TaxCodeArea = taxCodeArea;
            TaxpayerCode = taxpayerCode;
            TaxpayerName = taxpayerName;
        }

        public TaxReceivableDetails(
            int id, decimal? parcelNumber, decimal? taxCodeArea, string taxpayerCode, string taxpayerName, IEnumerable<PropertyTaxReceivable> propertyTaxReceivables)
            : this(id, parcelNumber, taxCodeArea, taxpayerCode, taxpayerName)
        {
            PropertyTaxReceivables = propertyTaxReceivables;
        }
    }

    public class PropertyTaxReceivable
    {
        // TREASAllPropertyTaxReceivableMaster – APTRM
        public decimal? ParcelExtension { get; }
        public int TaxYear { get; }
        public decimal? TaxAmount { get; }
        public string Type => "Bld";
        public IEnumerable<SpecialAssessmentTaxReceivable> SpecialAssessmentTaxReceivables { get; }
        public IEnumerable<PropertyTaxReceivableTransaction> Transactions { get; }

        public PropertyTaxReceivable(decimal? parcelExtension, int taxYear, decimal? taxAmount)
        {
            ParcelExtension = parcelExtension;
            TaxYear = taxYear;
            TaxAmount = taxAmount;
        }

        public PropertyTaxReceivable(decimal? parcelExtension, int taxYear, decimal? taxAmount,
            IEnumerable<SpecialAssessmentTaxReceivable> specialAssessmentTaxReceivables)
            : this(parcelExtension, taxYear, taxAmount)
        {
            SpecialAssessmentTaxReceivables = specialAssessmentTaxReceivables;
        }

        public PropertyTaxReceivable(
            decimal? parcelExtension, int taxYear, decimal? taxAmount, IEnumerable<SpecialAssessmentTaxReceivable> specialAssessmentTaxReceivables,
            IEnumerable<PropertyTaxReceivableTransaction> transactions)
            : this(parcelExtension, taxYear, taxAmount, specialAssessmentTaxReceivables)
        {
            Transactions = transactions;
        }
    }

    public class SpecialAssessmentTaxReceivable
    {
        // TREASSpecialAssessmentsTaxReceivableMaster => SAM
        public decimal? AssessmentAmount { get; }
        public decimal? PenaltyAmount { get; }
        public decimal? InterestPaid { get; }

        public SpecialAssessmentTaxReceivable(decimal? assessmentAmount, decimal? penaltyAmount, decimal? interestPaid)
        {
            AssessmentAmount = assessmentAmount;
            PenaltyAmount = penaltyAmount;
            InterestPaid = interestPaid;
        }
    }

    public class PropertyTaxReceivableTransaction
    {
        // TREASAllPropertyTaxReceivableTransactions => APTRD
        public decimal? ParcelExtension { get; }
        public decimal? TaxAmount { get; }
        public decimal? TaxYear { get; set; }
        public DateTime? TransactionDate { get; set; }
        public decimal? TransactionNumber { get; set; }
        public string Type => "Pd";


        public IEnumerable<SpecialAssessmentTransaction> SpecialAssessmentTransactions { get; }

        public PropertyTaxReceivableTransaction(decimal? parcelExtension, decimal? taxAmount, decimal? taxYear, DateTime? transactionDate, decimal? transactionNumber)
        {
            ParcelExtension = parcelExtension;
            TaxAmount = taxAmount;
            TaxYear = taxYear;
            TransactionDate = transactionDate;
            TransactionNumber = transactionNumber;
        }

        public PropertyTaxReceivableTransaction(
            decimal? parcelExtension, decimal? taxAmount, decimal? taxYear, DateTime? transactionDate, decimal? transactionNumber,
            IEnumerable<SpecialAssessmentTransaction> specialAssessmentTransactions)
            : this(parcelExtension, taxAmount, taxYear, transactionDate, transactionNumber)
        {
            SpecialAssessmentTransactions = specialAssessmentTransactions;
        }
    }

    public class SpecialAssessmentTransaction
    {
        // TREASSpecialAssessmentsTransactions => SAT
        public decimal? AssessmentPaid { get; }
        public decimal? PenaltyPaid { get; }
        public decimal? InterestPaid { get; }

        public SpecialAssessmentTransaction(decimal? assessmentPaid, decimal? penaltyPaid, decimal? interestPaid)
        {
            AssessmentPaid = assessmentPaid;
            PenaltyPaid = penaltyPaid;
            InterestPaid = interestPaid;
        }
    }
}