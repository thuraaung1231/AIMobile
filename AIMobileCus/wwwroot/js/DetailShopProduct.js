$(".selectItem").click(function (event) {
    var clickedImg = event.target;
    var src = clickedImg.getAttribute('src');

    $("#mainImg").attr("src", src)
})
$("#increaseButton").click(function () {
    var NumberOfItem = parseInt($("#numberOfItem").val());
    var IncreaseItem = ++NumberOfItem;
    $("#numberOfItem").val(IncreaseItem);
    var totalAmount = $("#productPrice").val() * IncreaseItem;
    $("#totalPrice").val(totalAmount)
})
$("#reduceButton").click(function () {
    var NumberOfItem = parseInt($("#numberOfItem").val());
    if (NumberOfItem == 1) {
        return;
    } else {
        var DecreaseItem = --NumberOfItem;
        $("#numberOfItem").val(DecreaseItem);
        var totalAmount = $("#productPrice").val() * DecreaseItem;
        $("#totalPrice").val(totalAmount)
    }
})