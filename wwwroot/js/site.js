window.refreshImage = (imageId) => {
    let img = document.getElementById(imageId);
    if (img) {
        img.src = img.src.split("?")[0] + "?t=" + new Date().getTime();
    }
};

window.reloadPage = () => {
    location.reload();
};