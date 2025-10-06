document.getElementById("editForm").addEventListener("submit", function(e){
    e.preventDefault();

    let updatedData = {
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

    xhrUpdate.onreadystatechange = function(){
        if(xhrUpdate.readyState === 4){
            if(xhrUpdate.status === 200){
                alert("User updated successfully ✅");
            } else {
                alert("Error updating user ❌");
            }
        }
    };
    xhrUpdate.send(JSON.stringify(updatedData));
});
