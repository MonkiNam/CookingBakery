var filePreview = document.getElementById("customFile");
var previewImage = document.getElementById('preview-image');
if (previewImage != null) {
    if (previewImage.getAttribute('src').length > 0) {
        document.getElementsByClassName('image-holder')[0].style.display = 'none';
    }
}

if (filePreview != null) {
    filePreview.onchange = function () {
        var src = URL.createObjectURL(this.files[0]);
        var name = this.files[0].name;
        document.getElementById('customFileLabel').textContent = name;
        previewImage.src = src;
        document.getElementsByClassName('image-holder')[0].style.display = 'none';
    }
}

() => {
    $('form > input').keyup(function () {

        var empty = false;
        $('form > input').each(function () {
            if ($(this).val() == '') {
                empty = true;
            }
        });

        if (empty) {
            $('#register').attr('disabled', 'disabled'); // updated according to http://stackoverflow.com/questions/7637790/how-to-remove-disabled-attribute-with-jquery-ie
        } else {
            $('#register').removeAttr('disabled'); // updated according to http://stackoverflow.com/questions/7637790/how-to-remove-disabled-attribute-with-jquery-ie
        }
    });
}

$(document).ready(function () {
    $('.toast').toast('show')
});
