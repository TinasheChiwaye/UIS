using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Funeral.BAL
{
    public class FuneralHelper
    {
        public static List<T> DataReaderMapToList<T>(IDataReader dr, bool isNextResult = false)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (HasColumn(dr, prop.Name))
                    {
                        if (!object.Equals(dr[prop.Name], DBNull.Value))
                        {
                            prop.SetValue(obj, dr[prop.Name], null);
                        }
                    }
                }
                list.Add(obj);
            }
            if (!dr.IsClosed && isNextResult == false)
            {
                dr.Close();
                dr.Dispose();
            }
            return list;
        }

        public static List<T> DataTableMapToList<T>(DataTable dt, bool isNextResult = false)
        {
            List<T> list = new List<T>();
            T obj = default(T);

            foreach (DataRow dr in dt.Rows)
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (HasColumn(dt, prop.Name))
                    {
                        if (!object.Equals(dr[prop.Name.ToString()], DBNull.Value))
                        {
                            prop.SetValue(obj, dr[prop.Name], null);
                        }
                    }
                }
                list.Add(obj);
            }


            return list;
        }

        public static bool HasColumn(IDataReader Reader, string ColumnName)
        {
            foreach (System.Data.DataRow row in Reader.GetSchemaTable().Rows)
            {
                if (row["ColumnName"].ToString() == ColumnName)
                    return true;
            } //Still here? Column not found. 
            return false;
        }

        public static bool HasColumn(DataTable Reader, string ColumnName)
        {
            foreach (DataColumn row in Reader.Columns)
            {
                if (row.ColumnName.ToString() == ColumnName)
                    return true;
            } //Still here? Column not found. 
            return false;
        }
    }
}
