function UpdateProgress()
{
   try
   {   
      if(document.all)e = event;
      var obj = document.getElementById('UpdateProgress');
      obj.style.display = 'block';
      var st = Math.max(document.body.scrollTop, document.documentElement.scrollTop);
      if(navigator.userAgent.toLowerCase().indexOf('safari') >= 0)st = 0;
      var leftPos = e.clientX - 10 ;
      if(leftPos < 0)leftPos = 0;
      obj.style.left = leftPos + 'px';
      obj.style.top = e.clientY - obj.offsetHeight - 1 + st + 'px';
      return true;
   }
   catch(e)
   {
      //alert(e + "\r\n Please Wait While Page Is Loading.....");
      
   }
}

document.onmousedown = function()
{
   UpdateProgress();
}