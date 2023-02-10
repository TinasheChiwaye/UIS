using Funeral.BAL;
using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Funeral.Web.DayPilot
{
    public class EventManager
    {
        private Controller controller;
        private string key;
        private int _funeralId;

        public EventManager(Controller controller, string key, int funeralId)
        {
            this.controller = controller;
            this.key = key;
            this._funeralId = funeralId;
            this.controller.Session[key] = generateData();
        } 
        public DataTable Data
        {
            get { return (DataTable)controller.Session[key]; }
        } 
        public DataTable FilteredData(DateTime start, DateTime end, string keyword)
        {
            string where = String.Format("NOT (([end] <= '{0:s}') OR ([start] >= '{1:s}')) and [text] like '%{2}%'", start, end, keyword);
            DataRow[] rows = Data.Select(where);
            DataTable filtered = Data.Clone();

            foreach (DataRow r in rows)
            {
                filtered.ImportRow(r);
            }

            return filtered;
        } 
        public EventManager(Controller controller, int funeralId) : this(controller, "default", funeralId)
        {
            _funeralId = funeralId;
        } 
        private DataTable generateData()
        {  
            var data = FuneralBAL.GetFuneralScheduleEvents(_funeralId);

            return data;
        } 
        public void EventEdit(string id, string name)
        {
            DataRow dr = Data.Rows.Find(id);
            if (dr != null)
            {
                dr["text"] = name;
                Data.AcceptChanges();
            }
        } 
        public void EventMove(string id, DateTime start, DateTime end)
        {
            DataRow dr = Data.AsEnumerable().Where(row => row.Field<Int64>("id") == Convert.ToInt64(id)).CopyToDataTable().Rows[0];
            if (dr != null)
            {
                dr["start"] = start;
                dr["end"] = end;
                FuneralBAL.FuneralScheduleEditEvent(start, end, Convert.ToInt32(id));
                Data.AcceptChanges(); 
                this.controller.Session[key] = generateData();
            }  
        }
        public Event Get(string id)
        {
            DataRow dr = Data.AsEnumerable().Where(row => row.Field<Int64>("id") == Convert.ToInt64(id)).CopyToDataTable().Rows[0];

            if (dr == null)
            {
                //return new Event();
                return null;
            }
            return new Event()
            {
                Id = (string)dr["id"].ToString(),
                Text = (string)dr["text"]
            };
        }
        public void EventCreate(DateTime start, DateTime end, string text, int funeralId, int userId)
        {
            FuneralBAL.FuneralScheduleAddEvent(funeralId, userId, text, start, end);
        } 
        public class Event
        {
            public string Id { get; set; }
            public string Text { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
        }

        public void EventDelete(string id)
        {
            DataRow dr = Data.Rows.Find(id);
            if (dr != null)
            {
                Data.Rows.Remove(dr);
                Data.AcceptChanges();
            }
        }
    }
}
