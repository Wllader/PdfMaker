

window.openHtmlInNewTab = (htmlContent) => {
    const newWindow = window.open("", "_blank");
    if (newWindow) {
        newWindow.document.write(htmlContent);
        newWindow.document.close()
    } else {
        alert("Popup blocked by browser");
    }
};