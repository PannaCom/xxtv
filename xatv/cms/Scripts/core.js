function showLoadingImage() {
    $("#loadingImage").show();
}
function hideLoadingImage() {
    $("#loadingImage").hide();
}
function formatCurrency(number) {
    if (number == null || number == "") return 0;
    return number.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
}