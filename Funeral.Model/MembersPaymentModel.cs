using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Funeral.Model
{
    public class MembersPaymentModel
    {
        public MembersPaymentModel()
        {
            CreateDate = System.DateTime.Now;
            MemberType = string.Empty;
            Title = string.Empty;
            FullNames = string.Empty;
            Surname = string.Empty;
            Gender = string.Empty;
            IDNumber = string.Empty;
            DateOfBirth = System.DateTime.Now;
            MemeberNumber = string.Empty;
            MemberSociety = string.Empty;
            fkiPlanID = 1;
            Active = true;
            PolicyStatus = string.Empty;
            //   parlourid =Convert.tou string.Empty;
            LastModified = System.DateTime.Now;
            ModifiedUser = string.Empty;
            Telephone = string.Empty;
            EasyPayNo = string.Empty;
        }

        public string ApplicationName { get; set; }
        public DateTime? CoverDate { get; set; }
         public string Telephone { get; set; }
        public int pkiMemberID { get; set; }
        public DateTime CreateDate { get; set; }
        public string MemberType { get; set; }
        public string Title { get; set; }
        public string FullNames { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string IDNumber { get; set; }
        public DateTime DateOfBirth { get; set; }

        public string MemeberNumber { get; set; }
        public string MemberSociety { get; set; }
        public int fkiPlanID { get; set; }
        public bool Active { get; set; }

        public string PolicyStatus { get; set; }
        public Guid parlourid { get; set; }

        public DateTime LastModified { get; set; }
        public string ModifiedUser { get; set; }
        public string Name { get { return string.Format("{0} {1}", Surname, FullNames); } }
        public string EasyPayNo { get; set; }
    }

    public class MembersPaymentDetailsModel : BaseViewModel
    {
        public MembersPaymentDetailsModel()
        {

            FullNames = string.Empty;
            IDNumber = string.Empty;
            MemeberNumber = string.Empty;
            Active = true;
            PolicyStatus = string.Empty;
            PlanName = string.Empty;
            PolicyLaps = 0;
            PlanSubscription = string.Empty;
            Spouse = 0;
            Children = 0;
            Adults = 0;
            WaitingPeriod = 0;
            JoiningFee = 0;
            Cover = 0;
            ParlourId = System.Guid.NewGuid();
            NextPaymentDate = System.DateTime.Now;
            LatePaymentPenalty = 0;
            MonthOwing = 0;
            Balance = 0;
            Amount = 0;
            ReceivedBy = string.Empty;
            Notes = string.Empty;
            PaymentDate = DateTime.Now;
            Branch = string.Empty;
            UserName = string.Empty;
            LatePaymentId = 0;
        }

        public int pkiMemberID { get; set; }
        public string FullNames { get; set; }
        public string IDNumber { get; set; }
        public int LatePaymentId { get; set; }

        public string MemeberNumber { get; set; }
        public bool Active { get; set; }

        public string PolicyStatus { get; set; }
        public Guid ParlourId { get; set; }

        public string PlanName { get; set; }
        public int PolicyLaps { get; set; }
        public string PlanSubscription { get; set; }

        public int Spouse { get; set; }
        public int Children { get; set; }
        public int Adults { get; set; }
        public int WaitingPeriod { get; set; }
        public decimal JoiningFee { get; set; }
        public decimal Cover { get; set; }

        //public Guid ParlourId { get; set; }

        public DateTime NextPaymentDate { get; set; }
        public decimal LatePaymentPenalty { get; set; }
        public int MonthOwing { get; set; }
        public decimal Balance { get; set; }
        public decimal Amount { get; set; }
        public string ReceivedBy { get; set; }
        public string Notes { get; set; }
        public string MethodOfPayment { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Branch { get; set; }
        public string UserName { get; set; }
    }

    public class FuneralPaymentsModel
    {
        public FuneralPaymentsModel()
        {
            FuneralID = 0;
            MemberID = 0;
            InvoiceID =0;
            DatePaid = DateTime.Now;
            AmountPaid =0;
            RecievedBy = string.Empty;
            Notes = string.Empty;
            PaidBy = string.Empty;
            UserName = string.Empty;
            ParlourId = System.Guid.NewGuid();
        }
        public int FuneralID { get; set; }
        public int MemberID { get; set; }
        public int InvoiceID { get; set; }
        public DateTime DatePaid { get; set; }
        public decimal AmountPaid { get; set; }
        public string RecievedBy { get; set; }
        public string PaidBy { get; set; }
        public string Notes { get; set; }
        public string UserName { get; set; }
        public Guid ParlourId { get; set; }
    }
}
