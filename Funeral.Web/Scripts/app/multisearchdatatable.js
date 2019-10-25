var datatables = new Array();

var datatableConfigs = function () {
    return {
        columnsArray: new Array(),
        DOMIDs: {
            AddEdit: '',
            List: '',
            Datatable: ''
        },
        tableConfigs: {
            Name:'',
            table: null,
            SearchUrls: ''
        },
        model1: {
            StatusId: '200',
            SortOrder: 'asc',
            SortBy: '',
            TotalRecord: 0,
            PageNum: 1,
            PageSize: 10,
            SarchText: ''
        },
        setsearchmodel: function (statusid, sortorder, sortby, totalrecord, pagenum, pagesize, searchtext) {
            this.model1.StatusId = statusid;
            this.model1.SortOrder = sortorder;
            this.model1.SortBy = sortby;
            this.model1.TotalRecord = totalrecord;
            this.model1.PageNum = pagenum;
            this.model1.PageSize = pagesize;
            this.model1.SarchText = searchtext;
        },
        InitDataTable: function (tableName) {            
            var matchedDatatable = datatables.find(x => { return x.tableConfigs.Name == tableName });
            //jQuery(document).ready(function () {
                jQuery.noConflict();
                matchedDatatable.tableConfigs.table = jQuery(jQuery('#' + matchedDatatable.DOMIDs.Datatable)[0])
                    .on('page.dt', function () {
                        var table = matchedDatatable.tableConfigs.table.DataTable();
                        var page = table.page.info();
                        matchedDatatable.model1.PageNum = page.page + 1;
                    })
                    .dataTable({
                        'bProcessing': true,
                        'bServerSide': true,
                        "bLengthChange": false,
                        'bFilter': false,
                        'ajax': {
                            "url": matchedDatatable.tableConfigs.SearchUrls,
                            "type": "POST",
                            "contentType": "application/json",
                            "data": function (d) {                                
                                console.log(matchedDatatable.tableConfigs.SearchUrls);
                                d['order'].forEach(function (item, index) {
                                    if (matchedDatatable.model1.SortBy != d['columns'][item.column]['data']) {
                                        matchedDatatable.model1.PageNum = 1;
                                        matchedDatatable.model1.SortOrder = "asc";
                                    }
                                    if (matchedDatatable.model1.SortBy == d['columns'][item.column]['data'] && matchedDatatable.model1.SortOrder != d['order'][index]['dir']) {
                                        matchedDatatable.model1.PageNum = 1;
                                        matchedDatatable.model1.SortOrder = d['order'][index]['dir'];
                                    }
                                    matchedDatatable.model1.SortBy = d['columns'][item.column]['data'];
                                    matchedDatatable.model1.SortOrder = d['order'][index]['dir'];
                                });
                                return JSON.stringify(matchedDatatable.model1);
                            },
                            dataFilter: function (data) {
                                var json = jQuery.parseJSON(data);
                                json.recordsTotal = json.TotalCount;
                                json.recordsFiltered = json.TotalCount;
                                json.data = json.Result;
                                return JSON.stringify(json);
                            }
                        },
                        "columns": matchedDatatable.columnsArray
                    });
            //})

        },
        eventConfigs: {
            pageChange: {
                pagechangeEvent: {},
                pageChangeDelegate: {},
                eventName: '',
                data: {},
                domElement: function (domElement) {
                    alert(this.pagechangeEvent);
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
                searchEvent: {},
                searchDelegate: {},
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
    }
};

//var y = new datatableConfigs();
//y.DOMIDs.List = 'FirstTableList';
//y.DOMIDs.AddEdit = 'FirstTableAddEdit';
//y.setsearchmodel("201", "desc", "column2", 74, 2, 15, 'Rami');
//y.eventConfigs.pageChange.pagechangeEvent = 'FirstTablePageChangeEvent';
//y.eventConfigs.pageChange.domElement('FirstTableDomElement');
//y.InitDataTable();
//datatables.push(y);

//y = new datatableConfigs();
//y.DOMIDs.List = 'SecondTableList';
//y.DOMIDs.AddEdit = 'SecondTableAddEdit';
//y.setsearchmodel("301", "desc", "column3", 75, 3, 16, 'Rami3');
//y.eventConfigs.pageChange.pagechangeEvent = 'SecondTablePageChangeEvent';
//y.eventConfigs.pageChange.domElement('SecondTableDomElement');
//y.InitDataTable();
//datatables.push(y);

//debugger;
//var matchedDatatable = datatables.find(x => { return x.DOMIDs.List = 'FirstTable' });
//console.log(matchedDatatable);

//datatableConfigs.DOMIDs.AddEdit = 'Test AddEdit';
//var l = datatableConfigs.DOMIDs.AddEdit;
//alert(l);

function DelegeateNotAssigned() {
    alert("Deligate is Not Assigned!");
}