$(".SelectProduct").on('click', function () {
    
    var ImageAndProductId = {};
    ImageAndProductId.ProductId = $(this).attr("id");
    ImageAndProductId.ImageId = $(this).children().first().attr("id")
    var url = "/ShopProduct/ViewShopProduct";
    $.ajax({
        type: 'POST',
        url: url,
        data: ImageAndProductId,
        success: function (response) {
            var data = JSON.stringify(response);
           
            if (data != null) {
                window.location.href = "/ShopProduct/DetailProduct?shopProductData=" + encodeURIComponent(JSON.stringify(response));
            }

        },
        failure: function (error) {
            console.log(error);
        }
    })
})
