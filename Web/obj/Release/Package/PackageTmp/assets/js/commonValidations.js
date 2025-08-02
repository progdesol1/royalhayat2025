function checkForCSS(strVal) {

    //CSS - Cross site scripting                                     // Example are below for each regular expression
    var regexpforHTMLTag1 = /(<|&#60|u003C)\s*(\S+)\s*[^>]*\s*(>|&#62|u003E)(.*)(<|&#60|U003C)\/\s*\2\s*(>|&#62|u003E)/i; //<script> <//script> <html> </html>
    var regexpforHTMLTag2 = /(<|&#60|u003C)\s*(\S+)\s*([^>]*)\s*(>|&#62|u003E)/i;                //<font face="Arial, Serif" size="+2" color="red">
    var regexpforXMLTag = /((<|&#60|u003C).[^(><.)]+(>|&#62|u003E))/i;                           //<servlet-name attr1=value attr2=value />
    var regexpForEqualVal = /(\s*\w+\s*)=\1/i;                                                   //link=1=1

    //alert(strVal);
    if (regexpforHTMLTag2.test(strVal) || regexpforHTMLTag1.test(strVal) ||
       regexpforXMLTag.test(strVal) || regexpForEqualVal.test(strVal) || !sqlInjection(strVal)) {
        $().toastmessage('showWarningToast', "UnSafe Input");
        //alert(">> UnSafe Input <<");
        return false;
    }
    else {
        // alert("Safe Input");
        return true;
    }
}


//*******************************************************************
//Purpose	        : This function checks the given strVal for SQL Injection.
//Input	            : Input for this funtion is the strVal contain safe input or SQL Injection.
//Output	        : This function will returns true if the field value is sate input or else if CSS returns false
//Limitation        : 
//Developer Name    : Narender E
//Date              : 18/08/2005.
//*******************************************************************
function sqlInjection(strVal) {
    var regexpforMETACHAR1 = /(\%27)|(&#32)|(u0027)|(\')|(\-\-)|(\%23)|(&#35)|(u0023)|(#)/i;  //Regex for detection of SQL meta-characters
    var regexpforMETACHAR2 = /((\%3D)|(&#61)|(u003D)|(=))[^\n]*((\%27)|(&#32)|(u0027)|(\')|(\-\-)|(\%3B)|(&#59)|(u003B)|(;))/i;  //Modified regex for detection of SQL meta-characters
    var regexpforORclause = /\w*((\%27)|(&#32)|(u0027)|(\'))(\s*)((\%6F)|(&#111)|(u006F)|o|(\%4F)|(&#79)|(u004F))((\%72)|(&#114)|(u0072)|r|(\%52)|(&#82)|(u0052))/i; //Regex for typical SQL Injection attack using OR
    var regexpforSQLwords = /((\%27)|(&#32)|(u0027)|(\'))(\s*)(union|select|insert|update|delete|drop)/i; //Regex for detecting SQL Injection with the UNION,SELECT,INSERT,UPDATE,DELETE,DROP keyword
    var regexpforMsSQL = /exec(\s|\+)+(s|x)p\w+/i;      //Regex for detecting SQL Injection attacks on a MS SQL Server

    if (regexpforMETACHAR1.test(strVal) || regexpforMETACHAR2.test(strVal) ||
        regexpforORclause.test(strVal) || regexpforSQLwords.test(strVal) ||
        regexpforMsSQL.test(strVal)) {
        return false;
    }
    else {
        return true;
    }
}
function securityCheck(form1) {
    debugger
    var str = form1.value;
    if (checkForCSS(str) && sqlInjection(str)) {
        return true;
    }
    else {
        form1.value = "";
        return false;
    }
}
function validationRequiredArr(arr) {

    var i, j = 0;
    var msg = "";
    var iValid = true;
    var focusfield;

    for (i = 0; i < arr.length; i++) {

        var field = arr[i][0];

        if (field.type == "select" || field.type == "Select") {

            var si = field.selectedIndex;
            if (si >= 0) {
                value = field.options[si].value;

            }
        }
        else {
            value = field.value;
        }
        if (trim(value).length == 0 || trim(value) == 'select' || trim(value) == 'Select') {
            if (j == 0) {
                focusfield = field;
                j++;
            }
            msg = msg + arr[i][1] + "\n";
            iValid = false;
        }

    }
    if (iValid == false) {
        if (window["alertDiv"])
            $().toastmessageDiv('showWarningToast', "alertMsgMain_ID", 0, msg);
            //alertDiv("alertMsgMain_ID",0,msg);
        else
            $().toastmessage('showWarningToast', msg);
        //alert(msg);
    }
    return iValid;
}
function trim(s) {
    return s.replace(/^\s\s*/, '').replace(/\s\s*$/, '');
}
function validationRequired(field) {

    if (field.type == "select-one") {
        var si = field.selectedIndex;
        if (si >= 0) {
            value = field.options[si].value;

        }
    } else {
        value = field.value;
    }

    if (trim(value).length == 0 || trim(value) == 'select') {

        return false;
    } else {

        return true;
    }
}
function validateEmailv2(email) {

    // a very simple email validation checking. 
    // you can add more complex email checking if it helps 
    if (email.length <= 0) {
        return true;
    }
    var splitted = email.match("^(.+)@(.+)$");
    if (splitted == null) return false;
    if (splitted[1] != null) {
        var regexp_user = /^\"?[\w-_\.]*\"?$/;
        if (splitted[1].match(regexp_user) == null) return false;
    }
    if (splitted[2] != null) {
        var regexp_domain = /^[\w-\.]*\.[A-Za-z]{2,4}$/;
        if (splitted[2].match(regexp_domain) == null) {
            var regexp_ip = /^\[\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\]$/;
            if (splitted[2].match(regexp_ip) == null) return false;
        }// if
        return true;
    }
    return false;
}
function isNull(e) {
    // alert("In ISNULL function");
    if ((e.value == "") || (e.selectedIndex == 0)) {
        //test+=e.name+"\n"	
        return true;
    }
}
/**
* The function isEmail checks for the validity of the email address
* the email address should be in the proper format
* ie., yourname@somebody.something. if email is not proper it alerts
* to enter the correct mail address.
*/
/*function isEmail(mail)
{
	if(mail.value!="")
	{
		email=mail.value
		len=mail.value.length-1
		s='@'
		p='.'
    q="\'";    
		s1=email.indexOf(s)
		//s2=email.indexOf(p)
		s2=email.lastIndexOf(p);
		
    s3=-1;
    s3=email.indexOf(q);
		if((s1<1)||(s1==len)||((s2-s1)==1)||(s2==len)||(s3!=-1) ||(s2<1)  )
		{
  		     alert("Please Enter Valid EMail id ")
           mail.value="";
           //mail.focus();
		     return false
		}
	}
	return true
}
*/


function isEmail(mail) {
    //var amountRegExp=new RegExp(/(^\d{0,11}\.\d{1,2}$)|(^\d{0,13}$)/);
    var mailRegExp = new RegExp(/^([a-zA-Z]{1}[\w\-\.]*\@([\da-zA-Z\-]{1,}\.){1,}[\da-zA-Z\-]{2,4})$/);

    if (mail.value.length == 0)
        return true;
    if (securityCheck(mail)) {
        var isValid = mailRegExp.test(mail.value);
        if (isValid == false) {
            $().toastmessage('showWarningToast', "Please Enter Valid Email id.");
            //alert('Please Enter Valid Email id.');
            mail.value = "";
            return false;
        }
        else {

            return true;
        }
    }
    else {
        $().toastmessage('showWarningToast', "Malicious Code Found. Please Enter valid data.");
        //alert('Malicious Code Found. Please Enter valid data.');
        mail.value = "";
        return false;
    }

}


/**
* The function isAlphaNumeric checks for the validity of the entered fields
* which should alphanumeric only. Other wise it alerts for proper valid data.
*/
function isAlphaNumeric(form1) {
    var len, str, str1, i;
    len = form1.value.length;
    str = form1.value;
    str1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-,.&!#() \n";

    if (securityCheck(form1)) {

        for (i = 0; i < len; i++) {
            if ((str1.indexOf(str.charAt(i))) == -1) {
                $().toastmessage('showWarningToast', "Enter Alphabets or Numbers in the field");
                //alert("Enter Alphabets or Numbers in the field");
                form1.value = "";
                //form1.focus();
                return false;
            }
        }
        return true;
    }
    else {
        $().toastmessage('showWarningToast', "Malicious Code Found. Please Enter valid data.");
        //alert('Malicious Code Found. Please Enter valid data.');
        form1.value = "";
        return false;
    }
}

function isNumeric_WhiteSpace(form1) {
    var len, str, str1, i;
    len = form1.value.length;
    str = form1.value;
    str1 = " 0123456789";
    if (securityCheck(form1)) {

        for (i = 0; i < len; i++) {
            if ((str1.indexOf(str.charAt(i))) == -1) {
                //alert("Please Enter Valild No");
                //          form1.value = "";
                //        form1.focus();
                return false;
            }
        }
        return true;
    }
    else {
        $().toastmessage('showWarningToast', "Malicious Code Found. Please Enter valid data.");
        //alert('Malicious Code Found. Please Enter valid data.');
        return false;
    }

}
/**
* The function isNumeric checks for the validity of the entered fields
* which should numeric only. Other wise it alerts for proper valid data.
*/

function isNumeric(form1) {

    var len, str, str1, i;
    len = form1.value.length;
    str = form1.value;
    str1 = "0123456789";
    if (securityCheck(form1)) {

        for (i = 0; i < len; i++) {
            if ((str1.indexOf(str.charAt(i))) == -1) {
                $().toastmessage('showWarningToast', "Enter Numeric Data in this field");
                //alert("Enter Numeric Data in this field");
                form1.value = "";
                form1.focus();
                return false;
            }
        }
        return true;
    }
    else {
        $().toastmessage('showWarningToast', "Malicious Code Found. Please Enter valid data.");
        //alert('Malicious Code Found. Please Enter valid data.');
        form1.value = "";
        form1.focus();
        return false;
    }

}
function isNumericWithPer(form1) {

    var len, str, str1, i;
    len = form1.value.length;
    str = form1.value;
    str1 = "0123456789%";
    if (securityCheck(form1)) {

        for (i = 0; i < len; i++) {
            if ((str1.indexOf(str.charAt(i))) == -1) {
                $().toastmessage('showWarningToast', "Enter Numeric Data in this field");
                //alert("Enter Numeric Data in this field");
                form1.value = "";
                form1.focus();
                return false;
            }
        }
        return true;
    }
    else {
        $().toastmessage('showWarningToast', "Malicious Code Found. Please Enter valid data.");
        //alert('Malicious Code Found. Please Enter valid data.');
        form1.value = "";
        form1.focus();
        return false;
    }

}


function isNumeric_mu(form1) {

    var len, str, str1, i;
    len = form1.value.length;
    str = form1.value;
    str1 = "0123456789";
    if (securityCheck(form1)) {

        for (i = 0; i < len; i++) {
            if ((str1.indexOf(str.charAt(i))) == -1) {
                $().toastmessage('showWarningToast', "Enter Numeric Data in this field");
                //alert("Enter Numeric Data in this field");
                form1.value = "";
                form1.focus();
                return false;
            }
        }
        return true;
    }
    else {
        $().toastmessage('showWarningToast', "Malicious Code Found. Please Enter valid data.");
        //alert('Malicious Code Found. Please Enter valid data.');
        form1.value = "";
        form1.focus();
        return false;
    }

}

function isNumeric_DecimalNegative(form1) {
    var len, str, str1, i, dec_counter, newi;
    len = form1.value.length;
    newi = 0;
    final_val = "";
    str = form1.value;
    if (securityCheck(form1)) {
        if (str.charAt(i) == '-') {
            str = str.substr(1, len);
            newi = 1;
        }

        str1 = "0123456789.";
        dec_counter = 0;
        dec_set = 0;
        after_dec = 0;
        for (i = newi; i < len; i++) {
            if (dec_set == 1) {
                after_dec++;
                if (after_dec > 2) {
                    final_val = str.substr(0, i);
                    form1.value = final_val;
                    $().toastmessage('showWarningToast', "Only two digits are allowed after decimal");
                    // alert("Only two digits are allowed after decimal");
                    return true;
                }

            }
            if (str.charAt(i) == '.') {
                if (dec_counter > 0) {
                    $().toastmessage('showWarningToast', "Only one Decimal Point is allowed.");
                    //alert("Only one Decimal Point is allowed.");
                    form1.value = "";
                    //            form1.focus();
                    return false;
                }
                else {
                    dec_counter++;
                }
                dec_set = 1;
            }
            if ((str1.indexOf(str.charAt(i))) == -1) {
                $().toastmessage('showWarningToast', "Enter Numeric Data in this field");
                //alert("Enter Numeric Data in this field");
                form1.value = "";
                form1.focus();
                return false;
            }
        }
        if (dec_set == 0) {
            if (len > 11) {
                $().toastmessage('showWarningToast', "Only 11 digits are allowed before decimal.");
                //alert("Only 11 digits are allowed before decimal.");
                form1.value = "";
                form1.focus();
                return false;
            }
        }
        return true;
    }
    else {
        $().toastmessage('showWarningToast', "Malicious Code Found. Please Enter valid data.");
        //alert('Malicious Code Found. Please Enter valid data.');
        return false;
    }
}

function get_numeric(form1) {
    var str = form1.value;
    len = form1.value.length;
    for (i = 0; i < len; i++) {
        if (str.charAt(i) != "0")
            break;

    }
    if (str.charAt(i) == "." && i != 0)
        i -= 1;
    str = str.substr(i, len);
    if (str == "")
        str = "0.00";
    return str;
}



function isNumeric_Decimalround(form1) {
    var len, str, str1, i, dec_counter;
    len = form1.value.length;
    final_val = "";
    str = form1.value;
    str1 = "0123456789.";
    dec_counter = 0;
    dec_set = 0;
    after_dec = 0;
    if (securityCheck(form1)) {

        //added by aditi to remove zeroes as prefix
        str = get_numeric(form1);
        len = str.length;
        for (i = 0; i < len; i++) {
            if (dec_set == 1) {
                after_dec++;
                if (after_dec > 2) {
                    final_val = str.substr(0, i);
                    form1.value = final_val;
                    alert('showWarningToast', "Only two digits are allowed after decimal.");
                    // alert("Only two digits are allowed after decimal.");
                    return true;
                }

            }
            if (str.charAt(i) == '.') {
                if (dec_counter > 0) {
                    $().toastmessage('showWarningToast', "Only one Decimal Point is allowed.");
                    //alert("Only one Decimal Point is allowed.");
                    form1.value = "";
                    //          form1.focus();
                    return false;
                }
                else {
                    dec_counter++;
                }
                dec_set = 1;
            }
            if ((str1.indexOf(str.charAt(i))) == -1) {
                $().toastmessage('showWarningToast', "Enter Numeric Data in this field");
                //alert("Enter Numeric Data in this field");
                form1.value = "";
                //      form1.focus();
                return false;
            }
        }
        if (dec_set == 0) {
            if (len > 13) {
                $().toastmessage('showWarningToast', "Only 13 digits are allowed before decimal");
                //alert("Only 13 digits are allowed before decimal");
                form1.value = "";
                //     form1.focus();
                return false;
            }
        }
        if ((str.charAt(len - 1)) == ".") {
            $().toastmessage('showWarningToast', "Enter Numeric Data in this field");
            //alert("Enter Numeric Data in this field");
            form1.value = "";
            //      form1.focus();
            return false;
        }
        form1.value = str;
        return true;
    }
    else {
        $().toastmessage('showWarningToast', "Malicious Code Found. Please Enter valid data.");
        //alert('Malicious Code Found. Please Enter valid data.');
        return false;
    }
}
function isDecimalNumericAmendAmount(form1, digits) {
    var len, str, str1, i, dec_counter;
    len = form1.value.length;
    final_val = "";
    str = form1.value;
    str1 = "0123456789.";
    dec_counter = 0;
    dec_set = 0;
    after_dec = 0;
    if (securityCheck(form1)) {

        //added by aditi to remove zeroes as prefix
        str = get_numeric(form1);
        len = str.length;
        for (i = 0; i < len; i++) {
            if (dec_set == 1) {
                after_dec++;
                if (after_dec > 2) {
                    final_val = str.substr(0, i);
                    if ((final_val.charAt(0)) == ".") {
                        final_val = 0 + final_val;
                    }
                    form1.value = final_val;
                    ale$().toastmessagert('showWarningToast', "Only two digits are allowed after decimal.");
                    //alert("Only two digits are allowed after decimal.");
                    return true;
                }

            }
            if (str.charAt(i) == '.') {
                if (dec_counter > 0) {
                    $().toastmessage('showWarningToast', "Enter Numeric Data in this field");
                    //alert("Enter Numeric Data in this field");
                    form1.value = "";
                    //          form1.focus();
                    return false;
                }
                else {
                    dec_counter++;
                }
                dec_set = 1;
            }
            if ((str1.indexOf(str.charAt(i))) == -1) {
                $().toastmessage('showWarningToast', "Enter Numeric Data in this field");
                //alert("Enter Numeric Data in this field");
                form1.value = "";
                return false;
            }
        }
        if (dec_set == 0) {
            if (len > digits) {
                $().toastmessage('showWarningToast', "The integer part of the no. cannot be longer than " + digits + " digits.");
                //alert("The integer part of the no. cannot be longer than " + digits + " digits.");
                form1.value = "";
                return false;
            }
        }
        else {
            if (str.indexOf(".") > digits) {
                $().toastmessage('showWarningToast', "The integer part of the no. cannot be longer than " + digits + " digits.");
                //alert("The integer part of the no. cannot be longer than " + digits + " digits.");
                form1.value = "";
                return false;
            }
        }
        if ((str.charAt(len - 1)) == ".") {
            $().toastmessage('showWarningToast', "Enter Numeric Data in this field");
            //alert("Enter Numeric Data in this field");
            form1.value = "";
            return false;
        }
        if ((str.charAt(0)) == ".") {
            str = 0 + str;
        }
        form1.value = str;
        return true;
    }
    else {
        $().toastmessage('showWarningToast', "Malicious Code Found. Please Enter valid data.");
        //alert('Malicious Code Found. Please Enter valid data.');
        return false;
    }
}
function isNumeric_Decimal(amount) {
    //var amountRegExp=new RegExp(/(^\d{0,11}\.\d{1,2}$)|(^\d{0,13}$)/);
    var amountRegExp = new RegExp(/(^\d{0,13}\.\d{1,2}$)|(^\d{0,15}$)/);
    if (securityCheck(amount)) {
        var isValid = amountRegExp.test(amount.value);
        if (isValid == false) {
            $().toastmessage('showWarningToast', "Please enter numeric data with two decimal places");
            //alert('Please enter numeric data with two decimal places');
            return false;
        }
        else {

            return true;
        }
    }
    else {
        $().toastmessage('showWarningToast', 'Malicious Code Found. Please Enter valid data.');
        //alert('Malicious Code Found. Please Enter valid data.');
        return false;
    }

}
function isNumeric_OneOrTwoDecimal(amount) {
    //var amountRegExp=new RegExp(/(^\d{0,11}\.\d{1,2}$)|(^\d{0,13}$)/);
    var amountRegExp = new RegExp(/(^\d{0,13}\.\d{1,2}$)|(^\d{0,15}$)/);
    if (securityCheck(amount)) {
        var isValid = amountRegExp.test(amount.value);
        if (isValid == false) {
            $().toastmessage('showWarningToast', 'Please enter numeric data with two decimal places');
            //alert('Please enter numeric data with two decimal places');
            return false;
        }
        else {

            return true;
        }
    }
    else {
        $().toastmessage('showWarningToast', 'Malicious Code Found. Please Enter valid data.');
        //alert('Malicious Code Found. Please Enter valid data.');
        return false;
    }

}

/**
* The function isAlpha checks for the validity of the entered fields
* which should characters only. Other wise it alerts for proper valid data.
*/
function isAlpha(form1) {

    var len, str, str1, i;

    str = form1.value;
    len = form1.value.length;
    str = form1.value;
    if (securityCheck(form1)) {
        str1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-,.\n ";
        for (i = 0; i < len; i++) {
            if ((str1.indexOf(str.charAt(i))) == -1) {

                form1.value = "";
                $().toastmessage('showWarningToast', "Enter only alphabets in this field.");
                // alert("Enter only alphabets in this field");
                return false;
            }
        }
        return true;
    }
    else {
        form1.value = "";
        $().toastmessage('showWarningToast', "Enter only alphabets in this field.");
        //alert("Enter only alphabets in this field");
        return false;
    }
}

/**
* The function isWebAdd checks for the validity of the entered fields
* which should in www.one.two only. Other wise it alerts for proper valid data.
*/
function isWebAdd(form2) {
    if (form2.value != "") {
        var add = form2.value;
        var len = add.length;
        var p = ".";
        var s = add.indexOf(p);
        var s1 = s + 1;
        var check = add.substring(0, 3);
        var count = add.substring(s1, len);
        var count1 = count.indexOf(p);
        if ((s == 0) || (add.charAt(len - 1) == p) || (add.charAt(s) == add.charAt(s + 1)) || (count1 == -1) || (check != "www")) {
            $().toastmessage('showWarningToast', "Enter Web Address starting with www");

            return false;
        }
    }
    return true;
}

// whitespace characters only.
function isWhitespace(s) {
    var i;

    // whitespace characters
    var whitespace = " \t\n\r";

    // Is s empty?
    if (isEmpty(s)) return true;

    // Search through string's characters one by one
    // until we find a non-whitespace character.
    // When we do, return false; if we don't, return true.

    for (i = 0; i < s.length; i++) {
        // Check that current character isn't whitespace.
        var c = s.charAt(i);

        if (whitespace.indexOf(c) == -1) return false;
    }
    // All characters are whitespace.
    return true;
}


function isEmpty(s) {
    return ((s == null) || (s.length == 0));
}

/* THE BELOW JAVA SCRIPT FUNCTIONS ARE WRITTEN BY D.SRINIVAS . PLEASE DON'T TOUCH THE CODE WITHOUT INTIMATION */


//  FUNCTION fing the all text areas the elements in the form 
var clearstatus = 0;


//Added By Kunal

function checkMaxLength(field, maxlimit, comment) {
    val = field.value;
    if (val.length > maxlimit) {
        //$().toastmessage(comment + " Length Should be less than " + maxlimit + " Characters");
        $().toastmessage(comment + " Length Should be less than " + maxlimit + " Characters");
        field.focus();
        return false;
    }
    return true;
}

function checkMendatory(field, comment) {
    val = field.value;
    if (val = "" || val.length < 1) {
        //alert("Please Enter "+comment);
        //field.focus();
        return false;
    }
    return true;
}
function checkMendatory(field) {

    val = field.value;
    if (val = "" || val.length < 1) {
        return false;
    }
    return true;
}
function checkSpecialChar(field) {

    value = field.value;
    var iChars = "%#*?&@#$%^\\\'/|\"";

    for (var i = 0; i < value.length; i++) {
        if (iChars.indexOf(value.charAt(i)) != -1) {
            field.value = "";
            $().toastmessage('showWarningToast', "Special characters are not allowed");
            //field.focus();
            return false;
        }
    }
    return true;
}
//Added By Kunal
function fn_clear(a) {
    var lelecount = a.elements.length;
    var num = 0;
    for (num = 0; num < lelecount; num++) {
        if (a.elements[num].type == "text") {

            a.elements[num].value = '';
        }
        if (a.elements[num].type == "select-one") {
            a.elements[num].selectedIndex = 0;
        }

    }
    clearstatus = 1;
}


/* DATE VALIDATION
  
THIS BELOW FUNCTION CHECKS THE DATE WETHER IT IS CORRECT DATE OR NOT. 
INTERNALLY IT CALLS chkdate(objName) function  */

var tempdate;

function checkdate(objName) {
    var datefield = objName;

    if (chkdate(objName) == false) {
        // datefield.select();
        //alert("The date is invalid.  Please try again.");
        $().toastmessage('showWarningToast', 'The date is invalid.  Please try again.');
        datefield.value = "";
        //  datefield.focus();
        return false;
    }
    else {
        return true;
    }
}

function chkdate(objName) {
    //alert("in side the check date"+objName.value);
    var strDate;
    var strDateArray;
    var strDay;
    var strMonth;
    var strYear;
    var intday;
    var intMonth;
    var intYear;
    var booFound = false;
    var datefield = objName;
    var strSeparatorArray = new Array("-", " ", "/", ".");
    var intElementNr;
    var err = 0;
    var curdate = new Date();
    var comparedate;
    var strMonthArray = new Array(12);
    strMonthArray[0] = "Jan";
    strMonthArray[1] = "Feb";
    strMonthArray[2] = "Mar";
    strMonthArray[3] = "Apr";
    strMonthArray[4] = "May";
    strMonthArray[5] = "Jun";
    strMonthArray[6] = "Jul";
    strMonthArray[7] = "Aug";
    strMonthArray[8] = "Sep";
    strMonthArray[9] = "Oct";
    strMonthArray[10] = "Nov";
    strMonthArray[11] = "Dec";
    strDate = datefield.value;



    if (strDate.length <= 5 || strDate.length > 10) {
        return false;
    }
    for (intElementNr = 0; intElementNr < strSeparatorArray.length; intElementNr++) {
        if (strDate.indexOf(strSeparatorArray[intElementNr]) != -1) {
            strDateArray = strDate.split(strSeparatorArray[intElementNr]);
            if (strDateArray.length != 3) {
                err = 1;
                return false;
            }
            else {
                strDay = strDateArray[0];
                strMonth = strDateArray[1];
                strYear = strDateArray[2];
            }
            booFound = true;
        }
    }
    if (booFound == false) {
        if (strDate.length > 5) {
            strDay = strDate.substr(0, 2);
            strMonth = strDate.substr(2, 2);
            strYear = strDate.substr(4);
        }
    }


    if (strYear.length == 3 || strYear.length > 4) {
        return false;
    }

    if (strYear.length == 1) {
        strYear = '200' + strYear;
    }

    if (strYear.length == 2) {
        strYear = '20' + strYear;
    }


    intday = parseInt(strDay, 10);
    if (intday < 10)
        strDay = "0" + intday;
    if (isNaN(intday)) {
        err = 2;
        return false;
    }
    intMonth = parseInt(strMonth, 10);
    if (intMonth < 10)
        intMonth = "0" + intMonth;
    if (isNaN(intMonth)) {
        for (i = 0; i < 12; i++) {
            if (strMonth.toUpperCase() == strMonthArray[i].toUpperCase()) {
                intMonth = i + 1;
                strMonth = strMonthArray[i];
                i = 12;
            }
        }
        if (isNaN(intMonth)) {
            err = 3;
            return false;
        }
    }
    intYear = parseInt(strYear, 10);
    if (isNaN(intYear)) {
        err = 4;
        return false;
    }
    if (intMonth > 12 || intMonth < 1) {
        err = 5;
        return false;
    }
    if ((intMonth == 1 || intMonth == 3 || intMonth == 5 || intMonth == 7 || intMonth == 8 || intMonth == 10 || intMonth == 12) && (intday > 31 || intday < 1)) {
        err = 6;
        return false;
    }
    if ((intMonth == 4 || intMonth == 6 || intMonth == 9 || intMonth == 11) && (intday > 30 || intday < 1)) {
        err = 7;
        return false;
    }
    if (intMonth == 2) {
        if (intday < 1) {
            err = 8;
            return false;
        }
        if (LeapYear(intYear) == true) {
            if (intday > 29) {
                err = 9;
                return false;
            }
        }
        else {
            if (intday > 28) {
                err = 10;
                return false;
            }
        }
    }

    tempdate = intMonth + "/" + strDay + "/" + strYear;
    datefield.value = strDay + "/" + intMonth + "/" + strYear;

    return true;
}


function LeapYear(intYear) {
    if (intYear % 100 == 0) {
        if (intYear % 400 == 0) { return true; }
    }
    else {
        if ((intYear % 4) == 0) { return true; }
    }
    return false;
}


// THIS BELOW FUNCTION CHECKS WETHER ENTERED DATE IS BEFORE SYSDATE OR NOT

function doSysdateCheck(from) {
    var passmonth;
    var passyear;
    var passday;
    var curdate = new Date();
    var curmonth = parseInt(curdate.getMonth() + 1);

    var curyear = parseInt(curdate.getYear());

    var curday = parseInt(curdate.getDate());
    /* alert(" curmonth:"+curmonth);
      alert(" cur year:"+curyear);
       alert(" cur day:"+curday); */
    if (chkdate(from) == false) {
        $().toastmessage('showWarningToast', "That date is invalid.  Please try again.");
        //alert("That date is invalid.  Please try again.");

        return false;
    }
    else {
        if (Date.parse(tempdate) > Date.parse(curdate)) {
            // from.select();
            $().toastmessage('showWarningToast', "Date must occur before Today's  date.");
            // alert("Date must occur before Today's  date.");

            // from.focus();
            return false;
        }
        else {
            return true;
        }

    }

}

function fnCheckPastDt(form) {
    if (fnCompareDates(form.value, getTodayDateAmend()) == false) {
        $().toastmessage('showWarningToast', "That Date cannot be a Future Date.");
        // alert('That Date cannot be a Future Date.');

        form.value = '';
        form.focus();
        return false;
    }
    return true;
}





/* This function used to door no validation */
/* changes in the door number srrln is done by Mayukh Mazumder */
function isdoorno(form1) {
    var len, str, str1, i, chk;
    len = form1.value.length;
    str = form1.value;

    str1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-,.&!'#()/ ";
    chk = "true";
    for (i = 0; i < len; i++) {
        if ((str1.indexOf(str.charAt(i))) == -1) {
            chk = "false";
            //return false
        }
    }
    if (chk == "false") {
        $().toastmessage('showWarningToast', "Enter Alphabets or Numbers\n or characters like -,.&!'#()/  in the field");
        //  alert("Enter Alphabets or Numbers\n or characters like -,.&!'#()/  in the field");
        form1.value = "";
        form1.focus();
    }

    return true;
}


/* start of  Functions written by Jayasheela */

// following function checks whether given is a leap year or not  
function leapYear(year) {
    if (year % 4 == 0)
        return true;

    return false;
}

// following function gives no.of days in the given month of given year
function getDays(month, year) {
    var ar = new Array(12);

    ar[0] = 31;
    ar[1] = (leapYear(year)) ? 29 : 28;//February
    ar[2] = 31;
    ar[3] = 30;
    ar[4] = 31;
    ar[5] = 30;
    ar[6] = 31;
    ar[7] = 31;
    ar[8] = 30;
    ar[9] = 31;
    ar[10] = 30;
    ar[11] = 31;

    // return number of days in the specified month(parameter)
    return ar[month];
}


/*  End of  functions written by jayasheela */



//Functions written by Harjot Singh Bambra
//Check for vaidation of Phone Number
function fn_ValidatePhone(phoneno) {
    var len, str, str1, i;
    len = phoneno.length;
    str1 = "0123456789-";
    for (i = 0; i < len; i++) {
        if ((str1.indexOf(phoneno.charAt(i))) == -1) {
            $().toastmessage('showWarningToast', "Enter Numeric Data in this field");
            // alert("Enter Numeric Data in this field");
            return false;
        }
    }
    return true;
}


//Validation Functions by Harjot Singh finish here


//  Removes the leading and trailing spaces
/*function trim(Field) 
{
    sString=Field.value;
    while (sString.substring(0,1) == ' ')
    {
    sString = sString.substring(1, sString.length);
    }
    while (sString.substring(sString.length-1, sString.length) == ' ')
    {
    sString = sString.substring(0,sString.length-1);
    }
  Field.value=sString;
}*/

function securityCheckAll() {
    var isValid = true;
    var flag = true;
    textArr = document.getElementsByTagName("INPUT");
    for (var i = 0; i < textArr.length; i++) {
        flag = securityCheck(textArr[i]);

        if (flag == false) {
            isValid = false;
        }
    }
    taArr = document.getElementsByTagName("TEXTAREA");
    for (i = 0; i < taArr.length; i++) {
        flag = securityCheck(taArr[i]);
        if (flag == false) {
            isValid = false;
        }
    }
    if (isValid == false) {
        $().toastmessage('showWarningToast', "Please enter Valid  Values");
        //alert("Please enter Valid  Values");
        return false;
    }

}

//added by Mukta for name validation permitting only alphabatic value
function isAlphabatic(name) {
    var len, str, str1, i;
    len = name.length;
    //str=form1.value;
    str1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz \n";


    for (i = 0; i < len; i++) {
        if ((str1.indexOf(name.charAt(i))) == -1) {
            name.value = "";
            //form1.focus();
            return false;
        }
    }
    return true;
}
function isAlphabatic1(form) {
    var len, str, str1, i;
    len = form.value.length;
    //str=form1.value;
    str1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz ";


    for (i = 0; i < len; i++) {
        if ((str1.indexOf(form.value.charAt(i))) == -1) {
            $().toastmessage('showWarningToast', "Please enter alphabetic value, no special characters are allowed except space.");
            // alert("Please enter alphabetic value, no special characters are allowed except space.");
            form.value = "";
            //form1.focus();
            return false;
        }
    }
    return true;
}

function validurl(field) {
    var msg = "";
    var isValid = true;
    var regexp = new RegExp(/(^((http\:\/\/)?[0-9A-Za-z\.]+)$)/);
    if (field.value != "") {
        if (!matchPattern(field.value, regexp)) {
            msg = "Please enter valid website url";
            isValid = false;
        }
        if (msg != "") {
            $().toastmessage('showWarningToast', msg);
            //alert(msg);
        }
    }
    return isValid;
}

// THIS BELOW FUNCTION IS USED TO VALIDATE PAN 
function fn_validatePAN(k) {
    var a, s;

    a = k.value;
    s = a.length;


    if (typeof a == "string")
        a = a.toUpperCase();

    k.value = a;

    for (var i = 0; i < s; i++) {
        if (".!@#$%^&*()+-".indexOf(a.charAt(i)) != -1) {
            $().toastmessage('showWarningToast', "This field should not contain special characters. Enter Again");
            //alert("This field should not contain special characters. Enter Again");
            k.value = "";
            break;
        }
    }

    if (s != 10 && s != 0) {
        $().toastmessage('showWarningToast', "Please enter a Valid PAN of length 10 characters");
        // alert("Please enter a Valid PAN of length 10 characters");
        k.value = "";
        return false;
    }
    else {
        if (s == 10) {

            if (!fn_isAlphaValue(a.substring(0, 5)) || !fn_isNumericValue(a.substring(5, 9)) || !fn_isAlphaValue(a.substring(9, 10))) {
                $().toastmessage('showWarningToast', "Please enter valid PAN (XXXXX9999X)");
                //alert("Please enter valid PAN (XXXXX9999X)");
                k.value = "";
                k.focus();
            }

        }
    }
}

function fn_isAlphaValue(a)                   //  Form level validation
{
    var s;
    s = a.length;
    for (var i = 0; i < s; i++) {
        if ("1234567890!@#$%^&*()+-".indexOf(a.charAt(i)) != -1) {
            return false;
        }
    }
    return true;
}

function fn_isNumericValue(a)                     // Form level validation
{
    var s;
    s = a.length;
    for (var i = 0; i < s; i++) {
        if ("1234567890.".indexOf(a.charAt(i)) == -1) {
            return false;
        }
    }
    return true;
}
function isAlphaNumericwithoutMessage(form1) {
    var len, str, str1, i;
    len = form1.value.length;
    str = form1.value;
    str1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-,.&!#() \n";

    if (securityCheck(form1)) {

        for (i = 0; i < len; i++) {
            if ((str1.indexOf(str.charAt(i))) == -1) {
                return false;
            }
        }
        return true;
    }
    else {
        $().toastmessage('showWarningToast', "Malicious Code Found. Please Enter valid data.");
        // alert('Malicious Code Found. Please Enter valid data.');
        form1.value = "";
        return false;
    }
}

/* Following function checks whether the first date is greater than the 
second date or not. if first date is greater than second date it returns
false and else it returns true

date values should be passed as first and second
 (ex:  fnCompareDates(txt_fromdate.value,txt_todate.value); ) */


/***   start of fnCompareDates ****/
function fnCompareDates(first, second) {
    var val1;
    var val2;

    var k = first.indexOf("/");
    var t = first.indexOf("/", 3);
    val1 = first.substr(k + 1, t - k - 1) + "/" + first.substr(0, k) + "/" + first.substr(t + 1, first.length);

    k = second.indexOf("/");
    t = second.indexOf("/", 3);
    val2 = second.substr(k + 1, t - k - 1) + "/" + second.substr(0, k) + "/" + second.substr(t + 1, second.length);

    if (Date.parse(val1) > Date.parse(val2)) {
        // alert("in if");
        return false;
    }
    else {
        //alert("in else");
        return true;
    }
}
function setToDate(fromdt, period) {
    var frm = fromdt.split("/");
    var frmD = frm[0];
    var frmM = frm[1];
    var frmY = frm[2];
    var day = "";
    var finalDate = "";

    if (period == "Annually" || period == "ANNL" || period == "A") {
        //alert('in todate annually')
        var Year = 0;
        if (frmM == "01" || frmM == "02" || frmM == "03") {
            Year = frmY;
        }
        else {
            Year = (frmY * 1) + 1;
        }
        finalDate = "31/03/" + Year;

    }
    else if (period == "Quarterly" || period == "QRTR" || period == "Q") {
        //alert('in todate qrtrly')
        if (frmM == "01" || frmM == "02" || frmM == "03") {
            finalDate = "31/03/" + frmY;
        }
        else if (frmM == "04" || frmM == "05" || frmM == "06") {
            finalDate = "30/06/" + frmY;
        }
        else if (frmM == "07" || frmM == "08" || frmM == "09") {
            finalDate = "30/09/" + frmY;
        }
        else if (frmM == "10" || frmM == "11" || frmM == "12") {
            finalDate = "31/12/" + frmY;
        }

    }
    else if (period == "Monthly" || period == "MNTL" || period == "M") {
        if (frmM == "01" || frmM == "03" || frmM == "05" || frmM == "07" ||
         frmM == "08" || frmM == "10" || frmM == "12") {
            day = "31";
        }
        else if (frmM == "04" || frmM == "06" || frmM == "09" || frmM == "11") {
            day = "30";
        }
        else if (LeapYear(frmY) && frmM == "02") {
            day = "29";
        }
        else if (!LeapYear(frmY) && frmM == "02") {
            day = "28";
        }
        finalDate = day + "/" + frmM + "/" + frmY;

    }
    //alert(finalDate)
    return TrimNew(finalDate);

}
function TrimNew(text) {
    var text1 = LTrimNew(text);
    var text2 = RTrimNew(text1);
    return text2;
}
function LTrimNew(text) {
    while (text.charAt(0) == ' ')
        text = text.substring(1, text.length);
    return text;

}
function RTrimNew(text) {
    while (text.charAt(text.length - 1) == ' ')
        text = text.substring(0, text.length - 1);
    return text;

}
function compDate(adate, bdate, msg) {

    var val1;
    var val2;

    var k = adate.indexOf("/");
    var t = adate.indexOf("/", 3);
    val1 = adate.substr(k + 1, t - k - 1) + "/" + adate.substr(0, k) + "/" + adate.substr(t + 1, adate.length);

    k = bdate.indexOf("/");
    t = bdate.indexOf("/", 3);
    val2 = bdate.substr(k + 1, t - k - 1) + "/" + bdate.substr(0, k) + "/" + bdate.substr(t + 1, bdate.length);

    if (Date.parse(val1) > Date.parse(val2)) {
        $().toastmessage('showWarningToast', msg);
        // alert(msg);
        return false;
    }
    else {
        return true;
    }
}

function checkAll(ele_name, chkRef) {
    if (chkRef.checked)
        selectAll(ele_name, "1");
    else
        selectAll(ele_name, "2");
}

function selectAll(ele_name, selectFlag) {
    var inputArr = document.getElementsByName(ele_name);

    if (inputArr != null) {
        var len = inputArr.length;
        for (count = 0; count < len; count++) {
            var inputRef = inputArr[count];
            if (inputRef.type == "checkbox") {
                if (selectFlag == '1')
                    inputRef.checked = 'true';
                else
                    inputRef.checked = '';
            }
        }
    }
}
function valFutureDate(from) {
    if (from.value != '') {
        var passmonth;
        var passyear;
        var passday;
        var serverDate = toDayDate();
        var serverDateArray = serverDate.split("/");
        var curdate = new Date(serverDateArray[2], serverDateArray[1] - 1, serverDateArray[0]);
        //alert("Server Date is "+curdate+ "  and pc date is  "+new Date());
        var curmonth = parseInt(curdate.getMonth() + 1);
        var curyear = parseInt(curdate.getYear());
        var curday = parseInt(curdate.getDate());
        var todayDate = curday + "/" + curmonth + "/" + curyear;
        if (chkdate(from) == false) {
            $().toastmessage('showWarningToast', "Please fill in dd/mm/yyyy format");
            //alert("Please fill in dd/mm/yyyy format");
            from.value = "";
            from.focus();
            return false;
        }
        else {
            if (!fnCompareDates(from.value, todayDate)) {
                $().toastmessage('showWarningToast', "Date should not be a future date.");
                //alert("Date should not be a future date.");
                from.value = "";
                from.focus();
                return false;
            }
            else {
                return true;
            }
        }
    }
}
function toDayDate() {
    var currentDate = new Date();
    var month = currentDate.getMonth() + 1;
    var day = currentDate.getDate();
    var year = currentDate.getFullYear();
    var todayDate = day + "/" + month + "/" + year;

    var k = todayDate.indexOf("/");
    var t = todayDate.indexOf("/", 3);
    var date = todayDate.substr(k + 1, t - k - 1) + "/" + todayDate.substr(0, k) + "/" + todayDate.substr(t + 1, todayDate.length);

    return date;
}

function checkBranchSpecialChar(field) {

    value = field.value;

    var iChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-,.&()/ ";

    for (var i = 0; i < value.length; i++) {

        if (iChars.indexOf(value.charAt(i)) == -1) {
            $().toastmessage('showWarningToast', "No special characters is allowed except -,.()&/ and space .");
            //alert("No special characters is allowed except -,.()&/ and space .");
            field.focus();
            return false;
        }
    }
    return true;
}
function checkTextareaMaxLength(field, maxlimit) {
    if (field.value.length >= maxlimit) {
        $().toastmessage('showWarningToast', "You have exceeded the Maximum limit of " + maxlimit + "\nText above maxlimit will be truncated");
        // alert("You have exceeded the Maximum limit of " + maxlimit + "\nText above maxlimit will be truncated");
        final_val = field.value.substr(0, (maxlimit - 1));
        field.value = final_val;
        field.focus();
        return false;
    }
}
function checkTextareaMinLength(field, min) {
    if (field.value.length <= maxlimit) {
        $().toastmessage('showWarningToast', "You have exceeded the Minimum limit of " + maxlimit + "\nText above min limit will be truncated");
        //alert("You have exceeded the Minimum limit of " + maxlimit + "\nText above min limit will be truncated");
        final_val = field.value.substr(0, (maxlimit - 1));
        field.value = final_val;
        field.focus();
        return false;
    }
}
function validateTIN(form) {
    var tin = form.value;
    if (!isNumeric(form)) {
        return false;
    }
    else if (!(tin.length == 11)) {
        $().toastmessage('showWarningToast', "Please enter 11 digit TIN");
        //alert("Please enter 11 digit TIN");
        form.value = "";
        form.focus();
        return false;
    }
    return true;
}
/**
 * @author 348506
 * @info This function sanitizes(removes everything other than the specified params) 
 * 		 the given input as per given parameters. No alerts.
 * 'allowSet' :- {ANI, AI, N} ANI:Alphanumeric AI:Alphabetic N:Numeric
 * 'othrAllow':- takes other special characters that should be allowed in addition.	
 * @param text
 * @param allowSet
 * @param othrAllow
 * @return sanitized text
 * 
 * @plan alert msg logic to be added
 * @plan leading trailing spaces logic to be added
 */
function sanitizeInput(text, allowSet, othrAllow) {

    var set = null;
    var reRegex = "";

    if ((allowSet == null || TrimNew(allowSet) == "") && (othrAllow == null || othrAllow == ""))
        return (text);

    var obj = new Object();
    obj['ANI'] = 'a-zA-Z0-9()';
    obj['AI'] = 'a-zA-Z';
    obj['N'] = '0-9';

    if (allowSet != null && obj[allowSet.toUpperCase()] != null) {
        set = obj[allowSet.toUpperCase()];
    }
    else
        set = "";

    if (othrAllow != null && othrAllow != "") {
        var charSet = othrAllow.split("");
        reRegex = new RegExp("[^\\" + charSet.join("|\\") + set + "]", "g");
    }
    else
        reRegex = new RegExp("[^" + set + "]", "g");

    if (text != null && text != "")
        text = text.replace(reRegex, '');

    return (text);
}
/*
 sanitizeOperation Function to sanitize all malicious characters
 Arguments to pass - (Array, Flag_Value)
 Array contains [field, field_label, allowed_char_Set, other_allowed_chars]
 Flag_Value may be "Y" or "N"
 */
function sanitizeOperation(arr, flag) {

    var i, j = 0;
    var msg = "";
    var sanitizedText = "";
    var field = "";
    var label = "";
    var allowSet = "";
    var othrAllow = "";

    for (i = 0; i < arr.length ; i++) {
        field = arr[i][0];
        label = arr[i][1];
        allowSet = arr[i][2];
        othrAllow = arr[i][3];

        value = field.value;

        if (trim(value).length != 0) {
            sanitizedText = sanitizeInput(value, allowSet, othrAllow);
            if (value.length != sanitizedText.length) {

                othrAllow = othrAllow.replace('\n', "");
                othrAllow = othrAllow.replace('\r', "");

                if (allowSet == "ANI")
                    msg = msg + label + " : Only alphanumeric characters and these [" + othrAllow + "] special charaters are allowed.\n";
                else if (allowSet == "AI")
                    msg = msg + label + " : Only alphabetic characters and these [" + othrAllow + "] special charaters are allowed.\n";
                else if (allowSet == "N")
                    msg = msg + label + " : Only numeric characters and these [" + othrAllow + "] special charaters are allowed.\n";

                j++;
            }
        }
    }
    if (flag == "Y") {
        if (j > 0) {
            if (confirm(msg + "\n" + "Do you wish to automatically remove all unwanted characters?")) {
                for (i = 0; i < arr.length ; i++) {
                    field = arr[i][0];
                    label = arr[i][1];
                    allowSet = arr[i][2];
                    othrAllow = arr[i][3];

                    value = field.value;
                    arr[i][0].value = sanitizeInput(value, allowSet, othrAllow);
                }
                return false;
            }
        }
        else {
            return true;
        }
    }
    else {
        if (j > 0) {
            $().toastmessage('showWarningToast', msg);
            //alert(msg);
            return false;
        }
        else {
            return true;
        }
    }
}
function disableFieldsGeneral(fields) {
    if (fields != null && fields.length > 0) {
        for (i = 0; i < fields.length; i++) {
            var field = document.getElementById(fields[i]);
            if (field)
                field.disabled = true;
        }
    }
}
function enableFieldsGeneral(fields) {
    if (fields != null && fields.length > 0) {
        for (i = 0; i < fields.length; i++) {
            var field = document.getElementById(fields[i]);
            if (field)
                field.disabled = false;
        }
    }

}
function isNumberExceptZero(form1) {
    var len, str, str1, i;
    len = form1.value.length;
    str = form1.value;
    str1 = "0123456789";
    if (str == 0) {
        $().toastmessage('showWarningToast', "Enter Numeric Data Except 0 in this field");
        //alert("Enter Numeric Data Except 0 in this field");
        form1.value = "";
        form1.focus();
        return false;
    }
    if (securityCheck(form1)) {
        for (i = 0; i < len; i++) {
            if ((str1.indexOf(str.charAt(i))) == -1) {
                $().toastmessage('showWarningToast', "Enter Numeric Data in this field");
                // alert("Enter Numeric Data in this field");
                form1.value = "";
                form1.focus();
                return false;
            }
        }
        return true;
    }
    else {
        $().toastmessage('showWarningToast', "Malicious Code Found. Please Enter valid data.");
        //alert('Malicious Code Found. Please Enter valid data.');
        form1.value = "";
        form1.focus();
        return false;
    }
}

function toggleDisplay(a) { a = document.getElementById(a); a.style.display = (a.style.display == "") ? "none" : ""; }

function clearFields(fieldIds) {
    if (fieldIds == null)
        return false;

    var array = null;
    var type = typeof (fieldIds);
    if (type.toLowerCase() == 'string') {
        if ("" != fieldIds)
            array = fieldIds.split(",");
    }
    else if (type.toLowerCase() == 'object') {
        if (fieldIds.length > 0)
            array = fieldIds;
    }
    if (array != null) {
        for (i = 0; i < array.length; i++) {
            var id = document.getElementById(fieldIds[i]);
            if (id)
                id.value = "";
        }
    }
}

//Comparing period dates
function fnCompPeriodDates(first, second) {
    var val1;
    var val2;
    var k = first.indexOf("/");
    var t = first.indexOf("/", 3);
    val1 = first.substr(k + 1, t - k - 1) + "/" + first.substr(0, k) + "/" + first.substr(t + 1, first.length);

    k = second.indexOf("/");
    t = second.indexOf("/", 3);
    val2 = second.substr(k + 1, t - k - 1) + "/" + second.substr(0, k) + "/" + second.substr(t + 1, second.length);
    if (Date.parse(val2) <= Date.parse(val1)) {
        return false;
    } else {
        return true;
    }
}

function validPrd(D1, D2, msg) {
    if (D1.value == "") {
        $().toastmessage('showWarningToast', "Please also fill in " + msg + " from date");
        //alert("Please also fill in " + msg + " from date");
        return false;
    }

    if (D2.value == "") {
        $().toastmessage('showWarningToast', "Please also fill in " + msg + " to date");
        //alert("Please also fill in " + msg + " to date");
        return false;
    }

    if (!checkdate(D1))
        return false;

    if (!checkdate(D2))
        return false;
    fdate = D1.value;
    tdate = D2.value;

    if (!fnCompPeriodDates(fdate, tdate)) {
        $().toastmessage('showWarningToast', "Please enter a valid date, " + msg + " to date can not be less than from date");
        //alert("Please enter a valid date, " + msg + " to date can not be less than from date");
        return false;
    }

    return true;
}

//added by abhishek for amendment
//PSA stands for Plot, Street and area

function isAlphaNumericPSA(form1) {
    var len, str, str1, i;
    len = form1.value.length;
    str = form1.value;
    str1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-.,'&/() \n";

    if (securityCheck(form1)) {

        for (i = 0; i < len; i++) {
            if ((str1.indexOf(str.charAt(i))) == -1) {
                // alert("Enter Alphabets or Numbers in the field");
                //form1.value = "";
                //form1.focus();
                return false;
            }
        }
        return true;
    }
    else {
        // alert('Malicious Code Found. Please Enter valid data.');
        //form1.value = "";
        return false;
    }
}

function isNumericAmendment(form1) {
    var len, str, str1, i;
    len = form1.value.length;
    str = form1.value;
    str1 = "0123456789";
    for (i = 0; i < len; i++) {
        if ((str1.indexOf(str.charAt(i))) == -1) {
            // alert("Enter Alphabets or Numbers in the field");
            //form1.value = "";
            //form1.focus();
            return false;
        }
    }
    return true;
}

function isAlphanumericAmendment(form1) {
    var len, str, str1, i;
    len = form1.value.length;
    str = form1.value;
    str1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789`~!$%^*@#&'{};:<>?\, ";


    for (i = 0; i < len; i++) {
        if ((str1.indexOf(str.charAt(i))) == -1) {
            // alert("Enter Alphabets or Numbers in the field");
            //form1.value = "";
            //form1.focus();
            return false;
        }
    }
    return true;

}

function isAlphabeticAmendment(form1) {
    var len, str, str1, i;
    len = form1.value.length;
    str = form1.value;
    str1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz.,\n' ";

    for (i = 0; i < len; i++) {
        if ((str1.indexOf(str.charAt(i))) == -1) {
            // alert("Enter Alphabets or Numbers in the field");
            //form1.value = "";
            //form1.focus();
            return false;
        }
    }
    return true;
}


function isEmailAmendment(mail) {
    //var amountRegExp=new RegExp(/(^\d{0,11}\.\d{1,2}$)|(^\d{0,13}$)/);
    var mailRegExp = new RegExp(/^([a-zA-Z]{1}[\w\-\.]*\@([\da-zA-Z\-]{1,}\.){1,}[\da-zA-Z\-]{2,4})$/);

    if (mail.value.length == 0)
        return true;
    if (securityCheck(mail)) {
        var isValid = mailRegExp.test(mail.value);
        if (isValid == false) {
            //			alert('Please Enter Valid EMail id .');
            //			mail.value="";
            return false;
        }
        else {

            return true;
        }
    }
    else {
        $().toastmessage('showWarningToast', 'Malicious Code Found. Please Enter valid data.');
        //alert('Malicious Code Found. Please Enter valid data.');
        //		mail.value="";
        return false;
    }

}

function isAlphanumericTradingName(form1) {
    var len, str, str1, i;
    len = form1.value.length;
    str = form1.value;
    str1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789.&-' ";

    for (i = 0; i < len; i++) {
        if ((str1.indexOf(str.charAt(i))) == -1) {
            // alert("Enter Alphabets or Numbers in the field");
            //form1.value = "";
            //form1.focus();
            return false;
        }
    }
    return true;
}

function isAlphanumericDescriptionOfBussAct(form1) {
    $().toastmessage('showWarningToast', "inside validate method");
    //alert("inside validate method");

    var len, str, str1, i;
    len = form1.value.length;
    str = form1.value;
    str1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789.- ";
    $().toastmessage("form1::" + form1);
    //alert("form1::" + form1);
    $().toastmessage("form1::" + form1);
    //alert("str1::" + str1);
    if (securityCheck(form1)) {
        // $().toastmessage('showWarningToast', "inside if");
        //  alert("inside if");

        for (i = 0; i < len; i++) {
            // $().toastmessage("inside for");
            //alert("inside for");
            if ((str1.indexOf(str.charAt(i))) == -1) {
                // $().toastmessage("1 false");
                //alert("1 false");
                // alert("Enter Alphabets or Numbers in the field");
                //form1.value = "";
                //form1.focus();
                return false;
            }
        }
        //$().toastmessage("true");
        //		alert("true");
        return true;
    }
    else {
        // $().toastmessage("2 false");
        //alert("2 false");
        // alert('Malicious Code Found. Please Enter valid data.');
        //form1.value = "";
        return false;
    }
}

function isNumericSourceOfIncome(form1) {
    var len, str, str1, i, dec_counter, newi;
    len = form1.value.length;
    newi = 0;
    final_val = "";
    str = form1.value;

    if (securityCheck(form1)) {
        if (str.charAt(i) == '-') {
            str = str.substr(1, len);
            newi = 1;
        }

        str1 = "0123456789.";
        dec_counter = 0;
        dec_set = 0;
        after_dec = 0;
        for (i = newi; i < len; i++) {
            if (dec_set == 1) {
                after_dec++;
                if (after_dec > 2) {
                    final_val = str.substr(0, i);
                    form1.value = final_val;
                    $().toastmessage('showWarningToast', "Only two digits are allowed after decimal");
                    //		alert("Only two digits are allowed after decimal");
                    return true;
                }

            }
            if (str.charAt(i) == '.') {
                if (dec_counter > 0) {
                    $().toastmessage('showWarningToast', "Only one Decimal Point is allowed.");
                    //alert("Only one Decimal Point is allowed.");
                    form1.value = "";
                    //					form1.focus();
                    return false;
                }
                else {
                    dec_counter++;
                }
                dec_set = 1;
            }
            if ((str1.indexOf(str.charAt(i))) == -1) {
                $().toastmessage('showWarningToast', "Enter Numeric Data in this field");
                //alert("Enter Numeric Data in this field");
                form1.value = "";
                form1.focus();
                return false;
            }
        }
        if (dec_set == 0) {
            if (len > 13) {
                $().toastmessage('showWarningToast', "Only 13 digits are allowed before decimal.");
                //alert("Only 13 digits are allowed before decimal.");
                form1.value = "";
                form1.focus();
                return false;
            }
        }
        return true;
    }
    else {
        $().toastmessage('showWarningToast', 'Malicious Code Found. Please Enter valid data.');
        //alert('Malicious Code Found. Please Enter valid data.');
        return false;
    }
}

function isNumericSourceOfIncomeallDecimal(form1) {
    var len, str, str1, i, dec_counter, newi;
    len = form1.value.length;
    newi = 0;
    final_val = "";
    str = form1.value;

    if (securityCheck(form1)) {
        if (str.charAt(i) == '-') {
            str = str.substr(1, len);
            newi = 1;
        }

        str1 = "0123456789.";
        dec_counter = 0;
        dec_set = 0;
        after_dec = 0;
        for (i = newi; i < len; i++) {
            if (dec_set == 1) {
                after_dec++;
                if (after_dec > 4) {
                    final_val = str.substr(0, i);
                    form1.value = final_val;
                    $().toastmessage('showWarningToast', "Only four digits are allowed after decimal");
                    //		alert("Only two digits are allowed after decimal");
                    return true;
                }

            }
            if (str.charAt(i) == '.') {
                if (dec_counter > 0) {
                    $().toastmessage('showWarningToast', "Only one Decimal Point is allowed.");
                    //alert("Only one Decimal Point is allowed.");
                    form1.value = "";
                    //					form1.focus();
                    return false;
                }
                else {
                    dec_counter++;
                }
                dec_set = 1;
            }
            if ((str1.indexOf(str.charAt(i))) == -1) {
                $().toastmessage('showWarningToast', "Enter Numeric Data in this field");
                //alert("Enter Numeric Data in this field");
                form1.value = "";
                form1.focus();
                return false;
            }
        }
        if (dec_set == 0) {
            if (len > 13) {
                $().toastmessage('showWarningToast', "Only 13 digits are allowed before decimal.");
                //alert("Only 13 digits are allowed before decimal.");
                form1.value = "";
                form1.focus();
                return false;
            }
        }
        return true;
    }
    else {
        $().toastmessage('showWarningToast', 'Malicious Code Found. Please Enter valid data.');
        //alert('Malicious Code Found. Please Enter valid data.');
        return false;
    }
}


function isAlphanumericBusinessAssetName(form1) {

    var len, str, str1, i;
    len = form1.value.length;
    str = form1.value;
    str1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789.-'& \n";

    for (i = 0; i < len; i++) {

        if ((str1.indexOf(str.charAt(i))) == -1) {
            return false;
        }
    }

    return true;

}

function isNumericBusinessAssetValue(form1) {
    var len, str, str1, i, dec_counter, newi;
    len = form1.value.length;
    newi = 0;
    final_val = "";
    str = form1.value;

    if (str.charAt(i) == '-') {
        str = str.substr(1, len);
        newi = 1;
    }

    str1 = "0123456789.";
    dec_counter = 0;
    dec_set = 0;
    after_dec = 0;
    for (i = newi; i < len; i++) {
        if (dec_set == 1) {
            after_dec++;
            if (after_dec > 2) {
                final_val = str.substr(0, i);
                form1.value = final_val;
                //	alert("Only two digits are allowed after decimal");
                $().toastmessage('showWarningToast', "Only two digits are allowed after decimal");
                return false;
            }

        }
        if (str.charAt(i) == '.') {
            if (dec_counter > 0) {
                $().toastmessage('showWarningToast', "Only one Decimal Point is allowed.");
                //alert("Only one Decimal Point is allowed.");
                form1.value = "";
                //				form1.focus();
                return false;
            }
            else {
                dec_counter++;
            }
            dec_set = 1;
        }
        if ((str1.indexOf(str.charAt(i))) == -1) {
            //alert("Enter Numeric Data in this field");
            form1.value = "NotNumeric";
            form1.focus();
            return false;
        }
    }
    if (dec_set == 0) {
        if (len > 13) {
            $().toastmessage('showWarningToast', "Only 13 digits are allowed before decimal.");
            //alert("Only 13 digits are allowed before decimal.");
            form1.value = "";
            form1.focus();
            return false;
        }
    }
    return true;

}

function isAlphabeticNameAmd(form1) {
    var len, str, str1, i;
    len = form1.value.length;
    str = form1.value;
    str1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz.- \n";

    for (i = 0; i < len; i++) {

        if ((str1.indexOf(str.charAt(i))) == -1) {
            return false;
        }
    }

    return true;

}

function isAlphaNumericAmdPOBox(form1) {
    var len, str, str1, i;
    len = form1.value.length;
    str = form1.value;
    str1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789 \n";

    for (i = 0; i < len; i++) {
        if ((str1.indexOf(str.charAt(i))) == -1) {
            // alert("Enter Alphabets or Numbers in the field");
            //form1.value = "";
            //form1.focus();
            return false;
        }
    }
    return true;

}

function isAlphaNumericAmd(form1) {
    var len, str, str1, i;
    len = form1.value.length;
    str = form1.value;
    str1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-.,'&/() \n";

    for (i = 0; i < len; i++) {
        if ((str1.indexOf(str.charAt(i))) == -1) {
            // alert("Enter Alphabets or Numbers in the field");
            //form1.value = "";
            //form1.focus();
            return false;
        }
    }
    return true;

}

function isEmailAmd(mail) {
    //var amountRegExp=new RegExp(/(^\d{0,11}\.\d{1,2}$)|(^\d{0,13}$)/);
    var mailRegExp = new RegExp(/^([a-zA-Z]{1}[\w\-\.]*\@([\da-zA-Z\-]{1,}\.){1,}[\da-zA-Z\-]{2,4})$/);

    if (mail.value.length == 0)
        return true;

    var isValid = mailRegExp.test(mail.value);
    if (isValid == false) {
        //		alert('Please Enter Valid EMail id .');
        //		mail.value="";
        return false;
    }
    else {
        return true;
    }
}

function isAlphanumericFieldAmdAnx(form1) {
    var len, str, str1, i;
    len = form1.value.length;
    str = form1.value;
    str1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-.,'&/() \n";

    for (i = 0; i < len; i++) {
        if ((str1.indexOf(str.charAt(i))) == -1) {
            // alert("Enter Alphabets or Numbers in the field");
            //form1.value = "";
            //form1.focus();
            return false;
        }
    }
    return true;
}

function isAlphaNumericTradingDetailsAmdAnx(form1) {
    var len, str, str1, i;
    len = form1.value.length;
    str = form1.value;
    str1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789,&- \n";

    for (i = 0; i < len; i++) {
        if ((str1.indexOf(str.charAt(i))) == -1) {
            // alert("Enter Alphabets or Numbers in the field");
            //form1.value = "";
            //form1.focus();
            return false;
        }
    }
    return true;
}

function isAlphaNumericLicenseNumberAmdAnx(form1) {
    var len, str, str1, i;
    len = form1.value.length;
    str = form1.value;
    str1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789.-\n";

    for (i = 0; i < len; i++) {
        if ((str1.indexOf(str.charAt(i))) == -1) {
            // alert("Enter Alphabets or Numbers in the field");
            //form1.value = "";
            //form1.focus();
            return false;
        }
    }
    return true;
}

function isNumericNumberOfEmployeesAmdAnx(form1) {
    var len, str, str1, i;
    len = form1.value.length;
    str = form1.value;
    str1 = "0123456789\n";

    for (i = 0; i < len; i++) {
        if ((str1.indexOf(str.charAt(i))) == -1) {
            // alert("Enter Alphabets or Numbers in the field");
            //form1.value = "";
            //form1.focus();
            return false;
        }
    }
    return true;
}

function isPercentage(fieldObject) {
    if (!isNumericBusinessAssetValue(fieldObject)) {
        return false;
    }
    if (parseFloat(fieldObject.value, 10) > 100.00) {
        $().toastmessage('showWarningToast', "Percentage cannot not be more than 100");
        // alert("Percentage cannot not be more than 100");
        fieldObject.value = '';
        fieldObject.focus();
        return false;
    }
    if (parseFloat(fieldObject.value, 10) == 0) {
        $().toastmessage('showWarningToast', "Percentage should be more than zero");
        //alert("Percentage should be more than zero");
        fieldObject.value = '';
        fieldObject.focus();
        return false;
    }
    return true;
}

function isValidAlphaNumericPassport(form1) {
    var len, str, str1, i;
    len = form1.value.length;
    str = form1.value;
    str1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    for (i = 0; i < len; i++) {
        if ((str1.indexOf(str.charAt(i))) == -1) {
            // alert("Enter Alphabets or Numbers in the field");
            //form1.value = "";
            //form1.focus();
            return false;
        }
    }
    return true;
}

//end
/*Mehul Start Date Format for Enter Date Manual */
function checkdate(objName) {
    var datefield = objName;

    if (objName.value != 'dd/mm/yyyy' && objName.value != '') {
        if (chkdate(objName) == false) {
            datefield.select();
            $().toastmessage('showWarningToast', 'Please fill in dd/mm/yyyy format');
            // alert("Please fill in dd/mm/yyyy format");
            datefield.value = "";
            datefield.select();
            datefield.focus();
           
        }
        else {
            return true;
        }
    }
}

function checkdateamd(objName) {
    allowTodayAndFutureDateAmd(objName);
    var datefield = objName;
    if (trim(objName.value) != 'dd/mm/yyyy' && trim(objName.value) != '') {
        if (chkdateamd(objName) == false) {
            datefield.select();
            $().toastmessage('showWarningToast', 'Please fill in dd/mm/yyyy format');
            //alert("Please fill in dd/mm/yyyy format");
            datefield.value = "";
            datefield.select();
            datefield.focus();
            return false;
        }
        else {
            return true;
        }
    }
}

//Patel Dhaval1: 
function allowTodayAndFutureDateAmd(dateObj) {
    if (!checkdate(dateObj)) {
        return false;
    }
    var inputDate = dateObj.value;
    var serverDate = new Date();
    var curmonth = parseInt(serverDate.getMonth() + 1);
    var curyear = parseInt(serverDate.getFullYear());
    var curday = parseInt(serverDate.getDate());
    var todayDate = curday + "/" + curmonth + "/" + curyear;
    if (Date.parse(todayDate) != Date.parse(inputDate)) {
        if (!fnCompareDates1(todayDate, dateObj.value)) {
            $().toastmessage('showWarningToast', "Date should not be past date");
            //alert("Date should not be past date");
            dateObj.value = "";
            return false;
        }
    }
    return true;
}
function chkdate(objName) {
    var strDate;
    var strDateArray;
    var strDay;
    var strMonth;
    var strYear;
    var intday;
    var intMonth;
    var intYear;
    var booFound = false;
    var datefield = objName;
    var strSeparatorArray = new Array("-", " ", "/", ".");
    var intElementNr;
    var err = 0;
    var curdate = new Date();
    var comparedate;
    var strMonthArray = new Array(12);
    strMonthArray[0] = "Jan";
    strMonthArray[1] = "Feb";
    strMonthArray[2] = "Mar";
    strMonthArray[3] = "Apr";
    strMonthArray[4] = "May";
    strMonthArray[5] = "Jun";
    strMonthArray[6] = "Jul";
    strMonthArray[7] = "Aug";
    strMonthArray[8] = "Sep";
    strMonthArray[9] = "Oct";
    strMonthArray[10] = "Nov";
    strMonthArray[11] = "Dec";
    strDate = datefield.value;



    if (strDate.length <= 5 || strDate.length > 10) {
        return false;
    }
    for (intElementNr = 0; intElementNr < strSeparatorArray.length; intElementNr++) {
        if (strDate.indexOf(strSeparatorArray[intElementNr]) != -1) {
            strDateArray = strDate.split(strSeparatorArray[intElementNr]);
            if (strDateArray.length != 3) {
                err = 1;
                return false;
            }
            else {
                strDay = strDateArray[0];
                strMonth = strDateArray[1];
                strYear = strDateArray[2];
            }
            booFound = true;
        }
    }
    if (booFound == false) {
        if (strDate.length > 5) {
            strDay = strDate.substr(0, 2);
            strMonth = strDate.substr(2, 2);
            strYear = strDate.substr(4);
        }
    }


    if (strYear.length == 3 || strYear.length > 4) {
        return false;
    }

    if (strYear.length == 1) {
        strYear = '200' + strYear;
    }

    if (strYear.length == 2) {
        strYear = '20' + strYear;
    }


    intday = parseInt(strDay, 10);
    if (isNaN(intday)) {
        err = 2;
        return false;
    }
    intMonth = parseInt(strMonth, 10);
    if (intMonth < 10)
        intMonth = "0" + intMonth;
    if (isNaN(intMonth)) {
        for (i = 0; i < 12; i++) {
            if (strMonth.toUpperCase() == strMonthArray[i].toUpperCase()) {
                intMonth = i + 1;
                strMonth = strMonthArray[i];
                i = 12;
            }
        }
        if (isNaN(intMonth)) {
            err = 3;
            return false;
        }
    }
    intYear = parseInt(strYear, 10);
    if (isNaN(intYear)) {
        err = 4;
        return false;
    }
    if (intMonth > 12 || intMonth < 1) {
        err = 5;
        return false;
    }
    if ((intMonth == 1 || intMonth == 3 || intMonth == 5 || intMonth == 7 || intMonth == 8 || intMonth == 10 || intMonth == 12) && (intday > 31 || intday < 1)) {
        err = 6;
        return false;
    }
    if ((intMonth == 4 || intMonth == 6 || intMonth == 9 || intMonth == 11) && (intday > 30 || intday < 1)) {
        err = 7;
        return false;
    }
    if (intMonth == 2) {
        if (intday < 1) {
            err = 8;
            return false;
        }
        if (LeapYear(intYear) == true) {
            if (intday > 29) {
                err = 9;
                return false;
            }
        }
        else {
            if (intday > 28) {
                err = 10;
                return false;
            }
        }
    }

    tempdate = intMonth + "/" + strDay + "/" + strYear;
    datefield.value = strDay + "/" + intMonth + "/" + strYear;

    return true;
}
function chkdateamd(objName) {
    var strDate;
    var strDateArray;
    var strDay;
    var strMonth;
    var strYear;
    var intday;
    var intMonth;
    var intYear;
    var booFound = false;
    var datefield = objName;
    var strSeparatorArray = new Array("-", " ", "/", ".");
    var intElementNr;
    var err = 0;
    var curdate = new Date();
    var comparedate;
    var strMonthArray = new Array(12);
    strMonthArray[0] = "Jan";
    strMonthArray[1] = "Feb";
    strMonthArray[2] = "Mar";
    strMonthArray[3] = "Apr";
    strMonthArray[4] = "May";
    strMonthArray[5] = "Jun";
    strMonthArray[6] = "Jul";
    strMonthArray[7] = "Aug";
    strMonthArray[8] = "Sep";
    strMonthArray[9] = "Oct";
    strMonthArray[10] = "Nov";
    strMonthArray[11] = "Dec";
    strDate = trim(datefield.value);


    if (strDate.length <= 5 || strDate.length > 10) {
        return false;
    }
    for (intElementNr = 0; intElementNr < strSeparatorArray.length; intElementNr++) {
        if (strDate.indexOf(strSeparatorArray[intElementNr]) != -1) {
            strDateArray = strDate.split(strSeparatorArray[intElementNr]);
            if (strDateArray.length != 3) {
                err = 1;
                return false;
            }
            else {
                strDay = strDateArray[0];
                strMonth = strDateArray[1];
                strYear = strDateArray[2];
            }
            booFound = true;
        }
    }
    if (booFound == false) {
        if (strDate.length > 5) {
            strDay = strDate.substr(0, 2);
            strMonth = strDate.substr(2, 2);
            strYear = strDate.substr(4);
        }
    }


    if (strYear.length == 3 || strYear.length > 4) {
        return false;
    }

    if (strYear.length == 1) {
        strYear = '200' + strYear;
    }

    if (strYear.length == 2) {
        strYear = '20' + strYear;
    }


    intday = parseInt(strDay, 10);
    if (isNaN(intday)) {
        err = 2;
        return false;
    }
    intMonth = parseInt(strMonth, 10);
    if (intMonth < 10)
        intMonth = "0" + intMonth;
    if (isNaN(intMonth)) {
        for (i = 0; i < 12; i++) {
            if (strMonth.toUpperCase() == strMonthArray[i].toUpperCase()) {
                intMonth = i + 1;
                strMonth = strMonthArray[i];
                i = 12;
            }
        }
        if (isNaN(intMonth)) {
            err = 3;
            return false;
        }
    }
    intYear = parseInt(strYear, 10);
    if (isNaN(intYear)) {
        err = 4;
        return false;
    }
    if (intMonth > 12 || intMonth < 1) {
        err = 5;
        return false;
    }
    if ((intMonth == 1 || intMonth == 3 || intMonth == 5 || intMonth == 7 || intMonth == 8 || intMonth == 10 || intMonth == 12) && (intday > 31 || intday < 1)) {
        err = 6;
        return false;
    }
    if ((intMonth == 4 || intMonth == 6 || intMonth == 9 || intMonth == 11) && (intday > 30 || intday < 1)) {
        err = 7;
        return false;
    }
    if (intMonth == 2) {
        if (intday < 1) {
            err = 8;
            return false;
        }
        if (LeapYear(intYear) == true) {
            if (intday > 29) {
                err = 9;
                return false;
            }
        }
        else {
            if (intday > 28) {
                err = 10;
                return false;
            }
        }
    }

    tempdate = intMonth + "/" + strDay + "/" + strYear;
    datefield.value = strDay + "/" + intMonth + "/" + strYear;

    return true;
}

function isNumeric_DecimalNegativeNew(form1) {
    var len, str, str1, i, dec_counter, newi;
    len = form1.value.length;
    newi = 0;
    final_val = "";
    str = form1.value;
    if (securityCheck(form1)) {
        if (str.charAt(i) == '-') {
            str = str.substr(1, len);
            newi = 1;
        }

        str1 = "0123456789.";
        dec_counter = 0;
        dec_set = 0;
        after_dec = 0;
        for (i = newi; i < len; i++) {
            if (dec_set == 1) {
                after_dec++;
                if (after_dec > 2) {
                    final_val = str.substr(0, i);
                    form1.value = final_val;
                    $().toastmessage('showWarningToast', "Only two digits are allowed after decimal");
                    //alert("Only two digits are allowed after decimal");
                    return true;
                }

            }
            if (str.charAt(i) == '.') {
                if (dec_counter > 0) {
                    $().toastmessage('showWarningToast', "Only one Decimal Point is allowed.");
                    //alert("Only one Decimal Point is allowed.");
                    form1.value = "";
                    //            form1.focus();
                    return false;
                }
                else {
                    dec_counter++;
                }
                dec_set = 1;
            }
            if ((str1.indexOf(str.charAt(i))) == -1) {
                $().toastmessage('showWarningToast', "Enter Numeric Data in this field");
                //alert("Enter Numeric Data in this field");
                form1.value = "";
                form1.focus();
                return false;
            }
        }
        if (dec_set == 0) {
            if (len > 20) {
                $().toastmessage('showWarningToast', "Only 20 digits are allowed before decimal.");
                //alert("Only 20 digits are allowed before decimal.");
                form1.value = "";
                form1.focus();
                return false;
            }
        }
        return true;
    }
    else {
        $().toastmessage('showWarningToast', 'Malicious Code Found. Please Enter valid data.');
        //alert('Malicious Code Found. Please Enter valid data.');
        return false;
    }
}


/*Mehul End Date Format for Enter Date Manual */

// E-Refund :: Starts
function isNameRfnd(form1) {
    var len, str, str1, i;
    len = form1.value.length;
    str = form1.value;
    //  str1="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789/-,.& \n";  
    str1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz '\n";
    //  if(securityCheck(form1))
    //  { 

    for (i = 0; i < len; i++) {
        if ((str1.indexOf(str.charAt(i))) == -1) {
            $().toastmessage('showWarningToast', "Enter only Alphabets with white space in the field");
            //alert("Enter only Alphabets with white space in the field");
            form1.value = "";
            //         form1.focus();
            setTimeout(function () { form1.focus(); }, 10);
            return false;
        }
    }
    return true;
    //  }
    //  else
    //  {    
    //    alert('Malicious Code Found. Please Enter valid data.');
    //    form1.value="";
    //    return false;
    //  }
    //  return true;
}

function isAlphaNumericRfnd(form1) {
    var len, str, str1, i;
    len = form1.value.length;
    str = form1.value;
    str1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-,.&!#()' \n";


    for (i = 0; i < len; i++) {
        if ((str1.indexOf(str.charAt(i))) == -1) {
            $().toastmessage('showWarningToast', "Enter Alphabets or Numbers in the field");
            //alert("Enter Alphabets or Numbers in the field");
            form1.value = "";
            //form1.focus();
            return false;
        }
    }
    return true;

}
// E-Refund :: Ends