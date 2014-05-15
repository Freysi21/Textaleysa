$(document).ready(function () {
    var hide = document.getElementById("search-string");
    if (string == null)
    {
        var string = hide.innerHTML;
        var stringinput = document.getElementById("SearchBar");
        stringinput.style.display = string;
    }
    hide.style.display = "none";
    
});