

// JScript File

  String.prototype.trim = function () 
    {
    return this.replace(/^\s*/, "").replace(/\s*$/, ""); 
    }

 function noCopyMouse(e) 
        {
		    if (event.button == 2 || event.button == 3)
		     {			    
			    return false;
		     }
		    return true;
	    }
	


	   function noCopyKey(e)
	    {	   
                var forbiddenKeys = new Array('c','x','v');
	            var isCtrl;

                    if(window.event)
                    {
		            if(window.event.ctrlKey)
			            isCtrl = true;
		            else
			            isCtrl = false;
                    }
                    else
                    {
                            if(e.ctrlKey)
                	            isCtrl = true;
                            else
                	            isCtrl = false;
                	}

	            if(isCtrl)
	             {
		            for(i=0; i < forbiddenKeys.length; i++)
		             {
			            if(forbiddenKeys[i] == String.fromCharCode(window.event.keyCode).toLowerCase())
			             {				            
				            return false;
			             }
        		     }
	            }	    
           }
          	
       function TextOnly()
            {            
	            var AsciiValue=event.keyCode              
	            if((AsciiValue>=96 && AsciiValue<=122) || (AsciiValue>=65 && AsciiValue<=90) || (AsciiValue==8 || AsciiValue==127 || AsciiValue==32))
	                  event.returnValue=true;
		        else
		              event.returnValue=false;	            
            }
            
    function NumberOnly()
        {
           var AsciiValue=event.keyCode
	        if((AsciiValue>=48 && AsciiValue<=57) || (AsciiValue==8 || AsciiValue==127))
		        event.returnValue=true;
		    else
		        event.returnValue=false;
        }
       
        function TextAndNumberOnly()
            {            
	            var AsciiValue=event.keyCode              
	            if((AsciiValue>=48 && AsciiValue<=57) || (AsciiValue>=96 && AsciiValue<=122) || (AsciiValue>=65 && AsciiValue<=90) || (AsciiValue==8 || AsciiValue==127 || AsciiValue==32))
	                  event.returnValue=true;
		        else
		              event.returnValue=false;	            
            } 
       
        function TextOnlyWithSpecialChars()
    {
     var AsciiValue=event.keyCode              
	            if((AsciiValue>=96 && AsciiValue<=122) || (AsciiValue>=64 && AsciiValue<=90) || (AsciiValue==8 || AsciiValue==127 || AsciiValue==32 || AsciiValue==46 || AsciiValue==40 || AsciiValue==41 || AsciiValue==38 || AsciiValue==45 || AsciiValue==47))
	                  event.returnValue=true;
		        else
		              event.returnValue=false;	
    }  
    
    
    


 
        function NumberOnlyWithDash()
        {
           var AsciiValue=event.keyCode
	        if((AsciiValue>=48 && AsciiValue<=57) || (AsciiValue==8 || AsciiValue==127 || AsciiValue==45) )
		        event.returnValue=true;
		    else
		        event.returnValue=false;
        }





    function TextSpecialCharswithComma()
    {
     var AsciiValue=event.keyCode              
	            if((AsciiValue>=96 && AsciiValue<=122) || (AsciiValue>=64 && AsciiValue<=90) || (AsciiValue==8 || AsciiValue==127 || AsciiValue==32 || AsciiValue==46 || AsciiValue==40 || AsciiValue==41 || AsciiValue==38 || AsciiValue==45 || AsciiValue==47 || AsciiValue==44))
	                  event.returnValue=true;
		        else
		              event.returnValue=false;	
    }  
    
    
// Date Validation ( From Date Should Be Less Then To Date -  )
// First Argument - From Date
// Second Argument - To Date
// Third Argument - Span id
// How To Use - On Page Load Write Following Line & Change Argument
// btnSubmit.Attributes.Add( "OnClick", "ValidateCurrentDate('" + txtFromDate.ClientID + "','" + txtToDate.ClientID + "', 'Error', 'Message')" );


function ValidateDate( From, To, Error, Message )
{
   t1 = document.getElementById (To).value ;
   t2 = document.getElementById (From).value ; 
   
   if ( t1 != "" && t2 != "" )
   {
      var one_day = 1000 * 60 * 60 * 24;
      var x = t1.split( "/" );
      var y = t2.split( "/" );
      var date1 = new Date( x[2], ( x[1] - 1 ), x[0] );
      var date2 = new Date( y[2], ( y[1] - 1 ), y[0] )
      // Calculate difference between the two dates, and convert to hours
      _Diff = Math.ceil( ( date2.getTime() - date1.getTime() ) / ( one_day ) );

      var flag = true;

      if( _Diff > 0 )
      {
         document.getElementById( Error ).innerHTML = Message;
         flag = false;
      }
      return flag;
   }
}

function ValidateCurrentDate( From, To, Error, Message )
{
   t1 = To;
   t2 = document.getElementById (From).value ; 
   
   if ( t1 != "" && t2 != "" )
   {
      var one_day = 1000 * 60 * 60 * 24;
      var x = t1.split( "/" );
      var y = t2.split( "/" );
      var date1 = new Date( x[2], ( x[1] - 1 ), x[0] );
      var date2 = new Date( y[2], ( y[1] - 1 ), y[0] )
      // Calculate difference between the two dates, and convert to hours
      _Diff = Math.ceil( ( date2.getTime() - date1.getTime() ) / ( one_day ) );

      var flag = true;

      if( _Diff > 0 )
      {
         document.getElementById( Error ).innerHTML = Message;
         flag = false;
      }
      return flag;
   }
}
////////////////////////
// Function Created For Inserting Float Value In Text Box
// by Anudeep Jaiswal 22-Dec-2008  
function FloatOnly( Textbox ) {
    
   var tt = document.getElementById( Textbox.id ).value;

   var pressed = tt.indexOf( ".", 0 );

   if ( pressed == - 1 )
   point_pressed = false;
   else
   {
      var DecimalLength = tt.indexOf( '.' );
      var TextLength = tt.length;
      point_pressed = true;
      if ( ( TextLength -  DecimalLength ) == 3 )
      {
         return false;
      }
   }


   var AsciiValue = event.keyCode
   if( ! ( ( event.keyCode >= 48 && event.keyCode <= 57 ) || ( event.keyCode == 46 && ! point_pressed ) ) )
   {
      event.returnValue = false;
   }
}

// Text Counter

// Dynamic Version by: Nannette Thacker -->
// http://www.shiningstar.net -->
// Original by :  Ronnie T. Moore -->
// Web Site:  The JavaScript Source -->
// Use one function for multiple text areas on a page -->
// Limit the number of characters per textarea -->

 function textCounter(field,cntfield,maxlimit) {
 
 cntfield = document.getElementById( cntfield )
if (field.value.length > maxlimit) // if too long...trim it!
field.value = field.value.substring(0, maxlimit);
// otherwise, update 'characters left' counter
else
cntfield.innerHTML = maxlimit - field.value.length + " Character Left";
}//



// Clear All On Master Page
// Either Simple Or Ajax Enable Page OnClientClick

function ClearAllOnMasterPage()
{
   for( i = 0; i < document.forms['aspnetForm'].length ; i ++ )
   {
      var elm = ( document.forms['aspnetForm'].elements[i] );
      if ( elm.type == 'text' )
      elm.value = '';
      if ( elm.type == 'select-one' )
      elm.selectedIndex = 0;
      if ( elm.type == 'checkbox' )
      elm.checked = false;
      if ( elm.type == 'textarea' )
          elm.value = '';
      
   }
   return false;
}




function AlertOnDelete(Button)
 {
   msg = "Are you sure you want to delete this?";
   if (confirm(msg))
   {   
   document.getElementById(Button.id).setAttribute("value","yes");
   }
   else
   return false;
 }
 
 
function AlertOnInsert(Button)
 {
   msg = "Are You Sure?";
   if (confirm(msg))
   {   
   document.getElementById(Button.id).setAttribute("value","yes");
   }
   else
   return false;
 }
 
 
 
 
function EMailCheck(str)
{

   var at = "@"
   var dot = "."
   var lat = str.indexOf(at)
   var lstr = str.length
   var ldot = str.indexOf(dot)
   if (str.indexOf(at) == - 1)
   {     
      return false
   }
   if (str.indexOf(at) == - 1 || str.indexOf(at) == 0 || str.indexOf(at) == lstr)
   {     
      return false
   }

   if (str.indexOf(dot) == - 1 || str.indexOf(dot) == 0 || str.indexOf(dot) == lstr)
   {     
      return false
   }

   if (str.indexOf(at, (lat + 1)) != - 1)
   {    
      return false
   }

   if (str.substring(lat - 1, lat) == dot || str.substring(lat + 1, lat + 2) == dot)
   {     
      return false
   }

   if (str.indexOf(dot, (lat + 2)) == - 1)
   {     
      return false
   }

   if (str.indexOf(" ") != - 1)
   {      
      return false
   }
   return true
}



 function NumberOnly_New(evt)
        {      
        
          evt = (evt) ? evt : window.event;
          var AsciiValue = (evt.which) ? evt.which : evt.keyCode;
        
         
           
         
           
	        if((AsciiValue>=48 && AsciiValue<=57) || (AsciiValue==8 || AsciiValue==127))
		        return true;
		    else
		        return false;
        }


