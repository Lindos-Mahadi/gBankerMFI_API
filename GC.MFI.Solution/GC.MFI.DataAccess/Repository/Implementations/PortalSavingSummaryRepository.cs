using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.RequestModels;
using GC.MFI.Models.ViewModels;
using GC.MFI.Utility.Helpers;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.DataAccess.Repository.Implementations
{
    public class PortalSavingSummaryRepository : LegacyRepositoryBase<PortalSavingSummary>, IPortalSavingSummaryRepository
    {
        private readonly GBankerDbContext _context;
        public PortalSavingSummaryRepository(IDatabaseFactory databaseFactory,GBankerDbContext context) : base(databaseFactory)
        {
            this._context= context;
        }

        //public async Task<PortalSavingSummary> Create(PortalSavingSummary request)
        //{
        //    BeginTransaction();
        //    var model = new PortalSavingSummary()
        //    {
        //        OfficeID = request.OfficeID,
        //        MemberID = request.MemberID,
        //        ProductID = request.ProductID,
        //        CenterID = request.CenterID,
        //        SavingInstallment = request.SavingInstallment,
        //        CreateDate = DateTime.UtcNow,
        //        CreateUser = request.CreateUser,
        //        OpeningDate = DateTime.UtcNow,
        //        MemberNomines = request.MemberNomines,
        //        ApprovalStatus = false
        //    };
        //    DataContext.Add(model);
        //    CommitTransaction();
        //    return model;
        //}

        public void CreatePortalSavingSummary(PortalSavingSummaryFileUpload entity)
        {
            try
            {
                BeginTransaction();
                var model = new PortalSavingSummary()
                {
                    PortalSavingSummaryID = entity.PortalSavingSummaryID,
                    OfficeID = entity.OfficeID,
                    MemberID = entity.MemberID,
                    ProductID = entity.ProductID,
                    CenterID = entity.CenterID,
                    NoOfAccount = entity.NoOfAccount,
                    TransactionDate = entity.TransactionDate,
                    Deposit = entity.Deposit,
                    Withdrawal = entity.Withdrawal,
                    Balance = entity.Balance,
                    InterestRate = entity.InterestRate,
                    SavingInstallment = entity.SavingInstallment,
                    CumInterest = entity.CumInterest,
                    MonthlyInterest = entity.MonthlyInterest,
                    Penalty = entity.Penalty,
                    OpeningDate = entity.OpeningDate,
                    MaturedDate = entity.MaturedDate,
                    ClosingDate = entity.ClosingDate,
                    TransType = entity.TransType,
                    SavingStatus = 1,
                    //EmployeeId = entity.EmployeeId,
                    //MemberCategoryID= entity.MemberCategoryID,
                    Posted = entity.Posted,
                    IsActive = entity.IsActive,
                    InActiveDate = entity.InActiveDate,
                    Duration = entity.Duration,
                    InstallmentNo = entity.InstallmentNo,
                    CreateDate = entity.CreateDate,
                    CreateUser = entity.CreateUser,
                    //OrgID= entity.OrgID,
                    SavingAccountNo = entity.SavingAccountNo,
                    Ref_EmployeeID = entity.Ref_EmployeeID,
                    ApprovalStatus = false
                };
                DataContext.PortalSavingSummary.Add(model);
                DataContext.SaveChanges();


                // Nominee Image upload in FileUploadTable
                FileUploadTable[] nomineeImage = new FileUploadTable[entity.MemberNomines.Count];
                for (int i = 0; i < nomineeImage.Length; i++)
                {
                    Base64File Nimg = ImageHelper.GetFileDetails(entity.MemberNomines[i].Image);
                    nomineeImage[i] = new FileUploadTable
                    {
                        EntityId = model.PortalSavingSummaryID,
                        EntityName = "PortalSavingSummary",
                        PropertyName = "NomineeImage",
                        FileName = $"Nominee_Image_{entity.MemberNomines[i].NIDNumber}",
                        File = Nimg.DataBytes,
                        Type = Nimg.MimeType,
                        DocumentType = "NomineeImage"
                    };
                }
                DataContext.FileUploadTable.AddRange(nomineeImage);

                // Nominee NId upload in FileUploadTable

                FileUploadTable[] nomineeNID = new FileUploadTable[entity.MemberNomines.Count];
                for (int i = 0; i < nomineeNID.Length; i++)
                {
                    Base64File Nnid = ImageHelper.GetFileDetails(entity.MemberNomines[i].Nid);
                    nomineeNID[i] = new FileUploadTable
                    {
                        EntityId = model.PortalSavingSummaryID,
                        EntityName = "PortalSavingSummary",
                        PropertyName = "NomineeNID",
                        FileName = $"Nominee_NID_{entity.MemberNomines[i].NIDNumber}",
                        File = Nnid.DataBytes,
                        Type = Nnid.MimeType,
                        DocumentType = "NomineeNID"
                    };
                }
                DataContext.FileUploadTable.AddRange(nomineeNID);

                DataContext.SaveChanges();

                NomineeImageAndNidIdentity(model.PortalSavingSummaryID, entity.MemberNomines.ToList());

                // For Supporting Document File Upload
                if (entity.PortalSavingFileUpload != null)
                {
                    // BULT INSERT DATA
                    FileUploadTable[] file = new FileUploadTable[entity.PortalSavingFileUpload.Count];
                    for (int i = 0; i < entity.PortalSavingFileUpload.Count(); i++)
                    {
                        Base64File filesType = ImageHelper.GetFileDetails(entity.PortalSavingFileUpload[i].File);
                        file[i] = new FileUploadTable
                        {

                            EntityId = model.PortalSavingSummaryID,
                            EntityName = "PortalSavingSummary",
                            PropertyName = "SupportingDocument",
                            FileName = $"SupportingDocument_L{model.PortalSavingSummaryID}_{i + 1}",
                            Type = filesType.MimeType,
                            File = filesType.DataBytes,
                            DocumentType = entity.PortalSavingFileUpload[i].DocumentType,
                        };

                    }
                    DataContext.FileUploadTable.AddRange(file);
                    DataContext.SaveChanges();
                    SupportingDocumentIdentity(model.PortalSavingSummaryID);
                    CommitTransaction();

                }
                CommitTransaction();

            }
            catch(Exception ex)
            {
                RollbackTransaction();
                throw ex;
            }
            
            
        }

        public async Task<PagedResponse<IEnumerable<SavingSummaryViewModel>>> GetAllPortalSavingSummaryPaged(PaginationFilter<SavingSummaryViewModel> filter, long Id)
        {
            var TotalElement = DataContext.PortalSavingSummary.Count(t => t.MemberID == Id);

            var savingSummary =(from pps in DataContext.PortalSavingSummary
                                join pl in DataContext.Product on pps.ProductID equals pl.ProductID
                                join m in DataContext.Member on pps.MemberID equals m.MemberID
                                select new SavingSummaryViewModel
                                {
                                    PortalSavingSummaryID= pps.PortalSavingSummaryID,
                                    OfficeID=pps.OfficeID,
                                    MemberID=pps.MemberID,
                                    MemberName = m.FirstName,
                                    ProductID= (short)pl.ProductID,
                                    ProductName= pl.ProductName,
                                    CenterID=pps.CenterID,
                                    NoOfAccount=pps.NoOfAccount,
                                    TransactionDate = pps.TransactionDate,
                                    Deposit = pps.Deposit,
                                    Withdrawal = pps.Withdrawal,
                                    Balance= pps.Balance,
                                    InterestRate=pps.InterestRate,
                                    SavingInstallment=pps.SavingInstallment,
                                    CumInterest = pps.CumInterest,
                                    MonthlyInterest = pps.MonthlyInterest,
                                    Penalty=pps.Penalty,
                                    OpeningDate=pps.OpeningDate,
                                    MaturedDate=pps.MaturedDate,
                                    ClosingDate=pps.ClosingDate,
                                    TransType=pps.TransType,
                                    SavingStatus=pps.SavingStatus,
                                    EmployeeId = pps.EmployeeId,
                                    MemberCategoryID=pps.MemberCategoryID,
                                    Posted = pps.Posted,
                                    IsActive=pps.IsActive,
                                    InActiveDate=pps.InActiveDate,
                                    CreateDate=pps.CreateDate,
                                    CreateUser=pps.CreateUser,
                                    OrgID=pps.OrgID,
                                    SavingAccountNo = pps.SavingAccountNo,
                                    Ref_EmployeeID=pps.Ref_EmployeeID,
                                    ApprovalStatus = pps.ApprovalStatus,
                                    MemberNomines =pps.MemberNomines,
                                    MinLimit = pl.MinLimit,
                                    MaxLimit = pl.MaxLimit
                                })
                                    .Where(filter.search)
                                    .Where(x => x.MemberID == Id)
                                    .Skip(filter.pageNum > 0 ? (filter.pageNum - 1) * filter.pageSize : 0)
                                    .Take(filter.pageSize).ToList();
            //for(int i=0;i<savingSummary.Count();i++)
            //{
            //    var nominee = DataContext.NomineeXPortalSavingSummary.Where(t => t.PortalSavingSummaryID == savingSummary[i].PortalSavingSummaryID);
            //}

            return new PagedResponse<IEnumerable<SavingSummaryViewModel>>(
                savingSummary,
                filter.pageNum,
                filter.pageSize,
                TotalElement,
                TotalElement / filter.pageSize);
        }

        public async Task<IEnumerable<SavingSummaryViewModel>> getBySavingStatus(byte type, long memberId)
        {
            var savingSummary = (from pps in DataContext.PortalSavingSummary
                                 join pl in DataContext.Product on pps.ProductID equals pl.ProductID
                                 join m in DataContext.Member on pps.MemberID equals m.MemberID
                                 select new SavingSummaryViewModel
                                 {
                                     PortalSavingSummaryID = pps.PortalSavingSummaryID,
                                     OfficeID = pps.OfficeID,
                                     MemberID = pps.MemberID,
                                     MemberName = m.FirstName,
                                     ProductID = (short)pl.ProductID,
                                     ProductName = pl.ProductName,
                                     CenterID = pps.CenterID,
                                     Balance = pps.Balance,
                                     SavingInstallment = pps.SavingInstallment,
                                     SavingStatus = pps.SavingStatus,
                                     IsActive = pps.IsActive,
                                     InActiveDate = pps.InActiveDate,
                                     CreateDate = pps.CreateDate,
                                     CreateUser = pps.CreateUser,
                                     OrgID = pps.OrgID,
                                     ApprovalStatus = pps.ApprovalStatus,
                                     MinLimit = pl.MinLimit,
                                     MaxLimit = pl.MaxLimit
                                 }).Where(t => t.SavingStatus == type && t.MemberID == memberId);
                           
            return savingSummary;

        }

        public void SupportingDocumentIdentity(long PortalSavingId)
        {
            var getSupportingDocument = DataContext.FileUploadTable.Where(t => t.EntityId == PortalSavingId && t.PropertyName == "SupportingDocument").ToList();
            long[] SD = new long[getSupportingDocument.Count];
            for (int i = 0; i < getSupportingDocument.Count(); i++)
            {
                SD[i] = getSupportingDocument[i].FileUploadId;
            }
            var SDID = string.Join(",", SD);
            var getPortalSavingSummary = GetById(PortalSavingId);
            getPortalSavingSummary.SupportingDocumentsId = SDID;
            DataContext.SaveChanges();
        }
        public void NomineeImageAndNidIdentity(long savingId, List<NomineeXPortalSavingSummaryFile> file)
        {
            NomineeXPortalSavingSummary[] NomineeXSaving = new NomineeXPortalSavingSummary[file.Count];
            for(int i = 0; i < NomineeXSaving.Length ; i++)
            {
                var img = DataContext.FileUploadTable
                    .Where(t => t.EntityId == savingId 
                    && t.PropertyName == "NomineeImage" &&
                    t.FileName == $"Nominee_Image_{file[i].NIDNumber}").FirstOrDefault();
                var Nid = DataContext.FileUploadTable
                    .Where(t => t.EntityId == savingId
                    && t.PropertyName == "NomineeNID" &&
                    t.FileName == $"Nominee_NID_{file[i].NIDNumber}").FirstOrDefault();

                NomineeXSaving[i] = new NomineeXPortalSavingSummary
                {
                    PortalSavingSummaryID = savingId,
                    NomineeName = file[i].NomineeName,
                    NFatherName = file[i].NFatherName,
                    NAddressName = file[i].NAddressName,
                    NAlocation = file[i].NAlocation,
                    NIDNumber = file[i].NIDNumber,
                    ImageId = img.FileUploadId,
                    NIDId = Nid.FileUploadId
                };
            }
            DataContext.NomineeXPortalSavingSummary.AddRange(NomineeXSaving);
            DataContext.SaveChanges();
        }
    }
}