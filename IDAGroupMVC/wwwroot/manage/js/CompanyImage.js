$(function () {

    $(document).ready(function () {
        $(document).on("click", ".deleteImage", function () {
            $(this).parent().remove();
        })
    })

})

let posterInput = document.getElementById("posterInput")
let posterBox = document.getElementById("poster-image-box")
posterInput.onchange = function (e) {
    let files = e.target.files
    let filesarr = [...files]
    filesarr.forEach(x => {
        if (x.type.startsWith("image/")) {
            let reader = new FileReader()
            reader.onload = function () {
                posterBox.innerHTML = ""
                let newPosterImage = document.createElement("img")
                newPosterImage.style.width = "200px"
                newPosterImage.style.height = "180px"
                newPosterImage.setAttribute("src", reader.result)
                posterBox.appendChild(newPosterImage)
            }
            reader.readAsDataURL(x)
        }
    })
}
let imageInput = document.getElementById("imageInput")
let imageBox = document.getElementById("image-box")
imageInput.onchange = function (e) {
    let files = e.target.files
    let filesarr = [...files]
    filesarr.forEach(x => {
        if (x.type.startsWith("image/")) {
            let reader = new FileReader()
            reader.onload = function () {
                let newImage = document.createElement("img")
                newImage.style.width = "200px"
                newImage.style.height = "180px"
                newImage.setAttribute("src", reader.result)
                imageBox.appendChild(newImage)
            }
            reader.readAsDataURL(x)
        }
    })
}