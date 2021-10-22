function showCategoryForm(event) {
    document.getElementById("bookmakrFormContainer").style.display = "flex";
    document.getElementById("categoryId").value = event.target.id;
};

function hideCategoryForm() {
    document.getElementById("bookmakrFormContainer").style.display = "none";
    document.getElementById("bookmarkUrl").value = "";
    document.getElementById("bookmarkDescription").value = "";
    document.getElementById("categoryId").value = "";
}

function SaveBookmark() {
    var urlValue = document.getElementById("bookmarkUrl").value;
    var descriptionValue = document.getElementById("bookmarkDescription").value;
    var categoryId = document.getElementById("categoryId").value;
    var data = { Url: urlValue, shortDescription: descriptionValue, categoryId: categoryId };
    $.ajax({
        type: "POST",
        url: "https://localhost:44326/api/ApiBookmark/create",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        success: function (data) {
        }
    })
        .then((data) => {
            hideCategoryForm();
        })
        .catch((err) => {
            console.log(err);
    });
}