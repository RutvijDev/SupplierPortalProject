$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/admin/getallwarehouselist' },
        "columns": [
            { data: 'wareHouseID', "width": "10%" },
            { data: 'wareHouseName', "width": "37%" },
            { data: 'wareHouseAddress', "width": "37%" },
            {
                data: 'wareHouseID',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                            <a href="/admin/admin/UpdateWareHouse?id=${data}" class="btn btn-primary mx-1 py-1" > <i class="bi bi-pencil-square"></i> Edit</a >
                            <a href="/admin/admin/DeleteWareHouse?id=${data}" class="btn btn-danger mx-1 py-1"><i class="bi bi-trash3-fill"></i> Delete</a>
                            </div > `
                },
                "width": "16%"
            },
        ]
    });
}
