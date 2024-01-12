 var IsMobileDevice = {
     IsMobile: function()
     {
         return Module.SystemInfo.mobile;
     }
 };
 
 mergeInto(LibraryManager.library, IsMobileDevice);