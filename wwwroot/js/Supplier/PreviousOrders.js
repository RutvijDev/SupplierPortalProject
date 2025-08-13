$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/Supplier/Supplier/GetAllPreviousOrders' },
        "columns": [
            { data: 'supplierOrder.supplierOrderID' },
            {
                data: 'order.orderDate',
                "render": function (data) {
                    // Parse the date
                    const date = new Date(data);

                    // Extract components
                    const year = date.getFullYear();
                    const month = String(date.getMonth() + 1).padStart(2, '0');
                    const day = String(date.getDate()).padStart(2, '0');
                    const hours = String(date.getHours()).padStart(2, '0');
                    const minutes = String(date.getMinutes()).padStart(2, '0');
                    const seconds = String(date.getSeconds()).padStart(2, '0');

                    // Format as 'YYYY-MM-DD hh:mm:ss'
                    const formattedDate = `${year}-${month}-${day} ${hours}:${minutes}:${seconds}`;

                    return formattedDate;
                }
            },
            { data: 'product.productName' },
            { data: 'product.companyName' },
            { data: 'order.quantity' },
            {
                data: 'order.totalPrice',
                "render": function (data) {
                    return `₹ ${data}`;
                }
            },
            { data: 'supplierOrder.supplier_Order_Status' },
            {
                data: 'supplierOrder.supplier_Delivered_Date',
                "render": function (data) {
                    // Parse the date
                    const date = new Date(data);

                    // Extract components
                    const year = date.getFullYear();
                    const month = String(date.getMonth() + 1).padStart(2, '0');
                    const day = String(date.getDate()).padStart(2, '0');
                    const hours = String(date.getHours()).padStart(2, '0');
                    const minutes = String(date.getMinutes()).padStart(2, '0');
                    const seconds = String(date.getSeconds()).padStart(2, '0');

                    // Format as 'YYYY-MM-DD hh:mm:ss'
                    const formattedDate = `${year}-${month}-${day} ${hours}:${minutes}:${seconds}`;

                    return formattedDate;
                }
            },

        ]
    });
}
