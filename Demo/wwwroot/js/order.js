var dataTable;
$(document).ready(function () {
    var url = window.location.search;
    var status = "all"; // 預設為 "all"
    if (url.includes("Processing")) status = "Processing";
    else if (url.includes("Pending")) status = "Pending";
    else if (url.includes("Ready")) status = "Ready";
    else if (url.includes("Completed")) status = "Completed";
    else if (url.includes("Cancelled")) status = "Cancelled";

    loadDataTable(status);
});


function loadDataTable(status) {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: '/admin/order/getall?status='+status
        },
        "columns": [
            { data: 'id', "width": "10%" },
            { data: 'name', "width": "15%" },
            { data: 'phoneNumber', "width": "20%" },
            { data: 'applicationUser.email', "width": "20%" },
            { data: 'orderStatus', "width": "10%" },
            { data: 'orderTotal', "width": "10%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                        <a href="/admin/order/details?orderId=${data}" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i></a>
                        
                    </div>`
                },
                "width":"15%"
            }
        ]
    });
}

