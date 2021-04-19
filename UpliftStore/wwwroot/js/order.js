var dataTable;

$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("approved")) {
        loadDataTable("/api/orders/approved");
    }
    else if (url.includes("pending")) {
        loadDataTable("/api/orders/pending");
    }
    else {
        loadDataTable("/api/orders");
    }
});

function loadDataTable(url) {
    dataTable = $("#tblData").DataTable({
        "ajax": {
            "url": url,
            "type": "GET",
            "dataType": "json"
        },
        "columnDefs": [
            { targets: [1,3,4], className: 'dt-body-right' }
        ],
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "phoneNumber", "width": "20%" },
            { "data": "email", "width": "15%" },
            { "data": "quantityServices", "width": "15%" },
            { "data": "status", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/Order/Details/${data}" class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                                    <i class="far fa-edit"></i> &nbsp; Details
                                </a>
                            </div>`;
                },
                "width": "15%"
            }
        ],
        "language": {
            "emptyTable": "No data found."
        },
        "width": "100%"
    });
}
