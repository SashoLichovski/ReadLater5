﻿function showCategoryForm(event) {
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
            showMessage("Bookmark successfully created");
        })
        .catch((err) => {
            showMessage("Something went wrong. Please go back or refresh the page");
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

function addFavourite(id) {

    $.ajax({
        type: "GET",
        url: "https://localhost:44326/api/ApiBookmark/addFavourite/" + id,
        success: function (data) {
        }
    })
        .then((data) => {
            if (data == undefined) {
                showMessage("Added to favourites");
            } else {
                showMessage("Removed from favourites");
            }
        })
        .catch((err) => {
            showMessage("Something went wrong. Please go back or refresh the page");
        });
}

function showMessage(message) {
    var container = document.getElementById("container");
    var messageEle = document.createElement("div");
    messageEle.innerText = message;
    messageEle.classList.add("showMessage");

    var closeBtn = document.createElement("div");
    closeBtn.classList.add("far");
    closeBtn.classList.add("fa-times-circle");
    closeBtn.classList.add("pointer");
    closeBtn.style.marginLeft = "10px";
    closeBtn.addEventListener("click", function () {
        messageEle.remove();
    });

    messageEle.appendChild(closeBtn);

    container.appendChild(messageEle);
}