"use strict";
var fs = require('fs');
var webPage = require('webpage');
 var page = webPage.create();
 var system = require('system');
var url;
  phantom.outputEncoding='GB2312';
 console.log('1、'+system.args);
  console.log('1.2、'+system.args[1]);
 if (system.args.length === 1) { //当没有参数时，system.args.length==1,保存的是当前js文件名。
    console.log('2、'+system.args+'-请输入参数');
    phantom.exit(1);
} else {
var sitesContent='';//'SiteId,SiteCode,SiteName,SiteUrl,    TypeId,       SiteOrderNumber,SiteProfile';
 url=system.args[1];
	 page.zoomFactor = 0.5;
    page.viewportSize = { width: 600, height: 450 };//设置长宽
	page.open(url, function start(status) {
	    console.log('3、'+'输入的url: '+url);
	 console.log('4、status：'+status);
		if ( status === "success" ) {
				 var desc = page.evaluate(function () {
					 if(document.getElementsByName('description')&&document.getElementsByName('description').length>0)
					return document.getElementsByName('description')[0].content;//document.getElementById('myagent').innerText;
				 });
				 var keywords = page.evaluate(function () {
				     if (document.getElementsByName('keywords') && document.getElementsByName('keywords').length > 0)
					     return document.getElementsByName('keywords')[0].content;//document.getElementById('myagent').innerText;
						   });
					
					//sitesContent+='\n';
				 var sitecode = system.args[2];//'SITE' + new Date().getTime();
						sitesContent+=sitecode;
						sitesContent+='\r\n';
						sitesContent+=page.title;
						sitesContent+='\r\n';
						sitesContent+=url;
						sitesContent+='\r\n';
						sitesContent+=desc;  
						sitesContent += '\r\n';
						sitesContent += keywords;
						sitesContent += '\r\n';
						console.log(sitesContent);
						//sitesContent+='\r\n';
						  page.clipRect = {
							top:    0,
							left:   0,
							width:  600,
							height: 450
						  };
						 var imgbase64= 'data:image/jpg;base64,'+page.renderBase64("jpg");						
						sitesContent+=imgbase64;
						//console.log(sitesContent);
						//fs.write('data\\'+sitecode+'.txt', sitesContent, 'w');
						fs.write('tempdata\\sitedata_' + sitecode + '.txt', sitesContent, 'w');
		  
		             // page.render('data\\siteimg.jpg", {format: 'jpg', quality: '90'});
	   } else {
		  console.log("Page failed to load."); 
	   }
	
		 phantom.exit()
	
	});
	//page.close();
	
}


