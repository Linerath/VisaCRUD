function formatNumber(event) {
    if (!event || !event.value)
        return;

    event.value = parseFloat(event.value).toFixed(2);
}

function validateForm(form) {
    let regex = /^\d+(\.\d+)*$/;

    let price = $(form).find("#Price");

    if (!regex.test(price.val())) {
        price.addClass("has-error");
        return false;
    }
    else {
        price.removeClass("has-error");
    }

    return true;
}