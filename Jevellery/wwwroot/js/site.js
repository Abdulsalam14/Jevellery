var header = document.getElementById("header");
var logo = document.getElementById("logo");
var sticky = header.offsetTop + 300;

window.onscroll = function () {
    if (window.pageYOffset > sticky) {
        header.classList.add("sticky");
        logo.style.width = '20%';

    } else {
        header.classList.remove("sticky");
        logo.style.width = '25%';

    }

    if ( document.documentElement.scrollTop > 20) {
        document.getElementById("scrollTopBtn").style.display = "block";
    } else {
        document.getElementById("scrollTopBtn").style.display = "none";
    }
};

$(function () {
    $('[data-toggle="tooltip"]').tooltip()
})

document.getElementById("scrollTopBtn").addEventListener("click", function () {
    document.documentElement.scrollTop = 0;
});