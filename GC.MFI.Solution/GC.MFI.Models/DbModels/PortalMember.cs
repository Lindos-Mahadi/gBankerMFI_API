﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models.DbModels
{
    public partial class PortalMember : DbModelBase, IDbModelBase
    {
        public string MemberCode { get; set; }
         public int OfficeID { get; set; }
         public int CenterID { get; set; }
         public Int16 GroupID { get; set; }
         public DateTime JoinDate { get; set; }
         public string Gender { get; set; }
         public Byte MemberCategoryID { get; set; }
         public string MemberStatus{ get; set; }
         public int OrgID{ get; set; }
         public string FirstName { get; set; }
         public string LastName { get; set; }
         public string Email{ get; set; }
         public string Occupation { get; set; }
         public string Address { get; set; }
         public string Photo { get; set; }
         public string Phone { get; set; }
        public bool? HasRequestedApproval { get; set; }
    }
}
