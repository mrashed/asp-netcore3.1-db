var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/book",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "author", "width": "20%" },
            { "data": "isbn", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="/BookList/Edit?id=${data}" class='btn btn-success btn-sm text-white' 
                                                            style='cursor:pointer; width:70px;'>
                            Edit
                        </a>
                        <a href="/BookList/Upsert?id=${data}" class='btn btn-secondary btn-sm text-white' 
                                                              style='cursor:pointer; width:70px;'>
                            Upsert
                        </a>
                        &nbsp;
                        <a class='btn btn-danger text-white' style='cursor:pointer; width:70px;'
                            onclick=Delete('/api/book?id='+${data})>
                            Delete
                        </a>
                        </div>`;
                }, "width": "40%"
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    });
}

function Delete(url) {
    // Sweet alert
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover",
        icon: "warning",
        buttons: true,          // Adds cancel button.
        dangerMode: true
    }).then((willDelete) => {                   // This is a response to a promise
        if (willDelete) {
            $.ajax({
                type: "DELETE",                 // Which method to use in the controller
                url: url,                       // URL to the controller of the RESTful api
                success: function (data) {      // I think this is the function called if the ajax is successful
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