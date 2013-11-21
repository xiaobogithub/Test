var url = window.location.href;
if (url.indexOf("http:") >= 0 && url.indexOf("localhost") < 0) {
    url = url.replace("http:", "https:");
    window.location.replace(url)
}
else if (url.indexOf("https:") < 0 && url.indexOf("http:") < 0 && url.indexOf("localhost") < 0) {
    url = "https://" + url;
    window.location.replace(url)
}