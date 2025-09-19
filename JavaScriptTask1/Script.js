function $(id)
{
    return document.getElementById(id);
}
const userName = $("userName");
const imageName = $("imageName");
const fonts = $("fonts");
const incFontBtn = $("incFontBtn");
const decFontBtn = $("decFontBtn");
const resBtn = $("resBtn");
const output = $("output");

let fontSize = 18;

resBtn.addEventListener("click",()=>{
    output.innerText = ""; 
    let name = userName.value.trim();
    let image = imageName.value.trim();
   
    if (!name || !/^[a-zA-Z\s]+$/.test(name)) {
    let errorMsg = document.createElement("p");
    errorMsg.innerText = "Invalid Name";
    errorMsg.style.color = "red";
    output.appendChild(errorMsg);
    return; 
    }

    let massage = document.createElement("p");
    massage.innerText = "Welcome "+ name;
    massage.style.fontFamily = fonts.value;
    massage.style.fontSize = fontSize + "px";

    output.appendChild(massage);

    if(image.endsWith(".jpg") || image.endsWith(".png"))
    {
        let img = document.createElement("img");
        img.src = image;
        img.style.width = "200px";
        output.appendChild(img);

        let incPicBtn = document.createElement("button");
        incPicBtn.innerText = "incrase picture size"; 
        output.appendChild(incPicBtn);

        incPicBtn.addEventListener("click", ()=>
        {
            let currentWidth = img.offsetWidth;
            let neWidth = currentWidth + 20;
            let maxWidth = window.innerWidth;

            if (neWidth < maxWidth) {
                img.style.width = neWidth + "px";
            }
        });
    }else if (image) {
        let errorMsg = document.createElement("p");
        errorMsg.innerText = "Invalid Image (must be .jpg or .png)";
        errorMsg.style.color = "red";
        output.appendChild(errorMsg);
    }
   

});

incFontBtn.addEventListener("click", ()=>{
    if(fontSize < 40)
    {
        fontSize += 2;
    }
    update();
});

decFontBtn.addEventListener("click", ()=>{
    if(fontSize > 10)
    {
        fontSize -= 2;
    }
    update();
});

fonts.addEventListener("change", () => {
    update();
});

function update() {
    let texts = output.querySelectorAll("p");
    texts.forEach(p => {
        p.style.fontSize = fontSize + "px";
        p.style.fontFamily = fonts.value;
    });
}