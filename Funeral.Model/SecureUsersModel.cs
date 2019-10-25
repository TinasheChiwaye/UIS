using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
   public class SecureUsersModel
    {
       public SecureUsersModel()
        {
            UserName = string.Empty;
           UserName = string.Empty;
           Password = string.Empty;
          // parlourid = string.Empty;
           EmployeeSurname = string.Empty;
           EmployeeFullname = string.Empty;
           EmployeeIDNumber = string.Empty;
           EmployeeContactNumber = string.Empty;
           EmployeeAddress1 = string.Empty;
           EmployeeAddress2 = string.Empty;
           EmployeeAddress3 = string.Empty;
           EmployeeAddress4 = string.Empty;
           LastModified = System.DateTime.Now;
           ModifiedUser = string.Empty;
           Email = string.Empty;
           Code = string.Empty;
           AgentName = string.Empty;
           AgentSurname = string.Empty;

        }
       public int PkiUserID { get; set; }
       public string UserName { get; set; }
       public string Password { get; set; }
       public Guid parlourid { get; set; }
       public string EmployeeSurname { get; set; }
       public string EmployeeFullname { get; set; }
       public string EmployeeIDNumber { get; set; }
       public string EmployeeContactNumber { get; set; }
       public string EmployeeAddress1 { get; set; }
       public string EmployeeAddress2 { get; set; }
       public string EmployeeAddress3 { get; set; }
       public string EmployeeAddress4 { get; set; }
       public DateTime LastModified { get; set; }
       public string ModifiedUser { get; set; }
       public string Email { get; set; }
       public string Code { get; set; }
       public string AgentName { get; set; }
       public string AgentSurname { get; set; }
       public int CustomId1 { get; set; }
       public int CustomId2 { get; set; }
       public int CustomId3 { get; set; }
       public Guid BranchId{ get; set; }
        public bool Active { get; set; }
    }
}