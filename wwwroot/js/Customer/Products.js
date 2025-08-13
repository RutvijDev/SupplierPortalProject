$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/customer/customer/getallproducts' },
        "columns": [
            { data: 'productName', "width": "19%" },
            { data: 'companyName', "width": "14%" },
            { data: 'productCategory', "width": "13%" },
            {
                data: 'productPrice',
                "render": function (data) { 
                    return `₹ ${data}`;
                },
                "width": "11%"
            },
            {
                data: 'productImage',
                "render": function (data) {
                    return `<img src="${data}" alt="Product Image" style="width: 50%; height: 50%;" />`;
                },
                "width": "14%"
            },
            { data: 'productStatus', "width": "11%" },
            {
                data: 'productID',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                            <a href="/customer/customer/ViewProduct?id=${data}" class="btn btn-primary mx-1 py-1"><i class="bi bi-eye"></i> View</a>
                            <a href="/customer/customer/BuyProduct?id=${data}" class="btn btn-primary mx-1 py-1">Buy Now</a>
                            </div > `
                },
                "width": "16%"
            },
        ]
    });
}
