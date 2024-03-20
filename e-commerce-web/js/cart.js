const productsContainer = document.getElementById("productsContainer")
let products
let total = document.getElementById("total")

function getData() {
    products = JSON.parse(localStorage.getItem("data"))
    renderProducts()
}

function renderProducts() {
    productsContainer.innerHTML = `<div class=" col-12 text-center mb-5">
                    <h2 style="width:fit-content; margin:auto;" class="title text-black fw-bold py-2 position-relative">Shopping
                        Cart</h2>
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
                            <div class="d-flex justify-content-between m-auto align-items-center " style="max-width: 100px;">
                                <button onclick="remove(event, ${products[i].id})" class="rounded-circle f m-0 fs-1 border-0 bg-transparent" style="cursor: pointer; color: red;">-</button>
                                <p class="fw-bold mx-3 my-0">${products[i].amount}</p>
                                <button onclick="add(event, ${products[i].id})" style="cursor: pointer; color: green;" class="border-0 bg-transparent m-0 fs-2  fw-bold">+</butt>
                            </div>
                            <button onclick="removeFromCart(${products[i].id})" class="mb-3 cartBtn${products[i].id} btn py-2 rounded-0  fw-bold btn4 border-2 border-black bg-white">Remove From Cart</button>
                            
                        </div>
                    </div>
                    
                </div>
                <hr>
        `
    }
    caluclateTotal()
    if (products.length == 0) {
        let h2 = document.createElement("h2")
        h2.textContent = "You haven't added any items to the cart"
        h2.style.textAlign = "center"
        productsContainer.appendChild(h2)
    }
}
function caluclateTotal() {    
    let cost = products.reduce((tot, ele) => {
        let price = +ele.price.replace("$", "")
        return tot + (price * ele.amount)
        
    }, 0)
    
    total.textContent = `$${cost}`
}

function add(event, id) {
    // products[id].amount += 1
    let index = products.findIndex((ele) => ele.id === id)
    products[index].amount += 1
    event["target"].previousElementSibling.textContent = products[index].amount
    caluclateTotal()

}

function remove(event, id) {
    let index = products.findIndex((ele) => ele.id === id)
    products[index].amount -= 1
    if (products[index].amount == 0) {
        products.splice(index, 1)      
        renderProducts()
    }
    else {
        caluclateTotal()
        event["target"].nextElementSibling.textContent = products[index].amount
    }
}

function removeFromCart(id) {
    let index = products.findIndex((ele) => ele.id === id)
    products[index].amount = 0
    products.splice(index, 1)
    renderProducts()
}
function loadHome() {
    location.assign("index.html")
}

window.onload = getData

window.addEventListener("beforeunload", function () {
    localStorage.setItem("data", JSON.stringify(products))
})   