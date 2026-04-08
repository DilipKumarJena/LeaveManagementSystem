var timeout = '';
var timer = setInterval(function()
{
   timeout -= 1000;
   document.getElementById('countDown').innerHTML = time(timeout);
   if (timeout == 0)
   {
      clearInterval(timer);
      alert('Your session has expired!')
   }
}
, 1000);

function two(x)
{
   return ((x > 9) ? "" : "0") + x
}

function time(ms)
{
   var t = '';
   var sec = Math.floor(ms / 1000);
   ms = ms % 1000

   var min = Math.floor(sec / 60);
   sec = sec % 60;
   t = two(sec);

   var hr = Math.floor(min / 60);
   min = min % 60;
   t = hr + ":" + two(min) + ":" + t;

   return "You session will timeout in " + t ;
}