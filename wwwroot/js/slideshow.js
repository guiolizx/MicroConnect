const buttonsSlideshow = document.querySelectorAll(".ballButton");
const images = document.querySelectorAll(".headerImage");

let currentIndex = 0;
const intervalTime = 4000; 
const intervalTimeAfterClick = 8000;

let slideshowInterval = null;

function showImage(index) {
  images[currentIndex].classList.remove("active");
  buttonsSlideshow[currentIndex].classList.remove("selected");

  currentIndex = index;

  images[currentIndex].classList.add("active");
  buttonsSlideshow[currentIndex].classList.add("selected");
}

function startAutoSlide(delay = intervalTime) {
  clearInterval(slideshowInterval);
  slideshowInterval = setInterval(() => {
    const nextIndex = (currentIndex + 1) % images.length;
    showImage(nextIndex);
  }, delay);
}


buttonsSlideshow.forEach((ballButton, i) => {
  ballButton.addEventListener("click", () => {
    clearInterval(slideshowInterval);

    showImage(i);

    startAutoSlide(intervalTimeAfterClick);
  });
});


startAutoSlide(intervalTime);