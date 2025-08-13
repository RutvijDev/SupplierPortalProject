$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/Supplier/Supplier/GetAllProductData' },
        "columns": [
            { data: 'productID' },
            { data: 'productName' },
            { data: 'companyName' },
            { data: 'productCategory' },
            {
                data: 'productPrice',
                "render": function (data) {
                    return `₹ ${data}`;
                }
            },
            { data: "productQuantity" },
            {
                data: 'productImage',
                "render": function (data) {
                    return `<img src="${data}" alt="Product Image" style="width: 50%; height: 50%;" />`;
                }
            },
            { data: 'productStatus' },
            {
                data: 'productID',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                            <a href="/Supplier/Supplier/Edit?id=${data}" class="btn btn-primary mx-1 py-1" > <i class="bi bi-pencil-square"></i> Edit</a >
                            <a href="/Supplier/Supplier/Delete?id=${data}" class="btn btn-danger mx-1 py-1"><i class="bi bi-trash3-fill"></i> Delete</a>
                            </div > `
                }
            },
        ]
    });
}
