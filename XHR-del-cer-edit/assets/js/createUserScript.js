const form = document.getElementById("addUserForm");
const massage = document.getElementById("massage");

form.onsubmit = function (e) {
    e.preventDefault();

    const newData = {
        name: form.name.value,
        gender: form.gender.value,
        email: form.email.value,
        street: form.street.value,
        city: form.city.value,
        zipcode: form.zipcode.value,
        phone: form.phone.value,
        companyName: form.companyName.value,
    };

    const xhr = new XMLHttpRequest();
    xhr.open("POST", "https://68d9a2a290a75154f0dad472.mockapi.io/users");
    xhr.setRequestHeader("Content-Type", "application/json");

    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4) {
            if (xhr.status == 201) {
                massage.innerText = "Created Successfully";
                form.reset();
            } else {
                massage.innerText = "Error while creating doctor";
            }
        }
    };

    xhr.send(JSON.stringify(newData));
};
