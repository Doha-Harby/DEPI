const form = document.getElementById("form");

function displayError(eleId, msg, color = "red"){
    document.getElementById(eleId).innerText = msg;
    document.getElementById(eleId).style.color = color;
}

function validateName(){
    let value = form['name'].value.trim();
    if(value === ""){
        displayError("name-error","*Name is required");
        return false;
    }else if(!/^[a-zA-Z ]{3,20}$/.test(value)){
        displayError("name-error","*Invalid name");
        return false;
    }else{
        displayError("name-error","Valid","green");
        return true;
    }
}

function validateEmail(){
    let value = form['email'].value.trim();
    if(value === ""){
        displayError("email-error","*Email is required");
        return false;
    }else if(!/^[a-zA-Z]+[0-9._]*@[a-z]+\.[a-z]{2,}$/.test(value)){
        displayError("email-error","*Invalid email");
        return false;
    }else{
        displayError("email-error","Valid","green");
        return true;
    }
}

function validatePassword(){
    let value = form['password'].value;
    if(value === ""){
        displayError("password-error","*Password is required");
        return false;
    }else if(value.length < 6){
        displayError("password-error","*At least 6 characters");
        return false;
    }else{
        displayError("password-error","Valid","green");
        return true;
    }
}

function validateConfirmPassword(){
    let pass = form['password'].value;
    let confirm = form['confirmPassword'].value;
    if(confirm === ""){
        displayError("confirmPassword-error","*Please confirm password");
        return false;
    }else if(pass !== confirm){
        displayError("confirmPassword-error","*Passwords do not match");
        return false;
    }else{
        displayError("confirmPassword-error","Passwords match","green");
        return true;
    }
}

function validatePhone(){
    let value = form['phoneNumber'].value.trim();
    if(value === ""){
        displayError("phoneNumber-error","*Phone number is required");
        return false;
    }else if(!/^(01)[0-9]{9}$/.test(value)){
        displayError("phoneNumber-error","*Invalid phone (11 digits starting with 01)");
        return false;
    }else{
        displayError("phoneNumber-error","Valid","green");
        return true;
    }
}

function validateNational(){
    let value = form['national'].value.trim();
    if(value === ""){
        displayError("national-error","*National ID is required");
        return false;
    }else if(!/^[0-9]{14}$/.test(value)){
        displayError("national-error","*National ID must be 14 digits");
        return false;
    }else{
        displayError("national-error","Valid","green");
        return true;
    }
}

//  live validation 
form['name'].addEventListener("input", validateName);
form['email'].addEventListener("input", validateEmail);
form['password'].addEventListener("input", validatePassword);
form['confirmPassword'].addEventListener("input", validateConfirmPassword);
form['phoneNumber'].addEventListener("input", validatePhone);
form['national'].addEventListener("input", validateNational);

// final check 
form.onsubmit = function(e){
    if(!(validateName() & validateEmail() & validatePassword() & validateConfirmPassword() & validatePhone() & validateNational())){
        e.preventDefault(); 
    }
}