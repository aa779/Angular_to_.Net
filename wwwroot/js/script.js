$(document).ready(function () {
  var x = 0;
  var s = '';

  console.log('Hello!');

  var theForm = $('#the-form');
  theForm.hide();

  var buyButton = $('#buy-button');
  buyButton.click(function () {
    console.log("Buy a item");
  });

  var productInfo = $('.product-info li');
  productInfo.click(function () {
    console.log("You clicked on " + $(this).text());
  });

  var $loginToggle = $('#login-toggle');
  var $popupForm = $('.popup-form');
  $loginToggle.click( function () {
    $popupForm.fadeToggle(500);
  });
});