mergeInto(LibraryManager.library, {
    GetOS: function () {
        var userAgent = window.navigator.userAgent.toLowerCase();
        if (userAgent.indexOf("win") != -1) return "Windows";
        else if (userAgent.indexOf("mac") != -1) return "MacOS";
        else if (userAgent.indexOf("linux") != -1) return "Linux";
        else if (userAgent.indexOf("android") != -1) return "Android";
        else if (userAgent.indexOf("iphone") != -1 || userAgent.indexOf("ipad") != -1 || userAgent.indexOf("ipod") != -1) return "iOS";
        else return "Unknown";
    }
});