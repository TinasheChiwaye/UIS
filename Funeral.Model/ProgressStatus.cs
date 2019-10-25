namespace Funeral.Model
{
    public class ProgressStatus
    {
        public ProgressStatus()
        {
            //UnderWriterStatus = true;
            //PlanStatus = true;
            //BranchStatus = true;
            //SocietyStatus = true;
            //AddonProductStatus = true;
            //UserStatus = true;
            //CompanyStatus = true;
            //FuneralServiceStatus = true;
        }

        public bool UnderWriterStatus { get; set; }
        public bool PlanStatus { get; set; }
        public bool BranchStatus { get; set; }
        public bool SocietyStatus { get; set; }
        public bool AddonProductStatus { get; set; }
        public bool UserStatus { get; set; }
        public bool CompanyStatus { get; set; }
        public bool FuneralServiceStatus { get; set; }
        public bool DependentStatus { get; set; }
        public bool MemberStatus { get; set; }


    }
}
