const days = document.getElementById('days')
const hours = document.getElementById('hours')
const mintues = document.getElementById('minutes')
const seconds = document.getElementById('seconds')



function updateTimer() {

    seconds.textContent = String(+seconds.textContent - 1)
    if (+seconds.textContent  < 0) {
        seconds.textContent = "59"
        mintues.textContent = +mintues.textContent-1
    }
    if (+mintues.textContent < 0) {
        mintues.textContent = "59"
        hours.textContent = +hours.textContent-1
    }
    if (+hours.textContent < 0) {
        hours.textContent = "23"
        days.textContent = +days.textContent-1
    }
}

setInterval(updateTimer, 1000)