var patientInputField,patientInputIti,input,iti1;
jQuery(document).ready(function () {
    // Your jQuery code here
    jQuery("#selectAll").change(function () {
        jQuery(".check").prop("checked", jQuery(this).prop("checked"));
    });
});



if (document.getElementById("exampleModal")) {
    $(document).ready(function () {
        $("#exampleModal").modal('show');
    });
}




//if (document.getElementById("patientMobileNumber")) {
//    const phoneInputFieldForPatient = document.getElementById("patientMobileNumber");
//    const phoneInputPatient = window.intlTelInput(phoneInputFieldForPatient, {
//        utilsScript:
//            "https://cdnjs.cloudflare.com/ajax/libs/intl-tel-input/17.0.8/js/utils.js",
//    });
//}


function toggle_light_mode() {
    var app = document.getElementsByTagName("BODY")[0];
    if (localStorage.lightMode == "dark") {
        localStorage.lightMode = "light";
        app.setAttribute("light-mode", "light");
    } else {
        localStorage.lightMode = "dark";
        app.setAttribute("light-mode", "dark");
    }
}


if (document.getElementById("dateOfBirth")) {
    var today = new Date().toISOString().split('T')[0];
    // Set the max attribute of the date input to today's date
    document.getElementById("dateOfBirth").max = today;
}


if (document.getElementById("patientMobileNumber")) {
    patientInputField = document.querySelector("#patientMobileNumber");
    patientInputIti = window.intlTelInput(patientInputField, {
        initialCountry: "in",
        separateDialCode: true,
        utilsScript: "https://cdnjs.cloudflare.com/ajax/libs/intl-tel-input/17.0.8/js/utils.js"
    });
}


function validatePhoneNumber() {
    if (patientInputIti.isValidNumber()) {
        document.getElementById("textChange").innerHTML = "Valid";
        document.getElementById("textChange").classList.remove("invalid-text");
        document.getElementById("textChange").classList.add("valid-text");
    } else {
        document.getElementById("textChange").innerHTML = "Invalid";
        document.getElementById("textChange").classList.remove("valid-text");
        document.getElementById("textChange").classList.add("invalid-text");
    }
    const countryCodeInput = document.getElementById("patientCountryCode");
    countryCodeInput.value = patientInputIti.getSelectedCountryData().dialCode;
    console.log(countryCodeInput.value);
}



if (document.getElementById("OtherMobileNumber")) {
     input = document.querySelector("#OtherMobileNumber");
    iti1 = window.intlTelInput(input, {
        initialCountry: "in",
        separateDialCode: true,
        utilsScript: "https://cdnjs.cloudflare.com/ajax/libs/intl-tel-input/17.0.8/js/utils.js"
    });

}   //Validate phone number
function validatePhoneNumberForOthers() {
    if (iti1.isValidNumber()) {
        document.getElementById("textChangeOther").innerHTML = "Valid";
        document.getElementById("textChangeOther").classList.remove("invalid-text");
        document.getElementById("textChangeOther").classList.add("valid-text");
    } else {
        document.getElementById("textChangeOther").innerHTML = "Invalid";
        document.getElementById("textChangeOther").classList.remove("valid-text");
        document.getElementById("textChangeOther").classList.add("invalid-text");
    }

    const otherCode = document.getElementById("OtherCountryCode");
    otherCode.value = iti1.getSelectedCountryData().dialCode;

}


function ChangeData() {
    const inputFields = document.querySelectorAll(".formProperty");


    inputFields.forEach(function (inputField) {
        inputField.disabled = !inputField.disabled;
    });

    const editbutton = document.getElementById("EditButton");
    editbutton.classList.add("d-none");

    const btn = document.getElementById("saveBtn");
    btn.classList.remove("d-none");
   
    const cancelbtn = document.getElementById("cancelBtn");
    cancelbtn.classList.remove("d-none");

}

function CancelData() {
    const inputfields = document.querySelectorAll(".formProperty");
    inputfields.forEach(function (inputfield) {
        inputfield.disabled = !inputfield.disabled;
    });


    const editbutton = document.getElementById("EditButton");
    editbutton.classList.remove("d-none");

    const btn = document.getElementById("saveBtn");
    btn.classList.add("d-none");

    const cancelbtn = document.getElementById("cancelBtn");
    cancelbtn.classList.add("d-none");

}
