var cinput = document.getElementById("ci");
var valinput = document.getElementById("val_ci");
var random = document.getElementById("random");
var form = document.getElementById("formulario")
var submit = document.getElementById("formulario")


//cinput.addEventListener('keyup', function () {
//    ci_uy(cinput.value);
//});

form.addEventListener('submit', e => {        

    if (validate_ci(valinput.value) == false) {

        e.preventDefault();
        alert("Cedula Invalida");
    } 
});

random.addEventListener('click', e => {
    document.getElementById("ran_ci").value = random_ci();
});

function ci_uy(ci) {
    if (ci.length > 5) {
        ci = clean_ci(ci);
        document.getElementById("validation").value = validation_digit(ci);
    }
}

