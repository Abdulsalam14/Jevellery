var header = document.getElementById("header");
var logo = document.getElementById("logo");
var sticky = header.offsetTop + 300; // Header'ın 100px geçildiğinde sticky olması için gereken mesafe

window.onscroll = function () {
    if (window.pageYOffset > sticky) {
        header.classList.add("sticky");
        logo.style.width = '20%';

    } else {
        header.classList.remove("sticky");
        logo.style.width = '25%';

    }
};

$(function () {
    $('[data-toggle="tooltip"]').tooltip()
})