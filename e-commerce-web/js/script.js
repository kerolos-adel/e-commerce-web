const login = document.getElementById("login")
const reg = document.getElementById("register")
const logout = document.getElementById("logout")
const logoutLink = document.getElementById("logoutLink") 
const user = document.getElementById("user")
const cart = document.getElementById('cart')
let isSigned = localStorage.getItem("signed") === "true"
let btns
let itemCon = document.getElementById("items-con")
let opened = false
let fav = false
let num = document.getElementById("productsNum")



logoutLink.addEventListener('click', ()=> localStorage.setItem("signed", "false"))
cart.addEventListener("click", showItems)

function showItems(e) {
    if (e.target !== e.currentTarget) return;
    opened ? itemCon.style.display = "none" : itemCon.style.display = "flex"
    opened = !opened

}
// function hideCart(event) {
//     itemCon.style.display = "none"
//     opened = false
// }
let itemsInCart = []

window.onload = function () {
    let spinner = document.querySelector(".loader-container")
    spinner.style.display = "none"
    let main = document.querySelector("main")
    main.style.opacity = 1
    let body = document.querySelector("body")
    body.style.overflow = "visible"
    // main.style.transform = "none"
    if (isSigned) {
        login.style.display = "none"
        reg.style.display = "none"
        logout.style.display = "block"
        user.style.display = "block"
        cart.style.display = "block"
        user.firstElementChild.textContent = `${localStorage.getItem("firstName")} ${localStorage.getItem("lastName")}`

    }
    // btns = document.getElementsByClassName("cartBtn")
    if (localStorage.getItem("signed") === "true") {
        storedData.forEach((ele) => {
            itemsInCart.push({
                name: ele.name,
                amount: ele.amount,
                id: ele.id
            })
        })
    }
    draw()
}
window.onbeforeunload = storeData

function storeData() {
    let data = []
    data = JSON.stringify(products.filter((ele) => ele.amount != 0))

    favourites = JSON.stringify(products.filter((ele) => ele.fav))
    localStorage.setItem("data", data)
    localStorage.setItem("favs", favourites)
}



function addToCart(id) {
    if (!isSigned) {
        location.assign("login.html")
    }
    else {
        let amount = document.getElementById(`amount${id}`)
        amount = +amount.value
        if (amount > 0 && amount < 100) {
            let i = id
            products[i].amount += amount
            let index
            if (itemsInCart.some((value, ind) => {
                index = ind
                return value.name === products[i].name
            })) {
                itemsInCart[index].amount = products[i].amount
            }
            else {
                itemsInCart.push({
                    name: products[i].name,
                    amount: products[i].amount,
                    id: products[i].id
                })
            }
        
            let toastElList = document.querySelector('.toast')
            let body = document.querySelector('.toast-body')
            let con = document.querySelectorAll('.toast-container')
            con[1].style.visibility = "hidden"
            con[0].style.visibility = "visible"
            let toast = new bootstrap.Toast(toastElList)
            body.textContent = `You added ${products[i].name} to your Cart!`
            toast.show()
            
            let btn = document.querySelector(`.cartBtn${i}`)
            btn.onclick = null
            btn.onclick = () => {
                removeFromCart(i)
            }
            btn.textContent = "Remove From Cart"
       
            draw()
        }
    }

}

function removeFromCart(id) {
   
    let btn = document.querySelector(`.cartBtn${id}`)
    let i = id
    products[i].amount = 0
    let index = itemsInCart.findIndex((ele) => ele.id === id)
    itemsInCart.splice(index, 1)
    btn.onclick = null
    btn.onclick = function x() {
        addToCart(i)
    }
    btn.textContent = "Add To Cart"

    let toastElList = document.querySelectorAll('.toast')
    let body = document.querySelectorAll('.toast-body')
    let con = document.querySelectorAll('.toast-container')
    con[0].style.visibility = "hidden"
    con[1].style.visibility = "visible"
    let toast = new bootstrap.Toast(toastElList[1])
    body[1].textContent = `You Removed ${products[i].name} from your Cart!`
    toast.show()

    draw()
}

function draw() {
    itemCon.innerHTML = ""
    num.textContent = products.reduce((total, ele) => total + ele.amount, 0)
    if (itemsInCart.length == 0) {
        let p = document.createElement("p")
        p.textContent = "Cart Empty"
        p.style.textAlign = "center"
        p.style.fontSize = "16px"
        itemCon.appendChild(p)
    }
    else {
        for (let i = 0; i < itemsInCart.length; i++) {
            itemCon.innerHTML += `
            <div class="d-flex mb-2 bg-secondary bg-opacity-10 justify-content-between align-items-center  px-2 py-3 rounded" style="min-width:250px;">
                <p class="m-0 fs-6" >${itemsInCart[i].name}</p>
                <div class=''>
                    <span  onclick="remove(event, ${itemsInCart[i].id})" class="px-2" style="cursor: pointer; color: red;">-</span>
                    <span class="m-0 fs-6">${itemsInCart[i].amount}</span>
                    <span onclick="add(event, ${itemsInCart[i].id})" style="cursor: pointer; color: green;" class="px-2 fw-bold">+</span>
                </div>
                </div>
         
            `
        }
        itemCon.innerHTML += ` <button onclick="clearCart()" class="border-2 border-black  btn rounded-0 fw-bold btn4 bg-white" style="transition: .3s; ">Clear Cart</button>`
    }
}

function add(event, id) {
    products[id].amount += 1
    num.textContent = (+num.textContent) +1 
    let index = itemsInCart.findIndex((ele) => ele.id === id)
    itemsInCart[index].amount = products[id].amount
    event["target"].previousElementSibling.textContent = products[id].amount 
}
function remove(event, id) { 
    products[id].amount -= 1
    num.textContent = (+num.textContent) - 1 
    let index = itemsInCart.findIndex((ele) => ele.id === id)
    itemsInCart[index].amount = products[id].amount
    if (products[id].amount == 0) {
        itemsInCart.splice(index, 1)
        let btn = document.querySelector(`.cartBtn${id}`)
        btn.onclick = null
        btn.onclick = function () {
            addToCart(id)
        }
        btn.textContent = "Add To Cart"

        draw()
        
    }
    else {
        event["target"].nextElementSibling.textContent = products[id].amount
    }
}

function clearCart() { 
    products.forEach((ele) => ele.amount = 0)
    itemsInCart.splice(0)
    for (let i = 0; i < products.length; i++){
        let btn = document.querySelector(`.cartBtn${i}`)
        if (btn) {
            btn.onclick = null
            btn.onclick = function () {
                addToCart(i)
            }
            btn.textContent = "Add To Cart"
        }
    }

    draw()
}
function reset(event) {
    if (!event["target"].value) {
        event["target"].value = 1
    }
}


function favourite(id) {
    if (!isSigned) {
        location.assign("login.html")
    }
    else {
        let fav = document.querySelector(`.fav${id}`)
        if (products[id].fav) {
            fav.style.color = ""
        }
        else {
            fav.style.color = "red"
        }
        products[id].fav = !products[id].fav
    }
}
