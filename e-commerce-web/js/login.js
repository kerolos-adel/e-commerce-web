const email = document.getElementById("email")
const password = document.getElementById("password")
const form = document.getElementById("logForm")
const err = document.getElementById("err")

form.addEventListener("submit", checkData)

function checkData(event) {
    event.preventDefault()
    let storedEmail = localStorage.getItem("email")
    let storedPassword = localStorage.getItem("password")
    
    if (storedEmail === email.value && storedPassword === password.value) {
        localStorage.setItem("signed", "true")
        location.assign("index.html")
        localStorage.setItem("data", JSON.stringify([]))
    }
    else {
        err.style.display = "block"
    }
}
