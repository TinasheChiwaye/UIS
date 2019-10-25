using Funeral.DAL;
using Funeral.Model;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace Funeral.BAL
{
    public class CustomDetailsBAL
    {
        public static int CustomDetailsSave(CustomDetails model)
        {
            return CustomDetailsDAL.CustomDetailsSave(model);
        }

        public static void CustomDetailsUpdate(CustomDetails model)
        {
            CustomDetailsDAL.CustomDetailsUpdate(model);

        }

        public static void CustomDetailsDelete(CustomDetails model)
        {
            CustomDetailsDAL.CustomDetailsDelete(model);
        }

        public static CustomDetails GetCustomDetails(int Id, Guid ParlourId, int CustomType)
        {
            DataTable dr = CustomDetailsDAL.GetCustomDetailsdt(Id, ParlourId, CustomType);
            var model = FuneralHelper.DataTableMapToList<CustomDetails>(dr).FirstOrDefault();
            model.CustomType = (CustomDetailsEnums.CustomDetailsType)CustomType;
            return model;
        }

        public static List<CustomDetails> GetAllCustomDetailsByParlourId(Guid ParlourId, int CustomType)
        {
            DataTable dr = CustomDetailsDAL.GetAllCustomDetailsByParlourIddt(ParlourId, CustomType);
            List<CustomDetails> model = FuneralHelper.DataTableMapToList<CustomDetails>(dr);
            foreach (var item in model)
            {
                item.CustomType = (CustomDetailsEnums.CustomDetailsType)CustomType;
            }
            return model;
        }
    }
}
