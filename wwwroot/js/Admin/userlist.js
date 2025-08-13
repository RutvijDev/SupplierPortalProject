$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url:'/admin/admin/getalluserlist'},
        "columns": [
            { data: 'id', "width": "3%" },
            { data: 'userName', "width": "14%" },
            { data: 'fullName', "width": "18%" },
            { data: 'email', "width": "20%" },
            { data: 'phoneNumber', "width": "14%" },
            { data: 'role', "width": "15%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                            <a href="/admin/admin/edituser?id=${data}" class="btn btn-primary mx-1 py-1" > <i class="bi bi-pencil-square"></i> Edit</a >
                            <a href="/admin/admin/delete?id=${data}" class="btn btn-danger mx-1 py-1"><i class="bi bi-trash3-fill"></i> Delete</a>
                            </div > `
                },
                "width": "16%"
            },
        ]   
    });
}
