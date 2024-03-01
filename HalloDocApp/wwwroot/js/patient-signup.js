const togglePassword = document.querySelector('#togglePassword');
const password = document.querySelector('#passwordfield');
togglePassword.addEventListener('click', function (e) {
    // toggle the type attribute
    const type = password.getAttribute('type') === 'password' ? 'text' : 'password';
    password.setAttribute('type', type);
    // toggle the eye / eye slash icon
    this.classList.toggle('bi-eye');
});

const ConfirmtogglePassword = document.querySelector('#confirmtogglePassword');
const cnfpassword = document.querySelector('#confirmpasswordfield');
ConfirmtogglePassword.addEventListener('click', function (e) {
    // toggle the type attribute
    const type = cnfpassword.getAttribute('type') === 'password' ? 'text' : 'password';
    cnfpassword.setAttribute('type', type);
    // toggle the eye / eye slash icon
    this.classList.toggle('bi-eye');
});


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

