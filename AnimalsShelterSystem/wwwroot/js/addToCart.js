const buyButton = Array.from(document.querySelectorAll('.buy-button'));
buyButton.forEach(b => b.addEventListener('click', addTocart));

console.log(buyButton)
toastr.options = {
    "closeButton": true,
    "debug": false,
    "newestOnTop": false,
    "progressBar": false,
    "positionClass": "toast-top-right",
    "preventDuplicates": true,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "800",
    "onclick": null,
    "onCloseClick": null,
    "extendedTimeOut": 0,
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut",
    "tapToDismiss": false
};


function addTocart(e) {
    const careid = e.currentTarget.getAttribute('careId');
    const productId = e.currentTarget.getAttribute('productId')
    console.log(e.currentTarget.getAttribute('careId'));
    //var data = JSON.stringify({
    //    'productId': e.currentTarget.getAttribute('productId'),
    //    'careId': e.currentTarget.getAttribute('careId')
    //});
   // console.log(data)
    $.ajax({
        type: 'GET',
        
        url: '/Orders/AddToCart/',
        data: {
            "productId":e.currentTarget.getAttribute('productId'),
            "careId": e.currentTarget.getAttribute('careId')
                }
        ,
        success: function (response) {

            toastr.success('Successfully added item to cart!');
           const badge = document.querySelector('.cart-badge');
            const items = Array.from(response.items.map(item => item.quantity));
            let count = items.reduce((sum, current) => sum + current, 0);
            console.log(count);
            badge.textContent = count;


        },
        error: function (response) {
            console.log(response);
        }

    });
}