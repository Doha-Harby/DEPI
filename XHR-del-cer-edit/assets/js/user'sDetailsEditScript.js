function $(id) {
    return document.getElementById(id);
}

let id = location.search.split("=")[1]; //ID => url

// ---------- GET USER DATA ----------
let xhr = new XMLHttpRequest();
xhr.open("GET", `https://68d9a2a290a75154f0dad472.mockapi.io/users/${id}`);
xhr.onreadystatechange = function () {
    if (xhr.readyState === 4 && xhr.status === 200) {
        let data = JSON.parse(xhr.responseText);
        $("name").value = data.name;
        $("gender").value = data.gender;
        $("email").value = data.email;
        $("street").value = data.street;
        $("city").value = data.city;
        $("zipcode").value = data.zipcode;
        $("phone").value = data.phone;
        $("companyName").value = data.companyName;
    }
};
xhr.send();

// ---------- UPDATE USER DATA ----------
function updateUser() {
    let updatedUser = {
        name: $("name").value,
        gender: $("gender").value,
        email: $("email").value,
        street: $("street").value,
        city: $("city").value,
        zipcode: $("zipcode").value,
        phone: $("phone").value,
        companyName: $("companyName").value
    };

    let xhrUpdate = new XMLHttpRequest();
    xhrUpdate.open("PUT", `https://68d9a2a290a75154f0dad472.mockapi.io/users/${id}`, true);
    xhrUpdate.setRequestHeader("Content-Type", "application/json");
    xhrUpdate.onreadystatechange = function () {
        if (xhrUpdate.readyState === 4) {
            if (xhrUpdate.status === 200) {
                alert("User updated successfully!");
            } else {
                alert("Update failed!");
            }
        }
    };
    xhrUpdate.send(JSON.stringify(updatedUser));
}function updateUser() {
    let updatedUser = {
        name: $("name").value,
        gender: $("gender").value,
        email: $("email").value,
        street: $("street").value,
        city: $("city").value,
        zipcode: $("zipcode").value,
        phone: $("phone").value,
        companyName: $("companyName").value
    };

    let xhrUpdate = new XMLHttpRequest();
    xhrUpdate.open("PUT", `https://68d9a2a290a75154f0dad472.mockapi.io/users/${id}`, true);
    xhrUpdate.setRequestHeader("Content-Type", "application/json");
    xhrUpdate.onreadystatechange = function () {
        if (xhrUpdate.readyState === 4) {
            if (xhrUpdate.status === 200) {
                alert("User updated successfully! \n click ok to go back to the main bage");
                window.location.href = "index.html";
            } else {
                alert("Update failed!");
            }
        }
    };
    xhrUpdate.send(JSON.stringify(updatedUser));
}