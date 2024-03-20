
const Pname = document.getElementById("Pname")
const catagory = document.getElementById("Pcatagory")
const budget = document.getElementById("budget")
const productsContainer = document.getElementById("productsContainer")
let storedData = []
if (JSON.parse(localStorage.getItem("data"))) {
    storedData = JSON.parse(localStorage.getItem("data"))
}
let products = [
    {
        id: 0,
        name: "Golden Watch",
        image: "images/watch1.png",
        price: "$200.00", 
        catagory: "Accessories",
        amount: 0,
        fav: false

    },
    {
        id: 1,
        name: "Silver Watch",
        image: "images/watch2.png",
        price: "$100.00", 
        catagory: "Accessories",
        amount: 0,
        fav: false

    },
    {
        id: 2,
        name: "Brown Shoes",
        image: "images/shoes.png",
        price: "$80.00", 
        catagory: "Fashion",
        amount: 0,
        fav: false

    },
    {
        id: 3,
        name: "Grey Hoodie",
        image: "images/hoodie1.png",
        price: "$100.00", 
        catagory: "Fashion",
        amount: 0,
        fav: false

    },
    {
        id: 4,
        name: "Sunglasses",
        image: "images/sunglasses2.png",
        price: "$70.00", 
        catagory: "Accessories",
        amount: 0,
        fav: false

    },
    {
        id: 5,
        name: "White Cap",
        image: "images/cap1.png",
        price: "$30.00", 
        catagory: "Sport",
        amount: 0,
        fav: false

    },
    {
        id: 6,
        name: "Football Shoes",
        image: "images/football1.png",
        price: "$100.00", 
        catagory: "Sport",
        amount: 0,
        fav: false

    },
    {
        id: 7,
        name: "Blue T-Shirt",
        image: "images/shirt1.png",
        price: "$60.00", 
        catagory: "Fashion",
        amount: 0,
        fav: false

    },
    {
        id: 8,
        name: "Grey Pants",
        image: "images/pants2.png",
        price: "$50.00",
        catagory: "Fashion",
        amount: 0,
        fav: false

    },
    {
        id: 9,
        name: "Headphones",
        image: "images/headphones1.png",
        price: "$120.00",
        catagory: "Accessories",
        amount: 0,
        fav: false

    },
    {
        id: 10,
        name: "Argentina Shirt",
        image: "images/Argentina.png",
        price: "$110.00",
        catagory: "Sport",
        amount: 0,
        fav: false

    },
    {
        id: 11,
        name: "Addidas Shorts",
        image: "images/shorts2.png",
        price: "$75.00",
        catagory: "Sport",
        amount: 0,
        fav: false

    }

]
let productsToDraw = [...products]
let favourites = JSON.parse(localStorage.getItem("favs"))

Pname.addEventListener("keyup", filterInput)
catagory.addEventListener("keyup", filterInput)
budget.addEventListener("keyup", filterInput)

function filterInput() {
    productsToDraw = products.filter((product) => product.name.toLowerCase().includes(Pname.value.toLowerCase().trim()))
    productsToDraw = productsToDraw.filter((product) => product.catagory.toLowerCase().includes(catagory.value.toLowerCase().trim()))
    if (budget.value) {
        productsToDraw = productsToDraw.filter((product) => {
            let price = +product.price.replace("$", "")
            console.log(price)
            return price <= budget.value
        })
    }
    renderProducts()
}

function renderProducts() {
    
    productsContainer.textContent = ""
    for (let i = 0; i < products.length; i++) {
        if (productsToDraw.indexOf(products[i]) != -1) {
           
            productsContainer.innerHTML += `
        <div class="col-md-6 col-lg-3 box"  >
        <div class="text-center rounded" >
            <div class="image overflow-hidden position-relative">
                <img src="${products[i].image}" loading="lazy" class="img-fluid  bg-secondary bg-opacity-10 rounded-top" alt="Golden Watch">
            </div>
            <div class="px-4 py-4 border border-top-0 position-relative">
                <h5 class="fw-bold">${products[i].name}</h5>
                <p class="opacity-75 mb-1 " style="">Price: ${products[i].price}</p>
                <i class="fas fa-star py-2" style="color: rgb(255, 192, 34);"></i>
                <i class="fas fa-star" style="color: rgb(255, 192, 34);"></i>
                <i class="fas fa-star" style="color: rgb(255, 192, 34);"></i>
                <i class="fas fa-star" style="color: rgb(255, 192, 34);"></i>
                <i class="fas fa-star" style="color: rgb(255, 192, 34);"></i>
                <p class="opacity-75">Catagory: ${products[i].catagory}</p>
                <span class='opacity-75 fw-bold'>Quantity: </span>
                <input type="number" id='amount${products[i].id}'  onblur="reset(event)" min="1" max="99" value="1"  class='fw-bold mb-3 text-center ' style='width:40px; '>
                <i onclick='favourite(${products[i].id})' class="favBtn position-absolute rounded-circle p-2  fav${products[i].id} fas fa-heart fa-2x "></i>
                <div class="d-flex flex-column  ">
                <button onclick="addToCart(${products[i].id})"class="mb-3 border-2 border-black bg-white cartBtn${products[i].id} btn py-2 rounded-0 fw-bold btn4 text-black" style="transition: .3s; ; border: 2px solid var(--primary); ">Add to cart</button>
                </div>
            </div>
        </div>
    </div>
        `
            drawFav(i)
            if (products[i].amount != 0) {
                let btn = document.querySelector(`.cartBtn${i}`)
                btn.setAttribute("onclick", `removeFromCart(${i})`)
                btn.textContent = "Remove From Cart"
            }
            if (products[i].fav) {
                let heart = document.querySelector(`.fav${i}`)
                heart.style.color = "red"
            }
        }
    }
    
    productsContainer.innerHTML += `
                <div class="toast-container position-fixed bottom-0 end-0 p-3 text-white ">
                    <div id="liveToast" class="toast bg-success " role="alert" aria-live="assertive" aria-atomic="true">
                        <div class="toast-header bg-success text-white">
                            <i class="fas fa-check-circle pe-2"></i>
                            <strong class="me-auto">Item Added!</strong>
                            <small>1 min ago</small>
                            <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
                        </div>
                        <div class="toast-body">

                        </div>
                    </div>
                </div>
                <div class="toast-container position-fixed bottom-0 end-0 p-3 text-white ">
                    <div id="liveToast" class="toast bg-danger " role="alert" aria-live="assertive" aria-atomic="true">
                        <div class="toast-header bg-danger text-white">
                            <i class="fas fa-check-circle pe-2"></i>
                            <strong class="me-auto">Item Removed!</strong>
                            <small>1 min ago</small>
                            <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
                        </div>
                        <div class="toast-body">

                        </div>
                    </div>
                </div>`
    
    
    if (productsToDraw.length == 0) {
        let h2 = document.createElement("h2")
        h2.textContent = "No products were found!"
        h2.style.textAlign = "center"
        productsContainer.appendChild(h2)
    }
}

function getStoredData() {
    if (localStorage.getItem("signed") === "true") {
        products.forEach((ele, index) => {
            let i = storedData.findIndex((x) => x.id == ele.id)
            if (i != -1) {
                products[index].amount = storedData[i].amount
            }
        })
        favourites.forEach((ele) => {
            let i = ele.id
            products[i].fav = true
        })
    }

}
getStoredData()
renderProducts()

function drawFav(id) {
    let fav = document.querySelector(`.fav${id}`)
    if (products[id].fav) {
        fav.style.color = "red"
    }
    else {
        fav.style.color = ""
    }
}