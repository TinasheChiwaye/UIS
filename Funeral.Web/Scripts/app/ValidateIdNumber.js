Date.prototype.monthNames = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
Date.prototype.getMonthName = function () { return this.monthNames[this.getMonth()] };
Date.prototype.getShortMonthName = function () { return this.getMonthName().substr(0, 3); };

function Validate(idNumber, BirthDateControl) {
    var correct = true;
    if (idNumber.length != 13 || !isNumber(idNumber)) {
        correct = false;
    }
    var tempDate = new Date(idNumber.substring(0, 2), idNumber.substring(2, 4) - 1, idNumber.substring(4, 6));
    var id_date = tempDate.getDate();
    var id_month = tempDate.getMonth();
    var dMonth = id_month + 1;
    var dMonthName = tempDate.getMonthName();
    var id_year = idNumber.substring(0, 2);

    var cutoff = (new Date()).getFullYear() - 2000;
    var fullDate = id_date + " " + dMonthName + " " + (id_year > cutoff ? '19' : '20') + id_year;
    //var fullDate = id_date + " " + dMonthName + " " + id_year;
    if (!((tempDate.getYear() == idNumber.substring(0, 2)) && (id_month == idNumber.substring(2, 4) - 1) && (id_date == idNumber.substring(4, 6)))) {
        correct = false;
    }

    // get country ID for citzenship
    var citzenship = parseInt(idNumber.substring(10, 11)) == 0 ? "Yes" : "No";

    // apply Luhn formula for check-digits
    var tempTotal = 0;
    var checkSum = 0;
    var multiplier = 1;
    for (var i = 0; i < 13; ++i) {
        tempTotal = parseInt(idNumber.charAt(i)) * multiplier;
        if (tempTotal > 9) {
            tempTotal = parseInt(tempTotal.toString().charAt(0)) + parseInt(tempTotal.toString().charAt(1));
        }
        checkSum = checkSum + tempTotal;
        multiplier = (multiplier % 2 == 0) ? 1 : 2;
    }
    if ((checkSum % 10) != 0) {
        //    error.append('<p>ID number does not appear to be authentic - check digit is not valid</p>');
        correct = false;
    };


    // if no error found, hide the error message
    if (correct) {
        $("#" + BirthDateControl).val(fullDate);
        $("#" + BirthDateControl).val(fullDate).datepicker('update');

        var genderCode = idNumber.substring(6, 10);
        var gender = parseInt(genderCode) < 5000 ? "Female" : "Male";

        if (gender == "Female") {
            $("#tab-1 input[name=gender][value=1]").prop('checked', true);
        } else {
            $("#tab-1 input[name=gender][value=0]").prop('checked', true);
        }
        CalculateAge(fullDate);
    }
    else {
        if (idNumber != "") {
            alert("Id Number invalid");
            $("#txtBirthDay").val("");
            $("#tab-1 #txtAge").val("");
        }
    }
    return false;
}

function CalculateAge(fullDate) {
    var mdate = dateFormat(fullDate, 'mm-dd-yyyy', false);
    var yearThen = parseInt(mdate.substring(6, 10), 10);
    var monthThen = parseInt(mdate.substring(0, 2), 10);
    var dayThen = parseInt(mdate.substring(3, 5), 10);

    var today = new Date();
    var birthday = new Date(yearThen, monthThen - 1, dayThen);

    var differenceInMilisecond = today.valueOf() - birthday.valueOf();

    var year_age = Math.floor(differenceInMilisecond / 31536000000);
    if (isNaN(year_age)) {
        alert("Invalid birthday - Please try again!");
    }
    else {
        $("#tab-1 #txtAge").val(year_age);
    }
}
function isNumber(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
}

