using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Funeral.Model.DayPilot
{
    public class EventManager
    {
        private Controller controller;
        private string key;

        public EventManager(Controller controller, string key)
        {
            this.controller = controller;
            this.key = key;

            if (this.controller.Session[key] == null)
            {
                this.controller.Session[key] = generateData();
            }
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

        public EventManager(Controller controller) : this(controller, "default")
        {
        }

        private DataTable generateData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(string));
            dt.Columns.Add("text", typeof(string));
            dt.Columns.Add("start", typeof(DateTime));
            dt.Columns.Add("end", typeof(DateTime));

            dt.PrimaryKey = new DataColumn[] { dt.Columns["id"] };

            DataRow dr;
             
            dr = dt.NewRow();
            dr["id"] = 17;
            dr["start"] = Convert.ToDateTime("8:00:00").AddDays(1);
            dr["end"] = Convert.ToDateTime("8:00:01").AddDays(1);
            dr["text"] = "nd";
            dt.Rows.Add(dr);

            return dt;
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
            DataRow dr = Data.Rows.Find(id);
            if (dr != null)
            {
                dr["start"] = start;
                dr["end"] = end;
                Data.AcceptChanges();
            }
        }

        public Event Get(string id)
        {
            DataRow dr = Data.Rows.Find(id);
            if (dr == null)
            {
                //return new Event();
                return null;
            }
            return new Event()
            {
                Id = (string)dr["id"],
                Text = (string)dr["text"]
            };
        } 
        internal void EventCreate(DateTime start, DateTime end, string text, int funeralId, int userId)
        {
            
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
