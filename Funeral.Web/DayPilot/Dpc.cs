using DayPilot.Web.Mvc;
using DayPilot.Web.Mvc.Enums;
using DayPilot.Web.Mvc.Events.Calendar;
using System;
using System.Data;
using System.Linq;

namespace Funeral.Web.DayPilot
{
    public class Dpc : DayPilotCalendar
    {
        //custom variables
        private int _FuneralId { get; set; }
        private int _userId { get; set; }

        public Dpc(int? FuneralId, int userId)
        {
            _FuneralId = FuneralId.HasValue ? FuneralId.Value : 0;
            _userId = userId;
        }
        protected override void OnTimeRangeSelected(TimeRangeSelectedArgs e)
        {
            string name = (string)e.Data["name"];
            if (string.IsNullOrEmpty(name))
            {
                name = "(default)";
            }
            new EventManager(Controller, _FuneralId).EventCreate(e.Start, e.End, name, _FuneralId, _userId);
            Update();
        }

        protected override void OnEventMove(EventMoveArgs e)
        {
            if (new EventManager(Controller, _FuneralId).Get(e.Id) != null)
            {
                new EventManager(Controller, _FuneralId).EventMove(e.Id, e.NewStart, e.NewEnd);
                Update();
            }
        }

        protected override void OnEventClick(EventClickArgs e)
        {

        }

        protected override void OnEventResize(EventResizeArgs e)
        {
            new EventManager(Controller, _FuneralId).EventMove(e.Id, e.NewStart, e.NewEnd);
            Update();
        }

        private int i = 0;
        protected override void OnBeforeEventRender(BeforeEventRenderArgs e)
        {
            if (Id == "dpc_customization")
            {
                // alternating color
                int colorIndex = i % 4;
                string[] backColors = { "#FFE599", "#9FC5E8", "#B6D7A8", "#EA9999" };
                string[] borderColors = { "#F1C232", "#3D85C6", "#6AA84F", "#CC0000" };
                e.BackgroundColor = backColors[colorIndex];
                e.BorderColor = borderColors[colorIndex];
                i++;
            }
        }

        protected override void OnCommand(CommandArgs e)
        {
            switch (e.Command)
            {
                case "navigate":
                    StartDate = (DateTime)e.Data["start"];
                    Update(CallBackUpdateType.Full);
                    break;

                case "refresh":
                    Update();
                    break;

                case "previous":
                    StartDate = StartDate.AddDays(-7);
                    Update(CallBackUpdateType.Full);
                    break;

                case "next":
                    StartDate = StartDate.AddDays(7);
                    Update(CallBackUpdateType.Full);
                    break;

                case "today":
                    StartDate = DateTime.Today;
                    Update(CallBackUpdateType.Full);
                    break;

            }
        }

        protected override void OnInit(InitArgs initArgs)
        {
            Update(CallBackUpdateType.Full);
        }

        protected override void OnFinish()
        {
            if (UpdateType == CallBackUpdateType.None)
            {
                return;
            }
            Events = new EventManager(Controller, _FuneralId).Data.AsEnumerable();

            DataStartField = "start";
            DataEndField = "end";
            DataTextField = "text";
            DataIdField = "id";
        }
    }
}
