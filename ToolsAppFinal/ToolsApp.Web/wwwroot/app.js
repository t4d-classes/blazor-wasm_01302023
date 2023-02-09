
'use strict';

window.app = {
  setFocus(selector) {
    console.log("set focus: ", selector);
    const element = document.querySelector(selector);
    if (element) {
      element.focus();
    }
  },

  setupColorsRefresh(dotNetHelper /* colors data service */) {

    console.log("setup colors refresh")

    document.querySelector("#refreshColorsButton").addEventListener('click', async () => {

      console.log("called colors refresh")

      const colors = await dotNetHelper.invokeMethodAsync("All");
      console.log(colors);
    });

  }  
};
