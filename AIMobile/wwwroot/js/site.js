// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

    let CollapseButton = $(".CollapseButton");
    let CollapseArrow = $(".CollapseArrow");
    let collapseItem = $(".collapseItem");
    for (let i = 0; i < CollapseButton.length; i++) {
        CollapseButton.eq(i).on("click", function () {
            CollapseArrow.eq(i).toggleClass("rotate");
        });
        }
    for (let i = 0; i < collapseItem.length; i++) {
        collapseItem.eq(i).on("click", function () {
            removingClass();
            $(this).addClass("CollapseBg");
            let title = $(this).closest(".multi-collapse");
            let category = $(this).text();
            $("#Title").text(title.attr("id"));
            $("#Category").text(category);
            $(this).children().first().addClass("text-danger");
        });
        }
    function removingClass() {
            for (let i = 0; i < collapseItem.length; i++) {
        collapseItem.eq(i).removeClass("CollapseBg");
    collapseItem.eq(i).children().first().removeClass("text-danger");
            }
        }
    $("#FullScreen").on("click", function () {
        toggleFullScreen();
        });
    function toggleFullScreen() {
            if (!document.fullscreenElement) {
        document.documentElement.requestFullscreen();
            } else {
                if (document.exitFullscreen) {
        document.exitFullscreen();
                }
            }
        }

    let adjustWidth = true;
    $("#adjustNav").on("click", function () {
            if (adjustWidth) {
        $(".SideNav").removeClass("col-lg-2").addClass("col-lg-1");
    $("#adjustNav").removeClass("col-2").addClass("col-6");
    $("#adjustLogo").removeClass("col-10").addClass("col-6");
    $("#MainNav").removeClass("col-lg-10").addClass("col-lg-11");
    $(".navText").addClass("d-none");
    adjustWidth = false;
            } else {
        $(".SideNav").removeClass("col-lg-1").addClass("col-lg-2");
    $("#MainNav").removeClass("col-lg-11").addClass("col-lg-10");
    $(".navText").removeClass("d-none");
    $("#adjustNav").removeClass("col-6").addClass("col-2");
    $("#adjustLogo").removeClass("col-6").addClass("col-10");
    adjustWidth = true;
            }
        });
