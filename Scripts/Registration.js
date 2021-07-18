"use strict";
var $ = function (id) {
	return document.getElementById(id);
};


var processEntries = function () {
	var fname = ($("id_first_name").value);
	var lname = ($("id_last_name").value);
	var mail = ($("id_email").value);
	var validEmail = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/; //Regular Expression
	var password = ($("id_password1").value);
	var confirmPassword = ($("id_password2").value);
	var phoneNumber = ($("id_phone").value);
	var validPhoneNumber = /^(\([0-9]{3}\)\s*|[0-9]{3}\-)[0-9]{3}-[0-9]{4}$/; //Regular Expression
	var termsOfService = ($("id_tos").value);

	var error1 = "*";
	var error2 = "*";
	var error3 = "*";
	var error4 = "*";
	var error5 = "*";
	var error6 = "*";
	var error7 = "*";

	/*check for errors*/
	if (fname == "" || lname == "" || mail == "" || validEmail.test(mail) == false ||
		password == "" || confirmPassword == "" || password != confirmPassword || phoneNumber == "" ||
		validPhoneNumber.test(phoneNumber) == false || termsOfService.checked == 0) {
		/*validate first name*/
		if (fname == "") {
			error1 = "First name is required.";
		}
		else {
			var error1 = "";
		}
		$("error1").firstChild.nodeValue = error1;

		/*validate last name*/
		if (lname == "") {
			error2 = "Last name is required.";
		}
		else {
			var error2 = "";
		}
		$("error2").firstChild.nodeValue = error2;

		/*validate email*/
		if (mail == "") {
			error3 = "Email is required.";
		}
		else if (validEmail.test(mail) == false) {
			error3 = "Not a valid email."
		}
		else {
			var error3 = "";
		}
		$("error3").firstChild.nodeValue = error3;

		/*validate password*/
		if (password == "") {
			error4 = "Password is required.";
		}
		else {
			var error4 = "";
		}
		$("error4").firstChild.nodeValue = error4;

		/*validate confirm password*/
		if (confirmPassword == "") {
			error5 = "Confirm Password is required.";
		}
		else if (password != confirmPassword) {
			error5 = "Password and Confirm Password must match."
		}
		else {
			var error5 = "";
		}
		$("error5").firstChild.nodeValue = error5;

		/*validate phone number*/
		if (phoneNumber == "") {
			error6 = "Phone Number is required.";
		}
		else if (validPhoneNumber.test(phoneNumber) == false) {
			error6 = "Not a valid phone number."
		}
		else {
			var error6 = "";
		}
		$("error6").firstChild.nodeValue = error6;

		/*validate terms of service*/
		if (termsOfService == 0) {
			error7 = "Please read and agree to Terms of Service.";
		}
		else {
			var error7 = "";
		}
		$("error7").firstChild.nodeValue = error7;
	}
	else {
		var error1 = "";
		var error2 = "";
		var error3 = "";
		var error4 = "";
		var error5 = "";
		var error6 = "";
		var error7 = "";
	}
};

var clearEntries = function () {
	$("id_first_name").value = "";
	$("id_last_name").value = "";
	$("id_email").value = "";
	$("id_password1").value = "";
	$("id_password2").value = "";
	$("id_phone").value = "";
	$("id_tos").checked = 0;
	var error1 = "*";
	var error2 = "*";
	var error3 = "*";
	var error4 = "*";
	var error5 = "*";
	var error6 = "*";
	var error7 = "*";
	$("error1").firstChild.nodeValue = error1;
	$("error2").firstChild.nodeValue = error2;
	$("error3").firstChild.nodeValue = error3;
	$("error4").firstChild.nodeValue = error4;
	$("error5").firstChild.nodeValue = error5;
	$("error6").firstChild.nodeValue = error6;
	$("error7").firstChild.nodeValue = error7;
};


window.onload = function () {
	$("send").onclick = processEntries;
	$("reset").onclick = clearEntries;
};