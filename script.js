const slides = document.querySelectorAll('.slide');
const controls = document.querySelectorAll('.slider-button');
let currentIndex = 0;

function show(index) {
    slides.forEach(slide => {
        slide.classList.remove('active');
    });
    for (let i = 0; i < 3; i++) {
        const slideIndex = (index + i) % slides.length;
        slides[slideIndex].classList.add('active');
    }
    currentIndex = index;
}
controls.forEach((control) => {
    control.addEventListener('click', (event) => {
        if (event.target.closest('.prev')) {
            let index = currentIndex - 1;
            if (index < 0) {
                index = slides.length - 1;
            }
            show(index);
        } else if (event.target.closest('.next')) {
            let index = currentIndex + 1;
            if (index >= slides.length) {
                index = 0;
            }
            show(index);
        }
    });
});
show(currentIndex);

