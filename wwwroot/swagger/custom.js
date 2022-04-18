// It takes the document a sec to load the swagger stuff, loop until we find it
let swaggerLoaded = setInterval(() => {
	if (document.getElementsByClassName("renderedMarkdown").length > 0) {
	  clearInterval(swaggerLoaded);
	  // Disable event bubbling for summary elements
	  for(let summary of document.getElementsByTagName("summary")) {
		summary.addEventListener("click", e => e.stopPropagation());
	  }
	}
  }, 300); // check every 300ms