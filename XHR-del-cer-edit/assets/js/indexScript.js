const tbody = document.getElementById("tbody");
const xhr = new XMLHttpRequest();
xhr.open("GET","https://68d9a2a290a75154f0dad472.mockapi.io/users");
xhr.onreadystatechange = function () {
    if(xhr.readyState == 4 && xhr.status == 200){
         data = JSON.parse(xhr.responseText);
        for(let item of data){
           tbody.innerHTML += `
            <tr>
                <td>${item.id}</td>
                <td>${item.name}</td>
                <td>${item.gender}</td>
                <td>${item.email}</td>
                <td><a href="user'sDetailsEdit.html?id=${item.id}">Details and Edit</a></td>
                <td><button onclick = "deleteUser(this)" data-id="${item.id}">Delete</button></td>
            </tr>`;
        }
    }   
}
xhr.send();

function deleteUser(button){ //this == delete button
    let id = button.getAttribute("data-id");
    let xhr = new XMLHttpRequest();
    xhr.open("DELETE", `https://68d9a2a290a75154f0dad472.mockapi.io/users/${id}`);
    xhr.onreadystatechange = function (){
      if(xhr.readyState == 4){
            if (xhr.status === 200 || xhr.status === 204) {
                document.getElementById("massage").innerText = "Deleted ✅";
                document.getElementById("massage").style.color = "green";
                button.closest("tr").remove();
            } else {
                document.getElementById("massage").innerText = "Error ❌";
                document.getElementById("massage").style.color = "red";
            }
        }
    }
    xhr.send();
}