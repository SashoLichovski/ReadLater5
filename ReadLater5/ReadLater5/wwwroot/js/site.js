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
    debugger;
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
            ShowSuccessMessage("successfully created");
        })
        .catch((err) => {
            console.log(err);
    });
}

function ShowSuccessMessage(message) {
    var body = document.getElementsByTagName("body")[0];

    var message = document.createElement("div");
    message.style.position = "absolute";
    message.style.padding = "20px";
    message.style.margin = "30px";
    message.innerText = message;
    body.appendChild(message)
    message.event(onmouseover, function () {
        message.remove();
    });
}

$('select').on('change', function () {
    var newNameElement = document.getElementById("newCategoryName");
    if (this.value == "New Category") {
        newNameElement.disabled = true;
    } else {
        newNameElement.disabled = false;
    }
});