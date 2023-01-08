// <copyright file="Constants.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>

namespace GC.MFI.Helpers
{
    using System;

    /// <summary>
    /// Constants class.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Graph API base URL.
        /// </summary>
        public static readonly string GraphAPIBaseUrl = "https://graph.microsoft.com";

        /// <summary>
        /// Duration gap in minutes from now for which schedule for rooms will be fetched.
        /// </summary>
        public static readonly TimeSpan DurationGapFromNow = new TimeSpan(hours: 0, minutes: 5, seconds: 0);

        /// <summary>
        /// Default meeting duration in minutes.
        /// </summary>
        public static readonly TimeSpan DefaultMeetingDuration = new TimeSpan(hours: 0, minutes: 30, seconds: 0);
    }
    public class PatientHistoryColumns
    {
        public const string Medications = "_xhis_medication_value,xhis_medicationstartdate,xhis_refills,xhis_instructions2,new_medicalhistoryid";
        public const string Vitals = "new_bmi,new_bpdiastolic,new_bpsystolic,new_heartrate,new_height_in,new_oraltemperaturef,new_weight,xhis_labobservation,new_medicalhistoryid";
        public const string Encounters = "xhis_encounterdate,xhis_reasonforvisit,xhis_assessment,_new_procedure_value,_xhis_encounterprovider_value,new_medicalhistoryid";
        public const string Immunizations = "_xhis_vaccineadministered_value,xhis_vaccineadministeredon,_xhis_immunizationstatuscustomoption_value,new_medicalhistoryid";
        public const string Problems = "_xhis_problem_value,xhis_problemcomments,xhis_problemstartdate,_xhis_problemstatussnomed_value,_xhis_problemsprovider_value,new_medicalhistoryid";
        public const string Procedures = "_xhis_procedure_value,xhis_dateofprocedure,_xhis_procedurestatus_value,new_medicalhistoryid";
        public const string Allergies = "_xhis_allergy_value,_xhis_allergyreactionsnomed_value,xhis_allergyseverity,xhis_allergystartdate,new_medicalhistoryid";
        public const string GoalPlans = "_xhis_careplan_value,_xhis_careplanstatuscustomoption_value,xhis_prescribingprovider,xhis_dateprescribed,new_medicalhistoryid,new_date";
        public const string Status = "_xhis_patientstatus_value,xhis_admitdate,xhis_dischargedate,_new_location_value,new_medicalhistoryid,modifiedon";
        public const string Labs = "_xhis_labname_value,xhis_labresult1,_xhis_labunit_value,xhis_lab1normalrange,_xhis_labflag1_value,xhis_labobservation,_xhis_labproviderinternal_value,new_medicalhistoryid";
    }

    public enum PatientHistoryCategory
    {
        Medication = 100000003,
        Vitals = 100000007,
        Allergy = 100000004,
        Encounter = 100000018,
        Immunization = 100000009,
        Problem = 100000000,
        Procedure = 100000013,
        GoalPlan = 100000020,
        Status = 100000012,
        Lab = 100000008,

    }
    public enum PaymentType
    {
        Check = 100000000,
        Cash = 100000001,
        CreditCard = 100000002,
        ElectronicFundsTransfer = 100000003,
        Adjustment = 100000004
    }
}
