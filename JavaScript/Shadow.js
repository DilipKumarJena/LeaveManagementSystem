function applyShadow(targetElement, shadowColor, shadowOffset) {
  if (typeof(targetElement) != 'object') {
    targetElement = document.getElementById(targetElement);
  }
  var value = targetElement.firstChild.nodeValue;
  
  targetElement.style.position = 'relative';
  targetElement.style.zIndex = 0;
    
  var newEl = document.createElement('span');
  newEl.appendChild(document.createTextNode(value));
  newEl.className = 'shadowed';
  newEl.style.color = shadowColor;
  newEl.style.position = 'absolute';
  newEl.style.left = shadowOffset + 'px';
  newEl.style.top = shadowOffset + 'px';
  newEl.style.zIndex = -1;  
  targetElement.appendChild(newEl);
}