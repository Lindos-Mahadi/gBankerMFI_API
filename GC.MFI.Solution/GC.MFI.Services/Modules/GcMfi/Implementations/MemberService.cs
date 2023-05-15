using AutoMapper;
using GC.MFI.DataAccess;
using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.Utility.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.GcMfi.Implementations
{
    public class MemberService : LegacyServiceBase<Member>, IMemberService
    {
        private readonly IMemberRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileUploadRepository _uploadRepository;

        public MemberService(IMemberRepository repository, IFileUploadRepository uploadRepository, IUnitOfWork unitOfWork, IMapper _mapper) : base(repository, unitOfWork, _mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _uploadRepository = uploadRepository;
        }

        public async Task<IEnumerable<Member>> GetAllMember(string search)
        {
            var memberList = await _repository.GetAllMember(search);
            return memberList;
        }

        public async Task<Member> UpdateMemberProfile(MemberProfileUpdate memberProfile)
        {
            var dbMember = _repository.GetById(memberProfile.memberId);
            ///map only the updated fields
                dbMember.BanglaName= memberProfile.BanglaName;
                dbMember.BanglaName = memberProfile.BanglaName;
                dbMember.FatherNameBN = memberProfile.FatherNameBN;
                dbMember.MotherNameBN = memberProfile.MotherNameBN;
                dbMember.SpouseName = memberProfile.SpouseName;
                dbMember.SpouseNameBN = memberProfile.SpouseNameBN;
                dbMember.IdentTypeID = memberProfile.IdentTypeID;
                dbMember.CardIssueDate = memberProfile.CardIssueDate;
                dbMember.ExpireDate = memberProfile.ExpireDate;
                dbMember.OtherIdNo = memberProfile.OtherIdNo;
                dbMember.ProvidedByCountryID = memberProfile.ProvidedByCountryID;
                dbMember.FamilyContactNo = memberProfile.FamilyContactNo;
                dbMember.FamilyMember = memberProfile.FamilyMember;
                dbMember.RefereeName = memberProfile.RefereeName;
                dbMember.CoApplicantName = memberProfile.CoApplicantName;
                dbMember.TotalWealth = memberProfile.TotalWealth;
                dbMember.MemCategory = memberProfile.MemCategory;
                dbMember.TIN = memberProfile.TIN;
                dbMember.TaxAmount = memberProfile.TaxAmount;
                dbMember.FServiceName = memberProfile.FServiceName;
                dbMember.FinServiceChoiceId = memberProfile.FinServiceChoiceId;
                dbMember.TransactionChoiceId = memberProfile.TransactionChoiceId;
                dbMember.IsAnyFS = memberProfile.IsAnyFS;
                //dbMember.UpdateUser = DateTime.Now.ToString();

            Update(dbMember);
            
            return dbMember;
;
        }
        public async Task<Member> GetMemberByPortalId(long portalMemberId)
        {
            var memeber = await _repository.GetMemberByPortalId(portalMemberId);
            return memeber;
        }
        public async Task<string> GetImageByMemberID(long memberId)
        {
            return await _repository.GetImageByMemberID(memberId);
        }

        public async Task<string> UpdateMemberImage(string image, long memberId)
        {
            var getmember = _repository.GetById(memberId);
            var imagedetails = ImageHelper.GetFileDetails(image);
            if (getmember?.Image != null) 
            {
               var getimage = _uploadRepository.GetById((long)getmember.Image);
                if(getimage != null)
                {
                    getimage.File = imagedetails.DataBytes;
                    getimage.Type = imagedetails.MimeType;
                    getimage.EntityId = memberId;
                    getimage.PropertyName = "MemberImage";
                    getimage.FileName = $"Image.{imagedetails.MimeType}";
                    Save();
                }
                return image;
            }
            FileUploadTable fileUploadTable = new FileUploadTable();
            fileUploadTable.File = imagedetails.DataBytes;
            fileUploadTable.Type = imagedetails.MimeType;
            fileUploadTable.EntityId = memberId;
            fileUploadTable.PropertyName = "MemberImage";
            fileUploadTable.FileName = $"Image.{imagedetails.MimeType}";
            _uploadRepository.Add(fileUploadTable);
            Save();
            getmember.Image = fileUploadTable.FileUploadId;
            Save();
            return image;
        }
    }
}
