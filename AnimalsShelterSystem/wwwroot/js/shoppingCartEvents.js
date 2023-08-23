const plusButton = document.querySelectorAll('.plus-button');
plusButton.forEach(b => b.addEventListener('click', increaseNumberInCart));


const minusButton = document.querySelectorAll('.minus-button');
minusButton.forEach(b => b.addEventListener('click', decreaseNumberInCart));

const deleteButton = document.querySelectorAll('.delete-button');
deleteButton.forEach(b => b.addEventListener('click', deleteItemFromCart));



function increaseNumberInCart(e) {
    e.stopPropagation();
    const productId = e.currentTarget.getAttribute('productId');
    const careId = e.currentTarget.getAttribute('careId');
    const minusButton = document.querySelector(`.minus-button[productId='${productId}'][careId='${careId}']`);
    const quantity = document.querySelector(`.product-quantity[productId='${productId}'][careId='${careId}']`);
    const hiddenQuantity = document.querySelector(`.product-quantity-hidden[productId='${productId}'][careId='${careId}']`);
   

    const totalForThisItem = document.querySelector(`.product-total[productId='${productId}'][careId='${careId}']`);
  
   
    
    //var data = JSON.stringify({
    //    'productId': e.currentTarget.getAttribute('productId'),
    //    'careId': e.currentTarget.getAttribute('careId')
    //});
    // console.log(data)
    $.ajax({
        type: 'GET',

        url: '/Orders/AddToCart/',
        data: {
            "productId": e.currentTarget.getAttribute('productId'),
            "careId": e.currentTarget.getAttribute('careId')
        }
        ,
        success: function (response) {
            document.getElementById('total').textContent = response.total.toFixed(2);
            document.getElementById('total-items').textContent = response.totalItems;
            
            const newValues = Array.from(response.items).filter(i => i.id === productId && Number(i.careId)===Number(careId))[0];
            const badge = document.querySelector('.cart-badge');
           
            minusButton.disabled = false;
            quantity.textContent = newValues.quantity;
            hiddenQuantity.value = newValues.quantity;

            totalForThisItem.textContent = (newValues.quantity * newValues.price).toFixed(2);

           
            const cartItems = Array.from(response.items.map(item => item.quantity));
            let count = cartItems.reduce((sum, current) => sum + current, 0);
            badge.textContent = count > 0 ? count : '';





        },
        error: function (response) {
            console.log(response);
        }

    });
}

function decreaseNumberInCart(e) {
    e.stopPropagation();
    const productId = e.currentTarget.getAttribute('productId');
    const careId = e.currentTarget.getAttribute('careId');
    const minusButton = document.querySelector(`.minus-button[productId='${productId}'][careId='${careId}']`);
    const quantity = document.querySelector(`.product-quantity[productId='${productId}'][careId='${careId}']`);
    const hiddenQuantity = document.querySelector(`.product-quantity-hidden[productId='${productId}'][careId='${careId}']`);


    const totalForThisItem = document.querySelector(`.product-total[productId='${productId}'][careId='${careId}']`);

    if (quantity.value < 2) {
        return;
    }

    $.ajax({
        type: 'GET',
       /* dataType: 'JSON',*/
        url: '/Orders/RemoveFromCart/',
        data: {
            "productId": e.currentTarget.getAttribute('productId'),
            "careId": e.currentTarget.getAttribute('careId')
        }
        ,
        success: function (response) {
            document.getElementById('total').textContent = response.total.toFixed(2);
            document.getElementById('total-items').textContent = response.totalItems;

            const newValues = Array.from(response.items).filter(i => i.id === productId && Number(i.careId) === Number(careId))[0];
            

            quantity.textContent = newValues.quantity;
            hiddenQuantity.value = newValues.quantity;

            if (newValues.textContent === 1) {
                minusButton.disabled = true;
            }

            totalForThisItem.textContent = (newValues.quantity * newValues.price).toFixed(2);

            const badge = document.querySelector('.cart-badge');
            const cartItems = Array.from(response.items.map(item => item.quantity));
            let count = cartItems.reduce((sum, current) => sum + current, 0);
            badge.textContent = count > 0 ? count : '';

        },
        error: function (response) {
            console.log(response);
        }

    });
}

function deleteItemFromCart(e) {
    e.stopPropagation();

    const productId = e.currentTarget.getAttribute('productId');
    const careId = e.currentTarget.getAttribute('careId');
    //const productRow = e.currentTarget.parentElement.parentElement;
    const shoppingCartItems = document.querySelector('main[role="main"]');

    $.ajax({
        type: 'GET',
        url: '/Orders/DeleteFromCart/',
        data: { "productId": productId,"careId":careId }
        ,
        success: function (response) {

            const items = document.querySelector('#cart-items');
            items.innerHTML = response;

            const badge = document.querySelector('.cart-badge');
            const count = Array.from(document.querySelectorAll('.product-quantity')).map(e => e.textContent).reduce((sum, current) => Number(sum) + Number(current), 0);
            badge.textContent = count > 0 ? count : '';




        },
        error: function (response) {
            console.log(response);
        }

    });
}