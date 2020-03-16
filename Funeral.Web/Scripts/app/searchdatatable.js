var DOMIDs = {
    AddEdit: '',
    List: '',
    Datatable: ''
}
var columnsArray = new Array();
var tableConfigs = {
    table: null,
    SearchUrls: ''
};
var model1 = {
    StatusId: '200',
    SortOrder: 'asc',
    SortBy: '',
    TotalRecord: 0,
    PageNum: 1,
    PageSize: 10,
    SarchText: '',
    BookName: ''
};
function setsearchmodel(statusid, sortorder, sortby, totalrecord, pagenum, pagesize, searchtext, bookName = "") {
    model1.StatusId = statusid;
    model1.SortOrder = sortorder;
    model1.SortBy = sortby;
    model1.TotalRecord = totalrecord;
    model1.PageNum = pagenum;
    model1.PageSize = pagesize;
    model1.SarchText = searchtext;
    model1.BookName = bookName;
}
function InitDataTable() {
    jQuery(document).ready(function () {
        jQuery.noConflict();
        tableConfigs.table = jQuery(jQuery('#' + DOMIDs.Datatable)[0])
            .on('page.dt', function () {
                var table = tableConfigs.table.DataTable();
                var page = table.page.info();
                model1.PageNum = page.page + 1;
            })
            .dataTable({
                'bProcessing': true,
                'bServerSide': true,
                "bLengthChange": false,
                'bFilter': false,
                'ajax': {
                    "url": tableConfigs.SearchUrls,
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        console.log(tableConfigs.SearchUrls);
                        d['order'].forEach(function (item, index) {
                            if (model1.SortBy != d['columns'][item.column]['data']) {
                                model1.PageNum = 1;
                                model1.SortOrder = "asc";
                            }
                            if (model1.SortBy == d['columns'][item.column]['data'] && model1.SortOrder != d['order'][index]['dir']) {
                                model1.PageNum = 1;
                                model1.SortOrder = d['order'][index]['dir'];
                            }
                            model1.SortBy = d['columns'][item.column]['data'];
                            model1.SortOrder = d['order'][index]['dir'];
                        });
                        return JSON.stringify(model1);
                    },
                    dataFilter: function (data) {
                        var json = jQuery.parseJSON(data);
                        json.recordsTotal = json.TotalCount;
                        json.recordsFiltered = json.TotalCount;
                        json.data = json.Result;
                        return JSON.stringify(json);
                    }
                },
                "columns": columnsArray
            });
    })

}

var searchDelegate, pageChangeDelegate;
var searchEvent, pagechangeEvent;

var eventConfigs = {
    pageChange: {
        eventName: '',
        data: {},
        domElement: function (domElement) {
            return domElement;
        },
        dispatchEvent: function () {
            if (this.domElement == undefined)
                this.domElement = new object();

            this.attachEvent();
            this.domElement.dispatchEvent(this.pagechangeEvent);
        },
        action: function (delegate) {
            this.pageChangeDelegate = delegate;
        },
        attachEvent: function () {
            this.pagechangeEvent = new Event(this.eventName);
            this.pagechangeEvent.detail = this.data;

            if (this.domElement == undefined)
                this.domElement = new object();

            if (this.pageChangeDelegate == undefined)
                this.pageChangeDelegate = DelegeateNotAssigned;

            return this.domElement.addEventListener(this.eventName, this.pageChangeDelegate, true);
        }
    },
    searchClick: {
        eventName: '',
        data: {},
        domElement: function (domElement) {
            return domElement;
        },
        dispatchEvent: function () {
            if (this.domElement == undefined)
                this.domElement = new object();

            this.attachEvent();
            this.domElement.dispatchEvent(this.searchEvent);
        },
        action: function (delegate) {
            this.searchDelegate = delegate;
        },
        attachEvent: function () {
            this.searchEvent = new Event(this.eventName);
            this.searchEvent.detail = this.data;

            if (this.domElement == undefined)
                this.domElement = new object();

            if (this.searchDelegate == undefined)
                this.searchDelegate = DelegeateNotAssigned;

            return this.domElement.addEventListener(this.eventName, this.searchDelegate, true);
        }
    }
}

function DelegeateNotAssigned() {
    alert("Deligate is Not Assigned!");
}