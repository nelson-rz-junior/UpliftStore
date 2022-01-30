var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $("#tblData").DataTable({
        "ajax": {
            "url": "/api/webimages",
            "type": "GET",
            "dataType": "json"
        },
        "columnDefs": [
            {
                targets: [2],
                className: "dt-valign-middle"
            }
        ],
        "columns": [
            {
                "data": "name",
                "width": "50%"
            },
            {
                "data": "picture",
                "width": "25%",
                "render": function (data) {
                    return `<div class="text-center">
                                <img src="data:image/jpeg;base64, ${data}" height="50%" width="50%" />
                            </div>`;
                }
            },
            {
                "data": "id",
                "width": "25%",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/WebImage/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                                    <i class="far fa-edit"></i> &nbsp; Edit
                                </a>
                                &nbsp;
                                <a onclick="Delete('/api/webimage/${data}')" class="btn btn-danger text-white" style="cursor:pointer; width:100px;">
                                    <i class="far fa-trash-alt"></i> &nbsp; Delete
                                </a>
                            </div>`;
                }
            }
        ],
        "language": {
            "emptyTable": "No data found."
        },
        "width": "100%"
    });
}

function Delete(url) {
    swal({
        title: "Are you sure you want to delete?",
        text: "You will not be able to restore the data!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    })
    .then((willDelete) => {
        if (willDelete) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}
