const nameInput = document.getElementById("name");
const phoneInput = document.getElementById("phone");
const dateInput = document.getElementById("date");
const timeInput = document.getElementById("time");
const tableIdInput = document.getElementById("tableId");

const createBookingBtn = document.getElementById("createBookingBtn");
const showBookingsBtn = document.getElementById("showBookingsBtn");

const API_URL = "http://localhost:5139/api/booking/create";
async function CreateBooking() {
    const booking =
    {
        name: nameInput.value,
        phone: phoneInput.value,
        date: `${dateInput.value}T${timeInput.value}:00`,
        tableId: parseInt(tableIdInput.value)
    }
    try {
        const response = await fetch(API_URL,
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(booking)
            }
        );
        if (response.ok) {
            alert("Бронь создана")
        }
        else {
            const error = await response.text();
            alert("Ошибка:" + error);
        }
    }
    catch (err) {
        console.error(err);
        alert("ошибка подключения к серверу");
    }
}
createBookingBtn.addEventListener("click", CreateBooking);
showBookingsBtn.addEventListener("click", async function () {
    const phone = prompt("Введите номер телефона");
    const response = await fetch(
        `http://localhost:5139/api/booking/phone/${phone}`
    );
    const data = await response.json();
    showBookingsModel(data);
});
function showBookingsModel(bookings) {
    const message = document.getElementById("message");
    message.innerHTML = "";
    bookings.forEach(b => {
        const div = document.createElement("div");
        div.innerHTML = `
        <p>Дата: ${b.date}</p>
            <p>Стол: ${b.tableId}</p>
            <p>Статус: ${b.status}</p>

            <button onclick="cancelBooking(${b.id})">
                Отменить
            </button>
        `;
        message.appendChild(div);
    });
}
async function cancelBooking(id) {
    const response = await fetch(
        `http://localhost:5139/api/booking/cancel/${id}`,
        {
            method: "PUT"
        });
    if (response.ok) {
        alert("Бронь отменена");
        document.getElementById("showBookingsBtn").click();
    }
}