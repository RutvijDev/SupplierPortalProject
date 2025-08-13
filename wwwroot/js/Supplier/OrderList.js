function submitForm(supplierOrderID) {
    console.log("submitForm called for supplierOrderID: " + supplierOrderID);
    var form = document.getElementById(`form-${supplierOrderID}`);
    if (form) {
        console.log("Submitting form:", form);
        form.submit();
    } else {
        console.error("Form not found for supplierOrderID:", supplierOrderID);
    }
}

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    $('#tblData').DataTable({
        "ajax": { url: '/Supplier/Supplier/getallOrderList' },
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
                    return `${year}-${month}-${day} ${hours}:${minutes}:${seconds}`;
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
            {
                data: 'supplierOrder.supplier_Order_Status',
                "render": function (data, type, row) {
                    return `
                        <form class="form-inline" action="/Supplier/Supplier/OrderUpdate" method="post" id="form-${row.supplierOrder.supplierOrderID}">
                            <input type="hidden" value="${row.supplierOrder.supplierOrderID}" name="id" />
                            <select name="status" class="form-select">
                                <option value="Pending" ${data === 'Pending' ? 'selected' : ''}>Pending</option>
                                <option value="Shipped" ${data === 'Shipped' ? 'selected' : ''}>Shipped</option>
                            </select>
                        </form>
                    `;
                }
            },
            {
                data: 'supplierOrder.supplierOrderID',
                "render": function (data, type, row) {
                    return `
                        <div class="w-100 btn-group" role="group">
                            <button type="button" class="btn btn-primary mx-1 py-1" onclick="submitForm('${data}')">
                                <i class="bi bi-pencil-square"></i> Update
                            </button>
                        </div>
                    `;
                }
            },
        ]
    });
}
