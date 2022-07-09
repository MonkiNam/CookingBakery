// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var filePreview = document.getElementById("customFile");
if (filePreview != null) {
    if (filePreview.files.length != 0) {
        document.getElementsByClassName('image-holder')[0].style.display = 'none';
    }
    filePreview.onchange = function () {
        var src = URL.createObjectURL(this.files[0]);
        var name = this.files[0].name;
        document.getElementById('customFileLabel').textContent = name;
        document.getElementById('preview-image').src = src;
        document.getElementsByClassName('image-holder')[0].style.display = 'none';
    }
}
$(document).ready(function () {
    $('.toast').toast('show')
});
