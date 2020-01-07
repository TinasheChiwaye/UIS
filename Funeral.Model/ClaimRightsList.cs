using System;

namespace Funeral.Model
{
    public class ClaimRightsList
    {
        public Int64 ClaimRightsId { get; set; }
        public int ClaimStatusId { get; set; }
        public string StatusName { get; set; }
        public int RoleId { get; set; }
        public bool AvailableRights { get; set; }
        public string CreatedBy { get; set; }
    }
}
