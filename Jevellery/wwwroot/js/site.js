﻿var header = document.getElementById("header");
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

        error: function (xhr) {
            if (!xhr.status === 401) {
                alert('Error while getting cart products. Please try again later.');
            }
        }
    });
}

function plusProductQuantity() {
    let quantity = parseInt($('#productQuantityInput').val());
    quantity += 1;
    $('#productQuantityInput').val(quantity);
}

function minusProductQuantity() {
    let quantity = parseInt($('#productQuantityInput').val());
    if (quantity > 1) {

        quantity -= 1
        $('#productQuantityInput').val(quantity);

    }
}


function addToCartPR(productId) {
    let quantity = $('#productQuantityInput').val();
    $.ajax({
        url: `/Cart/Add?productId=${productId}&quantity=${quantity}`,
        type: 'GET',
        success: function () {
            getCartProducts();
        },

        error: function (xhr) {
            if (!xhr.status === 401) {
                alert('Error while getting cart products. Please try again later.');
            }
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
        success: function () {
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
            success: function () {
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




function calculateFilter() {

    let minVal = parseInt(rangeInputs[0].value);
    let maxVal = parseInt(rangeInputs[1].value);
    if (minVal != parseInt(rangeInputs[0].min) || maxVal != parseInt(rangeInputs[1].max)) {
        $("#filter-reset-btn").css('opacity', '1');
    }
    else {
        $("#filter-reset-btn").css('opacity', '0');
    }
    let minRange = parseInt(rangeInputs[0].min);
    let maxRange = parseInt(rangeInputs[1].max);
    let rangeWidth = maxRange - minRange;

    priceInputs[0].innerHTML = minVal;
    priceInputs[1].innerHTML = maxVal;
    range.style.left = ((minVal - minRange) / rangeWidth) * 100 + "%";
    range.style.right = ((maxRange - maxVal) / rangeWidth) * 100 + "%";
}

rangeInputs.forEach((input) => {
    input.addEventListener("input", (e) => {
        calculateFilter();
    });
});

function resetFilter() {
    let min = rangeInputs[0].min;
    let max = rangeInputs[1].max
    rangeInputs[0].value = min;
    rangeInputs[1].value = max;
    priceInputs[0].innerHTML = min;
    priceInputs[1].innerHTML = max;
    range.style.left = 0 + "%";
    range.style.right = 0 + "%";
    $("#filter-reset-btn").css('opacity', '0');
    let a = $('.active-filters');
    var b = a.length > 0 ? false : true
    submitFilter(b)

}





function submitFilter(b) {
    var form = document.getElementById('filterForm');
    var filterMax = form.querySelector('input[name="filterMax"]');
    var filterMin = form.querySelector('input[name="filterMin"]');

    if (b && parseInt(filterMax.value) == parseInt(filterMax.max) && parseInt(filterMin.value) == parseInt(filterMin.min)) return;
    if (parseInt(filterMax.value) == parseInt(filterMax.max)) {
        var parent = filterMax.parentNode;
        if (parent) {
            parent.removeChild(filterMax);
        }
    }
    if (parseInt(filterMin.value) == parseInt(filterMin.min)) {
        var parent = filterMin.parentNode;
        if (parent) {
            parent.removeChild(filterMin);
        }
    }


    form.submit();
}



function closeFilterSect() {
    $('.filter-sect').css('transform', 'translateX(-100%)');
    document.body.style.overflow = 'initial';
    $('.cart-overlay2').css('opacity', '0')
    $('.cart-overlay2').css('pointer-events', 'none');

}

function showFilter() {
    $('.filter-sect').css('transform', 'translateX(0%)');
    $('.cart-overlay2').css('opacity', '1');
    $('.cart-overlay2').css('pointer-events', 'auto')
    document.body.style.overflow = 'hidden';


}


let filter_categories = false;
function showFilterCategories() {
    if (filter_categories) {
        $('.filter-categories-container').show();
        $('.filter-category-top i').css('transform', 'rotate(180deg)');

    }
    else {
        $('.filter-categories-container').slideUp();
        $('.filter-category-top i').css('transform', 'rotate(0deg)');

    }
    filter_categories = !filter_categories;
}



function closeImage() {

    $('.image-overlay').hide();
}


function showImage() {
    $('.image-overlay').show();
}


document.querySelectorAll('.prod-desc-com > li:not(.activee)').forEach(item => {
    item.addEventListener('mouseenter', function () {
        this.parentNode.querySelector('.activee').classList.add('no-hover');
    });
    item.addEventListener('mouseleave', function () {
        this.parentNode.querySelector('.activee').classList.remove('no-hover');
    });
});


document.addEventListener('DOMContentLoaded', function () {
    const tabs = document.querySelectorAll('.prod-desc-com > li');
    tabs.forEach(tab => {
        tab.addEventListener('click', function () {
            console.log("worked")
            tabs.forEach(t => {
                t.classList.remove('activee');
            });

            this.classList.add('activee');

            if (this.textContent === 'Description') {
                $('.product-tab2').hide();
                $('.product-tab1').show();
            } else {
                $('.product-tab1').hide();
                $('.product-tab2').show();
            }

            const nonInteractiveItems = document.querySelector('.prod-desc-com > li:not(.activee)');
            nonInteractiveItems.addEventListener('mouseenter', function () {
                const activeItem = document.querySelector('.prod-desc-com > li.activee');
                if (activeItem) {
                    activeItem.classList.add('no-hover');
                }
            });
            nonInteractiveItems.addEventListener('mouseleave', function () {
                const activeItem = document.querySelector('.prod-desc-com > li.activee');
                if (activeItem) {
                    activeItem.classList.remove('no-hover');
                }
            });

            const activeItem = document.querySelector('.prod-desc-com > li.activee');
            activeItem.addEventListener('mouseenter', function () {
                activeItem.classList.remove('no-hover');
            });

        });
    });
});



function showQuickView(productId) {
    $(".quick-view").show();
    document.body.style.overflow = 'hidden';

    $.ajax({
        url: `/Product/GetProduct?productId=${productId}`,
        type: 'GET',
        success: function (data) {
            $("#quick-content").append(data);
        },

        error: function (xhr) {
            alert('Error while getting Product. Please try again later.');
        }
    });
}


function closeQuickView() {
    $(".quick-view").hide();
    $("#quick-content").find('.product-top').remove();

    document.body.style.overflow = 'initial';

}





function addWishlist(productId, button) {
    $.ajax({
        url: `/WishList/Add?productId=${productId}`,
        type: 'GET',
        success: function () {
            console.log('added wishlist')
        },

        error: function (xhr) {
            alert('Error while getting Product. Please try again later.');
        }
    });


    var iconElement = button.querySelector('.heart-icon');
    var text = button.querySelector('.tooltiptext');
    if (iconElement) {
        iconElement.classList.remove('fa-regular');
        iconElement.classList.add('fa-solid');
        iconElement.style.color = '#db9662';
        text.innerHTML="Remove Wishlist"
    }
    button.onclick = function () {
        removeWishlist( productId, button);
    };
}


function removeWishlist(productId, button) {
    $.ajax({
        url: `/WishList/Remove?id=${productId}`,
        type: 'GET',
        success: function () {
            console.log('removed wishlist')
        },

        error: function (xhr) {
            alert('Error while getting Product. Please try again later.');
        }
    });



    var iconElement = button.querySelector('.heart-icon');
    var text = button.querySelector('.tooltiptext');
    if (iconElement) {
        iconElement.classList.remove('fa-solid');
        iconElement.classList.add('fa-regular');
        iconElement.style.color = 'initial';
        text.innerHTML = "Add to Wishlist"
    }
    button.onclick = function () {
        addWishlist(productId, button);
    };
    getWishlistProducts();
}


function removeWishlistMain(productId) {
    $.ajax({
        url: `/WishList/Remove?id=${productId}`,
        type: 'GET',
        success: function () {
            console.log('removed wishlist')
            getWishlistProducts();

        },

        error: function (xhr) {
            alert('Error while getting Product. Please try again later.');
        }
    });

}



function getWishlistProducts() {
    $.ajax({
        url: `/WishList/GetWishlistProducts`,
        type: 'GET',
        success: function (data) {
            console.log(data);
            $(".wish").html(data);
        },

        error: function (xhr) {
            if (!xhr.status === 401) {
                alert('Error while getting cart products. Please try again later.');
            }
        }
    });
}








getCartProducts();
