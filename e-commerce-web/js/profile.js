let profileImage = document.getElementById("profileImage")
const productsContainer = document.getElementById("productsContainer")
let products
let user = document.getElementById("name")
let email = document.getElementById("email")


function getData() {

    products = JSON.parse(localStorage.getItem("favs"))
    if (!products) {
        products = []
    }
    console.log(products)
    renderProducts()
}




function renderProducts() {
    productsContainer.innerHTML = `<div class=" col-12 text-center mb-5">
                    <h2 style="width:fit-content; margin:auto;" class="title text-black fw-bold py-2 position-relative">Favourites</h2>
                </div>
                <hr>`
    for (let i = 0; i < products.length; i++) {


        productsContainer.innerHTML += `
        
        <div class="row align-items-center gy-4 text-center text-md-start"  >
                    <div class="col-md-5 box">
                        <img src="${products[i].image}" loading="lazy" class=" w-100 img-fluid  bg-secondary bg-opacity-10 rounded-top"
                            alt="Golden Watch">
                    </div>
                    <div class="col-md-7 text-center >
                        <div class="px-0 py-4   position-relative">
                            <h5 class="fw-bold">${products[i].name}</h5>
                            <p class="opacity-75 mb-1 fw-bold " >Price: ${products[i].price}</p>
                            <i class="fas fa-star py-2" style="color: rgb(255, 192, 34);"></i>
                            <i class="fas fa-star" style="color: rgb(255, 192, 34);"></i>
                            <i class="fas fa-star" style="color: rgb(255, 192, 34);"></i>
                            <i class="fas fa-star" style="color: rgb(255, 192, 34);"></i>
                            <i class="fas fa-star" style="color: rgb(255, 192, 34);"></i>
                            <p class="opacity-75">Catagory: ${products[i].catagory}</p>
                           
                            <i onclick='favourite(${products[i].id})' class="favBtn rounded-circle p-2  fav${products[i].id} fas fa-heart fa-2x "></i>
                            
                        </div>
                    </div>
                    
                </div>
                <hr>
        `
    }
    if (products.length == 0) {
        let h2 = document.createElement("h2")
        h2.textContent = "You haven't added items to your favourite"
        h2.style.textAlign = "center"
        productsContainer.appendChild(h2)
    }
}

function favourite(id) {
    let index = products.findIndex((ele) => ele.id === id)
    products[index].fav = false
    products.splice(index, 1)
    renderProducts()
    
}
window.onload = function () {
    getData()
    user.textContent = `${localStorage.getItem("firstName")} ${ localStorage.getItem("lastName") }`
    email.textContent = `${localStorage.getItem("email")}`
    // let image = (localStorage.getItem("image"))
    // console.log(image)
    // let url = URL.createObjectURL(image)
    // profileImage.src = url
    // console.log(profileImage)
}
window.addEventListener("beforeunload", function () {
    localStorage.setItem("favs", JSON.stringify(products))
})   