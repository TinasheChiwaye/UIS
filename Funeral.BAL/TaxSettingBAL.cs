using Funeral.DAL;
using Funeral.Model;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Funeral.BAL
{
    public class TaxSettingBAL
    {
        public static List<TaxSetting> GetAllTaxSettings()
        {
            DataTable dr = TaxSettingDAL.tblRightGetAlldt();
            return FuneralHelper.DataTableMapToList<TaxSetting>(dr).ToList();
        }
        public static int InsertRecordForTax(TaxSetting ModelTax)
        {
            return TaxSettingDAL.InsertRecordForTax(ModelTax);
        }
        
        public static TaxSetting GetTaxSettingById(int id)
        {
            DataTable dr = TaxSettingDAL.GetTaxByIddt(id);
            return FuneralHelper.DataTableMapToList<TaxSetting>(dr).FirstOrDefault();
        }

        public static int DeleteTaxSettingById(int ID)
        {
            return TaxSettingDAL.DeleteTaxSettingById(ID);
        }
        public static int UpdateRecordForTax(TaxSetting ModelTax)
        {
            return TaxSettingDAL.UpdateRecordForTax(ModelTax);
        }
    }
}
