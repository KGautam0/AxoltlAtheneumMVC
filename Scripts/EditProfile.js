"use strict";
var $ = function(id) {
	return document.getElementById(id);
};

/*Edit Name*/
var processNameEntries = function() {
	var fname = ($("newFName").value);
	var lname = ($("newLName").value);
	
	var error1 ="*";
	var error2 ="*";
		
	/*check for errors*/
	if(fname == "" || lname == ""){
		/*validate first name*/
		if(fname == ""){
			error1 = "First name is required.";
		}
		else {
			var error1 = ""; 
		}
		$("error1").firstChild.nodeValue = error1;

		/*validate last name*/
		if(lname == ""){
			error2 = "Last name is required.";
		}
		else {
			var error2 = ""; 
		}
		$("error2").firstChild.nodeValue = error2;

	}
	else{
		var error1 = ""; 
		var error2 = ""; 
	}
};

var clearNameEntries = function() {
	$("newFName").value = "";
	$("newLName").value = "";
    var error1 ="*";
	var error2 ="*";
	$("error1").firstChild.nodeValue = error1;
	$("error2").firstChild.nodeValue = error2;
};

/*Edit Phone*/
var processPhoneEntries = function() {
	var phoneNumber = ($("newPhone").value);
    var validPhoneNumber = /^(\([0-9]{3}\)\s*|[0-9]{3}\-)[0-9]{3}-[0-9]{4}$/; //Regular Expression
	var error3 ="*";
		
	/*check for errors*/
	if(phoneNumber == "" ||validPhoneNumber.test(phoneNumber) == false){
		/*validate phone number*/
		if(phoneNumber == ""){
			error3 = "Phone Number is required.";
		}
		else if(validPhoneNumber.test(phoneNumber) == false){
			error3= "Not a valid Phone Number."
		}
		else {
			var error3 = ""; 
		}
		$("error3").firstChild.nodeValue = error3;
	}
	else{
		var error3 = ""; 
	}
};

var clearPhoneEntries = function() {
	$("newPhone").value = "";
    var error3 ="*";
	$("error3").firstChild.nodeValue = error3;
};

/*Edit Address*/
var processAddressEntries = function() {
	var Line1 = ($("newLine1").value);
	var Line2 = ($("newLine2").value);
	var City = ($("newCity").value);
	var State = ($("newState").value);
	var Country = ($("newCountry").value);
	var Zip = ($("newZip").value);

	var error4 ="*";
	var error5 ="*";
	var error6 ="*";
	var error7 ="*";
	var error8 ="*";
		
	/*check for errors*/
	if(Line1 == "" || City == "" || State == "" || Country == "" || Zip == ""){
		/*validate Line1*/
		if(Line1 == ""){
			error4 = "Line 1 is required.";
		}
		else {
			var error4 = ""; 
		}
		$("error4").firstChild.nodeValue = error4;

		/*validate City*/
		if(City == ""){
			error5 = "City is required.";
		}
		else {
			var error5 = ""; 
		}
		$("error5").firstChild.nodeValue = error5;
		
		/*validate State*/
		if(State == ""){
			error6 = "State is required.";
		}
		else {
			var error6 = ""; 
		}
		$("error6").firstChild.nodeValue = error6;

		/*validate Country*/
		if(Country == ""){
			error7 = "Country is required.";
		}
		else {
			var error7 = ""; 
		}
		$("error7").firstChild.nodeValue = error7;

		/*validate Zip*/
		if(Zip == ""){
			error8 = "Zip is required.";
		}
		else {
			var error8 = ""; 
		}
		$("error8").firstChild.nodeValue = error8;
	}
	else{
		var error4 = "";
		var error5 = "";
		var error6 = ""; 
		var error7 = ""; 
		var error8 = "";   
	}
};

var clearAddressEntries = function() {
	$("newLine1").value = "";
	$("newLine2").value = "";
	$("newCity").value = "";
	$("newState").value = "";
	$("newCountry").value = "United States";
	$("newZip").value = "";

    var error4 ="*";
	var error5 ="*";
	var error6 ="*";
	var error7 ="*";
	var error8 ="*";

	$("error4").firstChild.nodeValue = error4;
	$("error5").firstChild.nodeValue = error5;
	$("error6").firstChild.nodeValue = error6;
	$("error7").firstChild.nodeValue = error7;
	$("error8").firstChild.nodeValue = error8;
};

/*Edit Card*/
var validateCardNumber = function(cardType, cardNumber) {
	/* source: https://stackoverflow.com/questions/9315647/regex-credit-card-number-tests*/
	var mastercardRegEx = /^(?:5[1-5][0-9]{14})$/;
	var visaRegEx = /^(?:4[0-9]{12}(?:[0-9]{3})?)$/;
    var discovRegEx = /^(?:6(?:011|5[0-9][0-9])[0-9]{12})$/; 
	var commonCardRegEx = /^(?:[0-9]{16})$/;

	if (cardType = "MasterCard"){
		return mastercardRegEx.test(cardNumber);
	}
	if (cardType = "VISA"){
		return visaRegEx.test(cardNumber);
	}
	if (cardType = "Discover"){
		return discovRegEx.test(cardNumber);
	}
	if (cardType = "Other"){
		return commonCardRegEx.test(cardNumber);
	}
	else
		return false;
}

var processCardEntries = function() {
	var cardName = ($("newCardName").value);
	var cardType;
	var cardSelect = document.querySelector('input[name="newCardType"]:checked');
	if (cardSelect != null){
		cardType = cardSelect.value;
	}
	else{
		cardType = "";
	}
	var cardNumber = ($("newCardNumber").value);
	var expirationDate = ($("newExpirationDate").value);
	var validExpirationDate = /^(0[1-9]|1[0-2])\/[0-9]{2}$/;
	var cardZip = ($("newCardZip").value);

	var error9 ="*";
	var error10 ="*";
	var error11 ="*";
	var error12 ="*";
	var error13 ="*";
		
	/*check for errors*/
	if(cardName == "" || cardType == "" || cardNumber == "" ||
	(validateCardNumber(cardType, cardNumber) == false) || expirationDate == "" || 
	(validExpirationDate.test(expirationDate) == false) || cardZip == ""){
		/*validate Cardholder Name*/
		if(cardName == ""){
			error9 = "Cardholder Name is required.";
		}
		else {
			var error9 = ""; 
		}
		$("error9").firstChild.nodeValue = error9;

		/*validate Type*/
		if(cardType == ""){
			error10 = "Card Type is required.";
		}
		else {
			var error10 = ""; 
		}
		$("error10").firstChild.nodeValue = error10;

		/*validate Card Number*/
		if(cardNumber == ""){
			error11 = "Card Number is required.";
		}
		else if(validateCardNumber(cardType, cardNumber) == false){
			error11 = "Not a valid Card Number."
		}
		else {
			var error11 = ""; 
		}
		$("error11").firstChild.nodeValue = error11;

		/*validate Expiration Date*/
		if(expirationDate == ""){
			error12 = "Expiration Date is required.";
		}
		else if(validExpirationDate.test(expirationDate) == false){
			error12 = "Not a valid Expiration Date."
		}
		else {
			var error12 = ""; 
		}
		$("error12").firstChild.nodeValue = error12;

		/*validate Card Zip*/
		if(cardZip == ""){
			error13 = "Card Zip is required.";
		}
		else {
			var error13 = ""; 
		}
		$("error13").firstChild.nodeValue = error13;
	}
	else{
		var error9 = ""; 
		var error10 = ""; 
		var error11 = ""; 
		var error12 = ""; 
		var error13 = ""; 
	}
};

var clearCardEntries = function() {
	$("newCardName").value = "";
	var cardSelect = document.querySelector('input[name="newCardType"]:checked');
	if (cardSelect != null){
		cardSelect.checked = 0;
	}
	$("newCardNumber").value = "";
	$("newExpirationDate").value = "";
	$("newCardZip").value = "";

    var error9 ="*";
	var error10 ="*";
	var error11 ="*";
	var error12 ="*";
	var error13 ="*";

	$("error9").firstChild.nodeValue = error9;
	$("error10").firstChild.nodeValue = error10;
	$("error11").firstChild.nodeValue = error11;
	$("error12").firstChild.nodeValue = error12;
	$("error13").firstChild.nodeValue = error13;
};

window.onload = function() {
	//Edit Name
	$("sendName").onclick = processNameEntries;
	$("resetName").onclick = clearNameEntries;
	//Edit Phone 
    $("sendPhone").onclick = processPhoneEntries;
	$("resetPhone").onclick = clearPhoneEntries;
	//Edit Address
	$("sendAddress").onclick = processAddressEntries;
	$("resetAddress").onclick = clearAddressEntries;
	//Edit Card
	$("sendCard").onclick = processCardEntries;
	$("resetCard").onclick = clearCardEntries;
};