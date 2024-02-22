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

    if (document.documentElement.scrollTop > 20) {
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


let overlay = document.getElementById("cart-overlay");
let cart = document.getElementById("cart");
function showCart() {
    overlay.style.display = "flex";
    cart.style.transform = "translateX(0%)";
    document.body.style.overflow = 'hidden';
}

function closeCart() {
    overlay.style.display = "none";
    cart.style.transform = "translateX(135%)";
    document.body.style.overflow = 'initial';
}

function addToCart(productId) {
    $.ajax({
        url: `/Cart/Add?productId=${productId}`,
        type: 'GET',
        success: function (data) {
            getCartProducts();
        },

        error: function () {
            alert('Error while getting user info');
        }
    });
}

function getCartProducts() {
    $.ajax({
        url: `/Cart/GetCartProducts`,
        type: 'GET',
        success: function (data) {
            $("#cart-content").html(data);
            let count = $("#cartItem-count").html() ? $("#cartItem-count").html() : 0;
            let sum = $("#cart-sum").html() ? $("#cart-sum").html() : "$0.00";
            $("#cart-item-count").html(count);
            console.log(sum);
            $(".header-cart-sum").html(sum);
        },

        error: function (xhr) {
            if (!xhr.status === 401) {
                alert('Error while getting cart products. Please try again later.');
            }
        }
    });
}

function removeItem(id) {
    $.ajax({
        url: `/Cart/Remove?id=${id}`,
        type: 'GET',
        success: function (data) {
            getCartProducts();
        },

        error: function (xhr) {
            if (!xhr.status === 401) {
                alert('Error while getting cart products. Please try again later.');
            }
        }
    });
}

function plusCartItem(id) {
    $.ajax({
        url: `/Cart/PlusCartItem?id=${id}`,
        type: 'GET',
        success: function (data) {
            getCartProducts();
        },

        error: function (xhr) {
            if (!xhr.status === 401) {
                alert('Error while getting cart products. Please try again later.');
            }
        }
    });
}


var timer;

function inputFunc(event, id) {
    clearTimeout(timer);


    timer = setTimeout(function () {
        var newValue = parseInt(event.target.value);
        if (!isNaN(newValue)) {
            $("#cart-content").css("opacity", .5);
            if (newValue > 0) {
                $.ajax({
                    url: `/Cart/UpdateCartItem?id=${id}&quantity=${newValue}`,
                    type: 'GET',
                    success: function (data) {
                        getCartProducts();
                        $("#cart-content").css("opacity", 1);

                    },
                    error: function (xhr) {
                        if (xhr.status !== 401) {
                            alert('Error while updating cart item. Please try again later.');
                        }
                        $("#cart-content").css("opacity", 1);
                    }

                });
            }
            else {
                removeItem(id);
            }
        }
    }, 1000);
}

function minusCartItem(id) {
    if ($("#quantityInput").val() > 1) {
        $.ajax({
            url: `/Cart/MinusCartItem?id=${id}`,
            type: 'GET',
            success: function (data) {
                getCartProducts();
            },

            error: function (xhr) {
                if (!xhr.status === 401) {
                    alert('Error while getting cart products. Please try again later.');
                }
            }
        });
    }
    else {
        removeItem(id);
    }
}



const priceInputs = document.querySelectorAll(".price-input .price-value");
const rangeInputs = document.querySelectorAll(".range-input input");
const range = document.querySelector(".slider .progress");

let priceGap = 0;

let resetButton = $('<button onclick="resetFilter()" id="filter-reset-btn" class="explore-btn"><span>reset</span></button>');

rangeInputs.forEach((input) => {
    input.addEventListener("input", (e) => {
        let minVal = parseInt(rangeInputs[0].value);
        let maxVal = parseInt(rangeInputs[1].value);
        if (minVal != parseInt(rangeInputs[0].min) || maxVal != parseInt(rangeInputs[1].max)) {
            if ($("#filter-reset-btn").length == 0) {
                $(".filter-price-buttons").prepend(resetButton);

                $(".filter-price-buttons").css('justify-content', 'space-between');
            }
        }
        else {
            $(".filter-price-buttons").css('justify-content', 'right');
            $("#filter-reset-btn").remove();

        }
        let minRange = parseInt(rangeInputs[0].min);
        let maxRange = parseInt(rangeInputs[1].max);
        let rangeWidth = maxRange - minRange;

        priceInputs[0].innerHTML = minVal;
        priceInputs[1].innerHTML = maxVal;
        range.style.left = ((minVal - minRange) / rangeWidth) * 100 + "%";
        range.style.right = ((maxRange - maxVal) / rangeWidth) * 100 + "%";
    });
});

function resetFilter() {
    let min = rangeInputs[0].min;
    let max = rangeInputs[1].max
    rangeInputs[0].value = min;
    rangeInputs[1].value = max;
    priceInputs[0].innerHTML = min;
    priceInputs[1].innerHTML = max;
    $("#filter-reset-btn").remove();
    $(".filter-price-buttons").css('justify-content', 'right');
    range.style.left = 0+ "%";
    range.style.right = 0+ "%";

}


function applyFilter(min, max) {

}



function closeFilterSect() {
    $('.filter-sect').css('transform', 'translateX(-100%)');
    document.body.style.overflow = 'initial';
    $('.cart-overlay2').css('display', 'none');

}

function showFilter() {
    $('.filter-sect').css('transform', 'translateX(0%)');
    $('.cart-overlay2').css('display', 'flex');
    document.body.style.overflow = 'hidden';


}





getCartProducts();