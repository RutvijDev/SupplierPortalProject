function submitForm(orderID) {
    console.log("submitForm called for orderID: " + orderID);
    var form = document.getElementById(`form-${orderID}`);
    if (form) {
        console.log("Submitting form:", form);
        form.submit();
    } else {
        console.error("Form not found for orderID:", orderID);
    }
}


$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/DeliveryPerson/DeliveryPerson/GetAllOrderForDelivery' },
        "columns": [
            { data: 'order.orderID' },
            { data: 'order.customerName' },
            { data: 'product.productName' },
            { data: 'product.companyName' },
            { data: 'order.quantity' },
            {
                data: 'order.totalPrice',
                "render": function (data) {
                    return `₹ ${data}`;
                }
            },
            { data: 'order.shippingAddress' },
            {
                data: 'order.orderStatus',
                "render": function (data, type, row) {
                    return `
                        <form class="form-inline" action="/DeliveryPerson/DeliveryPerson/OrderStatusUpdate" method="post" id="form-${row.order.orderID}">
                            <input type="hidden" value="${row.order.orderID}" name="id" />
                            <select name="status" class="form-select">
                                <option value="Shipped" ${data === 'Shipped' ? 'selected' : ''}>Shipped</option>
                                <option value="Delivered" ${data === 'Delivered' ? 'selected' : ''}>Delivered</option>
                            </select>
                        </form>
                    `;
                }
            },
            {
                data: 'order.orderID',
                "render": function (data, type, row) {
                    return `
                        <td>
                            <div class="w-100 btn-group" role="group">
                                <button type="button" class="btn btn-primary mx-1 py-1" onclick="submitForm('${data}')">
                                    <i class="bi bi-pencil-square"></i> Update
                                </button>
                            </div>
                        </td>
                    `;
                }
            }

        ]
    });
}
