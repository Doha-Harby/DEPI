const table = document.getElementById("table");
const tbody = document.getElementById("tbody");
const xhr = new XMLHttpRequest();
xhr.open("GET","https://jsonplaceholder.typicode.com/todos");
xhr.onreadystatechange =function(){
    if(xhr.readyState == 4){
        data = JSON.parse(xhr.responseText);
        for(item of data){
          const completedMark = item.completed ? "✅" : "❌";                       
          const completedClass = item.completed  ? "completed-true" : "completed-false";

          tbody.innerHTML += 
          ` <tr>
              <td>${item.userId}</td>
              <td>${item.id}</td>
              <td>${item.title}</td>
              <td class="${completedClass}">
                ${completedMark}
              </td>
            </tr>`;
        }
    }  
}
xhr.send();

